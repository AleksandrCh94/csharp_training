using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;

namespace addressbook_tests_white
{
    public class ContactData : IComparable<ContactData>, IEquatable<ContactData>
    {
        public string FirstName { get; set; }    //хранит имя контаккта
        public string LastName { get; set; }     //хранит фамилию контаккта

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

        public bool Equals(ContactData other)
        {
            if (Object.ReferenceEquals(other, this))
            {
                return false;
            }
            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }
            return FirstName == other.FirstName && 
                LastName == other.LastName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LastName, FirstName);
        }
    }
}
