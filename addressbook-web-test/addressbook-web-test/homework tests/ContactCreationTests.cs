using System;                               // Подключение базовых типов и системных функций .NET
using System.IO;
using System.Text;                          // Подключение поддержки работы со строками и кодировками
using System.Text.RegularExpressions;       // Подключение классов для обработки регулярных выражений
using System.Threading;                     // Подключение инструментов управления потоками и паузами
using System.Collections.Generic;
using System.Xml;                      // Подключение фреймворка NUnit для выполнения тестов
using System.Xml.Serialization;                      // Подключение фреймворка NUnit для выполнения тестов
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using OpenQA.Selenium.BiDi.Network;


namespace WebAddressbookTests                           // Пространство имен для логической группировки классов проекта
{
    [NonParallelizable]                                 // Атрибут NUnit: запускает тесты данного класса строго последовательно
    [TestFixture]                                       // Атрибут NUnit: помечает класс как набор автоматических тестов
    public class ContactCreationTests : AuthTestBase    // Объявление класса тестов создания контактов, унаследованного от AuthTestBase
    {
        public static IEnumerable<ContactData> RandomContactDataProvider()
        {
            List<ContactData> contacts = new List<ContactData>();
            for (int i = 0; i < 5; i++)
            {
                contacts.Add(new ContactData(GenerateRandomString(10), GenerateRandomString(10))
                {
                    Address = (GenerateRandomString(20)),
                    HomePhone = (GenerateRandomString(10)),
                    MobilePhone = (GenerateRandomString(10)),
                    WorkPhone = (GenerateRandomString(10)),
                    Email = (GenerateRandomString(100)),
                    Email2 = (GenerateRandomString(100)),
                    Email3 = (GenerateRandomString(100))
                });
            }
            return contacts;
        }

        public static IEnumerable<ContactData> ContactDataFromXmlFile()
        {
            return (List<ContactData>)
                new XmlSerializer(typeof(List<ContactData>))
                    .Deserialize(new StreamReader(@"contacts.xml"));
        }

        public static IEnumerable<ContactData> ContactDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<ContactData>>(
                File.ReadAllText(@"contacts.json"));
        }

        [Test, TestCaseSource("ContactDataFromJsonFile")]       // Атрибут NUnit: помечает метод как тест-кейс для проверки создания заполненного контакта
        public void ContactCreationTest(ContactData contact)      // Тест-кейс: успешное добавление контакта с именем и фамилией
        {            
            List<ContactData> oldContacts = 
                app.Contacts.GetContactsList();         // Шаг 1: Считываем исходный список контактов с веб-страницы до выполнения операции создания

            app.Contacts.Create(contact);               // Шаг 2: Передаем модель контакта в хелпер для физического заполнения формы и сохранения записи на сайте через UI

            Assert.AreEqual(oldContacts.Count + 1,
                app.Contacts.GetContactsCount());       // Проверка 1 (Быстрая): Убеждаемся, что текущее количество строк в таблице на сайте (GetContactsCount) стало ровно на 1 больше

            List<ContactData> newContacts = 
                app.Contacts.GetContactsList();         // Шаг 3: Получаем новый, актуальный список контактов с сайта после успешного сохранения

            oldContacts.Add(contact);                   // Имитируем добавление нового контакта локально в наш старый список в оперативной памяти

            oldContacts.Sort();                         // Сортируем оба списка по алфавиту (сначала по фамилии, потом по имени благодаря CompareTo), так как новый контакт занял свое алфавитное место в таблице
            newContacts.Sort();
            
            Assert.AreEqual(oldContacts, newContacts);  // Проверка 2 (Глубокая): Сравниваем старый дополненный список и новый список с сайта поэлементно
        }

        [Test]                                          // Атрибут NUnit: помечает метод как тест-кейс для проверки создания пустого контакта
        public void BadNameContactCreationTest()        // Тест-кейс: успешное добавление контакта с пустыми текстовыми полями
        {
            ContactData contact = new ContactData("a'a", "");   // Создаем объект данных контакта с пустой строкой вместо имени

            List<ContactData> oldContacts = 
                app.Contacts.GetContactsList();         // Шаг 1: Считываем исходный список контактов до отправки формы

            app.Contacts.Create(contact);               // Шаг 2: Передаем модель контакта со спецсимволом в хелпер для сохранения в адресной книге

            Assert.AreEqual(oldContacts.Count + 1, 
                app.Contacts.GetContactsCount());       // Проверка 1 (Быстрая): Проверяем, что система успешно НЕ создала контакт и счетчик осталось прежним - ТЕСТ ПАДАЕТ

            List<ContactData> newContacts = 
                app.Contacts.GetContactsList();         // Шаг 3: Считываем новый актуальный список контактов с веб-страницы - ПРОПУСКАЕТСЯ

            oldContacts.Add(contact);                   // Добавляем созданный контакт "a'a" в наш старый локальный список в оперативной памяти - ПРОПУСКАЕТСЯ

            oldContacts.Sort();                         // Упорядочиваем списки по алфавиту для корректного сопоставления индексов элементов - ПРОПУСКАЕТСЯ
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);  // Проверка 2 (Глубокая): Сравниваем списки и контролируем, что имя со спецсимволом без искажений сохранилось в базе данных и вывелось на интерфейс - ПРОПУСКАЕТСЯ
        }
    }
}