using System;
using System.IO;

namespace Demo.SqlConnections.WinForm
{
    public class FileBasedSourceGatherer : ISourceGatherer
    {
        public string GetSourceFor(Type type)
        {
            var path = $"{Environment.CurrentDirectory}\\Commands\\{type.Name}.cs";

            return File.Exists(path) == false 
                ? "<WARNING:  Source Not Found>" 
                : File.ReadAllText(path);
        }
    }
}