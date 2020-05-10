using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class LoginTest {
        IWebDriver driver;
        string url = "http://localhost:56666/";

        private static List<object> invalidCredentials = new List<object>() {
            new object[] { "admin", "invalidPassword" }, // correct username, incorrect password
            new object[] { "UserNotExists", "admin" }, // incorrect username, correct password
            new object[] { "UserNotExists", "invalidPassword" }, // incorrect username, incorrect password
        };
        
        [SetUp]
        public void Initialization() {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(url);
            driver.Manage().Window.Maximize();
        }

        //Verify if a user will be able to login with a valid username and valid password.  
        [Test]
        public void TestThatLoginSucceedsWithValidCredentials() {
            IWebElement username = driver.FindElement(By.Id("Username"));
            username.SendKeys("admin");
            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("admin");
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("btnLogin")).Click();
            Console.WriteLine("successfull login");
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().Forward();
        }

        //Verify if a user cannot login with a valid username and an invalid password.
        [Test, TestCaseSource("invalidCredentials")]
        public void TestThatLoginFailsWithInValidCredentials(object[] credentials) {
            var userName = credentials[0].ToString();
            var passWord = credentials[1].ToString();
            Assert.IsNotEmpty(userName, "Expected username value");
            Assert.IsNotEmpty(passWord, "Expected passWord value");

            SendKeysToElement("Username", userName);
            SendKeysToElement("Password", passWord);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("btnLogin")).Click();
            
            Console.WriteLine("wrong credentials");
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().Back();
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().GoToUrl(url);
        }

        //Verify the login page for both, when the field is blank and Submit button is clicked.
        // TODO: I did not understand what this method is testing.
        [Test]
        public void spacebuttoncredentials() {
            IWebElement username = driver.FindElement(By.Id("Username"));
            username.SendKeys("");
            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("");
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("btnLogin")).Click();

            Console.WriteLine("Userid or password could not be Empty.");
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().Back();
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().GoToUrl(url);
        }

        // Helper functions
        private void SendKeysToElement(string webElementId, string textToEnter) {
            var webElement = driver.FindElement(By.Id("Password"));
            Assert.IsNotNull(webElement, "No element found with the id: " + webElement);
            webElement.SendKeys(textToEnter);
        }

        [TearDown]
        public void cleanup() {
            driver.Dispose();
            Console.WriteLine("Browser window is closed");
            driver.Quit();
        }
    }
}
