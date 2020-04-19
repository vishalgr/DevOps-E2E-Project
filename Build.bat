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
Rem		which does clean and then build.
Rem 2. Build.Bat --incremental

echo Command called: %0 %*
set SCRIPT_DIR=%~dp0
cd /D %SCRIPT_DIR%
set TARGETS_SCRIPT=%SCRIPT_DIR%Build.targets
set CPU_COUNT=1
Rem using VSVARS throws error in Jenkins: \Java\jre1.8.0_172\bin"" was unexpected at this time.
Rem Because of this issue, it is expected to provide this as a property from the properties file.
Rem
Rem TODO: Change the path to your VSinstallation path
Rem set VSVARS=C:\VS2017\Common7\Tools\VsDevCmd.bat
Rem initialize MSBuild path
Rem if "VSVARS" EQU "" (
Rem 	echo The environment variable 'VSVARS' is not set
Rem 	goto error
Rem )
Rem echo Invoking the VSVARS file: %VSVARS%
Rem call %VSVARS%
Rem if errorlevel 1 goto error

REM set MSBUILD_EXE=C:\VS2017\MSBuild\15.0\Bin\MSBuild.exe

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
if "%~1" NEQ "" (
    echo Unrecognized argument: %1
    goto error
)

Rem Error check
echo CLEAN %CLEAN%
echo FULL_BUILD %FULL_BUILD%
echo INCREMENTAL_BUILD %INCREMENTAL_BUILD%
REM TODO:
REM Build --full --incremental does not cates below error
if "%INCREMENTAL_BUILD%" EQU "true" if "%FULL_BUILD%" EQU "true" (
	echo Input arguments 'full ' and 'incremental' are mutually exclusive.
)

Rem invoke MSBuild
Rem Clean
if "%CLEAN%" EQU "true" (
	rem set BUILD_CMD_BASE=msbuild.exe /m:%CPU_COUNT% /t:Clean %TARGETS_SCRIPT% /verbosity:d
	echo MSBuild.exe /m:%CPU_COUNT% /t:Clean %TARGETS_SCRIPT%
	MSBuild.exe /m:%CPU_COUNT% /t:Clean %TARGETS_SCRIPT%
	if errorlevel 1 goto error
)

Rem Build
set BUILD_CMD_BASE=MSBuild.exe %TARGETS_SCRIPT% /m:%CPU_COUNT% /t:Build
echo BUILD_CMD_BASE %BUILD_CMD_BASE%
%BUILD_CMD_BASE%
if errorlevel 1 goto error

Rem TODO: Publish to be performed

echo Build succeeded
if defined BUILD_INTERACTIVE (
    pause
)
goto :eof

:error
echo Build failed
exit /b 1