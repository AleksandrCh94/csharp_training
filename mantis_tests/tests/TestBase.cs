using System;
using NUnit.Framework;              // Подключение библиотеки NUnit для использования тестовых атрибутов
using System.Text;                                  

namespace mantis_tests                           // Пространство имен проекта
{
    public class TestBase                               // Главный базовый класс, от которого наследуются все тестовые классы
    {                
        public static bool PERFORM_LONG_UI_CHECKS = true;
        protected ApplicationManager app;               // Защищенная переменная, видимая в наследниках, для работы с менеджером сайта

        [SetUp]                                         // Атрибут NUnit: этот метод автоматически вызывается ПЕРЕД каждым тест-кейсом
        public void InitApplication()                   // Метод подготовки окружения для очередного теста
        {                           
            app = ApplicationManager.GetInstance();     // Запрашиваем у Singleton живой экземпляр менеджера (окно откроется, если это первый тест)
            app.Driver.Url = "http://localhost/mantisbt-2.28.4/login_page.php"; // Команда хелперу навигации загрузить главную страницу сайта перед началом теста
        }
    }                               
}