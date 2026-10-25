using System;

namespace mantis_project_tests
{
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        public ProjectData()    
        {                          
        }

        public ProjectData(string name)
        {
            Name = name;
        }

        public bool Equals(ProjectData other)             
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

        public int CompareTo(ProjectData other)           
        {
            if (Object.ReferenceEquals(other, null))    
            {
                return 1;                               
            }

            int compare = string.Compare(Name, other.Name, StringComparison.OrdinalIgnoreCase);

            return compare;
        }

        public string Name { get; set; }

        public int Id { get; set; }
    }
}
