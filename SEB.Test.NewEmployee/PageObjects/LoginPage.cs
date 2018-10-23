using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using OpenQA.Selenium.Support.PageObjects;

namespace SEB.Test.NewEmployee.PageObjects
{
    class LoginPage : BasePage
    {
        public IWebElement UserNameField => Driver.FindElement(By.Id("txtUsername"));

        public IWebElement PasswordField => Driver.FindElement(By.Id("txtPassword"));

        public IWebElement LoginButton => Driver.FindElement(By.Id("btnLogin"));

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void EnterUserName(string userName)
        {
            UserNameField.Click();
            UserNameField.Clear();
            UserNameField.SendKeys(userName);
        }

        public void EnterPassword(string password)
        {
            PasswordField.Click();
            PasswordField.Clear();
            PasswordField.SendKeys(password);
        }
    }
}
