using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;
using SqliteDemo.Models.Repository;
namespace SqliteDemo.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new Credential());
        }
        [HttpPost]
        public ActionResult Login(Credential cr)
        {
            if (cr == null)
            {
                return View(new Credential());
            }
            if (( (cr.Password == null) || (cr.Password.Length == 0)))
            {
                TempData["message"] = "UserId and Password needed";
                return View(cr);
                
            }
            else
            {
                
               bool control = UserManager.AuthenticateUser(cr, Session);

                 if (control)
                 {
                     TempData["message"] = "Login Successful";
                     return RedirectToAction("Index", "Home");
                 }
                 else
                 {
                     TempData["message"] = " Invalid Login credentials";
                     return View(cr);
                 }


            }


        }
        public ActionResult Logout()
        {
            UserManager.LogoutUser(Session);
            return RedirectToAction("Index", "Home");
        }
    }
}