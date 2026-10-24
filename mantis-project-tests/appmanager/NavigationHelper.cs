using OpenQA.Selenium;                      
using OpenQA.Selenium.Support.UI;           
using System;                               

namespace mantis_project_tests                               
{
    public class NavigationHelper : HelperBase              
    {
        private string baseURL;                             

        public NavigationHelper(ApplicationManager manager, string baseURL)     
            : base(manager)                                                     
        {
            this.baseURL = baseURL;                                             
        }

        // Метод умного перехода на страницу авторизации
        public void GoToLoginPage()                                  
        {
            if (driver.Url == baseURL + "/login_page.php")            
            {
                return;                                             
            }
            driver.Navigate().GoToUrl(baseURL + "/login_page.php");   
        }

        // Метод умного перехода на страницу управления проектами
        public void GoToProjectsPage()                                
        {
            if (driver.Url == baseURL + "/manage_proj_page.php"                                         
                && IsElementPresent(By.XPath("//button[contains(text(), 'Создать новый проект')]")))    
            {
                return;                                             
            }
            driver.Navigate().GoToUrl(baseURL + "/manage_proj_page.php");      
        }
    }
}