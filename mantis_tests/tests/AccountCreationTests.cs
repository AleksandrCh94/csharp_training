using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace mantis_tests
{
    [TestFixture]
    public class AccountCreationTests : TestBase
    {
        [SetUp]
        public void setUpConfig()
        {
            app.Ftp.BackupFile("/config/config_inc.php");
            using (Stream localFile = File.Open("config_inc.php", FileMode.Open))
            {
                app.Ftp.Upload("/config/config_inc.php", localFile);
            }
        }

        [Test]
        public void TestAccountRegistration()
        {
            AccountData account = new AccountData()
            {
                Name = "testuser",
                Password = "password26",
                Email = "testuser26@localhost.localdomain"
            };

            List<AccountData> accounts = app.Admin.GetAllAccounts();

            AccountData existingAccount = accounts.Find(x => x.Name == account.Name);
            if (existingAccount != null)
            {
                app.Admin.DeleteAccount(existingAccount);
            }

            app.James.Delete(account);
            app.James.Add(account);

            app.Registration.Register(account);

            Assert.AreEqual(account.Name, app.Registration.GetAccountName());
        }

        [TearDown]
        public void restoreConfig()
        {
            app.Ftp.RestoreBackupFile("/config/config_inc.php");

        }
    }
}
