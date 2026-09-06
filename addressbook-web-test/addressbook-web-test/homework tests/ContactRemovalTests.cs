using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [NonParallelizable]
    [TestFixture] //метка
    public class ContactRemovalTests : AuthTestBase
    {
        [Test] //метка, выполнение теста
        public void ContactRemovalTest()
        {
            app.Contacts.Remove(1);
        }
    }
}