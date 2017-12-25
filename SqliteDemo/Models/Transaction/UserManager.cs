﻿using System;
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
  
            bool userChecker = UserPersistence.CheckUsername(newUser);

            // oldBook should be null, if this is a new book
            if (userChecker == true)
            {
                // set tomorrow as the official date added
                string salt = EncryptionManager.PasswordSalt;
                newUser.HashPassword = EncryptionManager.EncodePassword(newUser.password, salt);
                newUser.Salt = salt;
                newUser.Status = 1;
                newUser.IsAdmin = 0;
                return UserPersistence.AddUser(newUser);
            }
                return false;
        }


        /*
         * Transaction: Delete a book from the database
         * Returns true iff the book exists in the database and
         * it was successfully deleted.
         */
        public static bool DeleteUser(User delUser)
        {
            if (UserPersistence.getUserDB(delUser) == null)
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
            if (UserPersistence.getUserDB(changeUser) == null)
            {
                return false;
            }
            return UserPersistence.UpdateUserName(changeUser);
        }

        public static User[] GetAllUsers()
        {
            List<User> users = UserPersistence.GetAllUsers();
            if (users != null)
            {
                return UserPersistence.GetAllUsers().ToArray();
            }
            else
            {
                return (new User[0]);
            }
        }
        public static bool AuthenticateUser(User cr, HttpSessionStateBase session)
        {
            session["LoggedIn"] = false;
            session["IsAdmin"] = false;
            User user = UserPersistence.getUserDB(cr);
            if (user == null)
            {
                return false;
            }
            var hash = EncryptionManager.EncodePassword(cr.password, user.Salt);
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