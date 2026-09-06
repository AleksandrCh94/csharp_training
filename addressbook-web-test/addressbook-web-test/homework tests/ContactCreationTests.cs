using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [NonParallelizable]
    [TestFixture] //метка
    public class ContactCreationTests : AuthTestBase
    {        
        [Test] //метка, выполнение теста
        public void ContactCreationTest()
        {
            ContactData contact = new ContactData("alex");
            contact.LastName = "chernenkov";

            app.Contacts.Create(contact);
        }

        [Test] //метка, выполнение теста
        public void EmptyContactCreationTest()
        {
            ContactData contact = new ContactData("");
            contact.LastName = "";

            app.Contacts.Create(contact);
        }
    }
}