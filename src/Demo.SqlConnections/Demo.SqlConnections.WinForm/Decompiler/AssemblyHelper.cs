using System;
using System.Collections.Generic;
using System.Linq;

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace Demo.SqlConnections.WinForm.Decompiler
{
    public class AssemblyHelper
    {


        /// <summary>
        /// This Function will return simple method body, translating CLI into C#.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="indentStart"></param>
        /// <returns></returns>
        public static string GenerateMethodBody(MethodDefinition method, int indentStart)
        {
            var output = "";

            // If we have any variables declared within the scope of the Method
            // We should add them to the start of the body.
            // This is just a simple approach for now, to make it more readable.
            foreach (var variable in method.Body.Variables)
            {
                output += Indent(variable.VariableType.Name + " " + GetVariableName(variable) + ";", indentStart) + Environment.NewLine;
            }

            // Loop through our instructions as before
            // But this time, we actually want to do something with 
            // the instructions we have.
            foreach (var instruction in method.Body.Instructions)
            {
                output += ParseInstruction(method, instruction, indentStart);
            }

            return output;
        }

        /// <summary>
        /// Since we need to use ourselves.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="instruction"></param>
        /// <param name="indentStart"></param>
        /// <returns></returns>
        public static string ParseInstruction(MethodDefinition method, Instruction instruction, int indentStart)
        {
            var opcode = instruction.OpCode;
            string output = "";
            //output += AssemblyHelper.Indent("// {0}: {1} {2}", indentStart, instruction.Offset, instruction.OpCode, instruction.Operand) + Environment.NewLine;

            if (InstructionHelper.IsBranchConditional(opcode.Code))
            {
                output += Indent(GetIfStatement(method, instruction), indentStart) + Environment.NewLine;
                output += ParseInstruction(method, instruction.Operand as Instruction, indentStart + 1);
            }

            // Check if the instruction is for 
            // storing a value onto a variable
            if (InstructionHelper.IsStore(opcode.Code))
            {
                output += Indent(GetStoreLocation(method, instruction) + " = " + GetValueOf(method, instruction.Previous) + ";", indentStart) + Environment.NewLine;
            }
            // Check if we want to return the method
            // And if there is any value we want to return.
            else if (opcode.Code == Code.Ret)
            {
                if (InstructionHelper.IsLoad(instruction.Previous.OpCode.Code))
                    output += Indent("return " + GetValueOf(method, instruction.Previous) + ";", indentStart) + Environment.NewLine;
                else
                    output += Indent("return;", indentStart) + Environment.NewLine;
            }

            return output;
        }

        /// <summary>
        /// This function will try and return a variable name
        /// if one exists, if it doesnt. It will generate one using the typename
        /// and variable index.
        /// </summary>
        /// <param name="variable"></param>
        /// <returns></returns>
        public static string GetVariableName(VariableDefinition variable)
        {
            if (string.IsNullOrEmpty(variable.Name))
            {
                return (variable.VariableType.Name[0] + "" + variable.Index).ToLower();
            }
            return variable.Name;
        }

        public static string GetIfStatement(MethodDefinition method, Instruction instruction)
        {
            var target = "";
            var ifStatement = "if(";
            var prev = instruction.Previous;
            if (InstructionHelper.IsLoad(prev.OpCode.Code))
            {
                target = GetValueOf(method, prev);
            }
            if (InstructionHelper.IsBranchOnFalse(instruction.OpCode.Code))
            {
                ifStatement += "!";
            }
            return ifStatement + target + ")";
        }

        public static string GetConditionalStatement(MethodDefinition method, Instruction instruction)
        {
            var code = instruction.OpCode.Code;
            var val2 = instruction.Previous;
            var val1 = val2.Previous;

            if (InstructionHelper.IsEqualTo(code))
            {
                return GetValueOf(method, val1) + " == " + GetValueOf(method, val2);
            }
            else if (InstructionHelper.IsLessThan(code))
            {
                return GetValueOf(method, val1) + " < " + GetValueOf(method, val2);
            }
            else
            {
                return GetValueOf(method, val1) + " > " + GetValueOf(method, val2);
                // IsGreaterThan
            }
        }

        /// <summary>
        /// This function will return the name of the variable, argument, etc
        /// that we want to store our value into.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="instruction"></param>
        /// <returns></returns>
        public static string GetStoreLocation(MethodDefinition method, Instruction instruction)
        {
            var code = instruction.OpCode.Code;
            if (instruction.Operand != null && !InstructionHelper.IsStoreN(code))
                return instruction.Operand.ToString();

            if (InstructionHelper.IsStoreLocalVariable(code))
            {
                if (InstructionHelper.IsStoreN(code))
                {
                    /* Not yet implemented. */
                }
                else
                {
                    var index = InstructionHelper.GetCodeIndex(code);
                    return GetVariableName(method.Body.Variables[index]);
                }
            }

            if (InstructionHelper.IsStoreArgument(code))
            {
                if (InstructionHelper.IsStoreN(code))
                {
                    return instruction.Operand.ToString();
                }
                else
                {
                    var index = InstructionHelper.GetCodeIndex(code);
                    return method.Parameters[index].Name;
                }
            }

            return "";
        }

        /// <summary>
        /// This function will return a string, representing the values grabbed from the instruction
        /// </summary>
        /// <param name="instruction"></param>
        /// <returns></returns>
        public static string GetValueOf(MethodDefinition method, Instruction instruction)
        {
            var code = instruction.OpCode.Code;

            // If this is a conditional instruction. lets return the value of that.
            if (InstructionHelper.IsConditional(instruction.OpCode.Code))
            {
                return GetConditionalStatement(method, instruction);
            }


            // Check if we are trying to load a value
            // Can be, Load a field, load a constant value, argument
            // variable and more.
            if (InstructionHelper.IsLoad(code))
            {

                // Check if we want to return a null value
                if (InstructionHelper.IsLoadNull(instruction.OpCode.Code))
                    return "null";

                // For now, we will settle with just handling integers.
                // We will have to add more if checks later to support
                // strings, variables, etc.
                if (InstructionHelper.IsLoadInteger(code))
                {
                    // In case the integer is bigger than 4
                    // We will need to get the value from the
                    // operand instead of the "index".
                    if (InstructionHelper.IsLoadN(code))
                    {
                        return instruction.Operand.ToString();
                    }
                    else
                    {
                        // The GetCodeIndex will return the value used from
                        // the constant load. For instance ldc.i4.4 will return
                        // the value 4, ldc.i4.3 will return value 3. Etc.
                        return InstructionHelper.GetCodeIndex(code).ToString();
                    }
                }

                // Check if we want to load a string
                // if so, then return the value from the operand.
                if (InstructionHelper.IsLoadString(code))
                {
                    return "\"" + instruction.Operand + "\"";
                }

                // If we want to load a local variable.
                // So be it!
                if (InstructionHelper.IsLoadLocalVariable(code))
                {
                    if (InstructionHelper.IsLoadN(code))
                    {
                        var targetVariable = instruction.Operand as VariableDefinition;
                        return GetVariableName(targetVariable);
                    }
                    else
                    {
                        var index = InstructionHelper.GetCodeIndex(code);
                        return GetVariableName(method.Body.Variables[index]);
                    }
                }

                // If we want to load the value of a argument
                // Then this is necessary.
                if (InstructionHelper.IsLoadArgs(code))
                {
                    if (InstructionHelper.IsLoadN(code))
                    {
                        return instruction.Operand.ToString();
                    }
                    else
                    {
                        var index = InstructionHelper.GetCodeIndex(code);
                        if (method.IsConstructor)
                        {
                            index--;
                        }
                        return method.Parameters[index].Name;
                    }
                }
            }

            // If our instructions are to call a method
            if (InstructionHelper.IsCallMethod(code))
            {
                // We will need to grab the information of that specific method
                // By casting the Operand into a MethodDefinition.
                var callingMethod = instruction.Operand as MethodDefinition;
                // Note: We are not managing any parameters yet.
                return callingMethod.Name + "()";
            }
            // Return a empty value if we can't determine what we want to return.
            return "";
        }

        public static string Indent(string inputFormat, int numberOfIndents, params object[] vars)
        {
            var output = inputFormat;
            for (var i = 0; i < numberOfIndents; i++)
            {
                output = "    " + output;
            }

            if (vars.Length > 0)
                return string.Format(output, vars);

            return output;
        }

        public static string GetTypeDeclaration(TypeDefinition type)
        {
            var output = (type.IsPublic ? "public " : "private ") +
                         (type.IsSealed ? "sealed " : "") +
                         (type.IsAbstract ? "abstract " : "") +
                         (type.IsInterface ? "interface " : "") +
                         (type.IsEnum ? "enum " : "") +
                         (type.IsClass ? "class " : "");

            if (type.HasGenericParameters)
            {
                var parameters = type.GenericParameters.Select(t => t.Name).ToList();
                if (parameters.Count > 0)
                    return output + (type.Name.Replace("`1", "")) + "<" + string.Join(",", parameters) + ">";
            }

            return output + type.Name;
        }

        public static string GetFieldDeclaration(FieldDefinition field)
        {
            var output = (field.IsPublic ? "public " : "private ") +
                         (field.IsStatic ? "static " : "");

            return output + field.FieldType + " " + field.Name;
        }


        public static string GetMethodDeclaration(MethodDefinition method)
        {
            var output = (method.IsPublic ? "public " : "private ") +
                         (method.IsStatic ? "static " : "") +
                         (method.IsAbstract ? "abstract " : "");

            if (method.IsConstructor)
            {
                // We have removed the previous return type from
                // the constructors
                output += method.DeclaringType.Name;
            }
            else
            {
                // We use the GetSystemTypeName to return a usable type name.
                output += GetSystemTypeName(method.MethodReturnType.ReturnType) + " " + method.Name;
            }

            output = output.Replace("`1", "");

            if (method.HasParameters)
            {
                var parameters = new List<string>();
                foreach (var parameter in method.Parameters)
                {
                    parameters.Add(GetSystemTypeName(parameter.ParameterType) + " " + parameter.Name);
                }
                return output + "(" + string.Join(", ", parameters) + ")";
            }

            return output + "()";
        }

        public static string GetSystemTypeName(TypeReference reference)
        {
            // A quick and dirty fix
            if (reference.Name == "Void")
                return "void";

            if (reference.IsValueType)
            {
                var output = reference.Name.ToLower();

                if (output == "int16") return "short";
                if (output == "int32") return "int";
                if (output == "int64") return "long";

                return output;
            }

            return reference.Name;
        }
    }
}