@echo off
setlocal EnableDelayedExpansion
Rem This script initializes the required tools and invokes the build script. This script depends on the environment variable 
Rem VSVARS32=C:\VS2017\Common7\Tools\VsDevCmd.bat
Rem 
Rem Sets MSBuild path
Rem Sets environment variables required by the MSBuild
Rem
Rem Example: 
Rem 1. Build.Bat --clean
Rem 2. Build.Bat --full
Rem        which does clean and then build.
Rem 3. Build.Bat --incremental

Rem To build at server, below command to be used:
Rem 3. Build.Bat --server


echo Command called: %0 %*
set SCRIPT_DIR=%~dp0
cd /D %SCRIPT_DIR%
set TARGETS_SCRIPT=%SCRIPT_DIR%Build.targets
set CPU_COUNT=1
Rem using VSVARS throws error in Jenkins: \Java\jre1.8.0_172\bin"" was unexpected at this time.
Rem Because of this issue, it is expected to provide set MSBuild.exe path in the sytem variable 'PATH'.
Rem
Rem TODO: Change the path to your VSinstallation path
Rem set VSVARS=C:\VS2017\Common7\Tools\VsDevCmd.bat
Rem initialize MSBuild path
Rem if "VSVARS" EQU "" (
Rem     echo The environment variable 'VSVARS' is not set
Rem     goto error
Rem )
Rem echo Invoking the VSVARS file: %VSVARS%
Rem call %VSVARS%
Rem if errorlevel 1 goto error

Rem Defaullt arguments
set CLEAN=true
set FULL_BUILD=true

set INCREMENTAL_BUILD=

:PARSE_ARGS
if /I "%~1" EQU "--clean" (
    set CLEAN=true
    shift
    goto PARSE_ARGS
)
if /I "%~1" EQU "--incremental" (
    set INCREMENTAL_BUILD=true
    set CLEAN=false    
    set FULL_BUILD=false
    shift
    goto PARSE_ARGS
)
if /I "%~1" EQU "--full" (
    set CLEAN=true
    set FULL_BUILD=true
    shift
    goto PARSE_ARGS
)
if /I "%~1" EQU "--server" (
    set CLEAN=true
    set FULL_BUILD=true
    copy /y C:\Config\nuget.config %SCRIPT_DIR%
    shift
    goto PARSE_ARGS
)
if "%~1" NEQ "" (
    echo Unrecognized argument: %1
    goto error
)

Rem Error check
echo CLEAN %CLEAN%
echo FULL_BUILD %FULL_BUILD%
echo INCREMENTAL_BUILD %INCREMENTAL_BUILD%

Rem Pre-requisites check
if not exist "%SCRIPT_DIR%nuget.config" (
    echo "The file 'nuget.config' is missing at root directory. See 'nuget.config.template' for sample file"
    goto error
)

REM TODO:
REM Build --full --incremental does not cates below error
if "%INCREMENTAL_BUILD%" EQU "true" if "%FULL_BUILD%" EQU "true" (
    echo Input arguments 'full ' and 'incremental' are mutually exclusive.
)

Rem Invoke MSBuild
set BUILD_CMD_BASE=msbuild.exe /m:%CPU_COUNT% %TARGETS_SCRIPT% 

Rem Clean
if "%CLEAN%" EQU "true" (
    echo commandToExecute: %BUILD_CMD_BASE% /t:Clean
    %BUILD_CMD_BASE% /t:Clean
    if errorlevel 1 goto error
)

Rem Build and Publish
echo commandToExecute: %BUILD_CMD_BASE% /t:Build;Publish
%BUILD_CMD_BASE% /t:Build;Publish
if errorlevel 1 goto error

Powershell Write-host -BackgroundColor Green "Build succeeded"
goto :eof

:error
Powershell Write-host -BackgroundColor Red "Build failed"
exit /b 1