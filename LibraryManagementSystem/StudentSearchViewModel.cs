using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem
{
    public class StudentSearchViewModel
    {
        public int CourseId { get; set; }

        public int DepartmentId { get; set; }

        public int SessionId { get; set; }

        public int SemesterId { get; set; }

        public int RollNo { get; set; }
    }
}