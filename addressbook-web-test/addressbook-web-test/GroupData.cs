using System;
using System.Collections.Generic;
using System.Text;

namespace WebAddressbookTests
{
    public class GroupData
    {
        private string name;
        private string header = "";
        private string footer = "";

        public GroupData(string name) // конструктор
        {
            this.name = name;
        }

        public string Name { // свойство
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Header { // свойство
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        public string Footer { // свойство
            get
            {
                return footer;
            }
            set
            {
                footer = value;
            }
        }
    }
}
