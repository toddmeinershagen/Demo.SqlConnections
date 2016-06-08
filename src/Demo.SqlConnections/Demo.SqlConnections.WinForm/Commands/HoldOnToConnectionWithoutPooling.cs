using System.Collections.Generic;
using System.Data.SqlClient;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class HoldOnToConnectionWithoutPooling : Command
    {
        static HoldOnToConnectionWithoutPooling()
        {
            Connections = new List<SqlConnection>();
        }

        public override void Execute()
        {
            var connection = GetNewOpenConnectionByName("withoutPooling");
            Connections.Add(connection);
        }

        public override string Description => "Hold On To Connection Without Pooling";

        public static List<SqlConnection> Connections;
    }
}