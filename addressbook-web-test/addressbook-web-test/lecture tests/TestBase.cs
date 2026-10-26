using System;
using NUnit.Framework;              // Подключение библиотеки NUnit для использования тестовых атрибутов
using System.Text;                                  

namespace WebAddressbookTests                           // Пространство имен проекта
{
    public class TestBase                               // Главный базовый класс, от которого наследуются все тестовые классы
    {                
        public static bool PERFORM_LONG_UI_CHECKS = true;
        protected ApplicationManager app;               // Защищенная переменная, видимая в наследниках, для работы с менеджером сайта

        [SetUp]                                         // Атрибут NUnit: этот метод автоматически вызывается ПЕРЕД каждым тест-кейсом
        public void InitApplication()                   // Метод подготовки окружения для очередного теста
        {                           
            app = ApplicationManager.GetInstance();     // Запрашиваем у Singleton живой экземпляр менеджера (окно откроется, если это первый тест)
            app.Navigator.GoToHomePage();               // Команда хелперу навигации загрузить главную страницу сайта перед началом теста
        }

        public static string GenerateUniqueName(string prefix)
        {
            return $"{prefix}_{DateTime.Now:mmssfff}";
        }

        public static Random rnd = new Random();

        public static string GenerateRandomString(int max)
        {
            int l = Convert.ToInt32(rnd.NextDouble() * max);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < l; i++)
            {
                builder.Append(Convert.ToChar(32 + Convert.ToInt32(rnd.NextDouble() * 65)));
            }
            return builder.ToString();
        }
    }                               
}