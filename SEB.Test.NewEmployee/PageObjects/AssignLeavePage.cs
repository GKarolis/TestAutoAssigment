using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace SEB.Test.NewEmployee.PageObjects
{
    class AssignLeavePage : BasePage
    {
        public AssignLeavePage(IWebDriver driver) : base(driver) { }

        public IWebElement LeaveMenu => Driver.FindElement(By.Id("menu_leave_viewLeaveModule"));

        public IWebElement AssignLeaveMenu => Driver.FindElement(By.Id("menu_leave_assignLeave"));

        public IWebElement EmployeeName => Driver.FindElement(By.Id("assignleave_txtEmployee_empName"));

        public IWebElement LeaveType => Driver.FindElement(By.Id("assignleave_txtLeaveType"));

        public IWebElement FromDate => Driver.FindElement(By.Id("assignleave_txtFromDate"));

        public IWebElement ToDate => Driver.FindElement(By.Id("assignleave_txtToDate"));

        public IWebElement AssignButton => Driver.FindElement(By.Id("assignBtn"));

        public IWebElement CommentField => Driver.FindElement(By.Id("assignleave_txtComment"));

        public void EnterEmployeeName(string fullName)
        {
            EmployeeName.Click();
            EmployeeName.Clear();
            EmployeeName.SendKeys(fullName);
        }

        public void EnterComment(string comment)
        {
            CommentField.Click();
            CommentField.Clear();
            CommentField.SendKeys(comment);
        }
    }
}
