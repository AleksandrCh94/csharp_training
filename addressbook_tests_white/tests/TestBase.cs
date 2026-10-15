using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_tests_white
{
    public class TestBase
    {
        public ApplicationManager app;

        [SetUp]
        public void initApplication()
        {
            app = new ApplicationManager(); //создаётся новый экземпляр ApplicationManager
                                            //перед каждым тестом приложение запускается заново “с чистого листа”
        }

        [TearDown]
        public void stopApplication()
        {
            app.Stop();                     //закрывает приложение штатно, а не убийством процесса
        }
    }
}
