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
        public void TheGroupCreationTests()
        {
            navigationHelper.GoToHomePage();
            loginLogoutHelper.Login(new AccountData("admin", "secret"));
            navigationHelper.GoToGroupsPage();
            groupHelper.InitNewGroupCreation();
            GroupData group = new GroupData("aaa");
            group.Header = "wegwg";
            group.Footer = "wrwer";
            groupHelper.FillGroupForm(group);
            groupHelper.SubmitGroupCreation();
            groupHelper.ReturnToGroupsPage();
            loginLogoutHelper.Logout();
        }
    }
}