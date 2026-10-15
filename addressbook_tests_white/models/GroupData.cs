using System;
using System.Collections.Generic;
using System.Text;

namespace addressbook_tests_white
{
    public class GroupData : IComparable<GroupData>, IEquatable<GroupData>

    //Интерфейс IComparable<T> нужен, чтобы объекты GroupData можно было сортировать.
    //И C# будет использовать именно этот метод CompareTo, чтобы понять порядок элементов.
    //Здесь сортировка делегируется сравнению строк Name (через встроенный string.CompareTo)

    //Интерфейс IEquatable<T> даёт возможность сравнивать два объекта на равенство по смыслу
    //(а не по ссылке, как делает == по умолчанию для классов).
    {
        public string Name { get; set; }    //хранит имя группы

        public int CompareTo(GroupData other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(GroupData other)
        {
            if (Object.ReferenceEquals(other, this))
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
    }
}
