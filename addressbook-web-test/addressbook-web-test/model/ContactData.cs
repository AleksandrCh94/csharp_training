using System;
using System.Collections.Generic;
using System.Text;

namespace WebAddressbookTests
{
    public class ContactData
    {
        private string firstname;
        private string lastname = "";

        public ContactData(string firstname) // конструктор
        {
            this.firstname = firstname;
        }

        public string FirstName { // свойство
            get
            {
                return firstname;
            }
            set
            {
                firstname = value;
            }
        }

        public string LastName { // свойство
            get
            {
                return lastname;
            }
            set
            {
                lastname = value;
            }
        }
    }
}
