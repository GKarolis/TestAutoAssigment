using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SEB.Test.NewEmployee.PageObjects
{
    class AddNewEmployeePage : BasePage
    {
        public IWebElement PIM => Driver.FindElement(By.Id("menu_pim_viewPimModule"));

        public IWebElement AddEmployee => Driver.FindElement(By.Id("menu_pim_addEmployee"));

        public IWebElement FirstNameField => Driver.FindElement(By.Id("firstName"));

        public IWebElement LastNameField => Driver.FindElement(By.Id("lastName"));

        public IWebElement CreateLoginDetailsChkBox => Driver.FindElement(By.Id("chkLogin"));

        public IWebElement UserNameField => Driver.FindElement(By.Id("user_name"));

        public IWebElement PasswordField => Driver.FindElement(By.Id("user_password"));

        public IWebElement RepeatPasswordField => Driver.FindElement(By.Id("re_password"));

        public IWebElement SaveButton => Driver.FindElement(By.Id("btnSave"));

        public AddNewEmployeePage(IWebDriver driver) : base(driver) { }

        public void EnterFirstName(string firstName)
        {
            FirstNameField.Click();
            FirstNameField.Clear();
            FirstNameField.SendKeys(firstName);
        }

        public void EnterLastName(string lastName)
        {
            LastNameField.Click();
            LastNameField.Clear();
            LastNameField.SendKeys(lastName);
        }

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

        public void RepeatPassword(string repPassword)
        {
            RepeatPasswordField.Click();
            RepeatPasswordField.Clear();
            RepeatPasswordField.SendKeys(repPassword);
        }
    }
}
