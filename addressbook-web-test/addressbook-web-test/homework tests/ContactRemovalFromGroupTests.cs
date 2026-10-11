using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace WebAddressbookTests
{
    public class ContactRemovalFromGroupTests : AuthTestBase
    {
        [Test]
        public void TestContactRemovalFromGroup()
        {
            GroupData group = GroupData.GetAll()[0];
            List<ContactData> oldList = group.GetContacts();
            ContactData contact = ContactData.GetAll().Except(oldList).First();

            app.Contacts.RemoveFromGroup(contact, group);

            List<ContactData> newList = group.GetContacts();
            oldList.Add(contact);
            newList.Sort();
            oldList.Sort();

            Assert.AreEqual(oldList, newList);
        }
    }
}
