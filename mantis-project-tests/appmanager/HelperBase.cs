using OpenQA.Selenium;                     

namespace mantis_project_tests                         
{
    public class HelperBase                             
    {
        protected IWebDriver driver;                   
        protected ApplicationManager manager;         

        public HelperBase(ApplicationManager manager) 
        {
            this.manager = manager;                    
            driver = manager.Driver;                    
        }

        // Вспомогательный метод для безопасного ввода текста в поля формы
        public void Type(By locator, string text)           
        {
            if (text != null)                               
            {
                driver.FindElement(locator).Clear();       
                driver.FindElement(locator).SendKeys(text); 
            }
        }

        // Вспомогательный метод быстрой проверки физического присутствия элемента на веб-странице
        public bool IsElementPresent(By locator) 
        {
            try                             
            {
                driver.FindElement(locator);     
                return true;                
            }
            catch (NoSuchElementException) 
            {
                return false;   
            }
        }

        // Укорачиванием метода Click()
        public void Click(By locator)
        {
            driver.FindElement(locator).Click();
        }
    }
}
