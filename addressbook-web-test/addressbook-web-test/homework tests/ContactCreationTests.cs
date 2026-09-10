using System;                               // Подключение базовых системных типов .NET
using System.Text;                          // Подключение поддержки работы с кодировками и строками
using System.Text.RegularExpressions;       // Подключение классов для обработки регулярных выражений
using System.Threading;                     // Подключение инструментов для управления потоками и задержками
using NUnit.Framework;                      // Подключение тестового фреймворка NUnit

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

            app.Contacts.Create(contact);                   // Передаем модель контакта в хелпер контактов для заполнения формы на сайте
        }

        [Test]                                          // Атрибут NUnit: помечает метод как тест-кейс для проверки создания пустого контакта
        public void EmptyContactCreationTest()          // Тест-кейс: успешное добавление контакта с пустыми текстовыми полями
        {
            ContactData contact = new ContactData("");  // Создаем объект данных контакта с пустой строкой вместо имени
            contact.LastName = "";                      // Задаем пустое текстовое значение для фамилии контакта

            app.Contacts.Create(contact);               // Передаем пустую модель контакта в хелпер для сохранения в адресной книге
        }
    }
}