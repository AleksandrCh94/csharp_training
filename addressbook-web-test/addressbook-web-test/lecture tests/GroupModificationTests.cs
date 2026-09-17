using System;                               // Подключение базовых системных компонентов .NET
using System.Text;                          // Подключение поддержки работы с текстом и кодировками
using System.Text.RegularExpressions;       // Подключение классов для обработки регулярных выражений
using System.Threading;                     // Подключение инструментов управления задержками и потоками
using NUnit.Framework;                      // Подключение тестового фреймворка NUnit

namespace WebAddressbookTests                           // Пространство имен проекта для группировки кода
{
    [NonParallelizable]                                 // Атрибут NUnit: запускает тесты этого класса последовательно, запрещая параллелизацию
    [TestFixture]                                       // Атрибут NUnit: регистрирует этот класс в Обозревателе тестов как тестовый набор
    public class GroupModificationTests : AuthTestBase  // Объявление тестового класса, автоматически выполняющего вход в систему через AuthTestBase
    {
        [Test]                                          // Атрибут NUnit: помечает метод как запускаемый автоматический тест-кейс
        public void GroupModificationTest()             // Тест-кейс: проверка редактирования параметров существующей группы
        {
            GroupData newData = new GroupData("кerh");  // Создаем новый объект данных группы и сразу задаем ей измененное название "кerh"
            newData.Header = null;                      // Указываем, что заголовок (хедер) группы при модификации менять не нужно или оставить пустым
            newData.Footer = "xcb";                     // Задаем новое значение подвала (футера) группы — строку "xcb"

            app.Groups.GetOrCreateGroup(0);             // Проверка существования группы по указанному индексу n и её автоматическое создание при отсутствии

            List<GroupData> oldGroups = app.Groups.GetGroupList();

            app.Groups.Modify(0, newData);              // Вызываем хелпер групп и передаем команду изменить n-ую группу (индекс n), применив новые данные

            List<GroupData> newGroups = app.Groups.GetGroupList();
            oldGroups[0].Name = newData.Name;
            oldGroups.Sort();
            newGroups.Sort();
            Assert.AreEqual(oldGroups, newGroups);
        }
    }
}