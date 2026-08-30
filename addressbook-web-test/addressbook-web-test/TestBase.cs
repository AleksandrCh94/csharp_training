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
    public class TestBase
    {
        protected IWebDriver driver;
        private StringBuilder verificationErrors;
        protected string baseURL;
        //protected bool acceptNextAlert = true;

        protected LoginLogoutHelper loginLogoutHelper;
        protected NavigationHelper navigationHelper;
        protected GroupHelper groupHelper;

        [SetUp]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost/addressbook";
            verificationErrors = new StringBuilder();

            loginLogoutHelper = new LoginLogoutHelper(driver);
            navigationHelper = new NavigationHelper(driver, baseURL);
            groupHelper = new GroupHelper(driver);
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