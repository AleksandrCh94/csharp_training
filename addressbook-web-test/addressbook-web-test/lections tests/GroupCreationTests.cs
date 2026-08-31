using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [TestFixture] //метка
    public class GroupCreationTests : TestBase // наследование
    {       
        [Test] //метка, выполнение теста
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("aaa");
            group.Header = "wegwg";
            group.Footer = "wrwer";

            app.Groups.Create(group);
            app.Auth.Logout();
        }

        [Test] //метка, выполнение теста
        public void EmptyGroupCreationTests()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            app.Groups.Create(group);
            app.Auth.Logout();
        }
    }
}