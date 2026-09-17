using System;                               // Подключение основных системных компонентов .NET
using System.Collections.Generic;           // Подключение поддержки работы с коллекциями данных
using System.Text;                          // Подключение поддержки обработки текстовых строк

namespace WebAddressbookTests               // Пространство имен проекта
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>              // Класс-модель, описывающий сущность "Группа контактов"
    {
        private string name;                // Приватное поле для хранения названия группы
        private string header = "";         // Приватное поле для хедера (шапки) группы
        private string footer = "";         // Приватное поле для футера (подвала) группы

        public GroupData(string name)       // Конструктор класса, требующий название группы при её создании
        {
            this.name = name;               // Записываем переданное имя в приватное поле name
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            return "name " + Name;
        }

        public int CompareTo(GroupData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public string Name                  // Публичное свойство для работы с полем name
        {                                   
            get                             // Блок считывания названия группы
            {
                return name;                // Отдаем значение из приватного поля name
            }
            set                             // Блок изменения названия группы
            {
                name = value;               // Присваиваем новое значение приватному полю name
            }
        }

        public string Header                // Публичное свойство для работы с полем header
        {                                   
            get                             // Блок считывания текста шапки
            {
                return header;              // Отдаем значение из приватного поля header
            }
            set                             // Блок изменения текста шапки
            {
                header = value;             // Присваиваем новое значение приватному полю header
            }
        }

        public string Footer                // Публичное свойство для работы с полем footer
        {                                   
            get                             // Блок считывания текста подвала
            {
                return footer;              // Отдаем значение из приватного поля footer
            }
            set                             // Блок изменения текста подвала
            {
                footer = value;             // Присваиваем новое значение приватному полю footer
            }
        }
    }
}