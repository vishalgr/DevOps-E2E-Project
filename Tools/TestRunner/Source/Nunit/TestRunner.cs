using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.TestRunner.NUnit
{
    // TODO: Shall kill if anything running earlier and shall kill child processes at end of the execution.
    class TestRunner : ITestRunner {
        private string executor;

        public TestRunner(string executor) {
            this.executor = executor;
        }

        int ITestRunner.Execute(string testAssembly, DirectoryInfo outPutDirectory) {
            // TODO: Implementation is due.
            //string filepath = @"D:\DevopsTestResults\sample.txt";
            string xmlName;
            xmlName = Path.GetFileNameWithoutExtension(testAssembly);
            string outpath = outPutDirectory.FullName;
            Process process = new Process();
            process.StartInfo.FileName =executor;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.Arguments =testAssembly+" --result "+outpath+"\\"+xmlName+".xml";
            process.Start();
            process.StandardInput.Close();
            Console.WriteLine(process.StandardOutput.ReadToEnd());
            if (process.ExitCode != 0)
            {
                Console.WriteLine(process.StandardError.ReadToEnd());
            }
            process.WaitForExit();
            return process.ExitCode;
        }

        List<string> ITestRunner.FindTestAssemblies(DirectoryInfo searchDirectory) {
            // Test assemble example:DevOps.Tests.TestRunner
            List<string> testAssemblies = new List<string>();
             foreach (var file in searchDirectory.GetFiles(("Testing*.dll") )) {
                 testAssemblies.Add(file.FullName);
             }

             return testAssemblies;
        }
    }
}
