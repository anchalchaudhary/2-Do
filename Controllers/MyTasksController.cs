using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class MyTasksController : Controller
    {
        DBToDoListEntities db = new DBToDoListEntities();
        // GET: MyTasks
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewTask(Task model)
        {
            int loggedinUserID = Convert.ToInt32(Session["UserID"]);
            List<Task> taskList = db.tblTasks.Where(x => x.ID == loggedinUserID).Select(x => new Task
            {
                TaskID = x.TaskID,
                Title = x.Title,
                TaskDetail = x.TaskDetail,
                StartDate = x.StartDate,
                StartTime = x.StartTime,
                EndDate = x.EndDate,
                EndTime = x.EndTime
            }).ToList();

            ViewBag.TaskList = taskList;

            return View();
        }
        public ActionResult ViewTaskDetails(int TaskID)
        {
            List<Task> requiredTaskDetails = db.tblTasks.Where(x => x.TaskID == TaskID).Select(x => new Task
            {
                Title = x.Title,
                TaskDetail = x.TaskDetail,
                StartDate = x.StartDate,
                StartTime = x.StartTime,
                EndDate = x.EndDate,
                EndTime = x.EndTime
            }).ToList();

            ViewBag.RequiredTaskDetails = requiredTaskDetails;

            return PartialView("TaskDetailsPartial");
        }

        public JsonResult CompletedTask(int TaskID)
        {
            tblTask objtblTask = db.tblTasks.SingleOrDefault(x => x.Completed == 0 && x.TaskID == TaskID);
            if (objtblTask != null)
            {
                objtblTask.Completed = 1;
                db.SaveChanges();
            }
                return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteTask(int TaskID)
        {
            bool result = false;
            tblTask objtblTask = db.tblTasks.SingleOrDefault(x => x.TaskID == TaskID);
            if (objtblTask != null)
            {
                result = true;
                db.tblTasks.Remove(objtblTask);

                db.SaveChanges();

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}