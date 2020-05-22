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
$scriptExecutionStatus = -1
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
function RunExecutable($executable, $arguments) {
    Log ("Executing the executable '$executable' with arguments '$arguments'")
    $ProcessInfo= New-Object System.Diagnostics.ProcessStartInfo
    $ProcessInfo.FileName = $executable
    $ProcessInfo.RedirectStandardError = $true
    $ProcessInfo.RedirectStandardOutput = $true
    $ProcessInfo.UseShellExecute = $false
    $ProcessInfo.Arguments = $arguments

    $ProcessObj = New-Object System.Diagnostics.Process
    $ProcessObj.StartInfo = $processInfo
    $ProcessObj.Start() | Out-Null
    $ProcessObj.WaitForExit()
    $stdout = $ProcessObj.StandardOutput.ReadToEnd()    
    $exitCode = $ProcessObj.ExitCode
    Log("exit code: " + $exitCode)
    Log("StandardOutput: $stdout")

    # Check for execution status
    if($exitCode -ne 0) {
        $stderr = $ProcessObj.StandardError.ReadToEnd()
        Log("StandardError: $stderr")
    }
    return $exitCode
}

Function RunTestRunner() {
   # Construct TestRunner arguments
    $testRunnerArguments = @()
    $testRunnerArguments +="--Executor `"$Executor`" "
    $testRunnerArguments +="--TestFramework `"$TestFramework`" "
    $testRunnerArguments +="--AssemblyDirectory `"$AssemblyDirectory`" "
    $testRunnerArguments +="--OutputDirectory `"$OutputDirectory`" "
    
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
} catch {
    Log "Script execution failed"
    # TODO: Fix below object usage
    <#Log($Error[0].Exception)
    Log($Error[0].ScriptStackTrace    )
    #>
    $scriptExecutionStatus = -1
} finally {
     # Reserved place to perform cleanup activities.
     exit $scriptExecutionStatus
}