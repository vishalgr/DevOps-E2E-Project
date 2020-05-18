
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

# Variables
$testRunnerExe = "..\TestRunner\Output\DevOps.TestRunner.exe"
$csvConverterExe = "..\TestRunner\Output\DevOps.CSVConverter.exe"

# Functions
Function Log($message) {
    Write-Host ($(get-date -Format "yyyymmdd-HHMMss: ") + "$message")
}

# End of functions Functions
# TODO: 
# 1. Help message :referer to https://stackoverflow.com/questions/5237723/how-do-i-get-help-messages-to-appear-for-my-powershell-script-parameters
# 2. Start-Transcript: use this
# 3. 

try {

    Log "Script execution started"
    # Check pre-requisites
    If(-Not $testRunnerExe ) {
        throw "Test runner does not exists: $testRunnerExe"
    }
    If(-Not $csvConverterExe ) {
        throw "CSV converter does not exists: $csvConverterExe"
    }
    
    # Execute
    # TODO: Invoke below within a process and check the exit code.
    # Invoke TestRunner
    $testRunnerArguments = @()
    $testRunnerArguments += "--Executor `"$Executor`" "
    $testRunnerArguments += "--TestFramework `"$TestFramework`" "
    $testRunnerArguments += "--AssemlbyDirectory `"$AssemblyDirectory`" "
    $testRunnerArguments += "--OutputDirectory `"$OutputDirectory`" "
    &$testRunnerExe $testRunnerArguments

    # TODO: if exit code states failed, then no need to execute next command.

    # Invoke CSVConverter
    # Refer to the test runner code (available above)


    # check if everything is fine
    Log "Script execution completed successfully"

} catch {
    Log "Script execution failed"
    Write-Host "$_" -BackgroundColor Red
} finally {
    # Reserved place to perform cleanup activities.
}




#This returns current working directory if you wish to use!!

 

#$ScriptRoot = Split-Path -Parent $PSCommandPath

#$exe1  = "$ScriptRoot\TestRunner\Output\DevOps.TestRunner.exe"

#$exe2 = "$ScriptRoot\CSVConverter\CSVConverter\bin\Debug\CSVConverter.exe"

 <#

$exe1  = '.\TestRunner\Output\DevOps.TestRunner.exe'

$exe2 = ".\CSVConverter\CSVConverter\bin\Debug\CSVConverter.exe"


& $exe1 --Executor $Executor --TestFramework $TestFramework --OutputDirectory $OutputDirectory --AssemlbyDirectory $AssemblyDirectory

 

& $exe2 --AssemlbyDirectory $OutputDirectory --OutputDirectory $OutputDirectory
#>