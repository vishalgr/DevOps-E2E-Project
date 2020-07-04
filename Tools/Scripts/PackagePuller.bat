@echo off
echo Command called: %0 %*
set nugetPath=%SCRIPT_DIR%nuget.exe
if /I "%~1" EQU "" (
    Powershell Write-host -BackgroundColor Red "Parsing arguments cannot be null, please pass the arguments."
    goto help
)	
if /I "%~1" EQU "--PackageName" (
    set package=%~2
) else (
    Powershell Write-host -BackgroundColor Red "Invalid Arguments Passed, Try --help for more info about passing arguments"
    goto errorWithExit
)
if /I "%~3" EQU "--version" (
    set version=%~4
) else (
    Powershell Write-host -BackgroundColor Red "Invalid Arguments Passed, Try --help for more info about passing arguments"
    goto errorWithExit
)
if /I "%~5" EQU "--OutputDirectory" (
    set output=%~6
) else (
    Powershell Write-host -BackgroundColor Red "Invalid Arguments Passed, Try --help for more info about passing arguments"
    goto errorWithExit
)
if not exist "%nugetPath%" (
    Powershell Write-host -BackgroundColor Red "nuget.exe File Not Found in the specified directory"
    goto errorWithExit
)
if not exist "nuget.config" (
    Powershell Write-host -BackgroundColor Red "nuget.config File Not Found in the specified directory"
    goto errorWithExit
)

set cmdToExecute=%nugetPath% install %package% -Version %version% -OutputDirectory %output% 
echo %cmdToExecute%
%cmdToExecute%
rem nuget install NUnit -Version 3.11.0 -OutputDirectory c:\packages


:errorWithExit
exit /b 1

:help
Powershell Write-host -BackgroundColor Blue "Arguments to passed :"%~0" --PackageName nameofthePackage --version versionOfPackage --OutputDirectory outputDirectory-Path"
Powershell Write-host -BackgroundColor Blue "Example :"%~0" --PackageName NUnit --version 3.11.0 --OutputDirectory C:\Users\ABC\Desktop\Scripts_CALLERS