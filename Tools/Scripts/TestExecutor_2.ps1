$Executor = "C:\Program Files (x86)\NUnit.org\nunit-console\nunit3-console.exe"
$TestFramework = "NUnit"
$AssemblyDirectory = "D:\website\Website\bin"
$OutputDirectory = "D:\DEVOPS_test\Tools\files"
$exe1  = '.\TestRunner\Output\DevOps.TestRunner.exe'
$exe2 = ".\CSVConverter\CSVConverter\bin\Debug\CSVConverter.exe"

& $exe1 --Executor $Executor --TestFramework $TestFramework --OutputDirectory $OutputDirectory --AssemlbyDirectory $AssemblyDirectory

& $exe2 --AssemlbyDirectory $OutputDirectory --OutputDirectory $OutputDirectory
