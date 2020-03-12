Scalable Continuous integration and Continuous deployment using DevOps
Phase I

Reference: ScalableCI-CD_PlanningDoc.docx

1.	Objective: This document describes what and how to achieve the phase I of the solution.
Goal of this phase is to:
1.	Understand various tools which will be part of the initial product.
2.	Setup of Git server, Jenkins server (using docker), 
3.	Understand the roles of the management tools.
4.	Basic developer environment setup.
5.	Basic pipeline setup. Which includes:
Git, MS-scripts, Jenkins, VS codebase setup, TestRunner (with Nunit tests) and DB setup.
6.	Jenkins scripts are used as “Infrastructure as code” to trigger the build, run tests on different machines and push the passed builds to shared location.

2.	Way of working:
1.	There will be two teams. (you decide that).
2.	Two teams works simultaneously on assigned tasks.
3.	They share with complete technical details at end of every week (or the specific date you guys decided on).

3.	Teams and work:
3.1	Both the teams:Below tasks to be performed using batch script (4 hours). We need to as most the first level invocation happens with batch scripts as which does not requires any security restrictions and can be edited from any machine as it is in script format. (DLL’s cannot be debugged so easily but a script can be).
•	How to invoke exe or another batch.
•	Process input arguments. Ex: when TestArgs.Bat –Source-Directory c:\temp1 –Target-Directory D:\temp1 is provided, then script shall check both arguments are provided and throws error in case any extra argument provided or one of the input argument is missing.
•	Check execution status of the command being executed. (using ERRORLEVEL argument).
•	Return the exit value with proper error code (could be any integer value).


3.2	Both the teams:  Below tasks to be performed in powershell. (version 5 is good enough). Powershell script is used at various levels including communicating with cloud, Harbor and many other tools. Which is rich with .net libraries and almost anything can be done with powershellcommandlets.
•	String operations: Concat, string extraction
•	Read-write into file, connect to DB and run some queries.
•	Functions


3.3	Both the teams: C# will be the main programming language for the product development in our case. We are going to develop mock product with various tests using C#.
•	Basics of C#, OOPS, String operation.
•	NUNIT test case development using C#.
•	Connect to DB and run query using C#.

3.4	Both the teams: Basic MSBuild script. This is required to build the application.
•	Simple hello word MSbuild script
•	Property group and item group.
•	How to import other MSbuild scripts into a MSbuild script.
•	Invoke EXE using MSBuild.

3.5	Team 1:
•	How to perform source code repository using GIT. Which includes creating a repository at GitHub.
•	Basic git commands. Create repo, Clone repo, Pull, commit, push etc.


4.	Basic pipeline development: TBD:







Planning document
Scalable Continuous integration and Continuous deployment using devops


1.	Objective: Establish “Scalable Continuous integration and Continuous deployment by using DevOps” at different phases. This document provides high level guideline to achieving the intended solution.

2.	Description: 
This document describes the goal to achieve in 3 phases. Which also includes:
1.	Roles of responsibilities of each team member.
2.	Project management tools to be used and its roles.
3.	Timeline to achieve each phase.
4.	End product final assessment.
5.	Identifying improvements for future enhancement. 
















3.	Phase I:
Goal of this phase is to:
1.	Understand various tools which will be part of the initial product.
2.	Setup of Git server, Jenkins server (using docker), 
3.	Understand the roles of the management tools.
4.	Basic developer environment setup.
5.	Basic pipeline setup. Which includes:
Git, MS-scripts, Jenkins, VS codebase setup, TestRunner (with Nunit tests) and DB setup.
6.	Jenkins scripts are used as “Infrastructure as code” to trigger the build, run tests on different machines and push the passed builds to shared location.


4.	Phase II:
Goal of this phase is to:
1.	Enhance the solution to have Dockers, Containers and Cloud.
2.	Artifactory or Harbor tool to store the end product with automatic versioning.
3.	Enhance pipeline to support both Git and Svn.
4.	Enhance TestRunner to support MSTests.
5.	Various dashboard tools to show the pipeline health and status. Which are
a.	Pipeline health: html page shows the health of the pipeline. If expected results are not available, if pipeline is broken, expected machines are not up then that immediately reflects here.
b.	Test results activity: Html tool pulls information from the DB and show the live status of the test suites running in the various machines. This is required for developers to have quick feedback whether expected tests are passed or not.
c.	Test results dashboards:  Shows the code coverage for each archive, test results passing/failing trend per week (or for demanded duration).
5.	Phase III:
When this phase is reached, we already have a CI-CD running. Here most of the work will be related to cloud. Making the solution scalable. When end user wants to run X tests and want to have results in Y hours, then solution shall scale itself at various phases by including adjusting CPU usage at build servers, increasing number of VM’s in the cloud and provide the results in stipulated time. 
(Manikanta: I don’t have concrete technical details like how to achieve this, but I know for sure that this can be achieved).

6.	Timeline: TBD
7.	Roles and Responsibilities of individuals: TBD
8.	Way of working (WOW):
At the beginning two people will be working on one task. Going forward, at end of the phase I, individuals will work on different things. Everything shall be synced up among all of you, load shall be shared equally. One who is working on that area will be the expert in that but rest all aware how it works. In this way, one can reach the goal faster. 
There is no hard and fast rule, considering magnitude of the work which comes up amid of the development, WOW can be altered. The one proposed is ideal WOW in the Agile process.

How we can be a great team?
a.	Understand what to do before taking up work.
b.	Always do a spike and have a workable code, before taking up the product development.
c.	Challenge others with your knowledge.
d.	Open for constructive criticism. 
e.	Respect each other’s opinion and work.
f.	Appreciation is optional. (That shouldn’t be the motivation, in my view). 


9.	Documents to be delivered at end of the solution development:
It is required to understand the documents required to deliver at end of the product development (which includes time line to deliver them), to college and concerned authorities. The reason being:
1.	Documentation consumes lots of time. Being aware of what docs required upfront helps us to have better planning.
2.	No matter how much good work you do, if you don’t know how to present yourself and your product, it’s gonna eat dust.
3.	Depending on what we are presenting, we can also alter dashboard tools we develop, which benefits us to present our solution in better way.
4.	I would like to stress below point in your documentation. 
“Take away of this project.” : 
a.	You are going to learn software development tools and also project management tools.
b.	WOW is of industrial standards.


