using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_tests_white
{
    [TestFixture]
    public class GroupDeletingTests : TestBase
    {
        [Test]
        public void TestGroupDelete()
        {
            app.Groups.EnsureGroupExists();                         // гарантируем, что есть хотя бы 1 группа

            List<GroupData> oldGroups = app.Groups.GetGroupList();  //Вызывает метод GetGroupList() из GroupHelper
                                                                    //и сохраняет текущий список существующих групп — то есть состояние до удаления группы
            int indexToDelete = 1;          // избегаем удаления группы General

            app.Groups.Delete(indexToDelete);

            List<GroupData> newGroups = app.Groups.GetGroupList(); //Снова считывает список групп из UI — теперь уже после добавления новой группы.
                                                                   //Ожидается, что в этом списке будет на одну группу меньше, чем в oldGroups.
            oldGroups.RemoveAt(indexToDelete);

            oldGroups.Sort();
            newGroups.Sort();

            //Вывод в консоль старого и нового списков групп для 
            for (int i = 0; i < oldGroups.Count; i++)
            {
                Console.WriteLine($"[{i}] expected: '{oldGroups[i].Name}' | actual: '{newGroups[i].Name}'");
            }

            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}
