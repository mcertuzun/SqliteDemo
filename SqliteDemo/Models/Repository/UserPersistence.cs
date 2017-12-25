using System;
using System.Collections.Generic;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteDemo.Models.Repository
{
    public class UserPersistence
    {

      
        private static List<User> users;

        static UserPersistence()
        {
            users = new List<User>();

        }
        public static bool CheckUsername(User keyUser)
        {
            string sqlQuery = "select * from user where name='" + keyUser.name +"'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
          
            if (rows.Count != 0 )
            {
                return false;

            }else
            return true;
        }
        public static User GetUser(decimal userId)
        {
            foreach (User user in users)
            {
                if (userId == user.id)
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
             
                User userEfe = new User{
                    id =(decimal) dataRow[0],
                    name = (string)dataRow[1],
                    EmailAddress = (string)dataRow[2],
                    Salt = (string)dataRow[3],
                    HashPassword = (string)dataRow[4],
                    IsAdmin = (decimal)dataRow[5],
                    Status = (decimal)dataRow[6]
   
                };

                users.Add(userEfe);
            }

            return users;
        }
        
        public static bool AddUser(User user)
        {
            decimal num = UserPersistence.Countt();
           
                user.id = Countt();
                string sql = "INSERT INTO [user]([id],[name] ,[EmailAddress] ,[salt] ,[HashedPassword],[IsAdmin],[Status])VALUES("
                + user.id + ", '"
                + user.name + "', '"
                + user.EmailAddress + "', '"
                + user.Salt + "', '"
                + user.HashPassword + "', "
                + user.IsAdmin + ", "
                + user.Status + ");";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        
        }
        /*
         * Update a book that is in the database, replacing all field values except
         * the key field.
         * Return false if the book is not found, based on key field match.
         */
        public static bool UpdateUserName(User changeUser)
        {
            /*
            * This method use a SQL format (update) to update the 
            * book's title with using it's ISBN number
            */
            string sql = "Update user Set name='"
                + changeUser.name + "' Where id=" + changeUser.id + ";";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        public static bool UpdateUserEmail(User changeUser)
        {
            /*
            * This method use a SQL format (update) to update the 
            * book's title with using it's ISBN number
            */
            string sql = "Update user Set EmailAddress='"
                + changeUser.EmailAddress + "' Where id=" + changeUser.id + ";";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }


        //It is get the user from id information.
        public static User getUserDB(User keyUser)
        {
            string sqlQuery = "select * from user where name='" + keyUser.name+"'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
        
            if (rows.Count == 0)
            { 
                return null;
            }

        // It is choosing first user.
            object[] dataRow = rows[0];
          
            User user = new User {
                id = (decimal)dataRow[0],
                name = (string)dataRow[1],
                EmailAddress = (string)dataRow[2],
                Salt = (string)dataRow[3],
                HashPassword = (string)dataRow[4],
                IsAdmin = (decimal)dataRow[5],
                Status = (decimal)dataRow[6]
            };
            return user;
        }
      
       
        public static bool DeleteUser(User delUser)
        {
            string sql = "delete from user where id=" + delUser.id;
            if (RepositoryManager.Repository.DoCommand(sql) == 1)
            {
                return true;
            }
            return false;
        }
    }
 
}
