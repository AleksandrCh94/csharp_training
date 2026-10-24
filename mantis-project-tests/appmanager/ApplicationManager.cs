using System;                       
using OpenQA.Selenium;                     
using OpenQA.Selenium.Chrome;             

namespace mantis_project_tests           
{
    public class ApplicationManager     
    {
        private readonly IWebDriver driver;                       
        private readonly string baseURL;                           

        public LoginHelper Auth { get; set; }
        public ProjectHelper Project { get; set; }
        public NavigationHelper Navigator { get; set; }

        private static ApplicationManager instance;               

        private ApplicationManager()                              
        {
            ChromeOptions options = new ChromeOptions();

            // Отключаем Password Manager и уведомления об утечке/смене пароля
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("profile.password_manager_leak_detection", false);

            driver = new ChromeDriver(options);                    
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            baseURL = "http://localhost/mantisbt-2.28.4";  

            Auth = new LoginHelper(this);
            Project = new ProjectHelper(this);
            Navigator = new NavigationHelper(this, baseURL);

            AppDomain.CurrentDomain.ProcessExit += new EventHandler(OnProcessExit);     
        }                                                     

        public static ApplicationManager GetInstance()         
        {                                                     
            if (instance == null)                               
            {                                                 
                instance = new ApplicationManager();             
            }                                                 
            return instance;                                     
        }                                                     

        private static void OnProcessExit(object sender, EventArgs e)   
        {                                                               
            if (instance != null)                                     
            {                                                 
                instance.Stop();                                      
            }                                                 
        }                                                     

        public void Stop()                                             
        {                                                     
            try                                                        
            {                                                 
                if (driver != null)                                    
                {                                             
                    driver.Quit();                                     
                }                                             
            }                                                 
            catch (Exception)                                           
            {                                                 
                // Ignore errors if unable to close the browser        
            }                                                 
            finally                                                     
            {                                                 
                instance = null;                                        
            }                                                 
        }

        public IWebDriver Driver                             
        {                                                     
            get { return driver; }                            
        }                                                     
    }
}
