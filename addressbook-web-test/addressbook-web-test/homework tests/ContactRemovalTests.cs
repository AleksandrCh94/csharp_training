using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [TestFixture] //метка
    public class ContactRemovalTests : TestBase
    {
        [Test] //метка, выполнение теста
        public void ContactRemovalTest()
        {
            app.Contacts.Remove(1);
            app.Auth.Logout();
        }
    }
}