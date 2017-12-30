using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;
using SqliteDemo.Models.Repository;
using System.Web.Routing;

namespace SqliteDemo.Controllers
{
    /*
     * This Controller handles requests related to Events.
     */
    public class UserController : Controller
    {
        /*
         * Handle a request for a listing of events.
         */
        public ActionResult List()
        {
            User[] users = UserManager.GetAllUsers(); 
            return View(users);
        }

        /*
		 * Handle a GET request for the Add User form.
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
         * Handle the POST request from the Add Event form. The form parameters
         * are encapsulated in a Events object.
         */
        [HttpPost]
        public ActionResult AddUser(User UserEfe)
        {
            // Validate event data from the transaction
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
          * This method provides update the users informations
          * after it checks new informations are valid or not
          */

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
           decimal newStatus = newUser.Status; 

            newUser =(User) Session["user"];
            if (newUser == null)
            {
                return View("User", "ChangeUser");

            }
            string salt = EncryptionManager.PasswordSalt;
            if (newEmail != null && newName !=null)
            {
            User Users = new User
            {
                Id = newUser.Id,
                Name = newName,
                EmailAddress = newEmail,
                Salt = salt,
                HashPassword = EncryptionManager.EncodePassword("abc123", salt),
                IsAdmin = 0,
                Status = newStatus

            };
                bool result = UserPersistence.UpdateUserName(Users);
                if (result)
                {
                    ViewBag.message = "User Updated";
                }
                else
                {
                    ViewBag.message = "That user could not be Updated";
                }
            }
            else if(newEmail != null && newName == null)
            {

                User Users = new User
                {
                    Id = newUser.Id,
                    Name = newUser.Name,
                    EmailAddress = newEmail,
                    Salt = salt,
                    HashPassword = EncryptionManager.EncodePassword("abc123", salt),
                    IsAdmin = 0,
                    Status = newStatus

                };
                bool result = UserPersistence.UpdateUserName(Users);
                if (result)
                {
                    ViewBag.message = "User Updated";
                }
                else
                {
                    ViewBag.message = "That user could not be Updated";
                }
            }
            else
            {

                User Users = new User
                {
                    Id = newUser.Id,
                    Name = newName,
                    EmailAddress = newUser.EmailAddress,
                    Salt = salt,
                    HashPassword = EncryptionManager.EncodePassword("abc123", salt),
                    IsAdmin = 0,
                    Status = newStatus

                };

                bool result = UserPersistence.UpdateUserName(Users);
                if (result)
                {
                    ViewBag.message = "User Updated";
                }
                else
                {
                    ViewBag.message = "That user could not be Updated";
                }
            }
            User[] users = UserManager.GetAllUsers();
            return View("List", users);

        }

        /*
        * This method provides to change user's name
        */

        [HttpGet]
        public ActionResult UserName(string name)
        {
            name = (string)Session["UserId"];
            User user = UserPersistence.getUserName(name);
            if (user == null)
            {
                return HttpNotFound();
            }
            Session["user"] = user;
            return View("UserName", user);
        }
        [HttpPost]
        public ActionResult UserName(User newUser)
        {

            string newEmail = newUser.EmailAddress;
            string newName = newUser.Name;

            newUser = (User)Session["user"];
            if (newUser == null)
            {
                return View("User", "ChangeUser");

            }
            string salt = EncryptionManager.PasswordSalt;
            if (newEmail != null && newName != null)
            {
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
            }
            else if (newEmail != null && newName == null)
            {

                User Users = new User
                {
                    Id = newUser.Id,
                    Name = newUser.Name,
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
            }
            else
            {

                User Users = new User
                {
                    Id = newUser.Id,
                    Name = newName,
                    EmailAddress = newUser.EmailAddress,
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
            }
            return View(newUser);

        }

    }

}
