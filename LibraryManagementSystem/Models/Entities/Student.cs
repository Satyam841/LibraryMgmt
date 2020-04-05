using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem
{
    public class Student
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Rollno { get; set; }

        [ForeignKey("Semester")]
        public int SemesterId { get; set; }

        public Semester Semester { get; set; }

        [ForeignKey("Session")]
        public int SessionId { get; set; }

        public Session Session { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }

    }
}