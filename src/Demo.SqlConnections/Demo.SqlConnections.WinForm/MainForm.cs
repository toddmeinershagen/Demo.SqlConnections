using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

using Demo.SqlConnections.WinForm.Commands;
using Demo.SqlConnections.WinForm.Decompiler;


namespace Demo.SqlConnections.WinForm
{
    public partial class MainForm : Form
    {
        private readonly System.Windows.Forms.Timer _counterTimer = new System.Windows.Forms.Timer();

        public MainForm()
        {
            InitializeComponent();
            _counterTimer.Tick += _counterTimer_Tick;

            _counterTimer.Interval = 100;
            _counterTimer.Start();

            var instanceName = GetInstanceName();
            SqlServerCounters = new CounterRepository();
            AdoNetCounters = new CounterRepository(instanceName);

            UserConnectionsTreeNode = CounterTree.Nodes.Add(Counters.UserConnections.CategoryName).Nodes.Add(string.Empty);

            var adoNetTreeNode = CounterTree.Nodes.Add(Counters.NumberOfActiveConnectionPools.CategoryName);
            NumberOfActiveConnectionPoolGroupsTreeNode = adoNetTreeNode.Nodes.Add(string.Empty);
            NumberOfInactiveConnectionPoolGroupsTreeNode = adoNetTreeNode.Nodes.Add(string.Empty);
            NumberOfActiveConnectionPoolsTreeNode = adoNetTreeNode.Nodes.Add(string.Empty);
            NumberOfInactiveConnectionPoolsTreeNode = adoNetTreeNode.Nodes.Add(string.Empty);
            NumberOfPooledConnectionsTreeNode = adoNetTreeNode.Nodes.Add(string.Empty);
            NumberOfNonPooledConnectionsTreeNode = adoNetTreeNode.Nodes.Add(string.Empty);
            NumberOfReclaimedConnectionsTreeNode = adoNetTreeNode.Nodes.Add(string.Empty);
        }

        private void _counterTimer_Tick(object sender, EventArgs e)
        {
            UpdateCounterStats();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateCounterStats();
            CounterTree.ExpandAll();
            AcceptButton = ExecuteButton;

            CommandList.Items.Add(new ProperlyOpenAndCloseAConnection());
            CommandList.Items.Add(new ProperlyOpenAndCloseDifferentConnections());
            CommandList.Items.Add(new OpenAndThrowBeforeClosingConnection());
            CommandList.Items.Add(new OpenAndThrowBeforeClosingConnectionWithTryCatch());
            CommandList.Items.Add(new OpenAndThrowBeforeClosingConnectionWithUsing());
            CommandList.Items.Add(new HoldOnToConnection());
            CommandList.Items.Add(new HoldOnToConnectionWithoutPooling());
            CommandList.Items.Add(new HoldOnToConnectionWithMinPoolSize());
            CommandList.Items.Add(new HoldOnToConnectionWithMaxPoolSize());
            CommandList.Items.Add(new ReleaseHeldConnectionsWithoutDisposing());
            CommandList.Items.Add(new ReleaseHeldConnectionsAfterDisposing());
            CommandList.Items.Add(new ClearThisPool());
            CommandList.Items.Add(new ClearAllPools());
            CommandList.SelectedIndex = 0;
            CommandList.Select();
        }

        private void UpdateCounterStats()
        {
            const string format = "{0}:  {1}";
            UserConnectionsTreeNode.Text = string.Format(format, Counters.UserConnections.CounterName, SqlServerCounters.GetCounter(Counters.UserConnections).NextValue());

            NumberOfActiveConnectionPoolGroupsTreeNode.Text = string.Format(format, Counters.NumberOfActiveConnectionPoolGroups.CounterName, AdoNetCounters.GetCounter(Counters.NumberOfActiveConnectionPoolGroups).NextValue());
            NumberOfInactiveConnectionPoolGroupsTreeNode.Text = string.Format(format, Counters.NumberOfInactiveConnectionPoolGroups.CounterName, AdoNetCounters.GetCounter(Counters.NumberOfInactiveConnectionPoolGroups).NextValue());
            NumberOfActiveConnectionPoolsTreeNode.Text = string.Format(format, Counters.NumberOfActiveConnectionPools.CounterName, AdoNetCounters.GetCounter(Counters.NumberOfActiveConnectionPools).NextValue());
            NumberOfInactiveConnectionPoolsTreeNode.Text = string.Format(format, Counters.NumberOfInactiveConnectionPools.CounterName, AdoNetCounters.GetCounter(Counters.NumberOfInactiveConnectionPools).NextValue());
            NumberOfPooledConnectionsTreeNode.Text = string.Format(format, Counters.NumberOfPooledConnections.CounterName, AdoNetCounters.GetCounter(Counters.NumberOfPooledConnections).NextValue());
            NumberOfNonPooledConnectionsTreeNode.Text = string.Format(format, Counters.NumberOfNonPooledConnections.CounterName, AdoNetCounters.GetCounter(Counters.NumberOfNonPooledConnections).NextValue());
            NumberOfReclaimedConnectionsTreeNode.Text = string.Format(format, Counters.NumberOfReclaimedConnections.CounterName, AdoNetCounters.GetCounter(Counters.NumberOfReclaimedConnections).NextValue());
        }

        public CounterRepository SqlServerCounters { get; }
        public CounterRepository AdoNetCounters { get; }
        public TreeNode UserConnectionsTreeNode { get; }
        public TreeNode NumberOfActiveConnectionPoolGroupsTreeNode { get; }
        public TreeNode NumberOfInactiveConnectionPoolGroupsTreeNode { get; }
        public TreeNode NumberOfActiveConnectionPoolsTreeNode { get; }
        public TreeNode NumberOfInactiveConnectionPoolsTreeNode { get; }
        public TreeNode NumberOfPooledConnectionsTreeNode { get; }
        public TreeNode NumberOfNonPooledConnectionsTreeNode { get; }
        public TreeNode NumberOfReclaimedConnectionsTreeNode { get; set; }

        private static string GetInstanceName()
        {
            var instanceName = Assembly.GetEntryAssembly()
                .GetName()
                .Name;

            var pid = GetCurrentProcessId().ToString();
            instanceName = $"{instanceName}[{pid}]";
            return instanceName;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern int GetCurrentProcessId();

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            var command = CommandList.SelectedItem as Command;

            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Command Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CommandList_SelectedIndexChanged(object sender, EventArgs e)
        {
           //var gatherer = new DecompiledSourceGatherer();
            var gatherer = new FileBasedSourceGatherer();
            SourceTextBox.Text = gatherer.GetSourceFor(CommandList.SelectedItem.GetType());
        }
    }
}
