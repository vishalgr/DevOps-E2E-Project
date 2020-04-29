using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DevOps.TestRunner.NUnit
{
    // TODO: Shall kill if anything running earlier and shall kill child processes at end of the execution.
    class TestRunner : ITestRunner {
        private string executor;

        public TestRunner(string executor) {
            this.executor = executor;
        }

        int ITestRunner.Execute(string testAssembly) {
            // TODO: Implementation is due.
            DevOps.TestRunner.Arguments refer1= new DevOps.TestRunner.Arguments();
            Process process = new Process();
            process.StartInfo.FileName =executor;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments = "nunit3-console.exe  "+testAssembly+" --result D:\\DEVOPS_test-branches-BasicPipeline\\Tools\\TestRunner\\Output\\result.xml";
            process.Start();
            process.StandardInput.Close();
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            process.WaitForExit();
           // Console.ReadKey();
            process.Close();


            Console.WriteLine(executor);
            return 0;
        }

        List<string> ITestRunner.FindTestAssemblies(DirectoryInfo searchDirectory) {
            // Test assemble example:DevOps.Tests.TestRunner
            List<string> testAssemblies = new List<string>();
             foreach (var file in searchDirectory.GetFiles("Console*.dll")) {
                 testAssemblies.Add(file.FullName);
             }

             return testAssemblies;
        }
    }
}
