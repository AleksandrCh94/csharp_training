using System;                               // Подключение базовых типов и системных функций .NET
using System.IO;
using System.Text;                          // Подключение поддержки работы со строками и кодировками
using System.Text.RegularExpressions;       // Подключение классов для обработки регулярных выражений
using System.Threading;                     // Подключение инструментов управления потоками и паузами
using System.Collections.Generic;
using System.Xml;                      // Подключение фреймворка NUnit для выполнения тестов
using System.Xml.Serialization;                      // Подключение фреймворка NUnit для выполнения тестов
using Newtonsoft.Json;
using Excel = Microsoft.Office.Interop.Excel;
using NUnit.Framework;
using OpenQA.Selenium.BiDi.Network;

namespace WebAddressbookTests                       // Пространство имен для организации классов проекта
{
    [NonParallelizable]                             // Атрибут NUnit: запускает тесты этого класса последовательно (в один поток)
    [TestFixture]                                   // Атрибут NUnit: помечает класс как содержащий наборы тестов
    public class GroupCreationTests : AuthTestBase  // Объявление класса тестов создания групп, наследующего авторизацию из AuthTestBase
    {
        public static IEnumerable<GroupData> RandomGroupDataProvider()
        {
            List<GroupData> groups = new List<GroupData>();
            for (int i = 0; i < 5; i++)
            {
                groups.Add(new GroupData(GenerateRandomString(30))
                {
                    Header = (GenerateRandomString(100)),
                    Footer = (GenerateRandomString(100))
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromCsvFile()
        {
            List<GroupData> groups = new List<GroupData>();

            string[] lines = File.ReadAllLines(@"groups.csv");
            foreach (string l in lines)
            {
                string[] parts = l.Split(',');
                groups.Add(new GroupData(parts[0])
                {
                    Header = parts[1],
                    Footer = parts[2]
                });
            }
            return groups;
        }

        public static IEnumerable<GroupData> GroupDataFromXmlFile()
        {            
            return (List<GroupData>)
                new XmlSerializer(typeof(List<GroupData>))
                    .Deserialize(new StreamReader(@"groups.xml"));
        }

        public static IEnumerable<GroupData> GroupDataFromJsonFile()
        {
            return JsonConvert.DeserializeObject<List<GroupData>>(
                File.ReadAllText(@"groups.json"));
        }

        public static IEnumerable<GroupData> GroupDataFromExcelFile()
        {
            List<GroupData> groups = new List<GroupData>();
            Excel.Application app = new Excel.Application();
            app.Visible = true;
            Excel.Workbook wb = app.Workbooks.Open(Path.Combine
                (Directory.GetCurrentDirectory(), @"groups.xlsx"));
            Excel.Worksheet sheet = wb.Sheets[1];
            Excel.Range range = sheet.UsedRange;
            for (int i = 1; i <= range.Rows.Count; i++)
            {
                groups.Add(new GroupData()
                {
                    Name = range.Cells[i, 1].Value,
                    Header = range.Cells[i, 2].Value,
                    Footer = range.Cells[i, 3].Value
                });
            }
            wb.Close();
            app.Visible = false;
            app.Quit();
            return groups;
        }

        [Test, TestCaseSource("GroupDataFromExcelFile")]  // Атрибут NUnit: помечает метод как тест-кейс для проверки создания заполненной группы
        public void GroupCreationTest(GroupData group)   // Тест-кейс: успешное создание группы со всеми заполненными полями
        {          
            List<GroupData> oldGroups = 
                app.Groups.GetGroupsList();         // Шаг 1: Считываем исходный список групп с веб-страницы до выполнения операции создания

            app.Groups.Create(group);               // Шаг 2: Вызываем метод хелпера групп для физического добавления новой группы на сайт через UI

            Assert.AreEqual(oldGroups.Count + 1,
                app.Groups.GetGroupsCount());       // Проверка 1 (Быстрая): Убеждаемся, что текущее количество строк в таблице на сайте (GetGroupsCount) стало ровно на 1 больше

            List<GroupData> newGroups = 
                app.Groups.GetGroupsList();         // Шаг 3: Получаем новый, актуальный список групп с сайта после успешного сохранения
            
            oldGroups.Add(group);                   // Имитируем добавление новой группы локально в наш старый список в оперативной памяти

            oldGroups.Sort();                       // Сортируем оба списка по алфавиту (благодаря IComparable в GroupData), так как новая группа на сайте автоматически встала на свое алфавитное место
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);  // Проверка 2 (Глубокая): Сравниваем старый дополненный список и новый список с сайта поэлементно (проверяются имена групп)
        }

        [Test]                                      // Атрибут NUnit: помечает метод как тест-кейс для проверки создания пустой группы
        public void BadNameGroupCreationTest()      // Тест-кейс: успешное создание группы с пустыми строками во всех полях
        {
            GroupData group = new GroupData("a'a"); // Создаем объект группы с пустым текстовым значением вместо названия
            group.Header = "";                      // Задаем пустое текстовое значение для заголовка группы
            group.Footer = "";                      // Задаем пустое текстовое значение для подвала группы

            List<GroupData> oldGroups = 
                app.Groups.GetGroupsList();         // Шаг 1: Считываем исходный список существующих групп

            app.Groups.Create(group);               // Шаг 2: Передаем модель группы со спецсимволом в хелпер для создания на сайте

            Assert.AreEqual(oldGroups.Count + 1, 
                app.Groups.GetGroupsCount());       // Проверка 1 (Быстрая): Убеждаемся, что система НЕ создала группу и количество осталось прежним - ТЕСТ ПАДАЕТ

            List<GroupData> newGroups = 
                app.Groups.GetGroupsList();         // Шаг 3: Считываем новый список групп с веб-страницы - ПРОПУСКАЕТСЯ

            oldGroups.Add(group);                   // Добавляем созданную группу "a'a" в наш старый локальный список в памяти - ПРОПУСКАЕТСЯ

            oldGroups.Sort();                       // Сортируем списки для обеспечения одинакового порядка элементов - ПРОПУСКАЕТСЯ
            newGroups.Sort();

            Assert.AreEqual(oldGroups, newGroups);  // Проверка 2 (Глубокая): Сравниваем списки и проверяем, что имя группы со спецсимволом корректно сохранилось и отображается на сайте - ПРОПУСКАЕТСЯ
        }
    }
}