using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem
{
    public class Book
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Pulisher { get; set; }

        public string Author { get; set; }

        [ForeignKey("Semester")]
        public int SemesterId { get; set; }

        public Semester Semester { get; set; }

        public int BookCode { get; set; }
    }
}