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
            // Precondition: гарантируем, что есть хотя бы одна группа и один контакт
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
                contact = new ContactData("test first name", "test last name"); // Создаём новый контакт

                app.Contacts.Create(contact);                         // Физически создаём его на сайте

                contact = ContactData.GetAll().Last();                // Получаем созданный контакт с настоящим Id

                group = groups[0];                                    // Берём первую группу — новый контакт точно ни в одной не состоит
            }

            List<ContactData> oldList = group.GetContacts();          // Запоминаем состав группы ДО добавления

            app.Contacts.AddToGroup(contact, group);                   // Выполняем основное действие

            List<ContactData> newList = group.GetContacts();          // Считываем состав группы ПОСЛЕ добавления

            oldList.Add(contact);                                      // Моделируем ожидаемый результат
            oldList.Sort();
            newList.Sort();

            Assert.AreEqual(oldList, newList);                         // Проверяем результат
        }
    }
}