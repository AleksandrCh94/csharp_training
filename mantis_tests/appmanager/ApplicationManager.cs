using System;                               // Подключение базовых системных типов (.NET)
using OpenQA.Selenium;                      // Подключение интерфейсов Selenium WebDriver
using OpenQA.Selenium.Chrome;               // Подключение драйвера для управления браузером Chrome

namespace mantis_tests               // Пространство имен проекта
{
    public class ApplicationManager         // Главный класс-управленец для всей инфраструктуры тестов
    {
        private readonly IWebDriver driver;                         // Приватное поле для хранения экземпляра драйвера браузера
        private readonly string baseURL;                            // Приватное поле для хранения базового веб-адреса (URL) приложения

        public RegistrationHelper Registration { get; set; }
        public FtpHelper Ftp { get; set; }
        public JamesHelper James { get; set; }
        public MailHelper Mail { get; set; }

        private static ApplicationManager instance;                 // Статическое поле, хранящее единственный экземпляр этого класса во всей программе

        private ApplicationManager()                                // Приватный конструктор (запрещает создание через 'new' снаружи класса)
        {                                                     
            driver = new ChromeDriver();                            // Физический запуск и инициализация процесса браузера Google Chrome
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            baseURL = "http://localhost";   // Запись базового адреса тестируемого сайта в переменную
            Registration = new RegistrationHelper(this);
            Ftp = new FtpHelper(this);
            James = new JamesHelper(this);
            Mail = new MailHelper(this);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);     // Подписка метода OnProcessExit на событие закрытия приложения операционной системой
        }                                                     

        public static ApplicationManager GetInstance()              // Статический метод доступа к единственному экземпляру (Singleton)
        {                                                     
            if (instance == null)                                   // Проверка: если менеджер еще ни разу не создавался
            {                                                 
                instance = new ApplicationManager();                // Вызываем приватный конструктор и создаем единственный объект менеджера
            }                                                 
            return instance;                                        // Возвращаем существующий или только что созданный объект менеджера
        }                                                     

        private static void OnProcessExit(object sender, EventArgs e)   // Статический обработчик системного события завершения тестов
        {                                                               
            if (instance != null)                                       // Проверка: если менеджер существует и браузер запущен
            {                                                 
                instance.Stop();                                        // Вызываем метод безопасной остановки браузера
            }                                                 
        }                                                     

        public void Stop()                                              // Публичный метод для принудительного или планового закрытия браузера
        {                                                     
            try                                                         // Начало блока отлова возможных исключений (ошибок)
            {                                                 
                if (driver != null)                                     // Классическая проверка: если драйвер браузера существует
                {                                             
                    driver.Quit();                                      // Команда Selenium на закрытие всех окон и уничтожение процесса Chrome
                }                                             
            }                                                 
            catch (Exception)                                           // Блок перехвата любых ошибок, если браузер закрылся аварийно раньше времени
            {                                                 
                // Ignore errors if unable to close the browser         // Комментарий-заглушка: нам не важно, почему закрытие вызвало ошибку
            }                                                 
            finally                                                     // Блок, который выполнится гарантированно в любом случае
            {                                                 
                instance = null;                                        // Обнуляем статическую ссылку, чтобы очистить память для будущих запусков
            }                                                 
        }                                                     

        public IWebDriver Driver                              // Свойство для прямого доступа к самому драйверу Selenium
        {                                                     
            get { return driver; }                            // Возвращает скрытое приватное поле driver
        }                                                     
    }
}
