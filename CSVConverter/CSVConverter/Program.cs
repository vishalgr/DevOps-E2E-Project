using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.IO.Pipes;
using System.Xml;

namespace CSVConverter
{

    class TestCase {
        public string Name;
        public bool IsExecuted;
        public bool Status;
        public string TimeLapsed;
        public string FailureMessage = string.Empty;
    }

    class TestCases {
        private List<TestCase> allTestCases = new List<TestCase>();
        private const string csvHeader = "Name,Status,IsExecuted,TimeLapsed";
        private const string csvDelimiter = ",";
        public int PassedTests = 0;
        public int FailedTests = 0;

        public IReadOnlyCollection<TestCase> AllTestCases {
            get { return allTestCases; }
        }

        public IReadOnlyCollection<TestCase> UniqueTests
        {
            get { return allTestCases.Distinct().ToList(); }
        }

        public void AppendTest(TestCase testCase) {
            allTestCases.Add(testCase);
            if (testCase.Status == false) {
                PassedTests++;
            } else if (testCase.Status == true) {
                FailedTests++;
            }
        }

        public void WriteIntoCsv(string filePath) {
            StreamWriter csvFileWriter = new StreamWriter(filePath);
            // Write header
            csvFileWriter.WriteLine(csvHeader);

            // TODO: Dont dump error message, which contains new line characters. Which has to be pre-processed.
            foreach (var testCase in AllTestCases) {
                var testCaseInfo = new StringBuilder();
                testCaseInfo.Append(testCase.Name + csvDelimiter);
                testCaseInfo.Append(testCase.Status + csvDelimiter);
                testCaseInfo.Append(testCase.IsExecuted + csvDelimiter);
                testCaseInfo.Append(testCase.TimeLapsed + csvDelimiter);
                csvFileWriter.WriteLine(testCaseInfo.ToString());
            }
            csvFileWriter.Close();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the path where you have the xml file");
            //string resultFilePath = Console.ReadLine();
            // TODO: remove below line
            string resultFilePath =
                @"D:\Personal\Harshitha\basicFlow\Examples\HelloWorld\Output\Bin\TestResult.xml";
            FileInfo outputFile = new FileInfo(@"D:\DevopsTestResults\result3.csv");
            if (!outputFile.Directory.Exists) {
                Directory.CreateDirectory(outputFile.DirectoryName);
            }

            StringBuilder sb = new StringBuilder();
            string delimit = ",";
            XDocument testResultDoc = XDocument.Load(resultFilePath);
            var testCasesActual = testResultDoc.Descendants("test-case");
            var testCases = new TestCases();
            foreach (var testCaseActual in testCasesActual) {
                var testCase = new TestCase();
                testCase.Name = testCaseActual.Attribute("name").Value;
                testCase.IsExecuted = Convert.ToBoolean(testCaseActual.Attribute("executed").Value);
                testCase.Status = false;
                if (testCase.IsExecuted) {
                    testCase.Status = Convert.ToBoolean(testCaseActual.Attribute("success").Value);
                }

                if (testCase.Status == false) {
                    // TODO: Xpaths to be used.
                    testCase.FailureMessage =
                        testCaseActual.Element("failure").Element("message").Value;
                }
                testCase.TimeLapsed = testCaseActual.Attribute("time").Value;

                testCases.AppendTest(testCase);
            }

            testCases.WriteIntoCsv(outputFile.FullName);
        }
    }
}
