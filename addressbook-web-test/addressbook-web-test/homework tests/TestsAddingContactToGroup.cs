using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestsAddingContactToGroup : AuthTestBase
    {
        [Test]
        public void TestAddingContactToGroup()
        {
            // Предусловие: гарантируем, что есть хотя бы одна группа и один контакт
            app.Groups.GetOrCreateGroup(0);
            app.Contacts.GetOrCreateContact(0);

            List<GroupData> groups = GroupData.GetAll();         // Считываем из БД все существующие группы
            List<ContactData> allContacts = ContactData.GetAll(); // Считываем из БД все существующие контакты

            GroupData group = null;      // Сюда запишем найденную группу
            ContactData contact = null;  // Сюда запишем найденный контакт

            // Ищем пару "группа + контакт", где контакт НЕ входит в эту группу (сравнение по Id!)
            foreach (GroupData g in groups)                       // Перебираем каждую группу
            {
                List<ContactData> contactsInGroup = g.GetContacts(); // Получаем состав группы g

                foreach (ContactData c in allContacts)             // Перебираем каждый контакт
                {
                    bool isInGroup = false;                         // Флаг: входит ли контакт c в группу g

                    foreach (ContactData inGroup in contactsInGroup) // Перебираем контакты, уже состоящие в группе
                    {
                        if (inGroup.Id == c.Id)                      // Сравниваем по Id, а не по имени!
                        {
                            isInGroup = true;                        // Нашли совпадение по Id — контакт уже в группе
                            break;
                        }
                    }

                    if (!isInGroup)                                  // Если контакт НЕ найден в группе по Id
                    {
                        group = g;                                   // Запоминаем группу
                        contact = c;                                  // Запоминаем контакт
                        break;                                        // Нашли пару, выходим из внутреннего цикла
                    }
                }

                if (group != null)                                   // Если пара уже найдена
                {
                    break;                                            // Выходим и из внешнего цикла
                }
            }

            // Если подходящей пары не нашлось — все контакты уже во всех группах
            if (group == null)                                       // Если пара так и не найдена
            {
                contact = new ContactData(
                    TestBase.GenerateUniqueName("first"),
                    TestBase.GenerateUniqueName("last"));            // Создаём новый контакт с уникальным именем

                app.Contacts.Create(contact);                         // Физически создаём его на сайте

                contact = ContactData.GetAll().Last();                // Получаем созданный контакт с настоящим Id

                group = groups[0];                                    // Берём первую группу — новый контакт точно ни в одной не состоит
            }

            // ============ Основная часть теста ============

            List<ContactData> oldList = group.GetContacts();          // Запоминаем состав группы ДО добавления

            app.Contacts.AddToGroup(contact, group);                  // Выполняем основное действие

            List<ContactData> newList = group.GetContacts();          // Считываем состав группы ПОСЛЕ добавления

            // Проверка 1 (Быстрая): количество контактов должно увеличиться ровно на 1
            Assert.AreEqual(oldList.Count + 1, newList.Count,
                "Количество контактов в группе после добавления изменилось неправильно");

            // Проверка 2: добавленный контакт должен присутствовать в группе (ищем по Id)
            bool contactFound = false;
            foreach (ContactData c in newList)
            {
                if (c.Id == contact.Id)
                {
                    contactFound = true;
                    break;
                }
            }
            Assert.IsTrue(contactFound,
                $"Контакт с Id={contact.Id} не найден в группе после добавления");

            // Проверка 3 (Дополнительная): все старые контакты должны остаться в группе
            foreach (ContactData oldContact in oldList)
            {
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
                    $"Контакт с Id={oldContact.Id} пропал из группы после добавления другого контакта");
            }
        }
    }
}