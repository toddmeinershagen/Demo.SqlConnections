using System;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class OpenAndThrowBeforeClosingConnectionWithUsing : Command
    {
        public override void Execute()
        {
            using (var connection = GetNewOpenConnectionByName())
            {
                throw new ArgumentException();
            }
        }

        public override string Description => "Open and Throw Before Closing a Connection With Using";
    }
}