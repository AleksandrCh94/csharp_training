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
            // Предусловие: гарантируем, что есть хотя бы одна группа и один контакт
            app.Groups.GetOrCreateGroup(0);
            app.Contacts.GetOrCreateContact(0);

            List<GroupData> groups = GroupData.GetAll();          // Считываем из БД все существующие группы

            GroupData group = null;        // Сюда запишем найденную группу
            ContactData contact = null;    // Сюда запишем найденный контакт

            // Ищем группу, в которой уже есть хотя бы один контакт (его и будем удалять)
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
                ContactData newContact = new ContactData(
                    TestBase.GenerateUniqueName("first"),
                    TestBase.GenerateUniqueName("last"));            // Создаём новый контакт с уникальным именем

                app.Contacts.Create(newContact);                     // Физически создаём его на сайте

                group = groups[0];                                   // Берём первую группу
                contact = ContactData.GetAll().Last();               // Получаем созданный контакт с настоящим Id

                app.Contacts.AddToGroup(contact, group);             // Добавляем его в группу, чтобы было что удалять
            }

            // ============ Основная часть теста ============

            List<ContactData> oldList = group.GetContacts();         // Запоминаем состав группы ДО удаления

            app.Contacts.RemoveFromGroup(contact, group);            // Выполняем основное действие

            List<ContactData> newList = group.GetContacts();         // Считываем состав группы ПОСЛЕ удаления

            // Проверка 1 (Быстрая): количество контактов должно уменьшиться ровно на 1
            Assert.AreEqual(oldList.Count - 1, newList.Count,
                "Количество контактов в группе после удаления изменилось неправильно");

            // Проверка 2: удалённый контакт НЕ должен присутствовать в группе (ищем по Id)
            bool stillInGroup = false;
            foreach (ContactData c in newList)
            {
                if (c.Id == contact.Id)
                {
                    stillInGroup = true;
                    break;
                }
            }
            Assert.IsFalse(stillInGroup,
                $"Контакт с Id={contact.Id} всё ещё присутствует в группе после удаления");

            // Проверка 3 (Дополнительная): все остальные контакты должны остаться в группе
            foreach (ContactData oldContact in oldList)
            {
                if (oldContact.Id == contact.Id)
                {
                    continue;   // Пропускаем тот контакт, который мы удаляли — его там быть не должно
                }

                bool stillThere = false;
                foreach (ContactData c in newList)
                {
                    if (c.Id == oldContact.Id)
                    {
                        stillThere = true;
                        break;
                    }
                }
                Assert.IsTrue(stillThere,
                    $"Контакт с Id={oldContact.Id} пропал из группы после удаления другого контакта");
            }
        }
    }
}