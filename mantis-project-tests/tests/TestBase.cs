using System;
using NUnit.Framework;              

namespace mantis_project_tests                           
{
    public class TestBase                               
    {                
        protected ApplicationManager app;               

        [SetUp]                                         
        public void InitApplication()                   
        {                           
            app = ApplicationManager.GetInstance();     
            app.Navigator.GoToLoginPage();
        }

        public static string GenerateUniqueName(string prefix)
        {
            return $"{prefix}_{DateTime.Now:ssfff}";
        }
    }                               
}