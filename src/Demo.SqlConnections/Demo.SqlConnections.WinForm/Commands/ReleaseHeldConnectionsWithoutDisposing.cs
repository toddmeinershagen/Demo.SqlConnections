namespace Demo.SqlConnections.WinForm.Commands
{
    public class ReleaseHeldConnectionsWithoutDisposing : Command
    {
        public override void Execute()
        {
            HoldOnToConnection.Connectors.Clear();
        }

        public override string Description => "Release Held Connections Without Disposing";
    }
}