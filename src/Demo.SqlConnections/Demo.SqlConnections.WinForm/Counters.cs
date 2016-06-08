namespace Demo.SqlConnections.WinForm
{
    public class Counters
    {
        private static readonly string AdoNetCategoryName = ".NET Data Provider for SqlServer";

        public static readonly CounterKey NumberOfActiveConnectionPoolGroups = new CounterKey(AdoNetCategoryName, "NumberOfActiveConnectionPoolGroups");
        public static readonly CounterKey NumberOfInactiveConnectionPoolGroups = new CounterKey(AdoNetCategoryName, "NumberOfInactiveConnectionPoolGroups");
        public static readonly CounterKey NumberOfActiveConnectionPools = new CounterKey(AdoNetCategoryName, "NumberOfActiveConnectionPools");
        public static readonly CounterKey NumberOfInactiveConnectionPools = new CounterKey(AdoNetCategoryName, "NumberOfInactiveConnectionPools");
        public static readonly CounterKey NumberOfPooledConnections = new CounterKey(AdoNetCategoryName, "NumberOfPooledConnections");
        public static readonly CounterKey NumberOfNonPooledConnections = new CounterKey(AdoNetCategoryName, "NumberOfNonPooledConnections");
        public static readonly CounterKey NumberOfReclaimedConnections = new CounterKey(AdoNetCategoryName, "NumberOfReclaimedConnections");
        public static readonly CounterKey UserConnections = new CounterKey("SQLServer:General Statistics", "User Connections");
    }
}