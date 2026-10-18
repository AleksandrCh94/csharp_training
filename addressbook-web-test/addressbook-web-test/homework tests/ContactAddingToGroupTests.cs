using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactAddingToGroupTests : AuthTestBase
    {
        [Test]
        public void ContactAddingToGroupTest()
        {
            // Precondition: гарантируем, что есть хотя бы одна группа и один контакт
            app.Groups.GetOrCreateGroup(0);
            app.Contacts.GetOrCreateContact(0);

            List<GroupData> groups = GroupData.GetAll();        // Считываем из БД все существующие группы
            List<ContactData> allContacts = ContactData.GetAll(); // Считываем из БД все существующие контакты

            GroupData group = null;      // Сюда запишем найденную подходящую группу
            ContactData contact = null;  // Сюда запишем найденный подходящий контакт

            // Условие 3: ищем такую пару "группа + контакт", где контакт НЕ входит в группу
            foreach (GroupData g in groups)                      // Перебираем каждую группу по очереди
            {
                List<ContactData> contactsInGroup = g.GetContacts();  // Получаем список контактов, уже состоящих в этой группе

                foreach (ContactData c in allContacts)            // Перебираем каждый контакт по очереди
                {
                    if (!contactsInGroup.Contains(c))              // Проверяем: этого контакта НЕТ в текущей группе?
                    {
                        group = g;                                 // Нашли подходящую группу — запоминаем её
                        contact = c;                               // Нашли подходящий контакт — запоминаем его
                        break;                                     // Прерываем внутренний цикл — пара для этой группы найдена
                    }
                }

                if (group != null)                                 // Если пара уже найдена на этой группе
                {
                    break;                                          // Прерываем и внешний цикл — дальше искать не нужно
                }
            }

            // Условие 4: если свободной пары не нашлось (все контакты уже во всех группах) — создаём её искусственно
            if (group == null)                                     // Проверяем: пара так и не была найдена?
            {
                contact = new ContactData("test first name", "test last name"); // Создаём новый контакт в памяти (пока без Id)

                app.Contacts.Create(contact);                       // Физически создаём этот контакт на сайте через UI

                contact = ContactData.GetAll().Last();              // Заново читаем контакты из БД и берём последний — это и есть только что созданный (у него появился настоящий Id)

                group = groups[0];                                  // Новый контакт гарантированно ни в одной группе не состоит — берём для него любую, например первую, группу
            }

            List<ContactData> oldList = group.GetContacts();        // Запоминаем состав группы ДО добавления контакта

            app.Contacts.AddToGroup(contact, group);                 // Выполняем основное действие — добавляем контакт в группу через UI

            List<ContactData> newList = group.GetContacts();        // Считываем состав группы ПОСЛЕ добавления

            oldList.Add(contact);                                    // Моделируем ожидаемый результат локально: добавляем контакт в старый список
            newList.Sort();                                          // Сортируем, чтобы порядок элементов не влиял на сравнение
            oldList.Sort();

            Assert.AreEqual(oldList, newList);                       // Сравниваем ожидаемый и реальный состав группы
        }
    }
}