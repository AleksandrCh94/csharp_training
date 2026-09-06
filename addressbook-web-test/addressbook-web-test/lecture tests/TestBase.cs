using NUnit.Framework;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace WebAddressbookTests
{
    public class TestBase
    {
        protected ApplicationManager app;

        [OneTimeSetUp]  // Выполняется ОДИН раз перед ВСЕМИ тестами в классе
        public void InitApplication()
        {
            app = new ApplicationManager();
            app.Navigator.GoToHomePage();
        }

        [OneTimeTearDown]  // Выполняется ОДИН раз после ВСЕХ тестов в классе
        public void Cleanup()
        {
            if (app != null)
            {
                app.Stop();
                app = null;
            }
        }
    }
}