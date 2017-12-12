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

            string salt = EncryptionManager.PasswordSalt;
            users.Add(new User
            {
                id = 0,
                name = "Efe",
                EmailAddress= "efe@gmail.com",
                Salt = salt,
                HashPassword = EncryptionManager.EncodePassword("abc123", salt),
                IsAdmin = 1,
                Status = 1
            });

       //     User a = GetUser(0);
        //    UserPersistence.AddUser(a);

        }
        public static User GetUser(int userId)
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
        
        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();

            string sqlQuery = "select * from user";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {
             
                User userEfe = new User
                {
                    id =(decimal) dataRow[0],
                    name = (string)dataRow[1],
                    EmailAddress = (string)dataRow[2],
                    HashPassword = (string)dataRow[3],
                    IsAdmin = (int)dataRow[4],
                    Status = (int)dataRow[5],
                    Salt = (string)dataRow[6]
         
                };
                users.Add(userEfe);
            }

            return users;
        }
        
        public static bool AddUser(User user)
        {
                    string sql = "insert into user (id, name, EmailAddress, HashPassword, IsAdmin, Status, Salt) values ("
                + user.id + ", "
                + user.name + ", "
                + user.EmailAddress + ", "
                + user.HashPassword + ", "
                + user.IsAdmin + ", "
                + user.Status + ", "
                + user.Salt + ", " +  ")";
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



        public static User getUser(User keyUser)
        {
            string sqlQuery = "select * from user where id=" + keyUser.id;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            { 
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            object[] dataRow = rows[0];
          
            User user = new User { id = (decimal)dataRow[0], name = (string)dataRow[1], EmailAddress = (string)dataRow[2], HashPassword = (string)dataRow[3], IsAdmin = (int)dataRow[4], Status = (int)dataRow[5], Salt = (string)dataRow[6] };
            return user;
        }
        /*
         * Get one user from the repository, identified by userId UNUTAN OROSPU ÇOÇUĞU
         */
       
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
