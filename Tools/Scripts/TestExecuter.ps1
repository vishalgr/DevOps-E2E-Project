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

<# TODO: 

    1. Start-Transcript
    2. Help basecd text
#>


# TODO: Casing and indentaiton
$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

Write-Host "scriptPath: $scriptPath"

# Variables
$ReturnVal = -1
$Count = 1
$testRunnerExe = Join-Path -Path "$scriptPath" -ChildPath "..\TestRunner\Output\DevOps.TestRunner.exe"
$csvConverterExe = Join-Path -Path "$scriptPath" -ChildPath "..\CSVConverter\Output\DevOps.CSVConverter.exe"
#$csvConverterExe = Join-Path -Path "$scriptPath" -ChildPath "..\CSVConverter\Output\CSVConverter.exe"


# Functions
# TODO: Correct all indentations
Function Log($message) {
    Write-Host ($(get-date -Format "yyyymmdd-HHMMss: ") + "$message")
}

# Runs executable and returns exit code.
function RunExecutable($program, $PassedArguments) {
    
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
        $exitCode = $ProcessObj.ExitCode        
        return $exitCode
    }

try {
    Log "Script execution started"

    # Check pre-requisites
    If(-Not (Test-Path $testRunnerExe) ) {
        throw "Test runner does not exists: $testRunnerExe"
    } 
    If(-Not (Test-Path $csvConverterExe) ) {
        throw "CSV converter does not exists: $csvConverterExe"
    }

    # Execute

    # TODO: Invoke below within a process and check the exit code.

    # Construct TestRunner arguments
    $testRunnerArguments = @()
    $testRunnerArguments +="--Executor `"$Executor`" "
    $testRunnerArguments +="--TestFramework `"$TestFramework`" "
    $testRunnerArguments +="--AssemlbyDirectory `"$AssemblyDirectory`" "
    $testRunnerArguments +="--OutputDirectory `"$OutputDirectory`" "

    #&$testRunnerExe $testRunnerArguments
    $ReturnVal = RunExecutable -program "$testRunnerExe" -PassedArguments "$testRunnerArguments" 

    if($ReturnVal -ne 0) {
        throw "Test runner execution failed"
    }

    # Invoke CSVConverter
    RunExecutable -program "$csvConverterExe" -PassedArguments "$CSVConverterArguments" 

    # Construct CSVConverter arguments
    $CSVConverterArguments = @()
    $CSVConverterArguments += "--XmlFileDirectory `"$OutputDirectory`" "
    $CSVConverterArguments += "--OutputDirectory `"$OutputDirectory`" "

    Log "Script execution completed successfully"
    ReturnVal = 0
} catch {
    Log "Script execution failed"
    Write-Host "$_" -BackgroundColor Red
    ReturnVal = -1
} finally {
     # Reserved place to perform cleanup activities.
     exit ReturnVal
}