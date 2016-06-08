using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class HoldOnToConnection : Command
    {
        static HoldOnToConnection()
        {
            Connectors = new List<DbConnector>();
        }

        public override void Execute()
        {
            GetInstanceOfDbConnector();
        }

        public DbConnector GetInstanceOfDbConnector()
        {
            var connector = new DbConnector(GetNewOpenConnectionByName());
            Connectors.Add(connector);
            return connector;
        }

        public override string Description => "Hold On To Connection";
        public static List<DbConnector> Connectors { get; }

        public class DbConnector : IDisposable
        {
            private bool _disposed = false;

            public DbConnector(SqlConnection connection)
            {
                Connection = connection;
            }

            public void Dispose()
            {
                Close();
            }

            public void Close()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }
            protected virtual void Dispose(bool disposing)
            {
                if (_disposed) return;

                if (disposing)
                {
                    Connection.Dispose();
                }

                _disposed = true;
            }

            ~DbConnector()
            {
                Dispose(false);
            }

            private SqlConnection Connection { get; }
        }
    }
}