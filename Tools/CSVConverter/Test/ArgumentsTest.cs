using NUnit.Framework;
using System;
using System.IO;

namespace DevOps.Tests.CSVConverter
{
    [TestFixture]
    public class ArgumentsTest
    {
        private static string assemblyPath = System.Reflection.Assembly.GetAssembly(typeof(ArgumentsTest)).Location;
        private static string assemblyDir = Path.GetDirectoryName(assemblyPath);

        private static string xmlfileDir = Path.GetFullPath(
             Path.Combine(assemblyDir, @"..\..\..\Examples\HelloWorld\Output\Bin\")
          );
        private static DirectoryInfo outputDirectory = new DirectoryInfo(Path.Combine(assemblyDir, "Work"));

        public DevOps.CSVConverter.Arguments TestObject;

        //private static string xmlDirectory = @"D:\resultxmls";
        //private static string outputDirectory = @"D:\resultxmls";

        private static string[] validArguments = {
            "--xmlFileDirectory",
            xmlfileDir,
            "--OutputDirectory",
            outputDirectory.FullName
    };
        private static string[] inValidArguments = {
            "--xmlDirectory",
            xmlfileDir,
            "--OutputDirectory",
            outputDirectory.FullName
    };

        [SetUp]
        public void TestSetup() {
            Console.WriteLine("TestSetup goes here");
            if (!Directory.Exists(outputDirectory.FullName)) {
                Directory.CreateDirectory(outputDirectory.FullName);
            }
            TestObject = new DevOps.CSVConverter.Arguments();
        }

        [Test]
        public void TestThatWithValidArgumentsTheParsePasses() {
            bool returnValue = TestObject.Parse(validArguments);
            Assert.AreEqual(true, returnValue, "Expected different input");
        }

        [Test]
        public void TestThatWithInvalidArgumentsTheParseFails() {
            bool returnValue = TestObject.Parse(inValidArguments);
            Assert.AreEqual(false, returnValue, "Expected different input");
        }

     // TODO: Tests which calls exe and checks the ouput

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
 