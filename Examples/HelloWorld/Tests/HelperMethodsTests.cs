using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;

namespace HelloWorldTests {
    [TestFixture]
    public class HelperMethodsTests {
        
        private static List<string> inputStrings = new List<string>() {
            "Hello World",
            "Test input string"
        };
        private HelloWorld.HelperMethods testObject;

        [SetUp]
        public void TestSetup() {
            Console.WriteLine("TestSetup goes here");
            testObject = new HelloWorld.HelperMethods();
        }

        [TearDown]
        public void TestTearDown() {
            Console.WriteLine("TestSetup teardown here");
            testObject = null;
        }

        // Parametrised test
        // Test name strucutre: Test<APINAME><WHATToTest><ExpectedOutcome>
        [Test, TestCaseSource("inputStrings")]
        public void TestReturnInputShallReturnsTheInputsPass(string inputArgument) {
            var returnValue = testObject.ReturnTheInput(inputArgument);
            Assert.AreEqual(inputArgument, returnValue, "Expected different input");
        }

       
        [Test]
        public void TestReturnInputShallReturnsTheInputPass() {
            string inputValue = "test input";
            var returnValue = testObject.ReturnTheInput(inputValue);
            Assert.AreEqual(inputValue, returnValue, "Expected different input");
        }

        // Intentionally making this test fail.
        [Test]
        public void TestReturnInputShallReturnsTheInputFail()
        {
            string inputValue = "test input1";
            var returnValue = testObject.ReturnTheInput(inputValue);
            Assert.AreEqual("test input2", returnValue, "Expected different input");
        }
    }
}
