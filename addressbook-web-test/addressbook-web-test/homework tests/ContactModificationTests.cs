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
            ContactData newData = new ContactData("viktor");    // Создаем новый объект данных контакта и сразу задаем ему измененное имя
            newData.LastName = "doom";                          // Задаем новое значение фамилии контакта

            app.Contacts.GetOrCreateContact(0);             // Предусловие: Гарантируем наличие хотя бы одного контакта на первой позиции (индекс 0) — создаем его, если список пуст

            List<ContactData> oldContacts = 
                app.Contacts.GetContactsList();             // Шаг 1: Получаем исходный список контактов с веб-страницы до выполнения модификации

            ContactData oldData = oldContacts[0];           // Сохраняем во временную переменную старые данные изменяемого контакта (чтобы запомнить его уникальный Id для последующей проверки)

            app.Contacts.Modify(0, newData);                // Шаг 2: Вызываем хелпер контактов и передаем команду изменить самый первый контакт (индекс 0), применив новые данные newData

            Assert.AreEqual(oldContacts.Count, 
                app.Contacts.GetContactsCount());           // Проверка 1 (Быстрая): Убеждаемся, что общее количество контактов в таблице на сайте (GetContactsCount) осталось прежним

            List<ContactData> newContacts = 
                app.Contacts.GetContactsList();             // Шаг 3: Получаем новый, обновленный список контактов с веб-страницы после модификации

            oldContacts[0].FirstName = newData.FirstName;   // Имитируем изменение имени и фамилии локально в нашем старом списке в оперативной памяти для первой записи
            oldContacts[0].LastName = newData.LastName;

            oldContacts.Sort();                             // Сортируем оба списка, так как из-за изменения имени/фамилии контакт на сайте автоматически переместился на другую позицию по алфавиту
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);      // Проверка 2 (Глубокая): Сравниваем отсортированные списки (проверяются Фамилии и Имена контактов благодаря методу Equals в ContactData)

            foreach (ContactData contact in newContacts)    // Проверка 3 (Точечная): Пробегаем в цикле по новому списку и контролируем, что у контакта с тем же ID данные обновились корректно
            {
                if (contact.Id == oldData.Id)                // Находим среди актуальных контактов тот, который мы изменяли (по совпадению уникального Id)
                {
                    Assert.AreEqual(newData.LastName, contact.LastName);    // Проверяем, что его фамилия на сайте теперь совпадает с переданным newData.LastName
                    Assert.AreEqual(newData.FirstName, contact.FirstName);  // Проверяем, что его имя на сайте теперь совпадает с переданным newData.FirstName
                }
            }
        }
    }
}