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
            app.Navigator.GoToHomePage();
            app.Auth.Login(new AccountData("admin", "secret"));
            app.Contacts.InitNewContactCreation();
            ContactData contact = new ContactData("alex");
            contact.LastName = "chernenkov";
            app.Contacts.FillContactForm(contact);
            app.Contacts.SubmitContactCreation();
            app.Contacts.ReturnToHomePage();
            app.Auth.Logout();
        }
    }
}