using NUnit.Framework;                      // Подключение тестового фреймворка NUnit для работы с тестами
using System;                               // Подключение базового пространства имен .NET
using System.Security.Cryptography;
using System.Text;                          // Подключение поддержки работы с кодировками и текстовыми строками
using System.Text.RegularExpressions;       // Подключение классов для обработки регулярных выражений
using System.Threading;                     // Подключение инструментов управления потоками и ожиданиями выполнения

namespace WebAddressbookTests                       // Пространство имен проекта для логической связи всех файлов
{
    [NonParallelizable]                             // Атрибут NUnit: указывает запускать тесты этого класса последовательно (без параллелизации)
    [TestFixture]                                   // Атрибут NUnit: помечает класс как набор автоматических тестов
    public class ContactRemovalTests : AuthTestBase // Объявление класса тестов удаления контактов, наследующего сессию из AuthTestBase
    {
        [Test]                                      // Атрибут NUnit: помечает метод как запускаемый автоматический тест-кейс
        public void ContactRemovalTest()            // Тест-кейс: проверка удаления существующего контакта из адресной книги
        {
            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.GetOrCreateContact(0);     // Проверка предусловия: гарантируем наличие n-ого контакта перед его удалением
            app.Contacts.Remove(0);                 // Вызываем хелпер контактов для удаления n-ого контакта из списка (по порядковому индексу n)

            Assert.AreEqual(oldContacts.Count - 1, app.Contacts.GetContactsList());

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            ContactData toBeRemoved = oldContacts[0];
            oldContacts.RemoveAt(0);
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}