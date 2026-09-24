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
            app.Contacts.GetOrCreateContact(0);     // Предусловие: Гарантируем наличие хотя бы одного контакта на первой позиции (индекс 0) — создаем его, если список пуст

            List<ContactData> oldContacts = 
                app.Contacts.GetContactsList();     // Шаг 1: Получаем исходный список контактов с веб-страницы до выполнения удаления

            app.Contacts.Remove(0);                 // Шаг 2: Вызываем хелпер контактов для удаления самого первого контакта (по порядковому индексу 0)

            Assert.AreEqual(oldContacts.Count - 1,
                app.Contacts.GetContactsCount());   // Проверка 1 (Быстрая): Убеждаемся, что текущее количество строк на странице (GetContactsCount) ровно на 1 меньше, чем было изначально

            List<ContactData> newContacts = 
                app.Contacts.GetContactsList();     // Шаг 3: Получаем новый, обновленный список контактов с веб-страницы после удаления

            ContactData toBeRemoved = oldContacts[0];   // Запоминаем объект контакта, который мы намеревались удалить (самый первый из исходного списка)
            
            oldContacts.RemoveAt(0);                    // Моделируем удаление этого же контакта локально в нашем старом списке в оперативной памяти

            Assert.AreEqual(oldContacts, newContacts);  // Проверка 2 (Глубокая): Сравниваем модифицированный старый список и новый список с сайта (проверяются Фамилия и Имя благодаря IEquatable)

            foreach (ContactData contact in newContacts)    // Проверка 3 (Дополнительная): Поэлементно проверяем, что ID удаленного контакта больше не встречается ни у одного из оставшихся контактов
            {
                Assert.AreNotEqual(contact.Id, toBeRemoved.Id);
            }
        }
    }
}