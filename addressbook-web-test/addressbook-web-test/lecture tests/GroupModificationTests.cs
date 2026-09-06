using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests //пространство имен
{
    [NonParallelizable]
    [TestFixture] //метка
    public class GroupModificationTests : AuthTestBase // наследование
    {
        [Test] //метка, выполнение теста
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("qwe");
            newData.Header = null;
            newData.Footer = "qwe";

            app.Groups.Modify(1, newData);
        }
    }
}