using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ToDoList.Models;
using ToDoList.Repository;

namespace ToDoList.Controllers
{
    public class AccountController : Controller
    {

        DBToDoListEntities db = new DBToDoListEntities();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Registration(User model)
        {
            tblUser objtblUser = new tblUser();
            objtblUser.Name = model.Name;
            objtblUser.Username = model.Username;
            objtblUser.Email = model.Email;
            objtblUser.Password = Encryption.Encrypt(model.Password);
            objtblUser.Activated = 0;

            db.tblUsers.Add(objtblUser);

            db.SaveChanges();

            string activationUrl = "http://localhost:51722/Account/Activation?Id=" + objtblUser.ID + "&Email=" + Encryption.Encrypt(model.Email);
            string subject = "ACCOUNT VERIFICATION";
            string body = "Hello, " + model.Name + ". You've been registered. To activate your account click the link below."+ activationUrl;
            
            Mail.Send_Mail(model.Email, body, subject);

            return Json("Success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult Activation()
        {
            int ID = Convert.ToInt32(Request.QueryString["Id"]);
            tblUser objtblUser = db.tblUsers.SingleOrDefault(x => x.ID == ID);
            if(objtblUser != null)
            {
                objtblUser.Activated = 1;
                db.SaveChanges();
            }

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Login(User model)
        {
            string hashedPassword = Encryption.Encrypt(model.Password);
            string result = "Not logged in";
            tblUser objtblUser = db.tblUsers.SingleOrDefault(x => x.Email == model.Email && x.Activated == 1 && x.Password == hashedPassword);
            if(objtblUser!=null)
            {
                Session["UserID"] = objtblUser.ID;
                Session["Name"] = objtblUser.Name;
                result = "Logged in";
            }

            return Json(result,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {

            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login");

        }
    }
}