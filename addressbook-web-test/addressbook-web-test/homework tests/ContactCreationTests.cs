using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [TestFixture] //метка
    public class ContactCreationTests : TestBase
    {        
        [Test] //метка, выполнение теста
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("alex");
            contact.LastName = "chernenkov";

            app.Contacts.Create(contact);
            app.Auth.Logout();
        }

        [Test] //метка, выполнение теста
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("");
            contact.LastName = "";

            app.Contacts.Create(contact);
            app.Auth.Logout();
        }
    }
}