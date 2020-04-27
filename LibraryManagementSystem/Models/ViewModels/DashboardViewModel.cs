using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem
{
    public class DashboardViewModel
    {

        public IList<Student> RealeasedDate { get; set; }

        public IList<Student> DueDate { get; set; }

        public IList<Student> SubmissionDate { get; set; }

        public IList<Student> Name { get; set; }

        public IList<Student> Rollno { get; set; }

        public IList<Book> BookName { get; set; }

    }
}