using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SEB.Test.NewEmployee.PageObjects
{
    class ContactDetailsPage : BasePage
    {
        public ContactDetailsPage(IWebDriver driver) : base(driver) { }

        public IWebElement ContactDetailsSideNav => Driver.FindElement(By.XPath("//*[@id='sidenav']/li[2]/a"));

        public IWebElement EditButton => Driver.FindElement(By.XPath("//*[@id='btnSave'][@value='Edit']"));

        public IWebElement AddressStreet1Field => Driver.FindElement(By.Id("contact_street1"));

        public IWebElement AddressStreet2Field => Driver.FindElement(By.Id("contact_street2"));

        public IWebElement CityField => Driver.FindElement(By.Id("contact_city"));

        public IWebElement ZipCodeField => Driver.FindElement(By.Id("contact_emp_zipcode"));

        public IWebElement CountryDropDownList => Driver.FindElement(By.Id("contact_country"));

        public IWebElement MobilePhoneField => Driver.FindElement(By.Id("contact_emp_mobile"));

        public IWebElement WorkEmailField => Driver.FindElement(By.Id("contact_emp_work_email"));

        public IWebElement SaveButton => Driver.FindElement(By.Id("btnSave"));

        public void EnterAddress(string addr1, string addr2, string city, string zipPostalCode)
        {
            EnterStreetAddress1(addr1);
            EnterStreetAddress2(addr2);
            EnterCity(city);
            EnterZipCode(zipPostalCode);
        }

        public void EnterContactInfo(string mobile, string workEmail)
        {
            EnterMobileNumber(mobile);
            EnterWorkEmail(workEmail);
        }

        public void EnterStreetAddress1(string address1)
        {
            AddressStreet1Field.Click();
            AddressStreet1Field.Clear();
            AddressStreet1Field.SendKeys(address1);
        }

        public void EnterStreetAddress2(string address2)
        {
            AddressStreet2Field.Click();
            AddressStreet2Field.Clear();
            AddressStreet2Field.SendKeys(address2);
        }

        public void EnterCity(string city)
        {
            CityField.Click();
            CityField.Clear();
            CityField.SendKeys(city);
        }

        public void EnterZipCode(string zipCode)
        {
            ZipCodeField.Click();
            ZipCodeField.Clear();
            ZipCodeField.SendKeys(zipCode);
        }

        public void EnterMobileNumber(string mobile)
        {
            MobilePhoneField.Click();
            MobilePhoneField.Clear();
            MobilePhoneField.SendKeys(mobile);
        }

        public void EnterWorkEmail(string workEmail)
        {
            WorkEmailField.Click();
            WorkEmailField.Clear();
            WorkEmailField.SendKeys(workEmail);
        }
    }
}
