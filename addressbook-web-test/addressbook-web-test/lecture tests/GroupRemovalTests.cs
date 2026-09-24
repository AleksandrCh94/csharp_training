using System;                               // Подключение базового пространства имен .NET
using System.Text;                          // Подключение инструментов для кодирования и сборки текста
using System.Text.RegularExpressions;       // Подключение поддержки работы с регулярными выражениями
using System.Threading;                     // Подключение библиотеки для управления потоками и ожиданиями
using System.Collections.Generic;
using NUnit.Framework;                      // Подключение библиотек тестового фреймворка NUnit

namespace WebAddressbookTests                       // Пространство имен, объединяющее тесты проекта
{
    [NonParallelizable]                             // Атрибут NUnit: указывает запускать тесты этого класса строго последовательно
    [TestFixture]                                   // Атрибут NUnit: помечает класс как набор автоматических тестов
    public class GroupRemovalTests : AuthTestBase   // Объявление тестового класса, наследующего автоматический логин из AuthTestBase
    {
        [Test]                                      // Атрибут NUnit: помечает метод как отдельный выполняемый тест-кейс
        public void GroupRemovalTest()              // Тест-кейс: проверка удаления существующей группы
        {
            List<GroupData> oldGroups = app.Groups.GetGroupsList(); // Шаг 1: Получаем исходный список групп с сайта до выполнения удаления

            app.Groups.GetOrCreateGroup(0);         // Предусловие: Гарантируем наличие хотя бы одной группы на первой позиции (индекс 0) — создаем её, если список пуст
            
            app.Groups.Remove(0);                   // Шаг 2: Вызываем метод хелпера групп для удаления самой первой группы (по порядковому индексу 0)

            Assert.AreEqual(oldGroups.Count - 1, 
                app.Groups.GetGroupsCount());       // Проверка 1 (Быстрая): Убеждаемся, что текущее количество строк на странице (GetGroupsCount) ровно на 1 меньше, чем было изначально

            List<GroupData> newGroups = app.Groups.GetGroupsList(); // Шаг 3: Получаем новый, обновленный список групп с веб-страницы после удаления

            GroupData toBeRemoved = oldGroups[0];   // Запоминаем объект группы, которую мы намеревались удалить (самую первую из исходного списка)
            
            oldGroups.RemoveAt(0);                  // Моделируем удаление этой же группы локально в нашем старом списке в оперативной памяти
            
            Assert.AreEqual(oldGroups, newGroups);  // Проверка 2 (Глубокая): Сравниваем модифицированный старый список и новый список с сайта (проверяются имена групп благодаря IEquatable)

            foreach (GroupData group in newGroups)  // Проверка 3 (Дополнительная): Поэлементно проверяем, что ID удаленной группы больше не встречается ни у одной из оставшихся групп
            {
                Assert.AreNotEqual(group.Id, toBeRemoved.Id);
            }
        }
    }
}