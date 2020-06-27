@echo off
echo Command called: %0 %*
:PARSE_ARGS
if /I "%~1" EQU "--help" (
    goto help
)
if /I "%~1" EQU "--nuget" (
    set nuget=%~2
    shift
    shift
    goto PARSE_ARGS
)
if /I "%~1" EQU "--OutputDirectory" (
    set output=%~2
    shift
    shift
    goto PARSE_ARGS
)
if /I "%~1" NEQ "" (
    Powershell Write-host -BackgroundColor Red "Unknown argumnet: " %~1
    goto exitWithError
)

Rem error check
if not exist "%nuget%" (
    Powershell Write-host -BackgroundColor Red "nuget File Not Found: %nuget%"
    goto error
)
    
if not exist "*.nuspec" (
    Powershell Write-host -BackgroundColor Red ".nuspec File Not Found in the root directory"
	goto error
)

Rem Create the package
%nuget% pack -OutputDirectory %output%
echo the package is stored in "%output%"
if errorlevel <> 0 (
    Powershell Write-host -BackgroundColor Red "NuGet package creation failed"
    goto error
)

Rem Push the Nuget package
%nuget% push "%output%\*.nupkg" -src "github"
if errorlevel <> 0 (
    Powershell Write-host -BackgroundColor Red "Pushing NuGet package failed"
    goto error
)

echo "Command executed successfully"
exit /b 0

:exitWithError 
exit /b 1