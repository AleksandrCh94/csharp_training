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
    public class ContactInformationTests : AuthTestBase    // Объявление класса тестов создания контактов, унаследованного от AuthTestBase
    {
        [Test]                                              // Атрибут NUnit: помечает метод как тест-кейс для проверки создания заполненного контакта
        public void TableAndForm_Match_Test()                // Тест-кейс: успешное добавление контакта с именем и фамилией
        {
            ContactData fromTable = app.Contacts.GetContactInformationFromTable(0);
            ContactData fromForm = app.Contacts.GetContactInformationFromForm(0);

            Assert.AreEqual(fromTable, fromForm);
            Assert.AreEqual(fromTable.Address, fromForm.Address);
            Assert.AreEqual(fromTable.AllEmails, fromForm.AllEmails);
            Assert.AreEqual(fromTable.AllPhones, fromForm.AllPhones);
        }

        [Test]                                              // Атрибут NUnit: помечает метод как тест-кейс для проверки создания заполненного контакта
        public void DetailsAndForm_Match_Test()                // Тест-кейс: успешное добавление контакта с именем и фамилией
        {
            string fromForm = app.Contacts.GetContactInformationFromFormInString(0);
            string fromDetails = app.Contacts.GetContactInformationFromDetails(0).Trim();

            Assert.AreEqual(fromDetails, fromForm);
        }
    }
}