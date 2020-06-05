@echo off
echo Command called: %0 %*
:PARSE_ARGS
if /I "%~1" EQU "" (
goto empty
)
if /I "%~1" EQU "--help" (
    goto help
)
if /I "%~1" EQU "--nuget" (
    set nuget=%~2 	
) else (
	goto errorArgs
	)
if /I "%~3" EQU "--OutputDirectory" (
    set output=%~4
) else (
	goto errorArgs
	)
if /I "%~5" EQU "--packageName" (
    set packageName=%~6
) else (
	goto errorArgs
	)
if not exist "*.nuspec" (
	goto error2
)
if not exist "%nuget%" (
	goto error1
)
%nuget% pack -OutputDirectory %output%
		   if errorlevel 2 goto error
		   echo the package is stored in "%output%"
            echo Finished.
				if exist 'dir /b "%output%\%packageName%.nupkg"' (
                    %nuget% push "%output%\%packageName%.nupkg" -src "github"
					if errorlevel 1 goto error
                    echo pushed successfully
					) else (
					goto error3
					) 
					)
:errorArgs
Powershell Write-host -BackgroundColor Red "Invalid Arguments Passed, Try --help for more info about passing arguments"
exit /b 1

:help
Powershell Write-host -BackgroundColor Blue "Arguments to passed :--nuget nuget.exe-path --OutputDirectory outputDirectory-path --packageName name_of_the_package" 
Powershell Write-host -BackgroundColor Blue "Example : --nuget "D:\MasterClone\MasterClone\nuget.exe" --OutputDirectory  "D:\MasterClone\MasterClone\Output" --packageName packageName.1.0.0

exit /b 1

:error1
Powershell Write-host -BackgroundColor Red "nuget.exe File Not Found in the specified directory"
exit /b 1

:error2
Powershell Write-host -BackgroundColor Red ".nuspec File Not Found in the root directory"
exit /b 1

:error3
Powershell Write-host -BackgroundColor Red ".nupkg File Not Found"
exit /b 1

:error
Powershell Write-host -BackgroundColor Red "Execution Failed %errorlevel% "
exit /b 1

:empty
Powershell Write-host -BackgroundColor Red "Parsing arguments cannot be null,plz pass the arguments Try --help for more info about passing arguments"