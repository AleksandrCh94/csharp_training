using System;                               // Подключение базовых системных типов (Exception, String и т.д.)
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;                      // Подключение библиотеки Selenium для работы с браузером
using OpenQA.Selenium.Chrome;               // Подключение драйвера Chrome
using OpenQA.Selenium.Support.UI;           // Подключение вспомогательных классов Selenium WebDriver

namespace WebAddressbookTests               // Пространство имен для тестов адресной книги
{
    public class ApplicationManager         // Главный класс-менеджер приложения
    {
        // Защищенные поля (доступны только классу и наследникам)

        protected IWebDriver driver;                    // WebDriver для управления браузером (Chrome)
        protected string baseURL;                       // Базовый URL тестируемого приложения

        // Хелперы для различных действий

        protected LoginHelper loginHelper;              
        protected NavigationHelper navigationHelper;    
        protected GroupHelper groupHelper;              
        protected ContactHelper contactHelper;          
     
        public ApplicationManager()
        {
            driver = new ChromeDriver();                // Создаем новый экземпляр ChromeDriver
            baseURL = "http://localhost";               // Устанавливаем базовый URL

            // Инициализируем все хелперы, передавая им ссылку на текущий менеджер

            loginHelper = new LoginHelper(this);                        
            navigationHelper = new NavigationHelper(this, baseURL);     
            groupHelper = new GroupHelper(this);                        
            contactHelper = new ContactHelper(this);                    
        }

        // Публичный метод для остановки браузера

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

        // Свойство для доступа к помощнику авторизации (сокращение: app.Auth)

        public LoginHelper Auth
        {
            get { return loginHelper; }                 // Возвращаем экземпляр LoginHelper
        }

        // Свойство для доступа к помощнику навигации (сокращение: app.Navigator)

        public NavigationHelper Navigator
        {
            get { return navigationHelper; }            // Возвращаем экземпляр NavigationHelper
        }

        // Свойство для доступа к помощнику групп (сокращение: app.Groups)

        public GroupHelper Groups
        {
            get { return groupHelper; }                 // Возвращаем экземпляр GroupHelper
        }

        // Свойство для доступа к помощнику контактов (сокращение: app.Contacts)

        public ContactHelper Contacts
        {
            get { return contactHelper; }               // Возвращаем экземпляр ContactHelper
        }

        // Свойство для прямого доступа к WebDriver (для случаев, когда нужен сам driver)

        public IWebDriver Driver
        {
            get { return driver; }                      // Возвращаем экземпляр IWebDriver
        }        
    }
}