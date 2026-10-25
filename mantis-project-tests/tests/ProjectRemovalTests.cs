using NUnit.Framework;
using OpenQA.Selenium;

namespace mantis_project_tests
{
    [NonParallelizable]
    [TestFixture]
    public class ProjectRemovalTests : AuthTestBase
    {
        [Test]
        public void ProjectRemovalTest()
        {
            AccountData account = new AccountData("administrator", "root") { };

            ProjectData project = new ProjectData(TestBase
                .GenerateUniqueName("project"));

            app.Project.GetOrCreateProject(0, account, project);

            List<ProjectData> oldProjects = app.Project.GetProjectsList(account);

            app.Project.Remove(0);                                    

            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectsCount()); 

            List<ProjectData> newProjects = app.Project.GetProjectsList(account);  

            oldProjects.RemoveAt(0);                                       

            oldProjects.Sort();      
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
