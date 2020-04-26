using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        int ITestRunner.Execute(string testAssembly) {
            // TODO: Implementation is due.
            Console.WriteLine(executor);
            return 0;
        }

        List<string> ITestRunner.FindTestAssemblies(DirectoryInfo searchDirectory) {
            // Test assemble example:DevOps.Tests.TestRunner
            List<string> testAssemblies = new List<string>();
             foreach (var file in searchDirectory.GetFiles("DevOps.Tests.*.dll")) {
                 testAssemblies.Add(file.FullName);
             }

             return testAssemblies;
        }
    }
}
