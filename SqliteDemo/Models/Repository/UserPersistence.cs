using System;
using System.Collections.Generic;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteDemo.Models.Repository
{
    /*
    * This class manages CRUD (create, retrieve, update, delete) operations
    * for users.
    */
    public class UserPersistence
    {
        private static List<User> users;

        static UserPersistence()
        {
            users = new List<User>();
           
        }
        /*
         * This method adds an admin that's name is admin and password is abc123
         */
        public static void addAdmin()
        {
            string salt = EncryptionManager.PasswordSalt;
           User Users= new User
            {

                Id = 0,
                Name = "admin",
                EmailAddress = "admin@gmail.com",
                Salt = salt,
                HashPassword = EncryptionManager.EncodePassword("abc123", salt),
                IsAdmin = 1,
                Status = 1

            };
            AddUser(Users);
        }
        /*
         * This method checks the user name with the sql statement
         */
        public static bool CheckUsername(User KeyUser)
        {
            string sqlQuery = "select * from user where name='" + KeyUser.Name +"'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
          
            if (rows.Count != 0 )
            {
                return false;

            }else
            return true;
        }
        public static User GetUser(decimal UserId)
        {
            foreach (User user in users)
            {
                if (UserId == user.Id)
                {
                    return user;
                }
            }
            return null;
        }

        public static decimal Countt()
        { 
            string sqlQuery = "select * from user";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            return (decimal)rows.Count;
        }
            public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string sqlQuery = "select * from user";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {
             
                User UserEfe = new User{
                    Id =(decimal) dataRow[0],
                    Name = (string)dataRow[1],
                    EmailAddress = (string)dataRow[2],
                    Salt = (string)dataRow[3],
                    HashPassword = (string)dataRow[4],
                    IsAdmin = (decimal)dataRow[5],
                    Status = (decimal)dataRow[6]
   
                };

                users.Add(UserEfe);
            }

            return users;
        }
        /*
       * Add a user to the database.
       * Return true if the add succeeds.
       */
        public static bool AddUser(User user)
        {
            /*
         * This method use a SQL format (insert into) to insert an user
         */
            user.Id = Countt();
                string sql = "INSERT INTO user(Id,Name ,EmailAddress ,Salt ,HashedPassword ,IsAdmin ,Status )VALUES("
                + user.Id + ", '"
                + user.Name + "', '"
                + user.EmailAddress + "', '"
                + user.Salt + "', '"
                + user.HashPassword + "', "
                + user.IsAdmin + ", "
                + user.Status + ");";
            RepositoryManager.Repository.DoCommand(sql);
            return true;        
        }
        /*
         * Update the user name that is in the database, replacing all field values except
         * the key field.
         * Return false if the user is not found, based on key field match.
         */
        public static bool UpdateUserName(User ChangeUser)
        {
            /*
            * This method use a SQL format (update) to update the 
            * user's emailAddress, name and status with using it's id
            */
            string sql = sql = "Update user SET " +
                "EmailAddress='" + ChangeUser.EmailAddress +
                "', Name = '" + ChangeUser.Name +   "', Status=" + ChangeUser.Status+" WHERE id=" + ChangeUser.Id + ";";
            return true;
        }

        public static bool UpdateUser(User ChangeUser)
        {
            /*
            * This method use a SQL format (update) to update the 
            * User's emailAddress, Name with using it's id
            */
            string sql = "Update user SET " +
                "EmailAddress='"+ ChangeUser.EmailAddress + 
                "', Name = '"+ ChangeUser.Name +  "' WHERE id=" + ChangeUser.Id + ";";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }


        //It is get the user from id information.
        public static User getUserDB(User KeyUser)
        {
            string sqlQuery = "select * from user where name='" + KeyUser.Name+"'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            // It is choosing first user.
            if (rows.Count == 0)
            {
                return null;
            }
            object[] dataRow = rows[0];
            
      
      
            User user = new User {
                Id = (decimal)dataRow[0],
                Name = (string)dataRow[1],
                EmailAddress = (string)dataRow[2],
                Salt = (string)dataRow[3],
                HashPassword = (string)dataRow[4],
                IsAdmin = (decimal)dataRow[5],
                Status = (decimal)dataRow[6]
            };
            return user;
        }

        public static User getUserID(decimal id)
        {
            string sqlQuery = "select * from user where id=" + id;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            // It is choosing first user.
            object[] dataRow = rows[0];
            if (rows.Count == 0)
            {
                return null;
            }


            User user = new User
            {
                Id = (decimal)dataRow[0],
                Name = (string)dataRow[1],
                EmailAddress = (string)dataRow[2],
                Salt = (string)dataRow[3],
                HashPassword = (string)dataRow[4],
                IsAdmin = (decimal)dataRow[5],
                Status = (decimal)dataRow[6]
            };
            return user;
        }
        public static User getUserName(string name)
        {
            string sqlQuery = "select * from user where name='" + name + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            // It is choosing first user.
            if (rows.Count == 0)
            {
                return null;
            }
            object[] dataRow = rows[0];
           


            User user = new User
            {
                Id = (decimal)dataRow[0],
                Name = (string)dataRow[1],
                EmailAddress = (string)dataRow[2],
                Salt = (string)dataRow[3],
                HashPassword = (string)dataRow[4],
                IsAdmin = (decimal)dataRow[5],
                Status = (decimal)dataRow[6]
            };
            return user;
        }

        public static bool DeleteUser(User DelUser)
        {
            string sql = "delete from user where id=" + DelUser.Id;
            if (RepositoryManager.Repository.DoCommand(sql) == 1)
            {
                return true;
            }
            return false;
        }
    }
 
}
