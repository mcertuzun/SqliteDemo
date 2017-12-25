using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;
using SqliteDemo.Models.Repository;
namespace SqliteDemo.Controllers
{
    /*
     * This Controller handles requests related to Books.
     */
    public class UserController : Controller
    {
        /*
         * Handle a request for a listing of books.
         */
        public ActionResult List()
        {
            User[] users = UserManager.GetAllUsers(); 
            return View(users);
        }

        /*
		 * Handle a GET request for the Add Book form.
         */
        [HttpGet]
        public ActionResult AddUser()
        {
            return View(new User());
        }
        [HttpGet]
        public ActionResult DeleteUser()
        {

            User userEfe = new User();
            return View(userEfe);
        }

        [HttpPost]
        public ActionResult DeleteUser(User UserEfe)
        {


            if (UserEfe.Name == null)
            {
                ViewBag.message = "Error: A name is required";
                return View(UserEfe);
            }
            bool result = UserManager.DeleteUser(UserEfe);
            if (result)
            {
                ViewBag.message = "User Deleted";
            }
            else
            {
                ViewBag.message = "That user could not be deleted";
            }
            User[] users = UserManager.GetAllUsers();
            return View("List", users);
        }
        /*
         * Handle the POST request from the Add Book form. The form parameters
         * are encapsulated in a Book object.
         */
        [HttpPost]
        public ActionResult AddUser(User UserEfe)
        {
            // Validate book data from the transaction
            if (UserEfe == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again";
                return View(new User());
            }
            if (UserEfe.Name == null || UserEfe.Name.Length == 0)
            {
                ViewBag.message = "Error: A name is required";
                return View(UserEfe);
            }
            if (UserEfe.Id == 0)
            {
                ViewBag.message = "Error: An user id is required";
                return View(UserEfe);
            }

            // Add the book
            bool result = UserManager.AddNewUser(UserEfe);
            if (result)
            {
                ViewBag.message = "User added";
            }
            else
            {
                ViewBag.message = "That user could not be added";
            }

            User[] users = UserManager.GetAllUsers();
            return View(UserEfe);
        }


        /*
         * This method checks if the newbook is null after that it checks
         * if the ISBN number is 0 after that if the result is true
         * it gives the message of success.
         */
        [HttpGet]
        public ActionResult ChangeUser()
        {

            return View(new User());
        }
        [HttpPost]
        public ActionResult ChangeUser(User newUser)
        {

            if (newUser == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again";
                return View(new User());
            }

            if (newUser.Id == 0)
            {
                ViewBag.message = "Error: An id is required";
                return View(newUser);
            }


            bool result = UserManager.ChangeUser(newUser);
            if (result)
            {
                ViewBag.message = "User Updated";
            }
            else
            {
                ViewBag.message = "That user could not be Updated";
            }
            User[] users = UserManager.GetAllUsers();
            return View("List", users);
        }
    }

}
