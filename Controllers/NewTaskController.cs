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
            {
                Session["UserID"] = null;
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
        public ActionResult AddNewTask()
        {
            if (Session["UserID"] == null)
            {
                Session["UserID"] = null;
                return RedirectToAction("Login", "Account");
            }

            List<tblPriority> list = db.tblPriorities.ToList();
            ViewBag.PriorityList = new SelectList(list, "PriorityID", "Priority");

            return View();
        }
        [HttpPost]
        public ActionResult AddNewTask(Task model)
        {
            
            List<tblPriority> list = db.tblPriorities.ToList();
            ViewBag.PriorityList = new SelectList(list, "PriorityID", "Priority");

            if (Session["UserID"] == null)
            {
                Session["UserID"] = null;
                return RedirectToAction("Login", "Account");
            }
            else
            {
                int ID = Convert.ToInt32(Session["UserID"]);

                if (model.TaskID > 0) //EDITS THE EXISTING RECORD IN tblTasks
                {
                    tblTask objtblTaskUpdated = db.tblTasks.SingleOrDefault(x => x.TaskID == model.TaskID && x.ID == ID);

                    objtblTaskUpdated.Title = model.Title;
                    objtblTaskUpdated.TaskDetail = model.TaskDetail;
                    objtblTaskUpdated.StartDate = model.StartDate;
                    objtblTaskUpdated.StartTime = model.StartTime;
                    objtblTaskUpdated.EndDate = model.EndDate;
                    objtblTaskUpdated.EndTime = model.EndTime;
                    objtblTaskUpdated.Completed = model.Completed;
                    objtblTaskUpdated.PriorityID = model.PriorityID;

                    db.SaveChanges();
                }
                else //ADDS NEW RECORD TO tblTasks
                {
                    tblTask objtblTask = new tblTask();

                    objtblTask.Title = model.Title;
                    objtblTask.TaskDetail = model.TaskDetail;
                    objtblTask.StartDate = model.StartDate;
                    objtblTask.StartTime = model.StartTime;
                    objtblTask.EndDate = model.EndDate;
                    objtblTask.EndTime = model.EndTime;
                    objtblTask.Completed = 0;
                    objtblTask.PriorityID = model.PriorityID;
                    objtblTask.ID = Convert.ToInt32(Session["UserID"]);

                    db.tblTasks.Add(objtblTask);
                    db.SaveChanges();
                }
                return View(model);
            }
        }
    }
}