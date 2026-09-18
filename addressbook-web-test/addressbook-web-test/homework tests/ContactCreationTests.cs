using NUnit.Framework;                      // Подключение тестового фреймворка NUnit
using System;                               // Подключение базовых системных типов .NET
using System.Security.Cryptography;
using System.Text;                          // Подключение поддержки работы с кодировками и строками
using System.Text.RegularExpressions;       // Подключение классов для обработки регулярных выражений
using System.Threading;                     // Подключение инструментов для управления потоками и задержками

namespace WebAddressbookTests                           // Пространство имен для логической группировки классов проекта
{
    [NonParallelizable]                                 // Атрибут NUnit: запускает тесты данного класса строго последовательно
    [TestFixture]                                       // Атрибут NUnit: помечает класс как набор автоматических тестов
    public class ContactCreationTests : AuthTestBase    // Объявление класса тестов создания контактов, унаследованного от AuthTestBase
    {
        [Test]                                              // Атрибут NUnit: помечает метод как тест-кейс для проверки создания заполненного контакта
        public void ContactCreationTest()                   // Тест-кейс: успешное добавление контакта с именем и фамилией
        {
            ContactData contact = new ContactData("alex");  // Создаем объект данных контакта и передаем обязательное имя "alex"
            contact.LastName = "chernenkov";                // Заполняем поле фамилии создаваемого контакта строкой "chernenkov"
            
            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);                   // Передаем модель контакта в хелпер контактов для заполнения формы на сайте

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            //Console.WriteLine(string.Join("\n", oldContacts));
            //Console.WriteLine("\n");
            //Console.WriteLine(string.Join("\n", newContacts));          
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]                                          // Атрибут NUnit: помечает метод как тест-кейс для проверки создания пустого контакта
        public void EmptyContactCreationTest()          // Тест-кейс: успешное добавление контакта с пустыми текстовыми полями
        {
            ContactData contact = new ContactData("");  // Создаем объект данных контакта с пустой строкой вместо имени
            contact.LastName = "";                      // Задаем пустое текстовое значение для фамилии контакта

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);               // Передаем пустую модель контакта в хелпер для сохранения в адресной книге

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }

        [Test]                                          // Атрибут NUnit: помечает метод как тест-кейс для проверки создания пустого контакта
        public void BadNameContactCreationTest()          // Тест-кейс: успешное добавление контакта с пустыми текстовыми полями
        {
            ContactData contact = new ContactData("a'a");  // Создаем объект данных контакта с пустой строкой вместо имени
            contact.LastName = "";                      // Задаем пустое текстовое значение для фамилии контакта

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Create(contact);               // Передаем пустую модель контакта в хелпер для сохранения в адресной книге

            Assert.AreEqual(oldContacts.Count + 1, app.Contacts.GetContactsCount());

            List<ContactData> newContacts = app.Contacts.GetContactsList();
            oldContacts.Add(contact);
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}