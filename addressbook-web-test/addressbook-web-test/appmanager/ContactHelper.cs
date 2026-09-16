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
    public class ContactHelper : HelperBase                 // Объявление класса-помощника для работы с контактами, наследующего базовые методы Selenium из HelperBase
    {
        public ContactHelper(ApplicationManager manager)    // Конструктор хелпера, принимающий ссылку на главный менеджер приложения
            : base(manager)                                 // Перенаправление ссылки на менеджер в конструктор базового класса HelperBase
        {
        }

        public ContactHelper Create(ContactData contact)    // Высокоуровневый метод для полного цикла создания нового контакта
        {
            InitNewContactCreation();                       // Переход на страницу создания контакта по ссылке "add new"
            FillContactForm(contact);                       // Заполнение текстовых полей формы данными из переданного объекта
            SubmitContactCreation();                        // Подтверждение создания нажатием кнопки на форме
            ReturnToHomePage();                             // Возврат на главную страницу адресной книги
            return this;                                    // Возвращаем ссылку на текущий хелпер для возможности построения цепочки вызовов (Fluent Interface)
        }

        public ContactHelper Modify(int index, ContactData newData) // Высокоуровневый метод для редактирования контакта по его индексу в таблице
        {
            InitModifyCreation(index);                              // Открытие формы редактирования конкретного контакта нажатием на иконку карандаша
            FillContactForm(newData);                               // Перезаполнение формы новыми переданными текстовыми данными
            SubmitContactModification();                            // Подтверждение изменений нажатием кнопки обновления данных
            ReturnToHomePage();                                     // Возврат на главную страницу сайта к общему списку
            return this;                                            // Возвращаем ссылку на текущий объект для вызова методов по цепочке
        }

        public ContactHelper Remove(int index)  // Высокоуровневый метод для удаления контакта по его индексу в таблице
        {
            SelectContact(index);               // Установка чекбокса выбора напротив нужного контакта в строке таблицы
            RemoveContact();                    // Нажатие кнопки удаления контактов на панели управления
            ReturnToHomePage();                 // Возврат на главную страницу для обновления списка
            return this;                        // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper InitNewContactCreation()           // Низкоуровневый метод перехода к форме создания нового контакта
        {
            driver.FindElement(By.LinkText("add new")).Click(); // Поиск ссылки по её точной видимой строке "add new" и выполнение клика
            return this;                                        // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper GetOrCreateContact(int index)                      // Метод обеспечения предусловия: получение существующего контакта или его автосоздание при отсутствии
        {
            int rowIndex = index + 1;                                           // Вычисляем реальный номер строки в таблице (смещаем на 1, так как tr[1] — это заголовок)

            if (!IsElementPresent(By.XPath                                      // Проверка: если в таблице по указанному смещенному индексу строки отсутствует чекбокс контакта
                ("//table[@id='maintable']/tbody/tr["+rowIndex+"]/td/input")))  // XPath-локатор чекбокса с инкрементом индекса для пропуска строки заголовков
            {
                ContactData contact = new ContactData("alex");                  // Создаем тестовые данные имени нового контакта на случай его отсутствия
                contact.LastName = "chernenkov";                                // Задаем тестовую фамилию для создаваемого контакта

                Create(contact);                                                // Вызываем метод создания контакта, чтобы наполнить таблицу данными
            }                                                     
            return this;                                                        // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper SelectContact(int index)                                   // Низкоуровневый метод выбора чекбокса контакта по порядковому номеру строки
        {
            int rowIndex = index + 1;                                                   // Вычисляем реальный номер строки в таблице (смещаем на 1, так как tr[1] — это заголовок)

            driver.FindElement(By.XPath                                                 // Нахождение элемента чекбокса через динамический XPath, куда подставляется индекс строки
                ("//table[@id='maintable']/tbody/tr["+rowIndex+ "]/td/input")).Click(); // Выполнение клика для отметки контакта галочкой
            return this;                                                                // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper InitModifyCreation(int index)                              // Низкоуровневый метод открытия формы редактирования контакта через таблицу
        {
            int rowIndex = index + 1;                                                   // Вычисляем реальный номер строки в таблице (смещаем на 1, так как tr[1] — это заголовок)

            driver.FindElement(By.XPath                                                 // Поиск картинки-иконки редактирования в 8-й ячейке указанной по индексу строки таблицы контактов
                ("//table[@id='maintable']/tbody/tr["+ rowIndex + "]/td[8]/a/img"))
                .Click();                                                               // Клик по иконке для перехода к форме модификации
            return this;                                                                // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper FillContactForm(ContactData contact)   // Низкоуровневый метод заполнения полей формы создания/редактирования контакта
        {
            Type(By.Name("firstname"), contact.FirstName);          // Очистка и ввод имени контакта в текстовое поле с атрибутом name="firstname"
            Type(By.Name("lastname"), contact.LastName);            // Очистка и ввод фамилии контакта в текстовое поле с атрибутом name="lastname"
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper SubmitContactCreation()                // Низкоуровневый метод сохранения только что созданного контакта
        {
            driver.FindElement(By.XPath("//input[19]")).Click();    // Поиск кнопки отправки формы по её порядковому номеру (19-й тег input) и клик по ней
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper SubmitContactModification()            // Низкоуровневый метод сохранения отредактированных данных контакта
        {
            driver.FindElement(By.XPath("//input[20]")).Click();    // Поиск кнопки сохранения изменений по её порядковому номеру (20-й тег input) и клик по ней
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper RemoveContact()                        // Низкоуровневый метод инициации удаления выбранных контактов
        {
            driver.FindElement(By.Name("delete")).Click();          // Поиск управляющей кнопки удаления по её имени name="delete" и нажатие на неё
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper ReturnToHomePage()                     // Низкоуровневый метод перехода на главную страницу через ссылку завершения операции
        {
            driver.FindElement(By.LinkText("home page")).Click();   // Поиск ссылки с текстом "home page" и выполнение клика по ней
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }
    }
}
