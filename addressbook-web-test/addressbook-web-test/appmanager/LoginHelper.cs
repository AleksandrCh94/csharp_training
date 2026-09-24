using System;                               // Подключение базовых типов и системных функций .NET
using System.Text;                          // Подключение поддержки работы с кодировками и текстовыми строками
using System.Text.RegularExpressions;       // Подключение инструментов для работы с регулярными выражениями
using System.Threading;                     // Подключение инструментов управления задержками и потоками
using NUnit.Framework;                      // Подключение библиотек тестового фреймворка NUnit
using OpenQA.Selenium;                      // Подключение основных интерфейсов библиотеки Selenium WebDriver
using OpenQA.Selenium.Chrome;               // Подключение компонентов драйвера браузера Chrome
using OpenQA.Selenium.Support.UI;           // Подключение вспомогательных классов Selenium (ожидания и т.д.)

namespace WebAddressbookTests                           // Пространство имен проекта для логического объединения кода
{
    public class LoginHelper : HelperBase               // Объявление класса-помощника для управления сессиями авторизации, наследующего базовые методы из HelperBase
    {
        public LoginHelper(ApplicationManager manager)  // Конструктор хелпера, принимающий ссылку на главный менеджер приложения
            : base(manager)                             // Перенаправление полученной ссылки на менеджер в конструктор базового класса HelperBase
        {
        }

        public void Login(AccountData account)  // Метод умной авторизации на сайте с проверкой текущего состояния сессии
        {
            if (IsLoggedIn())                   // Проверка: если в браузере уже выполнен вход под какой-либо учетной записью
            {
                if (IsLoggedIn(account))        // Вложенная проверка: если имя текущего вошедшего пользователя совпадает с тем, под кем мы пытаемся зайти
                {
                    return;                     // Прерываем выполнение метода и выходим из него (мы уже авторизованы под нужным пользователем)
                }

                Logout();                       // Если вошел кто-то другой, вызываем метод выхода из системы (разлогиниваемся)
            }

            Type(By.Name("user"), account.Username);    // Находим поле ввода логина по атрибуту name="user" и вводим имя пользователя
            Type(By.Name("pass"), account.Password);    // Находим поле ввода пароля по атрибуту name="pass" и вводим пароль
            driver.FindElement(By.XPath
                ("//input[@value='Login']")).Click();   // Ищем кнопку подтверждения по XPath-локатору значения атрибута value и кликаем на неё
        }

        public bool IsLoggedIn()                            // Метод проверки факта авторизации в системе (любым пользователем)
        {
            return IsElementPresent(By.Name("logout"));     // Если на странице присутствует элемент (кнопка/ссылка) с атрибутом name="logout" — значит, вход выполнен
        }

        public bool IsLoggedIn(AccountData account)         // Метод проверки авторизации под конкретной учетной записью
        {
            return IsLoggedIn()                             // Сначала проверяем, что в систему в принципе осуществлен вход
                && GetLoggetUserName() == account.Username; // Затем сравниваем имя текущего пользователя в интерфейсе с ожидаемым логином из модели
        }

        public string GetLoggetUserName()                           // Метод извлечения имени текущего авторизованного пользователя из верстки сайта
        {
            string text = driver.FindElement(By.Name("logout"))
                .FindElement(By.TagName("b")).Text;                 // Находим родительский элемент name="logout", а внутри него — дочерний HTML-тег <b>, где приложение выводит имя вида "(admin)"
            return text.Substring(1, text.Length - 2);              // Парсинг строки: обрезаем первый и последний символы (обычно это круглые скобки вокруг имени, например, преобразуем "(admin)" в "admin")
        }

        public void Logout()                                        // Метод безопасного выхода из учетной записи
        {
            if (IsLoggedIn())                                       // Проверяем, авторизованы ли мы сейчас, чтобы избежать клика по несуществующей ссылке
            {
                driver.FindElement(By.LinkText("Logout")).Click();  // Находим ссылку по ее точному тексту "Logout" и выполняем по ней клик
            }
        }
    }
}
