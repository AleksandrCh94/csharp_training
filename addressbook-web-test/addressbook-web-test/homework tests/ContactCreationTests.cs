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
            ContactData contact = new ContactData("alex");  // Создаем объект данных контакта и передаем обязательное имя
            contact.LastName = "chernenkov";                // Заполняем поле фамилии создаваемого контакта
            
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
        public void EmptyContactCreationTest()          // Тест-кейс: успешное добавление контакта с пустыми текстовыми полями
        {
            ContactData contact = new ContactData("");  // Создаем объект данных контакта с пустой строкой вместо имени
            contact.LastName = "";                      // Задаем пустое текстовое значение для фамилии контакта

            List<ContactData> oldContacts = 
                app.Contacts.GetContactsList();         // Шаг 1: Считываем исходный список существующих контактов

            app.Contacts.Create(contact);               // Шаг 2: Передаем пустую модель контакта в хелпер для создания пустой записи в веб-интерфейсе

            Assert.AreEqual(oldContacts.Count + 1, 
                app.Contacts.GetContactsCount());       // Проверка 1 (Быстрая): Проверяем, что счетчик количества контактов в таблице на сайте увеличился на 1

            List<ContactData> newContacts = 
                app.Contacts.GetContactsList();         // Шаг 3: Считываем обновленный список контактов с веб-страницы после выполнения действия

            oldContacts.Add(contact);                   // Локально добавляем пустой контакт в старый список для синхронизации объектов в памяти

            oldContacts.Sort();                         // Сортируем списки для обеспечения идентичного порядка элементов перед сравнением
            newContacts.Sort();

            Assert.AreEqual(oldContacts, newContacts);  // Проверка 2 (Глубокая): Убеждаемся, что списки полностью совпадают и пустой контакт корректно отображается в DOM
        }

        [Test]                                          // Атрибут NUnit: помечает метод как тест-кейс для проверки создания пустого контакта
        public void BadNameContactCreationTest()        // Тест-кейс: успешное добавление контакта с пустыми текстовыми полями
        {
            ContactData contact = new ContactData("a'a");   // Создаем объект данных контакта с пустой строкой вместо имени
            contact.LastName = "";                          // Задаем пустое текстовое значение для фамилии контакта

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