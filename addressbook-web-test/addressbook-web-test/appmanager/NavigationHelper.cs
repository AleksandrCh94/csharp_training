using System;                               // Подключение базовых типов и системных функций .NET
using System.Text;                          // Подключение поддержки работы с кодировками и текстовыми строками
using System.Text.RegularExpressions;       // Подключение инструментов для работы с регулярными выражениями
using System.Threading;                     // Подключение инструментов управления задержками и потоками
using NUnit.Framework;                      // Подключение библиотек тестового фреймворка NUnit
using OpenQA.Selenium;                      // Подключение основных интерфейсов библиотеки Selenium WebDriver
using OpenQA.Selenium.Chrome;               // Подключение компонентов драйвера браузера Chrome
using OpenQA.Selenium.Support.UI;           // Подключение вспомогательных классов Selenium (ожидания и т.д.)

namespace WebAddressbookTests                               // Пространство имен проекта для логического объединения кода
{
    public class NavigationHelper : HelperBase              // Объявление класса-помощника для навигации по сайту, наследующего базовые поля из HelperBase
    {
        private string baseURL;                             // Приватное поле для хранения базового интернет-адреса тестируемого сайта

        public NavigationHelper
            (ApplicationManager manager, string baseURL)    // Конструктор хелпера, принимающий менеджер приложения и базовый URL-адрес
            : base(manager)                                 // Перенаправление ссылки на менеджер в конструктор базового класса HelperBase
        {
            this.baseURL = baseURL;                         // Сохраняем полученный адрес сайта во внутреннее поле baseURL текущего объекта
        }

        public void GoToHomePage()                                  // Метод умного перехода на главную страницу веб-приложения
        {
            if (driver.Url == baseURL + "/addressbook/")            // Проверка предусловия: если текущий URL в строке браузера уже совпадает с адресом главной страницы
            {
                return;                                             // Досрочно прерываем метод и выходим из него, чтобы не тратить время на перезагрузку страницы
            }
            driver.Navigate().GoToUrl(baseURL + "/addressbook/");   // Команда браузеру физически перейти по указанному веб-адресу главной страницы
        }

        public void GoToGroupsPage()                                // Метод умного перехода на страницу управления группами контактов
        {
            if (driver.Url == baseURL + "/addressbook/group.php"    // Проверка предусловия 1: если текущий адрес совпадает с адресом страницы групп
                && IsElementPresent(By.Name("new")))                // И проверка 2: на странице физически присутствует кнопка создания новой группы с именем name="new"
            {
                return;                                             // Прерываем выполнение метода и выходим, так как мы уже находимся в нужном месте
            }
            driver.FindElement(By.LinkText("groups")).Click();      // Находим в меню ссылку по ее точному тексту "groups" и нажимаем на нее для перехода
        }
    }
}