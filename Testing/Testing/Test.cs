using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;

namespace Testing
{
    [TestFixture]

    public class Test
    {
        IWebDriver driver;
        string url = "http://www.devopsbrainz.com/";
        [SetUp]
        public void Initialization()
        {
            

            driver = new ChromeDriver(@"C:\chromedriver_win32");

            driver.Navigate().GoToUrl(url);

            driver.Manage().Window.Maximize();


        }
        //Verify if a user will be able to login with a valid username and valid password.  
        [Test]
        public void TestLogin()
        {
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
        [Test]
        public void wrongcredentials()
        {
            IWebElement username = driver.FindElement(By.Id("Username"));
            username.SendKeys("admin");
            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys("12345");
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("btnLogin")).Click();
            Console.WriteLine("wrong credentials");
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().Back();
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().GoToUrl(url);
            

        }
        //Verify the login page for both, when the field is blank and Submit button is clicked.
        [Test]
        public void spacebuttoncredentials()
        {
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
        //other method for wrong credentials
        [Test]
        public void wrongcredentialsautomatic()
        {
            IWebElement username = driver.FindElement(By.Id("Username"));
            String value1 = "admin";
            String value2 = "123456";
            username.SendKeys(value1);
            IWebElement password = driver.FindElement(By.Id("Password"));
            password.SendKeys(value2);
            System.Threading.Thread.Sleep(1000);
            driver.FindElement(By.Id("btnLogin")).Click();
            Assert.AreNotEqual(value1, value2);
            Console.WriteLine("wrong credentials");
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().Back();
            System.Threading.Thread.Sleep(1000);
            driver.Navigate().GoToUrl(url);

        }
        [TearDown]
        public void cleanup()
        {

            driver.Close();
            Console.WriteLine("Browser window is closed");
        }
    }
}
