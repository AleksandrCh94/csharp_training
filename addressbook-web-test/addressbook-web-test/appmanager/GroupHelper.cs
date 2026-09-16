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
    public class GroupHelper : HelperBase               // Объявление класса-помощника для работы с группами, наследующего базовые методы Selenium из HelperBase
    {
        public GroupHelper(ApplicationManager manager)  // Конструктор хелпера, принимающий ссылку на главный менеджер приложения
            : base(manager)                             // Перенаправление ссылки на менеджер в конструктор базового класса HelperBase
        {
        }

        public GroupHelper Create(GroupData group)  // Высокоуровневый метод для полного цикла создания новой группы
        {
            manager.Navigator.GoToGroupsPage();     // Переход на страницу со списком групп через хелпер навигации
            InitNewGroupCreation();                 // Нажатие кнопки инициации создания новой группы
            FillGroupForm(group);                   // Заполнение текстовых полей формы данными из переданного объекта группы
            SubmitGroupCreation();                  // Подтверждение создания группы нажатием на кнопку отправки формы
            ReturnToGroupsPage();                   // Возврат обратно на страницу со списком всех групп
            return this;                            // Возвращаем ссылку на текущий хелпер для построения цепочки вызовов (Fluent Interface)
        }

        public GroupHelper Modify(int index, GroupData newData) // Высокоуровневый метод для редактирования группы по её индексу
        {
            manager.Navigator.GoToGroupsPage();                 // Переход на страницу со списком групп перед началом изменений
            SelectGroup(index);                                 // Выбор чекбокса нужной группы в списке
            InitGroupModification();                            // Нажатие кнопки редактирования выбранной группы
            FillGroupForm(newData);                             // Перезаполнение полей формы новыми текстовыми данными
            SubmitGroupModification();                          // Подтверждение изменений нажатием кнопки обновления данных
            ReturnToGroupsPage();                               // Возврат на страницу со списком групп для завершения операции
            return this;                                            // Возвращаем ссылку на текущий объект для вызова методов по цепочке
        }

        public GroupHelper Remove(int index)    // Высокоуровневый метод для удаления группы по её индексу
        {
            manager.Navigator.GoToGroupsPage(); // Переход на страницу со списком групп
            SelectGroup(index);                 // Выбор чекбокса удаляемой группы
            RemoveGroup();                      // Нажатие управляющей кнопки удаления групп
            ReturnToGroupsPage();               // Возврат на страницу списка групп для обновления интерфейса
            return this;                        // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper InitNewGroupCreation()       // Низкоуровневый метод нажатия на кнопку создания группы
        {
            driver.FindElement(By.Name("new")).Click(); // Поиск кнопки по атрибуту name="new" и выполнение клика по ней
            return this;                                // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper GetOrCreateGroup(int index)  // Метод обеспечения предусловия: проверка наличия группы или её создание «на лету»
        {
            manager.Navigator.GoToGroupsPage();         // Переход на страницу со списком групп
            if (!IsElementPresent(By.XPath
                ("//span[" + index + "]/input")))       // Проверка: если на странице отсутствует чекбокс группы с указанным порядковым номером
            {
                GroupData group = new GroupData("aaa"); // Подготовка тестовых данных названия новой группы
                group.Header = "wegwg";                 // Задание тестового заголовка для новой группы
                group.Footer = "wrwer";                 // Задание тестового подвала для новой группы

                Create(group);                          // Вызываем метод создания группы, чтобы наполнить таблицу данными
            }
            return this;                                // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper SelectGroup(int index)           // Низкоуровневый метод выбора чекбокса группы по её номеру в списке
        {
            driver.FindElement(By.XPath
                ("//span[" + index + "]/input")).Click();   // Поиск инпута внутри тега span по динамическому XPath-индексу и клик для выбора
            return this;                                    // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper InitGroupModification()          // Низкоуровневый метод перехода в режим редактирования группы
        {
            driver.FindElement(By.Name("edit")).Click();    // Поиск кнопки редактирования по атрибуту name="edit" и нажатие на неё
            return this;                                    // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper FillGroupForm(GroupData group)   // Низкоуровневый метод заполнения текстовых полей формы группы
        {
            Type(By.Name("group_name"), group.Name);        // Очистка и ввод названия группы в поле с именем name="group_name"
            Type(By.Name("group_header"), group.Header);    // Очистка и ввод заголовка группы в поле с именем name="group_header"
            Type(By.Name("group_footer"), group.Footer);    // Очистка и ввод подвала группы в поле с именем name="group_footer"
            return this;                                    // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper SubmitGroupCreation()            // Низкоуровневый метод отправки формы создания новой группы
        {
            driver.FindElement(By.Name("submit")).Click();  // Нахождение кнопки сохранения по атрибуту name="submit" и клик по ней
            return this;                                    // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper SubmitGroupModification()        // Низкоуровневый метод отправки формы редактирования существующей группы
        {
            driver.FindElement(By.Name("update")).Click();  // Нахождение кнопки применения изменений по атрибуту name="update" и клик по ней
            return this;                                    // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper RemoveGroup()                    // Низкоуровневый метод отправки команды на удаление выбранных групп
        {
            driver.FindElement(By.Name("delete")).Click();  // Поиск кнопки удаления по атрибуту name="delete" и клик по ней
            return this;                                    // Возвращаем ссылку на текущий объект хелпера
        }

        public GroupHelper ReturnToGroupsPage()                     // Низкоуровневый метод возврата к списку групп через текстовую ссылку
        {
            driver.FindElement(By.LinkText("group page")).Click();  // Нахождение ссылки по точному тексту "group page" и выполнение клика
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }
    }
}
