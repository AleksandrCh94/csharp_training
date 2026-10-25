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
            Click(By.CssSelector("a.back-to-login-link.pull-left"));
        }

        public void FillregistrationForm(AccountData account)
        {
            Type(By.Name("username"), account.Name);
            Type(By.Name("email"), account.Email);
        }

        public void SubmitRegistration()
        {
            Click(By.CssSelector("input[type='submit']"));
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
            Type(By.Name("realname"), account.Name);
            Type(By.Name("password"), account.Password);
            Type(By.Name("password_confirm"), account.Password);
        }

        public void SubmitPasswordForm()
        {
            Click(By.CssSelector("button[type='submit']"));
        }

        public void Login(AccountData account)
        {
            Type(By.Id("username"), account.Name);
            Click(By.CssSelector("input[value='Вход']"));
            Type(By.Id("password"), account.Password);
            Click(By.CssSelector("input[value='Вход']"));
        }

        public string GetAccountName()
        {
            string userName = driver.FindElement(By.CssSelector("span.user-info")).Text;
            return userName;
        }
    }
}