using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [NonParallelizable]
    [TestFixture] //метка
    public class GroupCreationTests : AuthTestBase // наследование
    {
        [Test] //метка, выполнение теста
        public void GroupCreationTest()
        {
            GroupData group = new GroupData("aaa");
            group.Header = "wegwg";
            group.Footer = "wrwer";

            app.Groups.Create(group);
        }

        [Test] //метка, выполнение теста
        public void EmptyGroupCreationTest()
        {
            GroupData group = new GroupData("");
            group.Header = "";
            group.Footer = "";

            app.Groups.Create(group);
        }
    }
}