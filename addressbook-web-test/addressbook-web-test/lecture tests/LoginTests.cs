using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [NonParallelizable]
    [TestFixture] //метка тестового класса
    public class LoginTests : TestBase
    {
        [Test] //метка выполнения теста
        public void LoginWithValidCredentials()
        {
            app.Auth.Logout();

            AccountData account = new AccountData("admin", "secret");
            app.Auth.Login(account);

            // проверка
            Assert.IsTrue(app.Auth.IsLoggedIn(account)); 
        }

        [Test] //метка выполнения теста
        public void LoginWithInvalidCredentials()
        {
            app.Auth.Logout();

            AccountData account = new AccountData("admin", "1236554");
            app.Auth.Login(account);

            // проверка
            Assert.IsFalse(app.Auth.IsLoggedIn(account));
        }
    }
}