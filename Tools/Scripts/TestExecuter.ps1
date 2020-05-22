[CmdletBinding()]

Param (

# Executor to execute the tests

    [Parameter(Mandatory=$true)]

$Executor,

# Add some description here

    [Parameter(Mandatory=$true)]

$TestFramework,

# Add some description here

    [Parameter(Mandatory=$true)]

$AssemblyDirectory,

# Add some description here

    [Parameter(Mandatory=$true)]

$OutputDirectory

)

$MyDir = Get-Location

# Variables
    $ReturnVal = -1
    $Count = 1
    $testRunnerExe = Join-Path -Path "$MyDir" -ChildPath "\TestRunner\Output\DevOps.TestRunner.exe"
    $csvConverterExe = Join-Path -Path "$MyDir" -ChildPath "\CSVConverter\Output\DevOps.CSVConverter.exe"

# Functions

    Function Log($message) 

    {

        Write-Host ($(get-date -Format "yyyymmdd-HHMMss: ") + "$message")

    }

    function toolcall($program,$PassedArguments)
     {
    
        $ProcessInfo= New-Object System.Diagnostics.ProcessStartInfo
        $ProcessInfo.FileName = $program
        $ProcessInfo.RedirectStandardError = $true
        $ProcessInfo.RedirectStandardOutput = $true
        $ProcessInfo.UseShellExecute = $false
        $ProcessInfo.Arguments = $PassedArguments
        $ProcessObj = New-Object System.Diagnostics.Process
        $ProcessObj.StartInfo = $processInfo
        $ProcessObj.Start() | Out-Null
        $ProcessObj.WaitForExit()
        $stdout = $ProcessObj.StandardOutput.ReadToEnd()
        $stderr = $ProcessObj.StandardError.ReadToEnd()
        Write-Host "stdout: $stdout"
        Write-Host "stderr: $stderr"
        Write-Host "exit code: " + $ProcessObj.ExitCode
        
        return $ProcessObj.ExitCode 
    }

    try 

    {

        Log "Script execution started"

        # Check pre-requisites

            If(-Not $testRunnerExe )
         
            {
            
                throw "Test runner does not exists: $testRunnerExe"
            }

            If(-Not $csvConverterExe ) 

            {

               throw "CSV converter does not exists: $csvConverterExe"
            }

        # Execute

        # TODO: Invoke below within a process and check the exit code.

        # Invoke TestRunner

        $testRunnerArguments = @()

        $testRunnerArguments +="--Executor `"$Executor`" "

        $testRunnerArguments +="--TestFramework `"$TestFramework`" "

        $testRunnerArguments +="--AssemblyDirectory `"$AssemblyDirectory`" "

        $testRunnerArguments +="--OutputDirectory `"$OutputDirectory`" "

        #&$testRunnerExe $testRunnerArguments
        $ReturnVal=toolcall -program "$testRunnerExe" -PassedArguments "$testRunnerArguments" 



        if($ReturnVal -eq 0)
        {
        toolcall -program "$csvConverterExe" -PassedArguments "$CSVConverterArguments" 
        }

        #CSVarguments

        $CSVConverterArguments = @()

        $CSVConverterArguments += "--XmlFileDirectory `"$OutputDirectory`" "

        $CSVConverterArguments += "--OutputDirectory `"$OutputDirectory`" "
        
        
        

        Log "Script execution completed successfully"

    } 

    catch 

    {

        Log "Script execution failed"

        Write-Host "$_" -BackgroundColor Red

    }

    finally 
    {

     # Reserved place to perform cleanup activities.
    }