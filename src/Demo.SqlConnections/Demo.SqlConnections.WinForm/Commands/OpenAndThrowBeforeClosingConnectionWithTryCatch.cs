using System;
using System.Data.SqlClient;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class OpenAndThrowBeforeClosingConnectionWithTryCatch : Command
    {
        public override void Execute()
        {
            SqlConnection connection = null;

            try
            {
                connection = GetNewOpenConnectionByName();
                throw new ArgumentException();
            }
            catch (Exception)
            {
                // ignore
            }
            finally
            {
                connection?.Close();
            }
        }

        public override string Description => "Open and Throw Before Closing a Connection With Try/Catch";
    }
}