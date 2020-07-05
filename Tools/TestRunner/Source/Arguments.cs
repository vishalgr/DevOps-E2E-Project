using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace DevOps.TestRunner
{
    // Test frameworks the current design supports
    public enum TestFrameWork {
        NUnit = 1,
        MSTest = 2
    }
    /// <summary>
    /// Process arguments and verify that invalid arguments are not provided
    ///  It does:
    /// -- Validates the input arguments
    /// -- Generates Arguments object
    /// </summary>
    public class Arguments {
        private string testFramework;
        private string executor;
        private DirectoryInfo assemblyDirectory;
        private FileInfo testSuite;
        private DirectoryInfo outPutDirectory;
        private static readonly string currentAssembly = typeof(Arguments).Assembly.GetName().Name;
        private readonly StringBuilder errorMessage = new StringBuilder();

        
        #region Public methods
        /// <summary>
        /// Parses input arguments and validates the same
        /// </summary>
        /// <param name="commandLinArgs"></param>
        /// <returns></returns>
        public bool Parse(string[] commandLinArgs) {
            if (commandLinArgs.Length < 1) {
                errorMessage.AppendLine("Input arguments cannot be null");
                return false;
            }

            var isParseSuccess = true;
            for (int i = 0; i < commandLinArgs.Length; i++) {
                switch (commandLinArgs[i].ToUpper().Trim()) {
                    case "--EXECUTOR":
                        executor = commandLinArgs[++i].Trim();
                        break;
                    case "--TESTFRAMEWORK":
                        testFramework = commandLinArgs[++i].Trim();
                        break;
                    case "--ASSEMBLYDIRECTORY":
                        assemblyDirectory = new DirectoryInfo(commandLinArgs[++i].Trim());
                        break;
                    case "--TESTSUITE":
                        testSuite = new FileInfo(commandLinArgs[++i].Trim());
                        break;
                    case "--OUTPUTDIRECTORY":
                        outPutDirectory = new DirectoryInfo(commandLinArgs[++i].Trim());
                        break;
                    default:
                        errorMessage.AppendLine("Invalid input argument: " + commandLinArgs[i]);
                        isParseSuccess = false;
                        break;

                }
            }

            return isParseSuccess && Validate();
        }

        // Usage help text
        public static string Usage {
            get {
                // Invoke way: --TestFramework --AssemlbyDirectory --OutputDirectory
                var newLine = Environment.NewLine;
                var help = new StringBuilder("Usage information:");
                help.AppendLine(
                    currentAssembly +
                    " --Executor <Executor path> --TestFramework <test framework> --AssemblyDirectory <directory path> --OutputDirectory <directory path> [--TestSuite <TestSuite xml path>]"
                );
                help.AppendLine("TestFramework: Name of the test framework. Ex: " + string.Join(", ", Enum.GetNames(typeof(TestFrameWork))));
                help.AppendLine("AssemblyDirectory: Directory where test assemblies are located");
                help.AppendLine("OutputDirectory: Directory where test results to be geneareated at");
                help.AppendLine("TestSuite: [Optional Argumnet] Xml file contains test assemblies to be executed");
                help.AppendLine(
                    "Example: " + currentAssembly +
                    @" --Executor D:\Output\Work\Binaries\NunitConsoleRunner\nunit3-console.exe --TestFramework NUnit --AssemblyDirectory C:\Tests --OutputDirectory C:\TestResults --TestSuite D:\TestSuite_Production1.xml"
                );
                return help.ToString();
            }
        }
        #endregion

        #region Public arguments

        public DirectoryInfo AssemblyDirectory {
            get {
                return assemblyDirectory;
            }
        }

        public DirectoryInfo OutputDirectory
        {
            get {
                return outPutDirectory;
            }
        }

        public string Executor
        {
            get
            {
                return executor;
            }
        }

        public TestFrameWork TestFrameWork {
            get {
                return (TestFrameWork) Enum.Parse(typeof(TestFrameWork), testFramework);
            }
        }

        public FileInfo TestSuite {
            get {
                return testSuite;
            }
        }

        public string ErrorMessage {
            get { return errorMessage.ToString(); }
        }

        #endregion

        #region Private methods
        private bool Validate() {
            bool isValidationSuccess = true;
            var isSuccess = Enum.TryParse(testFramework, true, out TestFrameWork testFrameworkOutput);
            if (!isSuccess) {
                isValidationSuccess = false;
                errorMessage.AppendLine(
                    string.Format(
                        "testFramework '{0}' is invalid. It must be one among the below: {1} ",
                        testFramework,
                        string.Join(", ", Enum.GetNames(typeof(TestFrameWork)))
                    )
                );
            }

            if (!checkDirectory(assemblyDirectory)) {
                isValidationSuccess = false;
            }
            
            if (!checkDirectory(outPutDirectory)) {
                Directory.CreateDirectory(outPutDirectory.FullName);
            }

            if (!File.Exists(executor)) {
                isValidationSuccess = false;
                errorMessage.AppendLine(
                    "The file does not exists:" + executor
                );
            }

            if ((testSuite != null) && !string.IsNullOrEmpty(testSuite.FullName) && !File.Exists(testSuite.FullName)) {
                isValidationSuccess = false;
                errorMessage.AppendLine(
                    "The file does not exists:" + testSuite.FullName
                );
            }

            return isValidationSuccess;
        }

        private bool checkDirectory(DirectoryInfo directory) {
            if (!directory.Exists)
            {
                errorMessage.AppendLine(
                    "The directory does not exists:" + outPutDirectory.FullName
                );
                return false;
            }

            return true;
        }
        #endregion
    }
}
