using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{

    public class StudentBookMappingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Tittle", db.Courses.Where(a => a.Id == 3));
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Tittle");
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Title");
            ViewBag.SessionId = new SelectList(db.Sessions, "Id", "SessionTitle");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StudentSearchViewModel searchViewModel)
        {
            ViewBag.CourseId = new SelectList(db.Courses, "Id", "Tittle", db.Courses.Where(a => a.Id == searchViewModel.CourseId));
            ViewBag.DepartmentId = new SelectList(db.Departments, "Id", "Tittle", db.Departments.Where(a => a.Id == searchViewModel.DepartmentId));
            ViewBag.SemesterId = new SelectList(db.Semesters, "Id", "Title", db.Semesters.Where(a => a.Id == searchViewModel.SemesterId));
            ViewBag.SessionId = new SelectList(db.Sessions, "Id", "SessionTitle", db.Sessions.Where(a => a.Id == searchViewModel.SessionId));

            Student student = db.Students.Include("Department").Include("Session").Include("Semester")
                .Where(a => a.Rollno == searchViewModel.RollNo).FirstOrDefault();


            return View(student);
        }

        /// <summary>
        /// Student library view   
        /// </summary>
        /// <param name="Id">Student Id</param>
        /// <returns></returns>
        /// /StudebtBookMapping/TableView/4
        public ActionResult TableView(int Id)
        {
            ViewBag.StudentId = Id;
            IList<StudentBookMapping> studentBookMappingList = db.StudentBookMappings.Include(a => a.Book).Include(a => a.Student)
                .Where(a => a.StudentId == Id).ToList();
            return View(studentBookMappingList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IssueBook(BookReleaseViewModel bookReleaseModel)
        {
            ViewBag.BookId = bookReleaseModel.BookId;
            Book existingBook = db.Books.Find(bookReleaseModel.BookId);

            if (existingBook != null)
            {
                StudentBookMapping newBookIssue = new StudentBookMapping
                {
                    BookId = existingBook.Id,
                    StudentId = bookReleaseModel.StudentId,
                    IssueDate = DateTime.Now.Date,
                    DueDate = bookReleaseModel.ToDate,
                    SubmissioDate = null
                };
                db.StudentBookMappings.Add(newBookIssue);
                db.SaveChanges();
                return RedirectToAction("TableView", new { Id = bookReleaseModel.StudentId });
            }


            return RedirectToAction("TableView", new { Id = bookReleaseModel.StudentId });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            StudentBookMapping bookMapping = db.StudentBookMappings.Find(id);


            if (bookMapping != null)
            {
                return View(bookMapping);

            }
            return RedirectToAction("Index");

        }


        [HttpPost]
        public ActionResult DeleteConfirm(int Id)
        {

            StudentBookMapping book = db.StudentBookMappings.Find(Id);


            db.StudentBookMappings.Remove(book);
            db.SaveChanges();


            return RedirectToAction("TableView", new { Id = book.StudentId });
        }


        public ActionResult ReturnBook(int id)
        {
            StudentBookMapping studentBook = db.StudentBookMappings.Find(id);
            studentBook.SubmissioDate = DateTime.Now;
            studentBook.Fine = CalculateFine(studentBook.DueDate, (DateTime)studentBook.SubmissioDate);
            db.Entry(studentBook).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("TableView", new { Id = studentBook.StudentId });
        }

        public int CalculateFine(DateTime DueDate, DateTime SubmissioDate)
        {



            var days = SubmissioDate.Subtract(DueDate).TotalDays;

            if (days > 0)
            {
                var amount = 5 * days;

                return (int)amount;
            }


            //Find how many days have been exceed 
            //no of exceed day * 5  
            //return amount
            return 0;
        }


        public ActionResult Dashboard()
        {
            ViewBag.TotalBooks = db.Books.Count();
            ViewBag.TotalStudents = db.Students.Count();

            IList<StudentBookMapping> TodaysList = db.StudentBookMappings.Include("Student").
                Where(a => a.IssueDate == DateTime.Now).ToList();

            ViewBag.TodaysBookRelease = TodaysList;

            IList<StudentBookMapping> ReturnList = db.StudentBookMappings.Include("Student").Include("Book").
                Where(a => a.DueDate == DateTime.Now || a.SubmissioDate == null).ToList();

            ViewBag.TodaysBookReturn = ReturnList;

            return View();
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }





}
