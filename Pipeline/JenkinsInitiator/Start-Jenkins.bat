@echo off
set THIS_SCRIPT_DIR=%~dp0
cmd.exe /c powershell.exe -NonInteractive -ExecutionPolicy Unrestricted -command "&{. %THIS_SCRIPT_DIR%\Start-Jenkins.ps1}"