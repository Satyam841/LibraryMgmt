using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem
{
    public class BookReleaseViewModel
    {
        public int StudentId { get; set; }
        public int BookId { get; set; }

        public DateTime ToDate { get; set; }


    }
}