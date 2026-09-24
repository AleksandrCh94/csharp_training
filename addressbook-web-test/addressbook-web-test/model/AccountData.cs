using System;                                                   // Подключение базового пространства имен .NET
using System.Collections.Generic;                               // Подключение поддержки работы с коллекциями (списки, словари)
using System.Text;                                              // Подключение классов для работы с кодировками и строками

namespace WebAddressbookTests                                   // Пространство имен, объединяющее все файлы проекта
{
    public class AccountData                                    // Класс-модель для хранения учетных данных пользователя (логин/пароль)
    {
        public AccountData(string username, string password)    // Конструктор класса, принимающий два текстовых параметра
        {
            Username = username;                           // Записываем переданный логин в автоматическое свойство Username
            Password = password;                           // Записываем переданный пароль в автоматическое свойство Password
        }

        public string Username { get; set; }    // Публичное автоматическое свойство для работы с логином (поле Username)

        public string Password { get; set; }    // Публичное автоматическое свойство для работы с паролем (поле Password)
    }
}