using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class TestBase
    {                
        protected ApplicationManager app;

        [SetUp] // метка выполнения кода перед тестами
        public void SetupTest()
        {
            app = new ApplicationManager();
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
        }

        [TearDown] // метка выполнения кода после тестов
        public void TeardownTest()
        {
            app.Stop();
        } 
    }
}