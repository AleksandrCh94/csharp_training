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
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            InitNewContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToHomePage(); 
            return this;
        }

        public ContactHelper Modify(int index,ContactData newData)
        {
            InitModifyCreation(index);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper Remove(int index)
        {
            SelectContact(index);
            RemoveContact();
            ReturnToHomePage();
            return this;
        }

        public ContactHelper InitNewContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            if (!IsElementPresent(By.XPath
                ($"//table[@id='maintable']/tbody/tr[{++index}]/td/input")))
            {
                ContactData contact = new ContactData("alex");
                contact.LastName = "chernenkov";

                Create(contact);
            }

            driver.FindElement(By.XPath
                ($"//table[@id='maintable']/tbody/tr[{++index}]/td/input")).Click();
            return this;
        }

        public ContactHelper InitModifyCreation(int index)
        {
            if (!IsElementPresent(By.XPath
                ($"//table[@id='maintable']/tbody/tr[{++index}]/td[8]/a/img")))
            {
                ContactData contact = new ContactData("alex");
                contact.LastName = "chernenkov";

                Create(contact);
            }

            driver.FindElement(By.XPath
                ($"//table[@id='maintable']/tbody/tr[{++index}]/td[8]/a/img")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.FirstName);
            Type(By.Name("lastname"), contact.LastName);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//input[19]")).Click();
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.XPath("//input[20]")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.Name("delete")).Click();
            return this;
        }

        public ContactHelper ReturnToHomePage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }
    }
}