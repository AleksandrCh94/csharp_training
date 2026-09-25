using System;                                   // Подключение базовых системных типов C#
using System.Collections.Generic;               // Подключение инструментов для работы со списками
using System.Text;                              // Подключение инструментов для работы с текстом
using System.Text.RegularExpressions;

namespace WebAddressbookTests                   // Пространство имен проекта адресной книги
{
    public class ContactData : IEquatable<ContactData>, 
        IComparable<ContactData>                // Класс-модель, представляющий структуру данных одного контакта на сайте
    {
        private string allPhones;
        private string allEmails;

        public ContactData(string firstname, string lastname)    // Конструктор класса, требующий обязательное указание имени контакта
        {
            FirstName = firstname;              // Сохраняем переданное в конструктор значение в свойство FirstName
            LastName = lastname;
        }

        public string FirstName { get; set; }   // Публичное автоматическое свойство для хранения Имени контакта

        public string LastName { get; set; }    // Публичное автоматическое свойство для хранения Фамилии контакта

        public string Address { get; set; }    // Публичное автоматическое свойство для хранения Адреса контакта

        public string HomePhone { get; set; }    // Публичное автоматическое свойство для хранения Домашнего телефона контакта

        public string MobilePhone { get; set; }    // Публичное автоматическое свойство для хранения Мобильного телефона контакта

        public string WorkPhone { get; set; }    // Публичное автоматическое свойство для хранения Рабочего телефона контакта

        public string Email { get; set; }    // Публичное автоматическое свойство для хранения Рабочего телефона контакта

        public string Email2 { get; set; }    // Публичное автоматическое свойство для хранения Рабочего телефона контакта

        public string Email3 { get; set; }    // Публичное автоматическое свойство для хранения Рабочего телефона контакта

        public string AllPhones
        {
            get
            {
                if (allPhones != null)
                {
                    return allPhones;
                }
                else
                {
                    return (CleanUp(HomePhone) + CleanUp(MobilePhone) + CleanUp(WorkPhone)).Trim();
                }
            }
            set
            {
                allPhones = value;
            }
        }

        public string AllEmails
        {
            get
            {
                if (allEmails != null)
                {
                    return allEmails;
                }
                else
                {
                    return (Email + "\r\n" + Email2 + "\r\n" + Email3).Trim();
                }
            }
            set
            {
                allEmails = value;
            }
        }

        private string CleanUp(string phone)
        {
            if (phone == null || phone == "")
            {
                return "";           
            }
            return Regex.Replace(phone, "[ ()-]", "") + "\r\n";            
        }

        public string Id { get; set; }          // Публичное автоматическое свойство для хранения уникального ID контакта (из HTML-атрибута)

        public bool Equals(ContactData other)           // Метод интерфейса IEquatable для сравнения текущего контакта с другим объектом ContactData
        {
            if (Object.ReferenceEquals(other, null))    // Проверка: если переданный объект равен null
            {
                return false;                           // Объекты гарантированно не равны
            }
            if (Object.ReferenceEquals(this, other))    // Проверка на идентичность ссылок: если оба указателя ведут на один и тот же объект в памяти
            {
                return true;                            // Объекты равны, глубокое сравнение полей не требуется
            }
            return LastName == other.LastName && 
                FirstName == other.FirstName;           // Контакты считаются равными, если у них полностью совпадают и Фамилия, и Имя
        }

        public override int GetHashCode()                   // Переопределение метода получения хэш-кода объекта (необходимо для корректной работы Equals в коллекциях)
        {
            return HashCode.Combine(LastName, FirstName);   // Генерируем уникальный целочисленный хэш-код на основе значений Фамилии и Имени
        }

        public override string ToString()                   // Переопределение метода преобразования объекта в строку (удобно для вывода логов в тестах при падении Assert)
        {
            return $"{LastName} {FirstName}";               // Возвращаем контакт в удобном для чтения формате "Фамилия Имя" с помощью интерполяции строк
        }

        public int CompareTo(ContactData other)             // Метод интерфейса IComparable для определения правил сортировки списка контактов
        {
            if (Object.ReferenceEquals(other, null))        // Проверка: если объект для сравнения не существует (null)
            {
                return 1;                                    // Текущий объект считается «больше» и уходит в конец списка
            }

            int compare = string.Compare(LastName, other.LastName, 
                StringComparison.OrdinalIgnoreCase);                // Сначала сравниваем фамилии без учета регистра букв (регистронезависимо)

            if (compare == 0)                                       // Если фамилии оказались абсолютно одинаковыми (например, однофамильцы)
            {
                compare = string.Compare(FirstName, other.FirstName, 
                    StringComparison.OrdinalIgnoreCase);            // Сравниваем имена, также без учета регистра букв
            }
            return compare;                                         // Возвращаем результат: меньше 0 (текущий объект идет раньше), 0 (равны), больше 0 (текущий идет позже)
        }
    }
}