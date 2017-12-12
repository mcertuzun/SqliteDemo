using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Repository;
namespace SqliteDemo.Models.Transaction
{
    public class UserManager
    {

        public static bool AddNewUser(User newUser)
        {
            // Verify that the book doesn't already exist
            User oldUser = UserPersistence.getUser(newUser);
            // oldBook should be null, if this is a new book
            if (oldUser != null)
            {
                return false;
            }

            // set tomorrow as the official date added
      
            return UserPersistence.AddUser(newUser);
        }


        /*
         * Transaction: Delete a book from the database
         * Returns true iff the book exists in the database and
         * it was successfully deleted.
         */
        public static bool DeleteUser(User delUser)
        {
            if (UserPersistence.getUser(delUser) == null)
            {
                return false;
            }
            return UserPersistence.DeleteUser(delUser);
        }


        /*
         * Transaction: Update a book in the database
         * Returns true iff the book exists in the database and
         * it was successfully changed.
         */
        public static bool ChangeUser(User changeUser)
        {
            if (UserPersistence.getUser(changeUser) == null)
            {
                return false;
            }
            return UserPersistence.UpdateUserName(changeUser);
        }

        public static User[] GetAllUsers()
        {
            List<User> books = UserPersistence.GetAllUsers();
            if (books != null)
            {
                return UserPersistence.GetAllUsers().ToArray();
            }
            else
            {
                return (new User[0]);
            }
        }
        public static bool AuthenticateUser(Credential cr, HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            User user = UserPersistence.GetUser(cr.UserId);
            if (user == null)
            {
                return false;
            }
            var hash = EncryptionManager.EncodePassword(cr.Password, user.Salt);
            if (hash == user.HashPassword)
            {      
                session["LoggedIn"] = true;
                session["IsAdmin"] = user.IsAdmin;
                return true;
            }
            else return false;
        }
        public static void LogoutUser(HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
        }
    }
}