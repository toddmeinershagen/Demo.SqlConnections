using System;
using System.Data.SqlClient;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class ClearAllPools : Command
    {
        public override void Execute()
        {
            SqlConnection.ClearAllPools();
        }

        public override string Description => "Clear All Connection Pools";
    }
}