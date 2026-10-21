using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_tests_white
{
    [TestFixture]
    public class ContactCreationTests : TestBase
    {
        [Test]
        public void ContactCreationTest()
        {
            //List<ContactData> oldContacts = app.Contacts.GetContactList();  //Вызывает метод GetGroupList() из GroupHelper
                                                                    //и сохраняет текущий список существующих групп — то есть состояние до добавления новой группы

            ContactData newContact = new ContactData()    //Создаёт новый объект ContactData с именем "test FirstName" и фамилией "test LastName"
                                                          //это данные для нового контакта, которого мы собираемся добавить
            {
                FirstName = "test FirstName",
                LastName = "test LastName"
            };

            app.Contacts.Add(newContact);   //Вызывает метод Add() из GroupHelper, который:
                                        //1.Открывает диалог групп
                                        //2.Кликает кнопку “New”
                                        //3.Вводит имя "white" в текстовое поле
                                        //4.Нажимает Enter
                                        //5.Закрывает диалог

            //List<ContactData> newContacts = app.Contacts.GetContactList(); //Снова считывает список групп из UI — теперь уже после добавления новой группы.
            //                                                       //Ожидается, что в этом списке будет на одну группу больше, чем в oldGroups.

            //oldContacts.Add(newContact);        // к старому списку (который был “до” добавления) вручную добавляется новая группа.

            ////сортировка списков идёт по имени (Name).
            //oldContacts.Sort();
            //newContacts.Sort();

            //for (int i = 0; i < oldContacts.Count; i++)
            //{
            //    Console.WriteLine($"[{i}] expected: '{oldContacts[i].FirstName} {oldContacts[i].LastName}' " +
            //        $"| actual: '{newContacts[i].FirstName} {newContacts[i].LastName}'");
            //}

            //Assert.AreEqual(oldContacts, newContacts);
        }
    }
}
