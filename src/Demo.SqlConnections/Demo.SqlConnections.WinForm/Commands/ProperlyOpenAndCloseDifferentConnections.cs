namespace Demo.SqlConnections.WinForm.Commands
{
    public class ProperlyOpenAndCloseDifferentConnections : Command
    {
        private static int _counter = 0;

        public override void Execute()
        {
            _counter++;
            var connectionString = $"{GetConnectionStringByName()};Application Name=App{_counter}";
            var connection = GetNewOpenConnection(connectionString);
            connection.Close();
        }

        public override string Description => "Properly Open/Close Different Connections";
    }
}