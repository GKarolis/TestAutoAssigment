using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SEB.Test.NewEmployee.PageObjects
{
    class BasePage
    {
        public BasePage(IWebDriver driver)
        {
            Driver = driver;
            //driver.Navigate().GoToUrl("https://opensource-demo.orangehrmlive.com");
        }

        public IWebDriver Driver { get; set; }
    }
}
