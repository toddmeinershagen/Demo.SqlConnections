using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading;

namespace Demo.SqlConnections.WinForm.Commands
{
    public abstract class Command
    {
        public abstract void Execute();
        public abstract string Description { get; }

        protected SqlConnection GetNewOpenConnection(string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }

        protected SqlConnection GetNewOpenConnectionByName(string connectionName = "default")
        {
            var connectionString = GetConnectionStringByName(connectionName);
            return GetNewOpenConnection(connectionString);
        }

        protected string GetConnectionStringByName(string connectionName = "default")
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        protected void Wait(TimeSpan wait)
        {
            Thread.Sleep(wait);
        }
    }
}