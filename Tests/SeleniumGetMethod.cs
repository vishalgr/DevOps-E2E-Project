using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using OpenQA.Selenium;

namespace WebApplication1.Tests
{
    public class SeleniumGetMethod
    {
        public static string GetText(IWebDriver driver, string element, string elementtype)
        {
            if (elementtype == "Id")
                return driver.FindElement(By.Id(element)).GetAttribute("value");
            if (elementtype == "Name")
                return driver.FindElement(By.Name(element)).GetAttribute("value");
            else return String.Empty;
        }
    }
}