using System;
using System.Linq;
using System.Text;

using Mono.Cecil;

namespace Demo.SqlConnections.WinForm.Decompiler
{
    public class DecompiledSourceGatherer : ISourceGatherer
    {
        public string GetSourceFor(Type type)
        {
            var path = $"{Environment.CurrentDirectory}\\{System.Reflection.Assembly.GetEntryAssembly().GetName().Name}.exe";
            var assembly = AssemblyDefinition.ReadAssembly(path);
            var builder = new StringBuilder();
            var typeDefinition = assembly.MainModule.Types.FirstOrDefault(x => x.Name == type.Name);
            WriteSourceCodeFor(typeDefinition, builder);

            return builder.ToString();
        }

        private void WriteSourceCodeFor(TypeDefinition type, StringBuilder builder, int indent = 0)
        {
            if (!type.IsNested && !string.IsNullOrEmpty(type.Namespace))
            {
                builder.AppendLine("namespace " + type.Namespace);
                builder.AppendLine("{");
                indent++;
            }

            var typeDeclaration = AssemblyHelper.GetTypeDeclaration(type);

            builder.AppendLine(AssemblyHelper.Indent("{0}", indent, typeDeclaration));
            builder.AppendLine(AssemblyHelper.Indent("{", indent));

            indent++;

            var methods = type.Methods;
            foreach (var method in methods)
            {
                var methodDeclaration = AssemblyHelper.GetMethodDeclaration(method);
                builder.AppendLine(AssemblyHelper.Indent("{0}", indent, methodDeclaration));
                if (!method.IsAbstract)
                {
                    builder.AppendLine(AssemblyHelper.Indent("{", indent));
                    builder.Append(AssemblyHelper.GenerateMethodBody(method, indent + 1));
                    builder.AppendLine(AssemblyHelper.Indent("}", indent));
                    builder.AppendLine();
                }
            }

            var fields = type.Fields;
            foreach (var field in fields)
            {
                var fieldDeclaration = AssemblyHelper.GetFieldDeclaration(field);
                builder.AppendLine(AssemblyHelper.Indent("{0}", indent, fieldDeclaration));
            }

            var nestedTypes = type.NestedTypes;
            foreach (var nestedType in nestedTypes)
            {
                WriteSourceCodeFor(nestedType, builder, indent);
            }

            indent--;
            builder.AppendLine(AssemblyHelper.Indent("}", indent));

            if (!string.IsNullOrEmpty(type.Namespace))
            {
                builder.AppendLine("}");
            }
        }
    }
}