using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
namespace DevOps.Test.LoginApplication
{
    [TestFixture]
    public class LoginTest
    {
        IWebDriver driver;
        string url = "https://localhost:44353/";

        private static List<object> invalidCredentials = new List<object>() {
            new object[] { "admin", "invalidPassword" }, // correct username, incorrect password
            new object[] { "UserNotExists", "admin" }, // incorrect username, correct password
            new object[] { "UserNotExists", "invalidPassword" }, // incorrect username, incorrect password
        };

        [SetUp]
        public void Initialization()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        //Verify if a user will be able to login with a valid username and valid password.  
        [Test]
        public void TestThatLoginSucceedsWithValidCredentials()
        {
            var userName = "admin";
            var passWord = "admin";
            Assert.AreSame("admin", userName, "Invalid username");
            Assert.AreSame("admin", passWord, "Invalid password");
            SendKeysToElement("ContentPlaceHolder1_TextBox1", userName);
            SendKeysToElement("ContentPlaceHolder1_TextBox2", passWord);
            driver.FindElement(By.Id("ContentPlaceHolder1_Button1")).Click();
            driver.Navigate().Forward();

            Console.WriteLine("successfull login");


        }

        //Verify if a user cannot login with a valid username and an invalid password.
        [Test, TestCaseSource("invalidCredentials")]
        public void TestThatLoginFailsWithInValidCredentials(object[] credentials)
        {
            var userName = credentials[0].ToString();
            var passWord = credentials[1].ToString();
            Assert.IsNotEmpty(userName, "Expected username value");
            Assert.IsNotEmpty(passWord, "Expected passWord value");

            SendKeysToElement("ContentPlaceHolder1_TextBox1", userName);
            SendKeysToElement("ContentPlaceHolder1_TextBox2", passWord);
            driver.FindElement(By.Id("ContentPlaceHolder1_Button1")).Click();
            Console.WriteLine("wrong credentials");       
        }



        // Helper functions
        private void SendKeysToElement(string webElementId, string textToEnter)
        {
            var webElement = driver.FindElement(By.Id(webElementId));
            Assert.IsNotNull(webElement, "No element found with the id: " + webElement);
            webElement.SendKeys(textToEnter);
        }

        [TearDown]
        public void cleanup()
        {
            driver.Dispose();
            Console.WriteLine("Browser window is closed");
            driver.Quit();
        }
    }
}
