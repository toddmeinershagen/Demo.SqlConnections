namespace Demo.SqlConnections.WinForm.Commands
{
    public class ProperlyOpenAndCloseAConnection : Command
    {
        public override void Execute()
        {
            var connection = GetNewOpenConnectionByName();
            connection.Close();
        }

        public override string Description => "Properly Open/Close a Connection";
    }
}