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
                    if (model.StartDate >= DateTime.Now.Date && model.EndDate >= model.StartDate)
                    {
                        if (model.StartTime == null || model.StartTime >= DateTime.Now.TimeOfDay)
                        {
                            if (model.EndTime == null || (model.EndTime >= model.StartTime && model.StartDate == model.EndDate) || (model.EndDate > model.StartDate))
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
                            else
                            {
                                ViewBag.EndTime = "alert";
                            }
                        }
                        else
                        {
                            ViewBag.StartTime = "alert";
                        }
                    }
                    else
                    {
                        ViewBag.StartEndDate = "alert";
                    }
                }
                else //ADDS NEW RECORD TO tblTasks
                {
                    if (model.StartDate >= DateTime.Now.Date && model.EndDate >= model.StartDate)
                    {
                        if (model.StartTime == null || model.StartTime >= DateTime.Now.TimeOfDay)
                        {
                            if (model.EndTime == null || (model.EndTime >= model.StartTime && model.StartDate == model.EndDate) || model.StartDate<model.EndDate)
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

                                ViewBag.Javascript = "alert";
                            }
                            else
                            {
                                ViewBag.EndTime = "alert";
                            }
                        }
                        else
                        {
                            ViewBag.StartTime = "alert";
                        }
                    }
                    else
                    {
                        ViewBag.StartEndDate = "alert";
                    }
                }
                return View(model);
            }
        }
    }
}