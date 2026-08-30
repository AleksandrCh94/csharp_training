using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class BaseTest
    {
        protected IWebDriver driver;
        protected StringBuilder verificationErrors;
        protected string baseURL;
        //protected bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        protected void GoToHomePage()
        {
            driver.Navigate().GoToUrl(baseURL);
        }

        protected void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
        }

        protected void GoToGroupsPage()
        {
            driver.FindElement(By.LinkText("groups")).Click();
        }

        protected void InitNewGroupCreation()
        {
            driver.FindElement(By.Name("new")).Click();
        }

        protected void FillGroupForm(GroupData group)
        {
            driver.FindElement(By.Name("group_name")).Click();
            driver.FindElement(By.Name("group_name")).SendKeys(group.Name);
            driver.FindElement(By.Name("group_header")).Click();
            driver.FindElement(By.Name("group_header")).SendKeys(group.Header);
            driver.FindElement(By.Name("group_footer")).Click();
            driver.FindElement(By.Name("group_footer")).SendKeys(group.Footer);
        }

        protected void SubmitGroupCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
        }

        protected void SelectGroup(int index)
        {
            driver.FindElement(By.XPath("//span[" + index + "]/input")).Click();
        }

        protected void RemoveGroup()
        {
            driver.FindElement(By.Name("delete")).Click();
        }

        protected void ReturnToGroupsPage()
        {
            driver.FindElement(By.LinkText("group page")).Click();
        }

        protected void Logout()
        {
            driver.FindElement(By.LinkText("Logout")).Click();
        }
    }

    //private bool IsElementPresent(By by)
    //{
    //    try
    //    {
    //        driver.FindElement(by);
    //        return true;
    //    }
    //    catch (NoSuchElementException)
    //    {
    //        return false;
    //    }
    //}

    //private bool IsAlertPresent()
    //{
    //    try
    //    {
    //        driver.SwitchTo().Alert();
    //        return true;
    //    }
    //    catch (NoAlertPresentException)
    //    {
    //        return false;
    //    }
    //}

    //private string CloseAlertAndGetItsText()
    //{
    //    try
    //    {
    //        IAlert alert = driver.SwitchTo().Alert();
    //        string alertText = alert.Text;
    //        if (acceptNextAlert)
    //        {
    //            alert.Accept();
    //        }
    //        else
    //        {
    //            alert.Dismiss();
    //        }
    //        return alertText;
    //    }
    //    finally
    //    {
    //        acceptNextAlert = true;
    //    }
    //}

    //private string CloseAlertAndGetItsText()
    //{
    //    try
    //    {
    //        IAlert alert = driver.SwitchTo().Alert();
    //        string alertText = alert.Text;
    //        if (acceptNextAlert)
    //        {
    //            alert.Accept();
    //        }
    //        else
    //        {
    //            alert.Dismiss();
    //        }
    //        return alertText;
    //    }
    //    finally
    //    {
    //        acceptNextAlert = true;
    //    }
    //}
}