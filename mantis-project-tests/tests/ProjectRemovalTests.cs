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
            app.Project.GetOrCreateProject(0);

            List<ProjectData> oldProjects = app.Project.GetProjectsList();

            app.Project.Remove(0);                                    

            Assert.AreEqual(oldProjects.Count - 1, app.Project.GetProjectsCount()); 

            List<ProjectData> newProjects = app.Project.GetProjectsList();  

            oldProjects.RemoveAt(0);                                       

            oldProjects.Sort();      
            newProjects.Sort();

            Assert.AreEqual(oldProjects, newProjects);
        }
    }
}
