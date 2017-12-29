using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;
using SqliteDemo.Models.Repository;
using System.Text.RegularExpressions;

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
            if (us.Name == null || us.Name.Length == 0)
            {
                ViewBag.message = "Error: A name is required";
                return View(us);
            }
            string validUserId = @"^[a-z][a-z0-9]*$";
            string validPassword = @"^[a-z0-9!@#$*]{5,12}$";
            string validEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
            Match matchMail = Regex.Match(us.EmailAddress, validEmail);
            Match match = Regex.Match(us.Name, validUserId);
            Match matchPass = Regex.Match(us.Password, validPassword);
            if (!match.Success)
            {
                TempData["Username"] = "Username is not in the correct format";
                return View(us);
            }
            if (!matchPass.Success)
            {
                TempData["Password"] = "Password is not in the correct format";
                return View(us);
            }
            if (!matchMail.Success)
            {
                TempData["Email"] = "Email is not in the correct format";
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
            return View(new Credential());
        }

   

        [HttpPost]
        //It checks the content of both the userid and password input field to prevent SQL Injection attacks
        public ActionResult Login(Credential credential)
        {
            if (credential == null)
            {
                return View(new Credential());

            }
            if (credential.Password == null || credential.Password == null || credential.UserName.Length == 0 ||  credential.Password.Length == 0)
            {
                TempData["message"] = "Re-enter User Id and Password without blank fields.";
             
                return View(credential);
            }
            
            string validUserId = @"^[a-z][a-z0-9]*$";
            string validPassword = @"^[a-z0-9!@#$*]{5,12}$";
            
            Match match = Regex.Match(credential.UserName, validUserId);
            Match matchPass = Regex.Match(credential.Password, validPassword);
            
            if (!match.Success)
            {
                TempData["Username"] = "Username is not in the correct format";
                return View(credential);
            }
            if (!matchPass.Success)
            {
                TempData["Password"] = "Password is not in the correct format";
                return View(credential);
            }
         

            bool accaptable = UserManager.AuthenticateUser(credential, Session);

            if (accaptable)
            {
                TempData["message"] = "Login Successfully";
                Session["userId"] = credential.UserName;
                if (credential.UserName == "admin")
                {
                    Session["IsAdmin"] = true;
                }
                else
                {
                    Session["IsAdmin"] = false;
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["message"] = "Invalid login attempt";
                return View(credential);
            }
            //return View(credential);
        }

        


        public ActionResult Logout()
        {
            UserManager.LogoutUser(Session);
            return RedirectToAction("Index", "Home");
        }
    }
}