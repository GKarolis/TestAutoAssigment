using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SEB.Test.NewEmployee.PageObjects;
using System.Threading;

namespace SEB.Test.NewEmployee
{
    class Tests : BaseTest
    {
        Random random = new Random();
        Employee employee = new Employee();
        DateTime date = DateTime.Today;   

        [Test]       
        public void LogIn()
        {
            LoginPage loginPage = new LoginPage(driver);

            loginPage.EnterUserName("Admin");
            loginPage.EnterPassword("admin123");
            loginPage.LoginButton.Click();

            AddNewEmployee();
        }

        public void AddNewEmployee()
        {
            AddNewEmployeePage addNewEmployee = new AddNewEmployeePage(driver);            

            driver.FindElement(By.XPath("//*[@id='menu_pim_viewPimModule']")).Click();
            driver.FindElement(By.XPath("//*[@id='menu_pim_addEmployee']")).Click();

            string firstName = employee.FirstName;
            string lastName = employee.LastName;
            string userName = employee.UserName;

            addNewEmployee.EnterFirstName(firstName);
            addNewEmployee.EnterLastName(lastName);
            addNewEmployee.CreateLoginDetailsChkBox.Click();
            addNewEmployee.EnterUserName(userName);
            addNewEmployee.EnterPassword("Password1");
            addNewEmployee.RepeatPassword("Password1");
            addNewEmployee.SaveButton.Click();

            AddPersonalDetails();
        }

        public void AddPersonalDetails()
        {
            PersonalDetailsPage addPersonalDetails = new PersonalDetailsPage(driver);

            DateTime start = new DateTime(1940, 1, 1);
            int range = (DateTime.Today - start).Days;
            var randomDate = start.AddDays(random.Next(range));

            addPersonalDetails.EditButton.Click();
            addPersonalDetails.GenderRadioButton.Click();

            var maritalStatus = addPersonalDetails.MaritalStatusDropDownList;
            var selectStatus = new SelectElement(maritalStatus);
            int statusCount = selectStatus.Options.Count();
            selectStatus.SelectByIndex(random.Next(1, statusCount));

            var nationality = addPersonalDetails.NationalityDropDownList;
            var selectNationality = new SelectElement(nationality);
            int nationalityCount = selectNationality.Options.Count();
            selectNationality.SelectByIndex(random.Next(1, nationalityCount));

            addPersonalDetails.DateOfBirth.Clear();
            addPersonalDetails.DateOfBirth.SendKeys(randomDate.ToString("yyyy-MM-dd"));
            addPersonalDetails.SaveButton.Click();

            AddContactDetails();
        }

        public void AddContactDetails()
        {
            ContactDetailsPage addContactDetails = new ContactDetailsPage(driver);

            addContactDetails.ContactDetailsSideNav.Click();
            addContactDetails.EditButton.Click();
            addContactDetails.EnterStreetAddress1("Address1");
            addContactDetails.EnterStreetAddress2("Address2");
            addContactDetails.EnterCity("City");
            addContactDetails.EnterZipCode("03210");

            var country = addContactDetails.CountryDropDownList;
            var selectCountry = new SelectElement(country);
            int countryCount = selectCountry.Options.Count();
            selectCountry.SelectByIndex(random.Next(1, countryCount));

            addContactDetails.EnterMobileNumber("37012345678");
            addContactDetails.EnterWorkEmail("work@email.com");
            addContactDetails.SaveButton.Click();

            AddJobDetails();
        }

