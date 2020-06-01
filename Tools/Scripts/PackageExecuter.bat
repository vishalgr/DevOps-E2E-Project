@echo off
echo Proceed with checking nuget.exe and downloading if missing.
rem downloads nuget.exe if not present 
if not exist nuget.exe (
    echo Downloading nuget.exe...
    powershell -Command "Invoke-WebRequest https://dist.nuget.org/win-x86-commandline/latest/nuget.exe -OutFile nuget.exe"
    echo Finished downloading
    ) 
else (
    echo nuget.exe located.
    )
rem checking if nuget.exe exists
if exist nuget.exe (
	rem checking if .nuspec exists
	if exist *.nuspec (
		rem creating .nupkg
            nuget pack
            echo Finished.
			rem checking if .nupkg exists
             if exist *.nupkg (
			 rem pushing the .nupkg file to github package registry
                    nuget push "*.nupkg" -src "github"
                    echo pushed successfully
                )else (
                echo .nupkg file is missing
            )
        )else (
        echo .nuspec file is missing
    )
  )