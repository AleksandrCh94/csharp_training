using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Text.RegularExpressions;

namespace mantis_tests
{
    public class RegistrationHelper : HelperBase
    {
        public RegistrationHelper(ApplicationManager manager) : base(manager) { }

        public void Register(AccountData account)
        {
            OpenMainPage();
            OpenRegistrationPage();
            FillregistrationForm(account);
            SubmitRegistration();
            String url = GetConfirmationUrl(account);
            FillAccountForm(url, account);
            SubmitPasswordForm();
            Login(account);
        }

        public void OpenMainPage()
        {
            manager.Driver.Url = "http://localhost/mantisbt-2.28.4/login_page.php";
        }

        public void OpenRegistrationPage()
        {
            driver.FindElement(By.CssSelector("a.back-to-login-link.pull-left")).Click();
        }

        public void FillregistrationForm(AccountData account)
        {
            driver.FindElement(By.Name("username")).SendKeys(account.Name);
            driver.FindElement(By.Name("email")).SendKeys(account.Email);
        }

        public void SubmitRegistration()
        {
            driver.FindElement(By.CssSelector("input[type='submit']")).Click();
        }

        public string GetConfirmationUrl(AccountData account)
        {
            String message = manager.Mail.GetLastMail(account);
            Match match = Regex.Match(message, @"http://\S*");
            return match.Value;
        }

        public void FillAccountForm(string url, AccountData account)
        {
            driver.Url = url;
            driver.FindElement(By.Name("realname")).SendKeys(account.Name);
            driver.FindElement(By.Name("password")).SendKeys(account.Password);
            driver.FindElement(By.Name("password_confirm")).SendKeys(account.Password);
        }

        public void SubmitPasswordForm()
        {
            driver.FindElement(By.CssSelector("button[type='submit']")).Click();
        }

        public void Login(AccountData account)
        {
            driver.FindElement(By.Id("username")).SendKeys(account.Name);
            driver.FindElement(By.CssSelector("input[value='Вход']")).Submit();
            driver.FindElement(By.Id("password")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("input[value='Вход']")).Submit();
        }

        public string GetAccountName()
        {
            string userName = driver.FindElement(By.CssSelector("span.user-info")).Text;
            return userName;
        }
    }
}