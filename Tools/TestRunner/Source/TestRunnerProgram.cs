using System;
using System.Collections.Generic;
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
            var testAssemblies = testRunner.FindTestAssemblies(arguments.AssemblyDirectory);
            if (testAssemblies.Count > 0)
            {
                foreach (var testAssembly in testAssemblies)
                {
                    int returnValue = testRunner.Execute(testAssembly, arguments.OutputDirectory);
                    if (returnValue < 0)
                    {
                        exitCode = -1;
                    }
                }
                Console.WriteLine("TestRunner executed successfully");
                return exitCode;
            }
            else
            {
                Console.WriteLine("Couldnt Find The Tests,Please Give The Correct Directory");
                exitCode = -1;
                return exitCode;
            }
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
