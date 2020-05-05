using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVConverter
{
   
    class Arguments
    {
        private DirectoryInfo assemblyDirectory;
        private DirectoryInfo outPutDirectory;
        private readonly StringBuilder errorMessage = new StringBuilder();
        private static readonly string currentAssembly = typeof(Arguments).Assembly.GetName().Name;

        #region Public methods
        public bool Parse(string[] commandLinArgs)
        {
            if (commandLinArgs.Length < 1)
            {
                errorMessage.AppendLine("Input arguments cannot be null");
                return false;
            }
            var isParseSuccess = true;
            for (int i = 0; i < commandLinArgs.Length; i++)
            {
                switch (commandLinArgs[i].ToUpper().Trim())
                {
                    case "--ASSEMLBYDIRECTORY":
                        assemblyDirectory = new DirectoryInfo(commandLinArgs[++i].Trim());
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
        public static string Usage
        {
            get
            {
                // Invoke way: --TestFramework --AssemlbyDirectory --OutputDirectory
                var newLine = Environment.NewLine;
                var help = new StringBuilder("Usage information:");
                help.AppendLine(
                    currentAssembly +
                    " --AssemlbyDirectory <directory path> --OutputDirectory <directory path>"
                );
              
                help.AppendLine("AssemlbyDirectory: Directory where XML assemblies are located");
                help.AppendLine("OutputDirectory: Directory where CSV results to be geneareated at");
                help.AppendLine(
                    "Example: " + currentAssembly +
                    @"  --AssemlbyDirectory C:\TestsResultsXml --OutputDirectory C:\TestResultsCsv"
                );
                return help.ToString();
            }
        }
        #endregion



        #region Public arguments
        public DirectoryInfo AssemblyDirectory
        {
            get
            {
                return assemblyDirectory;
            }
        }

        public DirectoryInfo OutputDirectory
        {
            get
            {
                return outPutDirectory;
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage.ToString(); }
        }

        #endregion


        #region Private methods
        private bool Validate()
        {
            bool isValidationSuccess = true;
            if (!checkDirectory(assemblyDirectory))
            {
                isValidationSuccess = false;
            }

            if (!checkDirectory(outPutDirectory))
            {
                isValidationSuccess = false;
            }
            return isValidationSuccess;
        }

        private bool checkDirectory(DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                errorMessage.AppendLine(
                    "The directory does not exists:" + directory.FullName
                );
                return false;
            }

            return true;
        }
        #endregion
    }
}