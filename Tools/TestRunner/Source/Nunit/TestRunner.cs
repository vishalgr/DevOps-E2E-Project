using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DevOps.TestRunner.NUnit
{
    // TODO: Shall kill if anything running earlier and shall kill child processes at end of the execution.
    class TestRunner : ITestRunner {
        private string executor;

        public TestRunner(string executor) {
            this.executor = executor;
        }

        int ITestRunner.Execute(string testAssembly, DirectoryInfo outPutDirectory) {
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
            if (process.ExitCode != 0) {
                Console.WriteLine(process.StandardError.ReadToEnd());
            }
            process.WaitForExit();
            return process.ExitCode;
        }

        List<string> ITestRunner.FindTestAssemblies(DirectoryInfo searchDirectory, string TestSuite = null) {
            // Test assemble example:DevOps.Tests.TestRunner
            List<string> testAssemblies = new List<string>();
            if (! string.IsNullOrEmpty(TestSuite)) {
                //var xmlPath = @"D:\Personal\Harshitha\RealizePipeline_Mani\Pipeline\TestSuites\TestSuite_Production1.xml";
                XDocument testResultDoc = XDocument.Load(TestSuite);
                var testAssemblyNodes = testResultDoc.Descendants("TestAssembly");
                foreach (var testAssembly in testAssemblyNodes) {
                    var assemblyPath = Path.Combine(searchDirectory.FullName, testAssembly.Value);
                    testAssemblies.Add(assemblyPath);
                }
            } else {
                var testAssembliesRaw = searchDirectory.GetFiles("DevOps.Tests.*.dll").ToList();
                testAssembliesRaw.AddRange(searchDirectory.GetFiles("DevOps.Test.*.dll"));
                foreach (var file in testAssembliesRaw) {
                    testAssemblies.Add(file.FullName);
                }
            }

            return testAssemblies;
        }
    }
}
