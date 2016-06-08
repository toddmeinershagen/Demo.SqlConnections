namespace Demo.SqlConnections.WinForm
{
    public class CounterKey
    {
        public CounterKey(string categoryName, string counterName)
        {
            CategoryName = categoryName;
            CounterName = counterName;
        }

        public string CategoryName { get; private set; }
        public string CounterName { get; private set; }

        public override int GetHashCode()
        {
            var hash = 17;
            hash = hash*23 + CategoryName.GetHashCode();
            hash = hash*23 + CounterName.GetHashCode();
            return hash;
        }
    }
}