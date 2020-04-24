using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;

namespace WebApplication1.Tests
{
    public class SeleniumSetMethod
    {
        public static void EnterText(IWebDriver driver, string element, string value, string elementtype)
        {
            if (elementtype.ToLower() == "Id")
                driver.FindElement(By.Id(element)).SendKeys(value);
            if (elementtype.ToLower() == "Name")
                driver.FindElement(By.Name(element)).SendKeys(value);
        }
        public static void Click(IWebDriver driver, string element, string elementtype)
        {
            if (elementtype.ToLower() == "Id")
                driver.FindElement(By.Id(element)).Click();
            if (elementtype.ToLower() == "Name")
                driver.FindElement(By.Name(element)).Click();
        }
    }
}