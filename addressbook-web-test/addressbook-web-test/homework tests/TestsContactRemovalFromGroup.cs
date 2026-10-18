using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactRemovalFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestContactRemovalFromGroup()
        {
            // Precondition: гарантируем, что есть хотя бы одна группа и один контакт
            app.Groups.GetOrCreateGroup(0);
            app.Contacts.GetOrCreateContact(0);

            List<GroupData> groups = GroupData.GetAll();   // Считываем из БД все существующие группы

            GroupData group = null;       // Сюда запишем найденную подходящую группу
            ContactData contact = null;   // Сюда запишем найденный подходящий контакт

            // Условие: ищем такую группу, в которой УЖЕ ЕСТЬ хотя бы один контакт
            foreach (GroupData g in groups)                    // Перебираем каждую группу по очереди
            {
                List<ContactData> contactsInGroup = g.GetContacts();  // Получаем список контактов, которые состоят в этой группе

                if (contactsInGroup.Count > 0)                  // Проверяем: в этой группе есть хотя бы один контакт?
                {
                    group = g;                                   // Нашли подходящую группу — запоминаем её
                    contact = contactsInGroup[0];                // Берём первый контакт из этой группы — именно его и будем удалять
                    break;                                        // Прерываем цикл — пара найдена, дальше искать не нужно
                }
            }

            // Если подходящей пары не нашлось - ни в одной группе нет ни одного контакта.
            // Создаём контакт и сами добавляем его в первую группу, чтобы было что удалять.
            if (group == null)                                   // Проверяем: пара так и не была найдена?
            {
                ContactData newContact = new ContactData("test first name", "test last name"); // Создаём новый контакт в памяти

                app.Contacts.Create(newContact);                  // Физически создаём этот контакт на сайте через UI

                group = groups[0];                                 // Берём первую группу из списка — в неё и добавим контакт
                contact = ContactData.GetAll().Last();             // Заново читаем контакты из БД и берём последний — это и есть только что созданный (с настоящим Id)

                app.Contacts.AddToGroup(contact, group);           // Добавляем этот контакт в группу через UI, чтобы было что удалять дальше
            }

            List<ContactData> oldList = group.GetContacts();      // Запоминаем состав группы ДО удаления контакта

            app.Contacts.RemoveFromGroup(contact, group);          // Выполняем основное действие — удаляем контакт из группы через UI

            List<ContactData> newList = group.GetContacts();      // Считываем состав группы ПОСЛЕ удаления

            oldList.Remove(contact);                                // Моделируем ожидаемый результат локально: убираем контакт из старого списка
            oldList.Sort();                                          // Сортируем, чтобы порядок элементов не влиял на сравнение
            newList.Sort();

            Assert.AreEqual(oldList, newList);                      // Сравниваем ожидаемый и реальный состав группы
        }
    }
}