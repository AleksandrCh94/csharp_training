using System;                                   // Подключение базовых системных типов C#
using System.Collections.Generic;               // Подключение инструментов для работы со списками
using System.Text;                              // Подключение инструментов для работы с текстом

namespace WebAddressbookTests                   // Пространство имен проекта адресной книги
{
    public class ContactData                    // Класс-модель, представляющий структуру данных одного контакта на сайте
    {
        private string firstname;               // Приватное поле для хранения имени контакта
        private string lastname = "";           // Приватное поле для хранения фамилии (по умолчанию пустая строка)

        public ContactData(string firstname)    // Конструктор класса, требующий обязательное указание имени контакта
        {
            this.firstname = firstname;         // Сохраняем переданное в конструктор имя в приватное поле firstname
        }

        public string FirstName                 // Публичное свойство для чтения и изменения приватного поля firstname
        {                                       
            get                                 // Блок чтения свойства
            {
                return firstname;               // Возвращаем значение из приватного поля firstname
            }
            set                                 // Блок записи нового значения свойства
            {
                firstname = value;              // Обновляем значение приватного поля firstname
            }
        }

        public string LastName                  // Публичное свойство для чтения и изменения приватного поля lastname
        {                                       
            get                                 // Блок чтения свойства
            {
                return lastname;                // Возвращаем значение из приватного поля lastname
            }
            set                                 // Блок записи нового значения свойства
            {
                lastname = value;               // Обновляем значение приватного поля lastname
            }
        }
    }
}