using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [NonParallelizable]
    [TestFixture] //метка
    public class ContactModificationTests : AuthTestBase
    {
        [Test] //метка, выполнение теста
        public void ContactModificationTest()
        {
            ContactData newData = new ContactData("viktor");
            newData.LastName = null;

            app.Contacts.Modify(1, newData);
        }
    }
}