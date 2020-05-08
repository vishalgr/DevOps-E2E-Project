using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using DevOps.TestRunner;
using System.Activities.Expressions;

namespace DevOps.Tests.TestRunner
{

    [TestFixture]
    public class ArgumentsTests
    {
        //string myString = "DevOps.TestRunner --Executor  D:\\Applications\\nunit-console\\nunit3-console.exe  --TestFramework  NUnit --AssemlbyDirectory D:\\Views\\Testing\\Testing\\bin\\Debug --OutputDirectory  D:\\DevopsTestResults";
        //string[] myString.Cast<char>().Cast<string>().ToArray();
        private DevOps.TestRunner.Arguments TestingObject;
        private static string[] InputToTest =  { "DevOps.TestRunner","--Executor"," D:\\Applications\\nunit-console\\nunit3-console.exe"," --TestFramework"," NUnit"," --AssemlbyDirectory", "D:\\Views\\Testing\\Testing\\bin\\Debug", "--OutputDirectory"," D:\\DevopsTestResults" };
        private static string[] InputToTest2 = { "DevOps.TestRunner", "--Executor", " D:\\Applications\\nunit3-console.exe", " --TestFramework", " NUnit", " --AssemlbyDirectory", "D:\\Views\\Testing\\Testing\\bin\\Debug", "--OutputDirectory", " D:\\DevopsTestResults" };
        [SetUp]
        public void TestSetup()
        {
            Console.WriteLine("TestSetup goes here");
            Console.WriteLine(InputToTest.GetType());
            Console.WriteLine(InputToTest2.GetType());
            TestingObject = new DevOps.TestRunner.Arguments();
        }

       
        [Test,TestCaseSource("InputToTest")]
        public void TestWillResturnTrueIfAlltheInputIsCorrect(string[] input)
        {
           
            bool returnValue = TestingObject.Parse(input);
            Assert.AreEqual(true, returnValue, "Expected different input");
        }

        [Test, TestCaseSource("InputToTest2")]
        public void TestWillResturnFalseIfAnyOftheInputIsWrong(string[] input)
        {

            bool returnValue = TestingObject.Parse(input);
            Assert.AreEqual(true, returnValue, "Expected different input");
        }





        [TearDown]
        public void TestTearDown()
        {
            Console.WriteLine("TestSetup teardown here");
            TestingObject = null;
        }

    }
}
