using System;

using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.IE;


namespace WebApplication1.Tests
{
    [TestFixture]

    public class Testcases
    {
        IWebDriver driver;
        string url = "https://localhost:44354/LoginP.aspx";
        [SetUp]
        public void Initialization()
        {
            
            driver = new ChromeDriver(@"C:\chromedriver_win32");
            
            driver.Navigate().GoToUrl(url);

            driver.Manage().Window.Maximize();
           

        }
        [Test]
        public void TestMethod()
        {
            // TODO: Add your test code here
            SeleniumSetMethod.EnterText(driver, "Username", "admin", "Id");
            SeleniumSetMethod.EnterText(driver, "Password", "admin", "Id");
            Console.WriteLine("The value from my username is:" + SeleniumGetMethod.GetText(driver, "Username", "Id"));

            Console.WriteLine("The value from my password is:" + SeleniumGetMethod.GetText(driver, "Password", "Id"));
            
            SeleniumSetMethod.Click(driver, "btn_Login", "Id");
            driver.Navigate().Forward();
            
            System.Threading.Thread.Sleep(15000);
        }

        [Test]
        public void TestMethod2()
        {
            SeleniumSetMethod.EnterText(driver, "Username", "admin", "Id");
            SeleniumSetMethod.EnterText(driver, "Password", "12345", "Id");
            Console.WriteLine("The value from my username is:" + SeleniumGetMethod.GetText(driver, "Username", "Id"));

            Console.WriteLine("The value from my password is:" + SeleniumGetMethod.GetText(driver, "Password", "Id"));

            driver.Navigate().Back();
            Console.WriteLine("Invalid input please provide correct input");
            driver.Navigate().GoToUrl(url);
            System.Threading.Thread.Sleep(15000);
        }

        [TearDown]
        public void cleanup()
        {

            driver.Close();
            Console.WriteLine("Browser window is closed");
        }
    }

}