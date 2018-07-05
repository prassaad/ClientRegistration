using System;
using System.Collections.Generic;

namespace Demoproject.Core
{
    public partial class Department
    {
        public Department()
        {
            this.Courses = new List<Course>();
        }

        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Enabled { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
