using System;
using System.Collections.Generic;

namespace Demoproject.Core
{
    public partial class Course
    {
        public int Id { get; set; }
        public string ShortName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CourseNumber { get; set; }
        public Nullable<int> MinGradeId { get; set; }
        public Nullable<int> MaxGradeId { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> TreeSort { get; set; }
        public Nullable<bool> Enabled { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Nullable<System.DateTime> ModifiedOn { get; set; }
        public virtual Department Department { get; set; }

    }
}
