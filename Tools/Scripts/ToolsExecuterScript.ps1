<#
.SYNOPSIS
    Powershell script which runs the tests and generates test execution summary in the format of csv file.
    
.DESCRIPTION
    Which invokes the testrunner, upon success the csvconverter is invoked. Final outcome are the:
        a. Test results
        b. Csv file summary reports contains the consolidated test results information. 

.EXAMPLE
    C:\PS> 
    TestExecuter.ps1 -Executor "C:\Program Files (x86)\NUnit.org\nunit-console\nunit3-console.exe" -TestFramework NUnit -AssemblyDirectory D:\github\DEVOPS_test\Tools\LoginApplication\Output -OutputDirectory D:\DEVOPS_test\Tools\files

#>
[CmdletBinding()]

Param (
    # The path to the executor. Ex: nunit-console.exe.
    [Parameter(Mandatory=$true)]
    $Executor,

    # Name of the test framework. Ex: NUnit, MSTest
    [Parameter(Mandatory=$true)]
    $TestFramework,

    # Directory where test assemblies are located. Ex:The path to the folder where the dll of the test cases are stored.
    [Parameter(Mandatory=$true)]
    $AssemblyDirectory,

    # Directory where test results to be generated. Ex: The xml and csv files storing location.
    [Parameter(Mandatory=$true)]
    $OutputDirectory
)

$scriptPath = split-path -parent $MyInvocation.MyCommand.Definition

Write-Host "scriptPath: $scriptPath"

# Variables
$scriptExecutionStatus = -1
$Count = 1
$testRunnerExe = Join-Path -Path "$scriptPath" -ChildPath "..\TestRunner\Output\DevOps.TestRunner.exe"
$csvConverterExe = Join-Path -Path "$scriptPath" -ChildPath "..\CSVConverter\Output\DevOps.CSVConverter.exe"


Function Log($message) {
    Write-Host ($(get-date -Format "yyyymmdd-HHMMss: ") + "$message")
}

# Runs executable and returns exit code.
function RunExecutable($executable, $arguments) {
    Log ("Executing the executable '$executable' with arguments '$arguments'")
    $processInfo= New-Object System.Diagnostics.ProcessStartInfo
    $processInfo.FileName = $executable
    $processInfo.RedirectStandardError = $true
    $processInfo.RedirectStandardOutput = $true
    $processInfo.UseShellExecute = $false
    $processInfo.Arguments = $arguments

    $processObj = New-Object System.Diagnostics.Process
    $processObj.StartInfo = $processInfo
    $processObj.Start() | Out-Null
    $processObj.WaitForExit()
    $stdout = $processObj.StandardOutput.ReadToEnd()    
    $exitCode = $processObj.ExitCode
    Log("exit code: " + $exitCode)
    Log("StandardOutput: $stdout")
      # Check for execution status
    if($exitCode -ne 0) {
        $stderr = $processObj.StandardError.ReadToEnd()
        Log("StandardError: $stderr")
    }
        return $exitCode
}

Function RunTestRunner() {
    # Construct TestRunner arguments
    $testRunnerArguments = @()
    $testRunnerArguments += "--Executor `"$Executor`" "
    $testRunnerArguments += "--TestFramework `"$TestFramework`" "
    $testRunnerArguments += "--AssemblyDirectory `"$AssemblyDirectory`" "
    $testRunnerArguments += "--OutputDirectory `"$OutputDirectory`" "
    $returnVal = RunExecutable $testRunnerExe $testRunnerArguments
    if($returnVal -ne 0) {
        throw "Test runner execution failed"
    }
}

Function RunCsvConverterExe() {
    # Construct CSVConverter arguments
    $CSVConverterArguments = @()
    $CSVConverterArguments += "--XmlFileDirectory `"$OutputDirectory`" "
    $CSVConverterArguments += "--OutputDirectory `"$OutputDirectory`" "
    # Invoke CSVConverter
    $returnVal = RunExecutable $csvConverterExe $CSVConverterArguments
    if($returnVal -ne 0) {
    throw "csvConverterExe execution failed"
    }
}

try {
    # Start of the transcript
    Start-Transcript -Path "$OutputDirectory\Transcript.txt" -Append
    Log "Script execution started"

    # Check pre-requisites
    If(-Not (Test-Path $testRunnerExe) ) {
      throw "Test runner does not exists: $testRunnerExe"
    } 
    If(-Not (Test-Path $csvConverterExe) ) {
      throw "CSV converter does not exists: $csvConverterExe"
    }
    RunTestRunner
    RunCsvConverterExe
    Log "Script execution completed successfully"
    $scriptExecutionStatus = 0
    Stop-Transcript
}

catch {
    Start-Transcript -Path "$OutputDirectory\Transcripterror.txt" -Append
    Log "Script execution failed"
    $errorMessage = ($Error[0].Exception.Message + $Error[0].Exception.StackTrace)
    Log("ExceptionMessage = " + $errorMessage)
    $scriptExecutionStatus = -1
    Stop-Transcript
} finally {
    # Reserved place to perform cleanup activities.
    exit $scriptExecutionStatus
}