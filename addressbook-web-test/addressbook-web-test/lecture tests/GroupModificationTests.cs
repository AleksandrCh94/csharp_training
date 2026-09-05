using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [TestFixture] //метка
    public class GroupModificationTests : TestBase // наследование
    {
        [Test] //метка, выполнение теста
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("qwe");
            newData.Header = "qwe";
            newData.Footer = "qwe";

            app.Groups.Modify(1, newData);
            app.Auth.Logout();
        }
    }
}