<#
    This script shall be executed from the jenkins node, typically during the OS start up, which does:
        a. Adds the curreent node to the Jenkins master
        b. Establishes connection with the master.
        
    TODO: At present this script uses hardcoded values of credentials, that shall be provided as environemnt variables.
#>
Start-Transcript -Path "StartJenkins.log" -Force -Verbose -Append
write-output "Start-Jenkins.ps1"
Set-ExecutionPolicy Unrestricted -Scope LocalMachine -Force -ErrorAction Ignore
$ErrorActionPreference = "Continue"

# Navigate to Jenkins dir
$workingDir = "c:\Jenkins4"
if(-Not (Test-Path $workingDir)) {
    mkdir $workingDir
}
Set-Location $workingDir

#TODO: Create environment variables JenkinsUserName, JenkinsPassword, JENKINS_URL at system level
# How to set env values using batch file, which can be retrieved via Powershell: setx  test1 "test1val"
# NOTE: Scripts fails without providing valid valies for the below arguments
$JenkinsUserName = "TO BE ENTERED"
$JenkinsUserPassword = "TO BE ENTERED"
$JENKINS_URL="TO BE ENTERED"

# Fetch required info
# Worst case, it is still possible to establish connection with some random name.
    # Note: 'computername' for all EC2 instances are same, hence the random number.
$Node_name = ""
try{
    $Node_name = ((Invoke-WebRequest -Uri http://169.254.169.254/latest/meta-data/public-hostname -UseBasicParsing).RawContent -split "`n")[-1]
} catch {
    write-output "Unable to fetch public dns name for the current EC2 instance"
}

if([string]::IsNullOrEmpty($Node_name)) {
    $Node_name = ($env:computername + "_"+ $(Get-Random))
}

# generate node info 
$nodeInfo = "<slave>
  <name>$Node_name</name>
  <description></description>
  <remoteFS>c:\Jenkins</remoteFS>
  <numExecutors>2</numExecutors>
  <mode>NORMAL</mode>
  <retentionStrategy class=`"hudson.slaves.RetentionStrategy\`"/>
  <launcher class=`"hudson.slaves.JNLPLauncher`">
    <workDirSettings>
      <disabled>false</disabled>
      <internalDir>remoting</internalDir>
      <failIfWorkDirIsMissing>false</failIfWorkDirIsMissing>
    </workDirSettings>
  </launcher>
  <label>win-test-slave TestMachine</label>
  <nodeProperties/>
  <userId>Administrator</userId>
</slave>"
Write-Output "NodeInfo: "
Write-Output $nodeInfo

# Download Jenkins cli
Write-Output "Downloading Jenkins-cli.jar using curl"
curl "$JENKINS_URL/jnlpJars/jenkins-cli.jar" -o jenkins-cli.jar

# curl "http://ec2-54-188-20-137.us-west-2.compute.amazonaws.com:8080/jnlpJars/jenkins-cli.jar" -o jenkins-cli.jar

# add the node to the Jenkins
Write-Output "Adding the node $Node_name"
$nodeInfo | java -jar jenkins-cli.jar -auth "$JenkinsUserName`:$JenkinsUserPassword" -s "$JENKINS_URL" create-node $Node_name

# download slave.jar
Write-Output "Downloading slave.jar using curl"
curl "$JENKINS_URL/jnlpJars/slave.jar" -o slave.jar

# Connect slave with the master
Write-Output "Connecting with the master"
Stop-Transcript -Verbose

java -jar slave.jar -jnlpUrl "${JENKINS_URL}/computer/${Node_name}/slave-agent.jnlp" -jnlpCredentials "$JenkinsUserName`:$JenkinsUserPassword"

# Delete node at shutting download.
# This is not being performed since Jenkins does this part for us.
# java -jar jenkins-cli.jar -auth "$JenkinsUserName`:$JenkinsUserPassword" -s "${JENKINS_URL}" delete-node $Node_name