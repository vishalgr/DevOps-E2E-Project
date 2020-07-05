@echo off
echo Command called: %0 %*
set SCRIPT_DIR=%~dp0
set nugetPath=%SCRIPT_DIR%nuget.exe
set nuspecPath=%SCRIPT_DIR%Package.nuspec


:PARSE_ARGS
if /I "%~1" EQU "" (
    Powershell Write-host -BackgroundColor Red "Parsing arguments cannot be null, please pass the arguments."
    goto help
    goto errorWithExit
)
if /I "%~1" EQU "--help" (
    goto help
)
if /I "%~1" EQU "--OutputDirectory" (
    set output=%~2
) else (
    Powershell Write-host -BackgroundColor Red "Invalid Arguments Passed, Try --help for more info about passing arguments"
    goto errorWithExit
)
if not exist "%nuspecPath%" (
    Powershell Write-host -BackgroundColor Red ".nuspec File Not Found in the root directory"
    goto errorWithExit
)
if not exist "%nugetPath%" (
    Powershell Write-host -BackgroundColor Red "nuget.exe File Not Found in the specified directory"
    goto errorWithExit
)
if not exist "%output%" (
    mkdir %output%
)
set cmdToExecute=%nugetPath% pack %nuspecPath% -OutputDirectory %output%
echo Command to execute: %cmdToExecute%
%cmdToExecute%

if errorlevel 2 goto error
echo the package is created successfully at "%output%"

if exist 'dir /b "%output%\*.nupkg"' (
    Rem Without the option 'SkipDuplicate', the command fails indicating 'Response status code does not indicate success: 409 (Conflict).'
    echo "Command to push the package to the repository: %nugetPath% push '%output%\*.nupkg' -src github -SkipDuplicate"
    %nugetPath% push "%output%\*.nupkg" -src "github" -SkipDuplicate
    if errorlevel 1 goto error
    echo pushed successfully
)

echo "PackageExecutor file executed successfully
exit /b 0

:errorWithExit
exit /b 1

:help
Powershell Write-host -BackgroundColor Blue "Arguments to passed : --OutputDirectory outputDirectory-path"
Powershell Write-host -BackgroundColor Blue "Example : --OutputDirectory  "D:\MasterClone\MasterClone\Output"
exit /b 1

:error
Powershell Write-host -BackgroundColor Red "Execution Failed %errorlevel% "
exit /b 1
