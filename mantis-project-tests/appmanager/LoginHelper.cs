using OpenQA.Selenium;

namespace mantis_project_tests
{
    public class LoginHelper : HelperBase
    {
        public LoginHelper(ApplicationManager manager) : base(manager) { }

        // Логинимся
        public void Login(AccountData account)
        {
            if (IsLoggedIn())
            {
                if (IsLoggedIn(account))
                {
                    return;
                }
                Logout();
            }
            
            Type(By.Id("username"), account.Username);
            Click(By.CssSelector("input[value='Вход']"));
            Type(By.Id("password"), account.Password);
            Click(By.CssSelector("input[value='Вход']"));
        }

        // Проверяем не заголинены ли мы уже
        private bool IsLoggedIn()
        {
            return IsElementPresent(By.XPath("//a[contains(@href, 'logout_page.php')]"));
        }

        // Проверяем правильно ли мы залогинились
        private bool IsLoggedIn(AccountData account)
        {
            return IsLoggedIn() && GetLoggedUserName() == account.Username;
        }

        // Получаем имя с сайта, под которым заголинились
        private string GetLoggedUserName()
        {
            string text = driver.FindElement(By.CssSelector("span.user-info")).Text;                 
            return text;
        }

        // Разлогиниваемся
        private void Logout()
        {
            if (IsLoggedIn())                                      
            {
                Click(By.XPath("//a[contains(@href, 'logout_page.php')]"));
            }
        }
    }
}