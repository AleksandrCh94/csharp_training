using OpenQA.Selenium;                      // Основные интерфейсы Selenium
using OpenQA.Selenium.Support.UI;           // WebDriverWait
using System;                               // Базовые типы .NET
using System.Collections.Generic;           // List, ICollection

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
        internal void Remove(int index)
        {
            manager.Navigator.GoToProjectsPage();
            GoToProjectPage(index);
            RemoveProject();
            SubmitRemoveProject();
        }

        // Проверяем, что список проектов не пустой, иначе создаем проект
        internal ProjectHelper GetOrCreateProject(int index)
        {
            if (!IsElementPresent(By.XPath(
                    $"(//table//tbody//tr//td//a[contains(@href, " +
                    $"'manage_proj_edit_page.php')])[{index + 1}]")))
            {
                ProjectData project = new ProjectData(TestBase
                    .GenerateUniqueName("project"));

                Create(project);
            }            
            return this;
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
        private void GoToProjectPage(int index)
        {
            Click(By.XPath(
        $"(//table//tbody//tr//td//a[contains(@href, " +
        $"'manage_proj_edit_page.php')])[{index + 1}]"));
        }

        // Нажимаем на кнопку "Удалить"
        private void RemoveProject()
        {
            Click(By.XPath("//button[contains(@formaction, 'manage_proj_delete.php')]"));
        }

        // Подтверждаем удаление проекта нажатием на кнопку "Удалить"
        private void SubmitRemoveProject()
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
        public List<ProjectData> GetProjectsList()
        {
            manager.Navigator.GoToProjectsPage();

            List<ProjectData> projects = new List<ProjectData>();

            ICollection<IWebElement> elements = driver.FindElements(
                By.XPath("//table//tbody//tr//td//a[contains(@href, 'manage_proj_edit_page.php')]"));

            foreach (IWebElement element in elements)
            {
                projects.Add(new ProjectData(element.Text));
            }

            return projects;
        }
    }
}
