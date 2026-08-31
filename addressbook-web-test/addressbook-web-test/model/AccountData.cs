using System;
using System.Collections.Generic;
using System.Text;

namespace WebAddressbookTests
{
    public class AccountData
    {
        private string username;
        private string password;

        public AccountData(string username, string password) // конструктор
        {
            this.username = username;
            this.password = password;
        }

        public string Username // свойство
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password // свойство
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
    }
}
