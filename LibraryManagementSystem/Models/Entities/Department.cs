using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem
{
    public class Department
    {

        public int Id { get; set; }

        public string Tittle { get; set; }

        [ForeignKey("Courses")]
        public int CourseId { get; set; }

        public Courses Courses { get; set; }

    }
}