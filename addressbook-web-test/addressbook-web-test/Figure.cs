using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace addressbook_web_tests
{
    internal class Figure
    {
        private bool colored = false; // информация об имени

        public bool Colored // свойство
        {
            get
            {
                return colored;
            }
            set
            {
                colored = value;
            }
        }
    }
}
