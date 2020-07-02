@echo off
echo Command called: %0 %*
:PARSE_ARGS
if /I "%~1" EQU "" (
Powershell Write-host -BackgroundColor Red "Parsing arguments cannot be null, please pass the arguments."
goto help
goto errorWithExit
)
if /I "%~1" EQU "--help" (
    goto help
)
if /I "%~1" EQU "--nuget" (
    set nuget=%~2
) else (
    Powershell Write-host -BackgroundColor Red "Invalid Arguments Passed, Try --help for more info about passing arguments"
    goto errorWithExit
    )
if /I "%~3" EQU "--OutputDirectory" (
    set output=%~4
) else (
    Powershell Write-host -BackgroundColor Red "Invalid Arguments Passed, Try --help for more info about passing arguments"
    goto errorWithExit
    )
if not exist "*.nuspec" (
Powershell Write-host -BackgroundColor Red ".nuspec File Not Found in the root directory"
    goto errorWithExit
)
if not exist "%nuget%" (
 Powershell Write-host -BackgroundColor Red "nuget.exe File Not Found in the specified directory"
    goto errorWithExit
)
if not exist "%output%" (
    mkdir %output%
)

%nuget% pack -OutputDirectory %output%
		   if errorlevel 2 goto error
		   echo the package is stored in "%output%"
            echo Finished.
				if exist 'dir /b "%output%\*.nupkg"' (
                    %nuget% push "%output%\*.nupkg" -src "github"
					if errorlevel 1 goto error
                    echo pushed successfully
					) else (
					goto error3
					)
					)
:errorWithExit
exit /b 1
:help
Powershell Write-host -BackgroundColor Blue "Arguments to passed :--nuget nuget.exe-path --OutputDirectory outputDirectory-path"
Powershell Write-host -BackgroundColor Blue "Example : --nuget "D:\MasterClone\MasterClone\nuget.exe" --OutputDirectory  "D:\MasterClone\MasterClone\Output"
exit /b 1
:error
Powershell Write-host -BackgroundColor Red "Execution Failed %errorlevel% "
exit /b 1
