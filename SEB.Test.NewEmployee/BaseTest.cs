using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SEB.Test.NewEmployee
{
    class BaseTest
    {
        public IWebDriver driver;
        public string browser = ConfigurationManager.AppSettings["browser"];

        [SetUp]
        public void Initialize()
        {
            //driver = new ChromeDriver();
            switch (browser)
            {
                case "Chrome":
                    driver = new ChromeDriver();
                    break;
                case "InternetExplorer":
                    driver = new InternetExplorerDriver();
                    break;
                //case "Firefox":    //Firefox neturiu, gryba pjauna pas mane.. 
                //    driver = new FirefoxDriver();
                //    break;
            }
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com");
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TearDown]
        public void EndTest()
        {
            driver.Close(); //Blagadariu..
        }
    }
}
