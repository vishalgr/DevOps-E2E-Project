using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOps.CSVConverter
{
   
   public class Arguments
    {
        private DirectoryInfo xmlfileDirectory;
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
                    case "--XMLFILEDIRECTORY":
                        xmlfileDirectory = new DirectoryInfo(commandLinArgs[++i].Trim());
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
                    " --XmlFileDirectory <directory path> --OutputDirectory <directory path>"
                );
              
                help.AppendLine("XmlFileDirectory: Directory where XML files are located");
                help.AppendLine("OutputDirectory: Directory where CSV results to be geneareated at");
                help.AppendLine(
                    "Example: " + currentAssembly +
                    @"  --XmlFileDirectory C:\TestsResultsXml --OutputDirectory C:\TestResultsCsv"
                );
                return help.ToString();
            }
        }
        #endregion

        #region Public arguments
        public DirectoryInfo XmlFileDirectory
        {
            get
            {
                return xmlfileDirectory;
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
            if (!checkDirectory(xmlfileDirectory))
            {
                isValidationSuccess = false;
            }

            if (!checkDirectory(outPutDirectory))
            {
                Directory.CreateDirectory(outPutDirectory.FullName);
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