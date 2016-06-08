using System;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class OpenAndThrowBeforeClosingConnection : Command
    {
        public override void Execute()
        {
            var connection = GetNewOpenConnectionByName();
            throw new ArgumentException();
            connection.Close();
        }

        public override string Description => "Open and Throw Before Closing a Connection";
    }
}