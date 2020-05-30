using DevOps.CSVConverter;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;


namespace DevOps.Tests.CSVConverter
{
    [TestFixture]
    public class ArgumentsTest
    {
        private static string assemblyPath = System.Reflection.Assembly.GetAssembly(typeof(ArgumentsTest)).Location;
        private static string assemblyDir = Path.GetDirectoryName(assemblyPath);

        private static DirectoryInfo xmlfileDir = new DirectoryInfo(Path.GetFullPath(
             Path.Combine(assemblyDir, @"..\..\..\Examples\HelloWorld\Output")
          ));
        private static DirectoryInfo outputDirectory = new DirectoryInfo(Path.GetFullPath(
             Path.Combine(assemblyDir, @"..\..\..\Examples\HelloWorld\Output")
          ));
        private static DirectoryInfo MissingxmlDirectory = new DirectoryInfo(Path.GetFullPath(
             Path.Combine(assemblyDir, @"..\..\..\Examples\HelloWorld\Output")
          ));

        public DevOps.CSVConverter.Arguments TestObject;
        public DevOps.CSVConverter.Program TestMainFuncObject;
  
        private static string[] validArguments = {
            "--xmlFileDirectory",
            xmlfileDir.FullName,
            "--OutputDirectory",
            outputDirectory.FullName
    };
        private static string[] inValidArguments = {
            "--xmlDirectory",
            xmlfileDir.FullName,
            "--OutputDirectory",
            outputDirectory.FullName
    };
        private static string[] wrongArguments = {
            "--xmlFileDirectory",
          MissingxmlDirectory.FullName,
            "--OutputDirectory",
            outputDirectory.FullName

        };
        [SetUp]
        public void TestSetup()
        {
            Console.WriteLine("TestSetup goes here");
            if (!Directory.Exists(outputDirectory.FullName)) {
                Directory.CreateDirectory(outputDirectory.FullName);
            }
            TestObject = new DevOps.CSVConverter.Arguments();
            TestMainFuncObject = new DevOps.CSVConverter.Program();

        }
        
        [Test]
        public void TestThatWithValidArgumentsTheParsePasses() {
           bool returnValue = TestObject.Parse(validArguments);
          Assert.AreEqual(true, returnValue, "Expected different input");
        }

        [Test]
        public void TestThatWithInvalidArgumentsTheParseFails()
        {
            bool returnValue = TestObject.Parse(inValidArguments);
            Assert.AreEqual(false, returnValue, "Expected different input");
        }
        //This test wont create the Output Folder since invalid arument are passesd and the test returns -1
        [Test]
        public void TestThatWithInValidArgumentsToTheMainMethodTheTestPasses()
        {
            int returnValue = DevOps.CSVConverter.Program.Main(inValidArguments);
            Assert.AreEqual(-1, returnValue, "Expected different input");
        }
        //This tests output will be -1 since wrong path of the xml file is passed
        [Test]
        public void TestWithWrongXMLFilePathTheOutputWillBeMINUSONE()
        {
            int returnValue = DevOps.CSVConverter.Program.Main(wrongArguments);
            Assert.AreEqual(-1, returnValue, "Expected different input");

        }
        //This Test Will Return 0 if the folder path passed contains required output
        [Test]
        public void TestThatReturnsZEROIfOutputFileisGenerated()
        {
            if (!File.Exists(outputDirectory.FullName + "\\ConsolidatedResults.csv"))
            {
                File.WriteAllText(Path.Combine(outputDirectory.FullName, "ConsolidatedResults.csv"),"dummyvalues");
            }
            int returnValue = DevOps.CSVConverter.Program.CheckOutputGenerated((outputDirectory.FullName+"\\ConsolidatedResults.csv"));
            if (File.Exists(outputDirectory.FullName + "\\ConsolidatedResults.csv"))
            {
                File.Delete(outputDirectory.FullName + "\\ConsolidatedResults.csv");
            }
            Assert.AreEqual(0, returnValue, "Expected different input");
        }
        //This Test Will Return -1 if the folder path passed does not contains required output
        [Test]
        public void TestThatReturnsMINUSONEIfOutputFileisNotGenerated()
        {
            if (File.Exists(outputDirectory.FullName + "\\ConsolidatedResults.csv"))
            {
                File.Delete(outputDirectory.FullName + "\\ConsolidatedResults.csv");
            }
                int returnValue = DevOps.CSVConverter.Program.CheckOutputGenerated((outputDirectory.FullName +"\\ConsolidatedResults.csv"));
            Assert.AreEqual(-1, returnValue, "Expected different input");
        }

        [TearDown]
        public void TestTearDown()
        {
            Console.WriteLine("TestSetup teardown here");
            if (!Directory.Exists(outputDirectory.FullName))
            {
                Directory.CreateDirectory(outputDirectory.FullName);
            }
            TestObject = null;
        }
    }
}
 