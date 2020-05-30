using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using DevOps.TestRunner;
using System.Activities.Expressions;
using System.IO;

namespace DevOps.Tests.TestRunner {

    [TestFixture]
    public class ArgumentsTests {
        private static string assemblyPath = System.Reflection.Assembly.GetAssembly(typeof(DevOps.Tests.TestRunner.ArgumentsTests)).Location;
        private static string assemblyDirectory = Path.GetDirectoryName(assemblyPath);

        // Note: NunitConsoleRunner have required binaires being copied as part of the build.
        private static string nunitConsoleRunner = Path.GetFullPath(
            Path.Combine(
                assemblyDirectory,
                @"..\..\..\Output\Work\Binaries\NunitConsoleRunner\nunit3-console.exe"
            )
        );

        private static string testAssemblyPath = Path.GetFullPath(
                Path.Combine(assemblyDirectory, @"..\..\..\Examples\HelloWorld\Output\Bin\")
            );

        //string myString = "DevOps.TestRunner --Executor  D:\\Applications\\nunit-console\\nunit3-console.exe  --TestFramework  NUnit --AssemlbyDirectory D:\\Views\\Testing\\Testing\\bin\\Debug --OutputDirectory  D:\\DevopsTestResults";
        //string[] myString.Cast<char>().Cast<string>().ToArray();
        private DevOps.TestRunner.Arguments TestingObject;
        private static DirectoryInfo outputDirecttDirectory = new DirectoryInfo(
                Path.Combine(assemblyDirectory, @"..\..\..\Examples\HelloWorld\Output\")
            );
        static object[] testCaseSourceData = {
            new Int32[] { 12, 3, 4 },
            new Int32[] { 12, 2, 6 },
            new Int32[] { 12, 4, 3 }
        };

        private static string[] validArguments = {
            "--Executor",
            nunitConsoleRunner,
            "--TestFramework",
            "NUnit",
            "--AssemblyDirectory",
            testAssemblyPath,
            "--OutputDirectory",
            outputDirecttDirectory.FullName
        };

        private static string[] inValidArguments = {
            "--Executor",
            nunitConsoleRunner,
            "--TestFramework",
            "NUnit",
            "--AssemlbyDirectory",
            testAssemblyPath,
            "--OutputDirectory",
            outputDirecttDirectory.FullName
        };

        [SetUp]
        public void TestSetup() {
            Console.WriteLine("TestSetup goes here");
            if (!Directory.Exists(outputDirecttDirectory.FullName)) {
                Directory.CreateDirectory(outputDirecttDirectory.FullName);
            }
            TestingObject = new DevOps.TestRunner.Arguments();
        }
       
        [Test]
        public void TestThatWithValidArgumentsTheParsePasses() {
            bool returnValue = TestingObject.Parse(validArguments);
            Assert.AreEqual(true, returnValue, "Expected different input");
        }

        [Test]
        public void TestThatWithInvalidArgumentsTheParseFails() {
            bool returnValue = TestingObject.Parse(inValidArguments);
            Assert.AreEqual(false, returnValue, "Expected different input");
        }

        // For example only
        [Test, TestCaseSource("testCaseSourceData")]
        public void SampleTestWhichUsesTestCaseSource(Int32[] inputValues) {
            Console.WriteLine(string.Join(",", inputValues));
        }

        [TearDown]
        public void TestTearDown() {
            Console.WriteLine("TestSetup teardown here");
            if (outputDirecttDirectory.Exists) {
                Directory.CreateDirectory(outputDirecttDirectory.FullName);
            }
            TestingObject = null;
        }
    }
}
