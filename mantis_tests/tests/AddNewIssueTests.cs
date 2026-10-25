using mantis_tests.Mantis;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mantis_tests
{
    [TestFixture]
    public class AddNewIssueTests : TestBase
    {
        [Test]
        public void AddNewIssue()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Id = "22"
            };

            IssueData issue = new IssueData()
            {
                Summary = "some short text",
                Description = "some long text",
                Category = "General"
            };
            app.API.CreateNewIssue(account, project, issue);
        }

        [Test]
        public void AddNewProject()
        {
            AccountData account = new AccountData()
            {
                Name = "administrator",
                Password = "root"
            };

            ProjectData project = new ProjectData()
            {
                Name = $"test_{DateTime.Now:ssfff}"
            };

            app.API.CreateNewProject(account, project);
        }
    }
}
