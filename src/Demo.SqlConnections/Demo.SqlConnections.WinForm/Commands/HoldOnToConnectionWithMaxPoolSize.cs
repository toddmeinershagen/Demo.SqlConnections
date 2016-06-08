using System.Collections.Generic;
using System.Data.SqlClient;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class HoldOnToConnectionWithMaxPoolSize : Command
    {
        static HoldOnToConnectionWithMaxPoolSize()
        {
            Connections = new List<SqlConnection>();
        }

        public override void Execute()
        {
            var connection = GetNewOpenConnectionByName("withMaxPoolSize");
            Connections.Add(connection);
        }

        public override string Description => "Hold On To Connection With Max Pool Size";

        public static List<SqlConnection> Connections;
    }
}