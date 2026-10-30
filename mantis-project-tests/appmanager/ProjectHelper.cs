using OpenQA.Selenium;                      
using OpenQA.Selenium.Support.UI;           
using System;                               
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using mantis_tests.Mantis;

namespace mantis_project_tests
{
    public class ProjectHelper : HelperBase
    {
        public ProjectHelper(ApplicationManager manager) : base(manager)
        {
        }

        // Создаем проект
        public ProjectHelper Create(ProjectData project)
        {
            manager.Navigator.GoToProjectsPage();
            InitNewProjectCreation();
            FillProjectForm(project);
            SubmitProjectCreation();
            return this;
        }

        // Удаляем проект
        public void Remove(int index)
        {
            manager.Navigator.GoToProjectsPage();
            GoToProjectPage(index);
            RemoveProject();
            SubmitRemoveProject();
        }

        // Нажимаем на кнопку "Создать проект"
        public void InitNewProjectCreation()
        {
            Click(By.XPath("//button[contains(text(), 'Создать новый проект')]"));
        }

        // Заполняем форму создания проекта названием проекта
        public void FillProjectForm(ProjectData project)
        {
            Type(By.Id("project-name"), project.Name);
        }

        // Нажимаем на кнопку "Добавить проект"
        public void SubmitProjectCreation()
        {
            Click(By.XPath("//input[@value='Добавить проект']"));
        }

        // переходим внутрь проекта
        public void GoToProjectPage(int index)
        {
            Click(By.XPath(
        $"(//table//tbody//tr//td//a[contains(@href, " +
        $"'manage_proj_edit_page.php')])[{index + 1}]"));
        }

        // Нажимаем на кнопку "Удалить"
        public void RemoveProject()
        {
            Click(By.XPath("//button[contains(@formaction, 'manage_proj_delete.php')]"));
        }

        // Подтверждаем удаление проекта нажатием на кнопку "Удалить"
        public void SubmitRemoveProject()
        {
            Click(By.XPath("//input[@value='Удалить проект']"));
        }

        // Считает количество строк (проектов) в таблице
        public int GetProjectsCount()
        {
            return driver.FindElements(By.XPath
                ("//table//tbody//tr//td//a[contains(@href, 'manage_proj_edit_page.php')]")).Count;
        }

        // Получаем список проектов
        public List<ProjectData> GetProjectsList(AccountData account)
        {
            Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();

            Mantis.ProjectData[] mantisProjects =
                client.mc_projects_get_user_accessible(account.Username, account.Password);

            List<ProjectData> projects = new List<ProjectData>();

            foreach (Mantis.ProjectData mantisProject in mantisProjects)
            {
                projects.Add(new ProjectData
                {
                    Id = mantisProject.id, 
                    Name = mantisProject.name
                });
            }

            return projects;
        }

        // Проверяем, что список проектов не пустой, иначе создаем проект
        public ProjectHelper GetOrCreateProject(int index, AccountData account, ProjectData projectData)
        {
            List<ProjectData> existingProjects = GetProjectsList(account);

            if (existingProjects.Count <= index)
            {
                Mantis.MantisConnectPortTypeClient client = new Mantis.MantisConnectPortTypeClient();
                Mantis.ProjectData project = new Mantis.ProjectData();
                project.name = projectData.Name;
                client.mc_project_add(account.Username, account.Password, project);
            }
            return this;
        }
    }
}
