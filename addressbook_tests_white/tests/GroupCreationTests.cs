using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_tests_white
{
    [TestFixture]
    public class GroupCreationTests : TestBase
    {
        [Test]
        public void TestGroupCreation()
        {
            List<GroupData> oldGroups = app.Groups.GetGroupList();  //Вызывает метод GetGroupList() из GroupHelper
                                                                    //и сохраняет текущий список существующих групп — то есть состояние до добавления новой группы

            GroupData newGroup = new GroupData()    //Создаёт новый объект GroupData с именем "white"
                                                    //это данные для новой группы, которую мы собираемся добавить
            {
                Name = "white"
            };

            app.Groups.Add(newGroup);   //Вызывает метод Add() из GroupHelper, который:
                                        //1.Открывает диалог групп
                                        //2.Кликает кнопку “New”
                                        //3.Вводит имя "white" в текстовое поле
                                        //4.Нажимает Enter
                                        //5.Закрывает диалог

            List<GroupData> newGroups = app.Groups.GetGroupList(); //Снова считывает список групп из UI — теперь уже после добавления новой группы.
                                                                   //Ожидается, что в этом списке будет на одну группу больше, чем в oldGroups.

            oldGroups.Add(newGroup);        // к старому списку (который был “до” добавления) вручную добавляется новая группа.

            //сортировка списков идёт по имени (Name).
            oldGroups.Sort();
            newGroups.Sort();

            for (int i = 0; i < oldGroups.Count; i++)
            {
                Console.WriteLine($"[{i}] expected: '{oldGroups[i].Name}' | actual: '{newGroups[i].Name}'");
            }

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
