using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Repository;

namespace SqliteDemo.Controllers
{
    public class SecureController : Controller
    {
        /*
        * this class provides secure
        */
        public ActionResult Index()
        {

            if ((Session["IsAdmin"] != null) && ((bool)Session["IsAdmin"]) == true)
            {
                return View();
            }
            else
            {
                TempData["message"] = "Unauthorized access request";
                return RedirectToAction("Index", "Home");
            }
        }
    }
}