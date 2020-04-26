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
            if (! arguments.Parse(args)) {
                Console.WriteLine("Parsing input arguments failed: " + arguments.ErrorMessage);
                Console.WriteLine(Arguments.Usage);
                throw new  Exception("Test runner execution failed");
            }

            ITestRunner testRunner = GetTestRunner(arguments);
            var testAssemblies = testRunner.FindTestAssemblies(arguments.AssemblyDirectory);
            foreach (var testAssembly in testAssemblies) {
                int returnValue = testRunner.Execute(testAssembly);
                if (returnValue < 0) {
                    exitCode = -1;
                }
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
