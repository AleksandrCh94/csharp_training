using System;                                   // Подключение базовых системных типов C#
using System.Collections.Generic;               // Подключение инструментов для работы со списками
using System.Text;                              // Подключение инструментов для работы с текстом

namespace WebAddressbookTests                   // Пространство имен проекта адресной книги
{
    public class ContactData : IEquatable<ContactData>, IComparable<ContactData>                     // Класс-модель, представляющий структуру данных одного контакта на сайте
    {
        private string firstname;               // Приватное поле для хранения имени контакта
        private string lastname = "";           // Приватное поле для хранения фамилии (по умолчанию пустая строка)

        public ContactData(string firstname)    // Конструктор класса, требующий обязательное указание имени контакта
        {
            this.firstname = firstname;         // Сохраняем переданное в конструктор имя в приватное поле firstname
        }

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return LastName == other.LastName && FirstName == other.FirstName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LastName, FirstName);
        }

        public override string ToString()
        {
            return $"{LastName} {FirstName}".Trim();
        }

        public int CompareTo(ContactData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }

            int compare = string.Compare(LastName, other.LastName, StringComparison.OrdinalIgnoreCase);

            if (compare == 0)
            {
                compare = string.Compare(FirstName, other.FirstName, StringComparison.OrdinalIgnoreCase);
            }
            return compare;
        }

        public string FirstName                 // Публичное свойство для чтения и изменения приватного поля firstname
        {                                       
            get                                 // Блок чтения свойства
            {
                return firstname;               // Возвращаем значение из приватного поля firstname
            }
            set                                 // Блок записи нового значения свойства
            {
                firstname = value?.Trim();      // Обновляем значение приватного поля firstname и удаляем пробелы по краям
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
                lastname = value?.Trim();       // Обновляем значение приватного поля lastname и удаляем пробелы по краям
            }
        }
    }
}