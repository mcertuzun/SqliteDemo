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
  
            bool userChecker = UserPersistence.CheckUsername(newUser);

            if (userChecker == true)
            {
                string salt = EncryptionManager.PasswordSalt;
                newUser.HashPassword = EncryptionManager.EncodePassword(newUser.Password, salt);
                newUser.Salt = salt;
                newUser.Status = 0;
                newUser.IsAdmin = 0;
                return UserPersistence.AddUser(newUser);
            }
                return false;
        }


        /*
         * Transaction: Delete an user from the database
         * Returns true if the user exists in the database and
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
         * Transaction: Update an user in the database
         * Returns true if the user exists in the database and
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
        public static bool AuthenticateUser(Credential credential, HttpSessionStateBase session)
        {
            session["Status"] = false;
            session["IsAdmin"] = false;

            User user1 = new User();
            user1.Name = credential.UserName;
            User user = UserPersistence.getUserDB(user1);
          //  System.Diagnostics.Debug.WriteLine("returned: " + user.Id);
            if (user == null)
            {
                return false;
            }
            var hash = EncryptionManager.EncodePassword(credential.Password, user.Salt);

            if (hash == user.HashPassword)
            {      
                session["LoggedIn"] = true;
                session["Status"] = true;

                if (credential.UserName == "admin")
                {
                    session["IsAdmin"] = true;
                }
                else
                {
                    session["IsAdmin"] = false;
                }

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