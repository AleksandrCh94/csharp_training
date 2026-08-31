using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_web_test.appmanager;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ApplicationManager
    {
        protected IWebDriver driver;
        protected string baseURL;

        protected AuthorizationHelper authorizationHelper;
        protected NavigationHelper navigationHelper;
        protected GroupHelper groupHelper;   

        public ApplicationManager()
        {
            driver = new ChromeDriver();
            baseURL = "http://localhost";

            authorizationHelper = new AuthorizationHelper(this);
            navigationHelper = new NavigationHelper(this, baseURL);
            groupHelper = new GroupHelper(this);
        }

        public IWebDriver Driver
        {
            get
            {
                return driver;
            }
        }

        public void Stop()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
        }

        public AuthorizationHelper Auth
        {
            get 
            { 
                return authorizationHelper; 
            }
        }

        public NavigationHelper Navigator
        {
            get 
            { 
                return navigationHelper; 
            }
        }

        public GroupHelper Groups
        {
            get
            {
                return groupHelper;
            }
        }
    }
}
