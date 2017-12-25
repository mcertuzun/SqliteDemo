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
        public ActionResult Register()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Register(User us)
        {
          
            // Validate user data from the transaction
            if (us == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again";
                return View(new User());
            }
            if (us.name == null || us.name.Length == 0)
            {
                ViewBag.message = "Error: A name is required";
                return View(us);
            }

            //User value=new User();

            // Add the user
           
            bool result = UserManager.AddNewUser(us);
            if (result)
            {
                ViewBag.message = "User added";
                
            }
            else
            {
                ViewBag.message = "That user could not be added";
            }

           // User[] users = UserManager.GetAllUsers();
            return View(us);
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User cr)
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