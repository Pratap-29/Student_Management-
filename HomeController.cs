using StudentManagement;
using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            StudentDb studentDb = new StudentDb();
            var Details = studentDb.StudentsDetais();
            return View(Details);
        }
        public ActionResult AddStudent()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddStudent(Student student)
        {
            if (ModelState.IsValid)
            {
                StudentDb studentDb = new StudentDb();
                bool isAdded = studentDb.AddStudent(student);
                if (isAdded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error while adding student.");
                }
            }
            return View(student);
        }
        public ActionResult EditStudent(int id)
        {
          StudentDb studentDb = new StudentDb();
            var row = studentDb.StudentsDetais().Find(model => model.StudentId == id);
            return View(row);
        }
        [HttpPost]
        public ActionResult EditStudent(int id,Student student)
        {
            if (ModelState.IsValid)
            {
                StudentDb studentDb = new StudentDb();
                bool isEdited = studentDb.EditStudent(student);
                if (isEdited)
                {
                    TempData["SuccessMessage"] = "Student edited successfully.";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Error while editing student.");
                }
            }
            return View(student);
        }
        public ActionResult DeleteStudent(int id)
        {
            StudentDb studentDb = new StudentDb();
            var row = studentDb.StudentsDetais().Find(model => model.StudentId == id);
            return View(row);
        }
        [HttpPost]
        public ActionResult DeleteStudent(int id,Student student)
        {
            StudentDb studentDb = new StudentDb();
            bool isDeleted = studentDb.DeleteStudent(id);
            if (isDeleted==true)
            {
                TempData["SuccessMessage"] = "Student deleted successfully.";
                return RedirectToAction("Index");
            }
           
            return View(student);

        }
    }
}