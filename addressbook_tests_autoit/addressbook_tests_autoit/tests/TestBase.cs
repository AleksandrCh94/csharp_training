using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace addressbook_tests_autoit
{
    public class TestBase
    {
        public ApplicationManager app;

        [SetUp]
        public void initApplication()
        {
            app = new ApplicationManager();
        }

        [TearDown]
        public void stopApplication()
        {
            app.Stop();
        }
    }
}
