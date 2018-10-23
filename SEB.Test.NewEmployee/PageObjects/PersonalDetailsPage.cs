using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEB.Test.NewEmployee.PageObjects
{
    class PersonalDetailsPage : BasePage
    {
        public PersonalDetailsPage(IWebDriver driver) : base(driver) { }

        public IWebElement EditButton => Driver.FindElement(By.XPath("//*[@id='btnSave'][@value='Edit']"));

        public IWebElement GenderRadioButton => Driver.FindElement(By.Id("personal_optGender_1"));

        public IWebElement MaritalStatusDropDownList => Driver.FindElement(By.Id("personal_cmbMarital"));

        public IWebElement NationalityDropDownList => Driver.FindElement(By.Id("personal_cmbNation"));

        public IWebElement DateOfBirth => Driver.FindElement(By.Id("personal_DOB"));

        public IWebElement SaveButton => Driver.FindElement(By.Id("btnSave"));
    }
}
