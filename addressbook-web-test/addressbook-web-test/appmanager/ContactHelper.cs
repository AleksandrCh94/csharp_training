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

        public ContactHelper GetOrCreateContact(int index)              // Метод обеспечения предусловия: получение существующего контакта или его автосоздание при отсутствии
        {
            if (!IsElementPresent(By.XPath                              // Проверка: если в таблице по указанному смещенному индексу строки отсутствует чекбокс контакта
                ("//table[@id='maintable']/tbody/tr" +
                "[" + (index + 2) + "]/td/input")))                     // XPath-локатор чекбокса с инкрементом индекса для пропуска строки заголовков
            {
                ContactData contact = new ContactData("alex", "che");   // Создаем тестовые данные имени нового контакта на случай его отсутствия

                Create(contact);                                        // Вызываем метод создания контакта, чтобы наполнить таблицу данными
            }                                                     
            return this;                                                // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper SelectContact(int index)                   // Низкоуровневый метод выбора чекбокса контакта по порядковому номеру строки
        {
            driver.FindElement(By.XPath                                 // Нахождение элемента чекбокса через динамический XPath, куда подставляется индекс строки
                ("//table[@id='maintable']/tbody/tr" +
                "[" + (index + 1) + "]/td/input")).Click();             // Выполнение клика для отметки контакта галочкой
            return this;                                                // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper InitModifyCreation(int index)              // Низкоуровневый метод открытия формы редактирования контакта через таблицу
        {
            driver.FindElements(By.Name("entry"))[index]                // Поиск картинки-иконки редактирования в 8-й ячейке указанной по индексу строки таблицы контактов
                .FindElements(By.TagName("td"))[7]
                .FindElement(By.TagName("a")).Click();                  // Клик по иконке для перехода к форме модификации
            return this;                                                // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper InitShowContactDetails(int index)              // Низкоуровневый метод открытия формы редактирования контакта через таблицу
        {
            driver.FindElements(By.Name("entry"))[index]                // Поиск картинки-иконки редактирования в 8-й ячейке указанной по индексу строки таблицы контактов
                .FindElements(By.TagName("td"))[6]
                .FindElement(By.TagName("a")).Click();                  // Клик по иконке для перехода к форме модификации
            return this;                                                // Возвращаем ссылку на текущий объект хелпера
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
            contactCache = null;
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper SubmitContactModification()            // Низкоуровневый метод сохранения отредактированных данных контакта
        {
            driver.FindElement(By.XPath("//input[20]")).Click();    // Поиск кнопки сохранения изменений по её порядковому номеру (20-й тег input) и клик по ней
            contactCache = null;
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper RemoveContact()                        // Низкоуровневый метод инициации удаления выбранных контактов
        {
            driver.FindElement(By.Name("delete")).Click();          // Поиск управляющей кнопки удаления по её имени name="delete" и нажатие на неё
            contactCache = null;
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }

        public ContactHelper ReturnToHomePage()                     // Низкоуровневый метод перехода на главную страницу через ссылку завершения операции
        {
            driver.FindElement(By.LinkText("home page")).Click();   // Поиск ссылки с текстом "home page" и выполнение клика по ней
            return this;                                            // Возвращаем ссылку на текущий объект хелпера
        }

        private List<ContactData> contactCache = null;              // Приватное поле для временного хранения списка контактов (кеша) в оперативной памяти

        public List<ContactData> GetContactsList()                  // Высокоуровневый метод получения актуального списка контактов с поддержкой кеширования
        {
            if (contactCache == null)                               // Проверка: если кеш пуст (это первый вызов или он был сброшен после удаления/создания/модификации)
            {
                contactCache = new List<ContactData>();             // Инициализируем новый пустой список контактов в памяти
                                
                ICollection<IWebElement> elements =
                    driver.FindElements(By.CssSelector("tr[name=\"entry\"]"));  // Поиск всех строк таблицы веб-страницы, представляющих записи контактов (тег tr с атрибутом name="entry")

                foreach (IWebElement element in elements)           // Последовательный обход каждого найденного веб-элемента строки таблицы
                {                    
                    IList<IWebElement> cells = element.FindElements
                        (By.TagName("td"));                             // Поиск всех ячеек (тегов td) внутри текущей строки таблицы для разделения данных

                    contactCache.Add(new ContactData(cells[2].Text, cells[1].Text) {   // Создание объекта ContactData. В конструктор передается текст из 3-й ячейки (индекс 2), где обычно находится имя
                        Id = element.FindElement(By.TagName("input"))
                        .GetAttribute("id")                             // Относительный поиск тега input внутри строки для извлечения уникального идентификатора контакта (атрибута id)
                    });
                }
            }
            return new List<ContactData>(contactCache);             // Возвращаем безопасную поверхностную копию списка из кеша, изолируя внутреннее поле от внешних изменений
        }

        public int GetContactsCount()                               // Высокоуровневый метод получения количества контактов в списке контактов
        {
            return driver.FindElements(By.CssSelector
                ("tr[name=\"entry\"]")).Count;                      // Находим все строки контактов на странице по CSS-селектору и сразу возвращаем их общее количество (Count)
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string address = cells[3].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(firstName, lastName)
            {
                Address = address,
                AllEmails = allEmails,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitModifyCreation(index);

            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                Email = email,
                Email2 = email2,
                Email3 = email3,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone = workPhone
            };
        }

        public string GetContactInformationFromFormInString(int index)
        {
            manager.Navigator.GoToHomePage();
            InitModifyCreation(index);

            List<string> infoList = new List<string>();

            infoList.Add(driver.FindElement(By.Name("firstname")).GetAttribute("value"));
            infoList.Add(driver.FindElement(By.Name("lastname")).GetAttribute("value"));
            infoList.Add(driver.FindElement(By.Name("address")).GetAttribute("value"));
            infoList.Add(driver.FindElement(By.Name("home")).GetAttribute("value"));
            infoList.Add(driver.FindElement(By.Name("mobile")).GetAttribute("value"));
            infoList.Add(driver.FindElement(By.Name("work")).GetAttribute("value"));
            infoList.Add(driver.FindElement(By.Name("email")).GetAttribute("value"));
            infoList.Add(driver.FindElement(By.Name("email2")).GetAttribute("value"));
            infoList.Add(driver.FindElement(By.Name("email3")).GetAttribute("value"));

            string info = string.Join("", infoList);
            return Regex.Replace(info, "[ ]", "");
        }

        public string GetContactInformationFromDetails(int index)
        {
            manager.Navigator.GoToHomePage();
            InitShowContactDetails(index);
            string info = driver.FindElement(By.Id("content")).Text;
            return Regex.Replace(info, "[ \r\nH:M]", "");
        }

        public int GetNumberOfSearchResults()
        {
            manager.Navigator.GoToHomePage();
            string text = driver.FindElement(By.TagName("label")).Text;
            Match m = new Regex(@"\d").Match(text);
            return Int32.Parse(m.Value);
        }
    }
}
