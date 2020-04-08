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
                .Where(a => a.DepartmentId == searchViewModel.DepartmentId && a.SessionId == searchViewModel.SessionId
                            && a.SemesterId == searchViewModel.SemesterId && a.Rollno == searchViewModel.RollNo).FirstOrDefault();


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
                    IssueDate = DateTime.Now,
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



        public ActionResult Dashboard()
        {
            IList<Book> bookList = db.Books.ToList();
            IList<Student> studentList = db.Students.ToList();

            ViewBag.TotalBooks = bookList.Count();
            ViewBag.TotalStudents = studentList.Count();

            return View();
        }
    }
}