        public void AddJobDetails()
        {
            JobDetailsPage addJobDetails = new JobDetailsPage(driver);

            addJobDetails.JobDetailsNav.Click();
            addJobDetails.EditButton.Click();

            var jobTitle = addJobDetails.JobTitleField;
            var selectJobTitle = new SelectElement(jobTitle);
            int titleCount = selectJobTitle.Options.Count();
            selectJobTitle.SelectByIndex(random.Next(1, titleCount));

            var empStatus = addJobDetails.EmploymentStatusField;
            var selectEmpStatus = new SelectElement(empStatus);
            int empStatusCount = selectEmpStatus.Options.Count();
            selectEmpStatus.SelectByIndex(random.Next(1, empStatusCount));

            addJobDetails.ContractStartDateField.Clear();
            DateTime startContract = new DateTime(1960, 1, 1);
            int rangeStartContract = (DateTime.Today - startContract).Days;
            var randomStartContractDate = startContract.AddDays(random.Next(rangeStartContract));
            addJobDetails.ContractStartDateField.SendKeys(randomStartContractDate.ToString("yyyy-MM-dd"));

            addJobDetails.ContractEndDateField.Clear();
            int rangeEndContract = (DateTime.Today - startContract).Days;
            var randomEndContractDate = randomStartContractDate.AddDays(random.Next(rangeEndContract));
            addJobDetails.ContractEndDateField.SendKeys(randomEndContractDate.ToString("yyyy-MM-dd"));

            addJobDetails.SaveButton.Click();
            AssignLeave();
        }

        public void AssignLeave()
        {
            AssignLeavePage assignLeave = new AssignLeavePage(driver);
            
            string firstName = employee.FirstName;
            string lastName = employee.LastName;
            string userName = employee.UserName;

            assignLeave.LeaveMenu.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            assignLeave.AssignLeaveMenu.Click();
            assignLeave.EnterEmployeeName(firstName + " " + lastName);

            var leaveType = assignLeave.LeaveType;
            var selectLeaveType = new SelectElement(leaveType);
            int leaveTypeCount = selectLeaveType.Options.Count();
            selectLeaveType.SelectByIndex(random.Next(1, leaveTypeCount));

            assignLeave.FromDate.Clear();
            int daysInYear = DateTime.IsLeapYear(date.Year) ? 366 : 365;
            int daysLeftInYearFrom = daysInYear - date.DayOfYear;
            DateTime randomleaveFromDate = DateTime.Today.AddDays(random.Next(daysLeftInYearFrom));
            assignLeave.FromDate.SendKeys(randomleaveFromDate.ToString("yyyy-MM-dd"));

            assignLeave.ToDate.Clear();
            int daysLeftInYearTo = daysInYear - randomleaveFromDate.DayOfYear;
            DateTime randomleaveToDate = randomleaveFromDate.AddDays(random.Next(daysLeftInYearTo));
            assignLeave.ToDate.SendKeys(randomleaveToDate.ToString("yyyy-MM-dd"));
            assignLeave.ToDate.SendKeys(Keys.Enter);

            Thread.Sleep(2000);

            assignLeave.EnterComment("some comments...");

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            IWebElement element = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("assignBtn")));

            Thread.Sleep(2000);

            assignLeave.AssignButton.SendKeys(Keys.Enter);

            Thread.Sleep(2000); //zaji**li strigt..

            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.FindElement(By.Id("confirmOkButton")).Click();

            driver.FindElement(By.XPath("//*[@id='welcome']")).Click();

            Thread.Sleep(2000);

            driver.FindElement(By.XPath("//*[@id='welcome-menu']/ul/li[2]/a")).SendKeys("");
            driver.FindElement(By.XPath("//*[@id='welcome-menu']/ul/li[2]/a")).Click();

            driver.FindElement(By.Id("txtUsername")).SendKeys(userName);
            driver.FindElement(By.Id("txtPassword")).SendKeys("Password1" + Keys.Enter);

            driver.FindElement(By.XPath("//*[@id='dashboard-quick-launch-panel-menu_holder']/table/tbody/tr/td[2]/div/a/img")).Click();

            var leaveDates = GetNumberOfWorkingDays(randomleaveFromDate, randomleaveToDate).ToString();
            
            var numberOfDaysDecimal = driver.FindElement(By.XPath("//*[@id='resultTable']/tbody/tr/td[5]")).Text;
            var numberOfDays = numberOfDaysDecimal.Substring(0, numberOfDaysDecimal.LastIndexOf("."));

            Assert.AreEqual(numberOfDays, leaveDates);
        }

        private static int GetNumberOfWorkingDays(DateTime start, DateTime stop)
        {
            int days = 0;
            while (start <= stop)
            {
                if (start.DayOfWeek != DayOfWeek.Saturday && start.DayOfWeek != DayOfWeek.Sunday)
                {
                    ++days;
                }
                start = start.AddDays(1);
            }
            return days;
        }
    }
}
