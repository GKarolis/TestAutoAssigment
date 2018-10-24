using System;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;
using SEB.Test.NewEmployee.PageObjects;
using System.Threading;

namespace SEB.Test.NewEmployee
{
    class NewEmployeeCreationAndAssignLeaveTest : BaseTest
    {
        Random random = new Random();
        Employee employee = new Employee();
        DateTime date = DateTime.Today;   

        [TestCase(TestName = "CreateNewEmployeeAndAssignLeave")]       
        public void LogIn()
        {
            LoginPage loginPage = new LoginPage(driver);

            loginPage.EnterCredentials();

            AddNewEmployee();
        }

        public void AddNewEmployee()
        {
            AddNewEmployeePage addNewEmployee = new AddNewEmployeePage(driver);

            addNewEmployee.GoToAddNewEmployeeTab();
            addNewEmployee.EnterFullName(employee.FirstName, employee.LastName);
            addNewEmployee.CreateLoginDetails(employee.UserName, "Password1");
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

            var selectStatus = new SelectElement(addPersonalDetails.MaritalStatusDropDownList);
            int statusCount = selectStatus.Options.Count();
            selectStatus.SelectByIndex(random.Next(1, statusCount));

            var selectNationality = new SelectElement(addPersonalDetails.NationalityDropDownList);
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
            addContactDetails.EnterAddress("Address1", "Address2", "City", "03210");

            var selectCountry = new SelectElement(addContactDetails.CountryDropDownList);
            int countryCount = selectCountry.Options.Count();
            selectCountry.SelectByIndex(random.Next(1, countryCount));

            addContactDetails.EnterContactInfo("37012345678", "work@email.com");
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
            
            assignLeave.LeaveMenu.Click();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            assignLeave.AssignLeaveMenu.Click();
            assignLeave.EnterEmployeeName(employee.FirstName + " " + employee.LastName);

            var selectLeaveType = new SelectElement(assignLeave.LeaveType);
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

            Thread.Sleep(2000);

            driver.SwitchTo().Window(driver.WindowHandles.Last());
            driver.FindElement(By.Id("confirmOkButton")).Click();

            assignLeave.WelcomeLinkButton.Click();

            Thread.Sleep(2000);

            assignLeave.LogOut();

            driver.FindElement(By.Id("txtUsername")).SendKeys(employee.UserName);
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
