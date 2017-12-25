using System;
using System.Web.Mvc;
using SqliteDemo.Models.Repository;

namespace SqliteDemo.Controllers
{
    /*
     * This Controller presents the default view, and handles the request
     * for database initialization.
     */
    public class HomeController : Controller
    {
        /*
         * View Home Page
         */
        public ActionResult Index()
        {
            return View();
        }
        
        /*
         * Initialize the database. This needs to be done only once, if the file
         * does not already exist.
         */
        public ActionResult InitDb()
        {
            bool result = RepositoryManager.Repository.Initialize();
            ViewBag.message = result ?
                "Repository Initialized" : "Repository Initialization Failed";
            return View("Index");
        }
      
    }
}