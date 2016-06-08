using System.Collections.Generic;
using System.Data.SqlClient;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class HoldOnToConnectionWithMinPoolSize : Command
    {
        static HoldOnToConnectionWithMinPoolSize()
        {
            Connections = new List<SqlConnection>();
        }

        public override void Execute()
        {
            var connection = GetNewOpenConnectionByName("withMinPoolSize");
            Connections.Add(connection);
        }

        public override string Description => "Hold On To Connection With Min Pool Size";

        public static List<SqlConnection> Connections;
    }
}