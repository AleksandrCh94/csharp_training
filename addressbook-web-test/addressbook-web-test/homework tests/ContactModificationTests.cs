using System;                               // Подключение базовых типов и системных функций .NET
using System.Text;                          // Подключение поддержки обработки текстовых строк и кодировок
using System.Text.RegularExpressions;       // Подключение инструментов для работы с регулярными выражениями
using System.Threading;                     // Подключение инструментов для управления задержками и потоками
using NUnit.Framework;                      // Подключение тестового фреймворка NUnit

namespace WebAddressbookTests                               // Пространство имен проекта для логической организации кода
{
    [NonParallelizable]                                     // Атрибут NUnit: запускает тесты этого класса строго в один поток (последовательно)
    [TestFixture]                                           // Атрибут NUnit: регистрирует этот класс в Обозревателе тестов как тестовый набор
    public class ContactModificationTests : AuthTestBase    // Объявление класса тестов модификации контактов, наследующего авторизацию из AuthTestBase
    {
        [Test]                                                  // Атрибут NUnit: помечает метод как запускаемый автоматический тест-кейс
        public void ContactModificationTest()                   // Тест-кейс: проверка редактирования параметров существующего контакта
        {
            ContactData newData = new ContactData("viktor");    // Создаем новый объект данных контакта и сразу задаем ему измененное имя "viktor"
            newData.LastName = "doom";                            // Указываем, что фамилию контакта при модификации менять не нужно (оставляем без изменений)

            app.Contacts.GetOrCreateContact(0);                 // Проверка предусловия: гарантируем наличие n-ого контакта перед его модификацией

            List<ContactData> oldContacts = app.Contacts.GetContactsList();

            app.Contacts.Modify(0, newData);                    // Вызываем хелпер контактов и передаем команду изменить n-ый контакт (индекс n), применив новые данные

            List<ContactData> newContacts = app.Contacts.GetContactsList();

            oldContacts[0].FirstName = newData.FirstName;
            oldContacts[0].LastName = newData.LastName;
            oldContacts.Sort();
            newContacts.Sort();
            //Console.WriteLine(string.Join("\n", oldContacts));
            //Console.WriteLine(string.Join("\n", newContacts));
            Assert.AreEqual(oldContacts, newContacts);
        }
    }
}