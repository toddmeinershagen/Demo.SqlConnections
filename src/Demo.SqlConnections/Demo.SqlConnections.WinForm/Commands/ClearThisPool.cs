using System.Data.SqlClient;

namespace Demo.SqlConnections.WinForm.Commands
{
    public class ClearThisPool : Command
    {
        public override void Execute()
        {
            using (var connection = GetNewOpenConnectionByName())
            {
                SqlConnection.ClearPool(connection);
            }
        }

        public override string Description => "Clear This Connection Pool";
    }
}