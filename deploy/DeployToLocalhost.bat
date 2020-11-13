powershell %~dp0DeployDB.ps1 -DatabaseUpgradeScriptsPath "%~dp0..\db" -DatabaseServerName "localhost" -DatabaseName "SpeedTestResults" -Verbose
if not "%errorlevel%"=="0" exit /b 1