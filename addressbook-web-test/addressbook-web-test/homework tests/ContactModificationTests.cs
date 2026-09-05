using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [TestFixture] //метка
    public class ContactModificationTests : TestBase
    {
        [Test] //метка, выполнение теста
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("viktor");
            newData.LastName = "barinov";

            app.Contacts.Modify(newData);
            app.Auth.Logout();
        }
    }
}