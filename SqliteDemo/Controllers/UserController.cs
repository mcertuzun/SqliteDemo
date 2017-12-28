using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;
using SqliteDemo.Models.Repository;
using System.Web.Routing;

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

            User UserEfe = new User();
            return View(UserEfe);
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
            if (users == null)
            {
                ViewBag.message = "There is not people for listing";
            }
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

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );
        }
        /*
         * This method checks if the newbook is null after that it checks
         * if the ISBN number is 0 after that if the result is true
         * it gives the message of success.
         */
        /*   public ActionResult Edit(int id = 0)
          {
              Movie movie = db.Movies.Find(id);
              if (movie == null)
              {
                  return HttpNotFound();
              }
              return View(movie);
          }

          //
          // POST: /Movies/Edit/5

         [HttpPost]
          public ActionResult Edit(Movie movie)
          {
              if (ModelState.IsValid)
              {
                  db.Entry(movie).State = EntityState.Modified;
                  db.SaveChanges();
                  return RedirectToAction("Index");
              }
              return View(movie);
          }*/
        [HttpGet]
        public ActionResult ChangeUser(int id)
        {
          
            User user= UserPersistence.getUserID(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            Session["user"]=user;
            return View("ChangeUser", user);


        }
        [HttpPost]
        public ActionResult ChangeUser(User newUser)
        {

            string newEmail = newUser.EmailAddress;
            string newName = newUser.Name;

            newUser =(User) Session["user"];
            if (newUser == null)
            {
                return View("User", "ChangeUser");

            }
            string salt = EncryptionManager.PasswordSalt;
            User Users = new User
            {
                Id = newUser.Id,
                Name = newName,
                EmailAddress = newEmail,
                Salt = salt,
                HashPassword = EncryptionManager.EncodePassword("abc123", salt),
                IsAdmin = 0,
                Status = 0

            };
            bool result = UserPersistence.UpdateUser(Users);
    
     
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
