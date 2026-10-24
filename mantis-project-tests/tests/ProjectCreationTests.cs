using NUnit.Framework;
using OpenQA.Selenium;

namespace mantis_project_tests
{
    [NonParallelizable]
    [TestFixture]
    public class ProjectCreationTests : AuthTestBase
    {
        [Test]
        public void ProjectCreationTest()
        {
            ProjectData project = new ProjectData(TestBase
                .GenerateUniqueName("project"));

            List<ProjectData> oldProjects = app.Project.GetProjectsList();  

            app.Project.Create(project);                                    

            Assert.AreEqual(oldProjects.Count + 1, app.Project.GetProjectsCount()); 

            List<ProjectData> newProjects = app.Project.GetProjectsList();  

            oldProjects.Add(project);                                       

            oldProjects.Sort();    
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
