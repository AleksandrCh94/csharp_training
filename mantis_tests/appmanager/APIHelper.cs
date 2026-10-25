using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using mantis_tests.Mantis;

namespace mantis_tests
{
    public class APIHelper : HelperBase
    {
        public APIHelper(ApplicationManager manager) : base(manager) { }

        public void CreateNewIssue(AccountData account, ProjectData project, IssueData issueData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.IssueData issue = new Mantis.IssueData();
            issue.summary = issueData.Summary;
            issue.description = issueData.Description;
            issue.category = issueData.Category;
            issue.project = new Mantis.ObjectRef();
            issue.project.id = project.Id;
            client.mc_issue_add(account.Name, account.Password, issue);
            client.mc_projects_get_user_accessible(account.Name, account.Password);
            //client.mc_project_add();
            //client.mc_project_delete();
        }
        public void CreateNewProject(AccountData account, ProjectData projectData)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
            Mantis.ProjectData project = new Mantis.ProjectData();
            project.name = projectData.Name;
            //issue.description = issueData.Description;
            //issue.category = issueData.Category;
            //issue.project = new Mantis.ObjectRef();
            //issue.project.id = project.Id;
            client.mc_project_add(account.Name, account.Password, project);
            //client.mc_projects_get_user_accessible(account.Name, account.Password);
            //client.mc_project_add();
            //client.mc_project_delete();
        }
    }
}
