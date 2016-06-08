namespace Demo.SqlConnections.WinForm.Commands
{
    public class ReleaseHeldConnectionsAfterDisposing : Command
    {
        public override void Execute()
        {
            foreach (var connector in HoldOnToConnection.Connectors)
            {
                connector.Dispose();
            }

            HoldOnToConnection.Connectors.Clear();
        }

        public override string Description => "Release Held Connections After Disposing";
    }
}