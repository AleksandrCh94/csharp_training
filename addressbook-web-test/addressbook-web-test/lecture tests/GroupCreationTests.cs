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
                GoToHomePage();
                GoToHomePage();
                Login(new AccountData("admin", "secret"));
                GoToGroupsPage();
                InitNewGroupCreation();
                GroupData group = new GroupData("aaa");
                group.Header = "wegwg";
                group.Footer = "wrwer";
                FillGroupForm(group);
                SubmitGroupCreation();
                ReturnToGroupsPage();
                Logout();
            }
        }
    }