using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SEB.Test.NewEmployee.PageObjects
{
    class JobDetailsPage : BasePage
    {
        public JobDetailsPage(IWebDriver driver) : base(driver) { }

        public IWebElement JobDetailsNav => Driver.FindElement(By.XPath("//*[@id='sidenav']/li[6]/a"));

        public IWebElement EditButton => Driver.FindElement(By.XPath("//*[@id='btnSave'][@value='Edit']"));

        public IWebElement JobTitleField => Driver.FindElement(By.Id("job_job_title"));

        public IWebElement EmploymentStatusField => Driver.FindElement(By.Id("job_emp_status"));

        public IWebElement JobCategoryField => Driver.FindElement(By.Id("job_eeo_category"));

        public IWebElement ContractStartDateField => Driver.FindElement(By.Id("job_contract_start_date"));

        public IWebElement ContractEndDateField => Driver.FindElement(By.Id("job_contract_end_date"));

        public IWebElement SaveButton => Driver.FindElement(By.Id("btnSave"));
    }
}
