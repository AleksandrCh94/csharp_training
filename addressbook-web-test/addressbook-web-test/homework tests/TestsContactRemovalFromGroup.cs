using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestsContactRemovalFromGroup : AuthTestBase
    {
        [Test]
        public void TestContactRemovalFromGroup()
        {
            // Precondition: гарантируем, что есть хотя бы одна группа и один контакт
            app.Groups.GetOrCreateGroup(0);
            app.Contacts.GetOrCreateContact(0);

            List<GroupData> groups = GroupData.GetAll();          // Считываем из БД все существующие группы

            GroupData group = null;        // Сюда запишем найденную группу
            ContactData contact = null;    // Сюда запишем найденный контакт

            // Ищем группу, в которой уже есть хотя бы один контакт
            foreach (GroupData g in groups)                         // Перебираем каждую группу
            {
                List<ContactData> contactsInGroup = g.GetContacts(); // Получаем состав группы g

                if (contactsInGroup.Count > 0)                    // Если в группе есть хотя бы один контакт
                {
                    group = g;                                      // Запоминаем группу
                    contact = contactsInGroup[0];                   // Берём первый контакт из группы (его Id точно верный, т.к. пришёл из БД)
                    break;                                           // Нашли пару, выходим
                }
            }

            // Если подходящей пары не нашлось — ни в одной группе нет контактов
            if (group == null)                                       // Если пара не найдена
            {
                ContactData newContact = new ContactData("test first name", "test last name"); // Создаём новый контакт

                app.Contacts.Create(newContact);                      // Физически создаём его на сайте

                group = groups[0];                                    // Берём первую группу
                contact = ContactData.GetAll().Last();                // Получаем созданный контакт с настоящим Id

                app.Contacts.AddToGroup(contact, group);              // Добавляем его в группу, чтобы было что удалять
            }

            List<ContactData> oldList = group.GetContacts();         // Запоминаем состав группы ДО удаления

            app.Contacts.RemoveFromGroup(contact, group);              // Выполняем основное действие

            List<ContactData> newList = group.GetContacts();         // Считываем состав группы ПОСЛЕ удаления

            oldList.Remove(contact);                                   // Моделируем ожидаемый результат
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);                         // Проверяем результат
        }
    }
}