using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using addressbook_web_test.model;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests //пространство имен
{
    [TestFixture] //метка
    public class GroupModificationTests : TestBase
    {
        [Test] //метка, выполнение теста
        public void GroupModificationTest()
        {
            GroupData newData = new GroupData("rgwrg");
            newData.Header = "ewrq";
            newData.Footer = "wrwer";

            app.Groups.Modify(1, newData);
        }
    }
}
 