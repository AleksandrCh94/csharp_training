using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;

namespace mantis_tests
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();

            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By
                .XPath("//table//tbody//tr//td//a[contains(@href, " +
                "'manage_user_edit_page.php')]"));

            foreach (IWebElement row in rows)
            {
                IWebElement link = row.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;

                accounts.Add(new AccountData
                {
                    Name = name,
                    Id = id
                });
            }

            return accounts;
        }

        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            Click(By.CssSelector("button[formaction='manage_user_delete.php']"));
            Click(By.XPath("//input[@value='Удалить учётную запись']"));
        }

        public IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "/login_page.php";

            Type(By.Id("username"), "administrator");
            Click(By.CssSelector("input[value='Вход']"));
            Type(By.Id("password"), "root");
            Click(By.CssSelector("input[value='Вход']"));

            return driver;
        }
    }
}
