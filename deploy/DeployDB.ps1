[CmdletBinding()]
Param(
	[string]$DatabaseUpgradeScriptsPath,	
	[string]$DatabaseServerName,
	[string]$DatabaseName,
    [switch]$DropDatabase
)

Write-Verbose "Database Upgrade Scripts Path: $DatabaseUpgradeScriptsPath"
Write-Verbose "Database Server: $DatabaseServerName"
Write-Verbose "Database Name: $DatabaseName"
Write-Verbose "Drop Database: $DropDatabase"

. .\DatabaseDeploymentFunctions.ps1

if ([System.IO.Path]::IsPathRooted($DatabaseUpgradeScriptsPath) -eq $false) {
	$DatabaseUpgradeScriptsPath = Join-Path $PSScriptRoot $DatabaseUpgradeScriptsPath
}

if ($DropDatabase) {
    Drop-Database -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName
}

if ((Test-Database $DatabaseServerName $DatabaseName) -eq $false)
{
    Write-Verbose "Database $DatabaseName does not exist, creating database..."
    Create-Database -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName
}

Upgrade-Database -DatabaseUpgradeScriptsPath $DatabaseUpgradeScriptsPath -DatabaseServerName $DatabaseServerName -DatabaseName $DatabaseName