using System;
using System.Collections.Generic;
using System.Text;

namespace addressbook_tests_autoit
{
    public class GroupData : IComparable<GroupData>, IEquatable<GroupData>
    {
        public string Name { get; set; }

        public int CompareTo(GroupData? other)
        {
            return this.Name.CompareTo(other.Name);
        }

        public bool Equals(GroupData? other)
        {
            return this.Name.Equals(other.Name);
        }
    }
}
