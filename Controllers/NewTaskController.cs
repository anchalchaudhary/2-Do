using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class NewTaskController : Controller
    {
        // GET: NewTask
        DBToDoListEntities db = new DBToDoListEntities();

        public ActionResult Index()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }
        public ActionResult AddNewTask()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");
            return View();
        }
        [HttpPost]
        public ActionResult AddNewTask(Task model)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Account");

            tblTask objtblTask = new tblTask();
            objtblTask.Title = model.Title;
            objtblTask.TaskDetail = model.TaskDetail;
            objtblTask.StartDate = model.StartDate;
            objtblTask.StartTime = model.StartTime;
            objtblTask.EndDate = model.EndDate;
            objtblTask.EndTime = model.EndTime;
            objtblTask.Completed = 0;
            objtblTask.ID = Convert.ToInt32(Session["UserID"]);

            db.tblTasks.Add(objtblTask);

            db.SaveChanges();

            return View();
        }
    }
}