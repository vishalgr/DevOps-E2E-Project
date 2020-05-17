
[CmdletBinding()]

Param

(

# Add some description here

[Parameter(Mandatory=$true, Position=0)]

$Executor='[Environment]::GetEnvironmentVariable("NUNIT", "Machine")',

 

# Add some description here

[Parameter(Mandatory=$true, Position=1)]

$TestFramework='Nunit',

 

# Add some description here

[Parameter(Mandatory=$true, Position=2)]

$AssemblyDirectory,

 

# Add some description here

[Parameter(Mandatory=$false, Position=3)]

$OutputDirectory

)



#This returns current working directory if you wish to use!!

 

#$ScriptRoot = Split-Path -Parent $PSCommandPath

#$exe1  = "$ScriptRoot\TestRunner\Output\DevOps.TestRunner.exe"

#$exe2 = "$ScriptRoot\CSVConverter\CSVConverter\bin\Debug\CSVConverter.exe"

 

$exe1  = '.\TestRunner\Output\DevOps.TestRunner.exe'

$exe2 = ".\CSVConverter\CSVConverter\bin\Debug\CSVConverter.exe"


& $exe1 --Executor $Executor --TestFramework $TestFramework --OutputDirectory $OutputDirectory --AssemlbyDirectory $AssemblyDirectory

 

& $exe2 --AssemlbyDirectory $OutputDirectory --OutputDirectory $OutputDirectory