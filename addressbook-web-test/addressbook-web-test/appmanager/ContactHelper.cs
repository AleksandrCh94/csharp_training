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
    public class ContactHelper : HelperBase
    {
        public ContactHelper(IWebDriver driver)
            : base(driver)
        {
        }

        public void InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
        }

        public void FillContactForm(ContactData contact)
        {
            driver.FindElement(By.Name("firstname")).Click();
            driver.FindElement(By.Name("firstname")).SendKeys(contact.FirstName);
            driver.FindElement(By.Name("lastname")).Click();
            driver.FindElement(By.Name("lastname")).SendKeys(contact.LastName);
        }

        public void SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//input[19]")).Click();
        }

        public void ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
        }
    }
}