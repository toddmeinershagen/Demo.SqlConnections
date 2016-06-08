date;

while ($true)
{
    Clear-Host
    Get-CimInstance Win32_PerfRawData_NETDataProviderforSqlServer_NETDataProviderforSqlServer `
        | format-table Name, `
        @{Label="Active Pool Groups"; Expression={$_.NumberOfActiveConnectionPoolGroups}}, `
        @{Label="Inactive Pool Groups"; Expression={$_.NumberOfInactiveConnectionPoolGroups}}, `
        @{Label="Active Pools"; Expression={$_.NumberOfActiveConnectionPools}}, `
        @{Label="Inactive Pools"; Expression={$_.NumberOfInactiveConnectionPools}}, `
        @{Label="Pooled"; Expression={$_.NumberOfPooledConnections}}, `
        @{Label="Non-Pooled"; Expression={$_.NumberOfNonPooledConnections}}, `
        @{Label="Reclaimed"; Expression={$_.NumberOfReclaimedConnections}} `
        -autosize
 
    Start-Sleep -s 1
}