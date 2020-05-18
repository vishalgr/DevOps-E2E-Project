using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevOps.CSVConverter;
using NUnit.Framework;

namespace DevOps.Tests.CSVConverter
{
    [TestFixture]
    public class ArgumentsTest
    {
        private static string xmlPath = System.Reflection.Assembly.GetAssembly(typeof(DevOps.Tests.CSVConverter.ArgumentsTest)).Location;
        private static string xmlDirectory = Path.GetDirectoryName(xmlPath);

        private static string xmlfilepath = Path.GetFullPath(
             Path.Combine(xmlDirectory, @"..\..\..\Examples\HelloWorld\Output\Bin\")
          );
        private static DirectoryInfo outputDirectory = new DirectoryInfo(Path.Combine(xmlDirectory, "Work"));

        public DevOps.CSVConverter.Arguments TestObject;

        //private static string xmlDirectory = @"D:\resultxmls";
        //private static string outputDirectory = @"D:\resultxmls";

        private static string[] validArguments = {
            "--xmlFileDirectory",
            xmlDirectory,
            "--OutputDirectory",
            outputDirectory.FullName
    };
        private static string[] inValidArguments = {
            "--xmlDirectory",
            xmlDirectory,
            "--OutputDirectory",
            outputDirectory.FullName
    };

        [SetUp]
        public void TestSetup()
        {
            Console.WriteLine("TestSetup goes here");
            if (!Directory.Exists(outputDirectory.FullName))
            {
                Directory.CreateDirectory(outputDirectory.FullName);
            }
            TestObject = new DevOps.CSVConverter.Arguments();
        }

        [Test]
        public void TestThatWithValidArgumentsTheParsePasses()
        {
            bool returnValue = TestObject.Parse(validArguments);
            Assert.AreEqual(true, returnValue, "Expected different input");
        }

        [Test]
        public void TestThatWithInvalidArgumentsTheParseFails()
        {
            bool returnValue = TestObject.Parse(inValidArguments);
            Assert.AreEqual(true, returnValue, "Expected different input");
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
 