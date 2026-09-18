using System;                               // Подключение основных системных компонентов .NET
using System.Collections.Generic;           // Подключение поддержки работы с коллекциями данных
using System.Text;                          // Подключение поддержки обработки текстовых строк

namespace WebAddressbookTests               // Пространство имен проекта
{
    public class GroupData : IEquatable<GroupData>, IComparable<GroupData>              // Класс-модель, описывающий сущность "Группа контактов"
    {       
        public GroupData(string name)       // Конструктор класса, требующий название группы при её создании
        {
            Name = name;               // Записываем переданное имя в приватное поле name
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

        public string Name { get; set; }                    // Публичное свойство для работы с полем name
        
        public string Header { get; set; }            // Публичное свойство для работы с полем header
        
        public string Footer { get; set; }                  // Публичное свойство для работы с полем footer

        public string Id { get; set; }                  // Публичное свойство для работы с полем footer
    }
}