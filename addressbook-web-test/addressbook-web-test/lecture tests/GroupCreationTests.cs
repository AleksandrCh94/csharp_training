using System;                               // Подключение базовых типов и системных функций .NET
using System.Text;                          // Подключение поддержки работы со строками и кодировками
using System.Text.RegularExpressions;       // Подключение классов для обработки регулярных выражений
using System.Threading;                     // Подключение инструментов управления потоками и паузами
using System.Collections.Generic;
using NUnit.Framework;                      // Подключение фреймворка NUnit для выполнения тестов

namespace WebAddressbookTests                       // Пространство имен для организации классов проекта
{
    [NonParallelizable]                             // Атрибут NUnit: запускает тесты этого класса последовательно (в один поток)
    [TestFixture]                                   // Атрибут NUnit: помечает класс как содержащий наборы тестов
    public class GroupCreationTests : AuthTestBase  // Объявление класса тестов создания групп, наследующего авторизацию из AuthTestBase
    {
        [Test]                                      // Атрибут NUnit: помечает метод как тест-кейс для проверки создания заполненной группы
        public void GroupCreationTest()             // Тест-кейс: успешное создание группы со всеми заполненными полями
        {
            GroupData group = new GroupData("aaad"); // Создаем объект группы и сразу задаем ей обязательное название "aaa"
            group.Header = "wegwg";                 // Заполняем поле заголовка (шапки) группы строкой "wegwg"
            group.Footer = "wrwer";                 // Заполняем поле подвала (футера) группы строкой "wrwer"

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);               // Вызываем метод хелпера групп для физического добавления группы на сайт

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            //Console.WriteLine(string.Join("\n", oldGroups));
            //Console.WriteLine(string.Join("\n", newGroups));
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]                                      // Атрибут NUnit: помечает метод как тест-кейс для проверки создания пустой группы
        public void EmptyGroupCreationTest()        // Тест-кейс: успешное создание группы с пустыми строками во всех полях
        {
            GroupData group = new GroupData("");    // Создаем объект группы с пустым текстовым значением вместо названия
            group.Header = "";                      // Задаем пустое текстовое значение для заголовка группы
            group.Footer = "";                      // Задаем пустое текстовое значение для подвала группы

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);               // Передаем пустую модель группы в хелпер для создания на сайте

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }

        [Test]                                      // Атрибут NUnit: помечает метод как тест-кейс для проверки создания пустой группы
        public void BadNameGroupCreationTest()        // Тест-кейс: успешное создание группы с пустыми строками во всех полях
        {
            GroupData group = new GroupData("a'a");    // Создаем объект группы с пустым текстовым значением вместо названия
            group.Header = "";                      // Задаем пустое текстовое значение для заголовка группы
            group.Footer = "";                      // Задаем пустое текстовое значение для подвала группы

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Create(group);               // Передаем пустую модель группы в хелпер для создания на сайте

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups.Add(group);
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}