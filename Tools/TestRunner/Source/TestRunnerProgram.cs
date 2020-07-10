using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.TestRunner
{
    class TestRunnerProgram {
        static int Main(string[] args) {
            int exitCode = 0;
            // Parse arguments
            var arguments = new Arguments();
            if (!arguments.Parse(args))
            {
                Console.WriteLine("Parsing input arguments failed: " + arguments.ErrorMessage);
                Console.WriteLine(Arguments.Usage);
                return -1;
                //throw new  Exception("Test runner execution failed");
            }

            ITestRunner testRunner = GetTestRunner(arguments);
            string testSuite = null;
            if (arguments.TestSuite != null) {
                testSuite = arguments.TestSuite.FullName;
            }
            var testAssemblies = testRunner.FindTestAssemblies(arguments.AssemblyDirectory, testSuite);
            exitCode = 0;
            foreach (var testAssembly in testAssemblies) {
                if (!File.Exists(testAssembly)) {
                    exitCode = -1;
                    Console.WriteLine("Test assembly does not exists: " + testAssembly);
                    continue;
                }
                // Return value should not decide the status of the execution, since we care only about test execution, not whether tests are passed or failed.
                int returnValue = testRunner.Execute(testAssembly, arguments.OutputDirectory);
                Console.WriteLine(
                    string.Format(
                        "The test assembly {0} executed with the return value of {1}",
                        testAssembly,
                        returnValue
                    )
                );
            }

            if (exitCode == 0) {
                Console.WriteLine("TestRunner executed successfully");
            } else {
                Console.WriteLine("TestRunner execution failed");
            }
            return exitCode;
        }
        

        private static ITestRunner GetTestRunner(Arguments arguments) {
            ITestRunner testRunner = null;
            switch (arguments.TestFrameWork) {
                case TestFrameWork.NUnit:
                    testRunner = new NUnit.TestRunner(arguments.Executor);
                    break;
                case TestFrameWork.MSTest:
                    testRunner = new MSTest.TestRunner();
                    break;
            }

            return testRunner;
        }
    }
}
