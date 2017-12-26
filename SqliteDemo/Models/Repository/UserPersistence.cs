﻿using System;
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
        
        public static bool AddUser(User user)
        {

                user.Id = Countt();
                string sql = "INSERT INTO [user]([id],[name] ,[EmailAddress] ,[salt] ,[HashedPassword],[IsAdmin],[Status])VALUES("
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
         * Update a book that is in the database, replacing all field values except
         * the key field.
         * Return false if the book is not found, based on key field match.
         */
        public static bool UpdateUserName(User ChangeUser)
        {
            /*
            * This method use a SQL format (update) to update the 
            * book's title with using it's ISBN number
            */
            string sql = "Update user Set name='"
                + ChangeUser.Name + "' Where id=" + ChangeUser.Id + ";";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        public static bool UpdateUserEmail(User ChangeUser)
        {
            /*
            * This method use a SQL format (update) to update the 
            * book's title with using it's ISBN number
            */
            string sql = "Update user Set EmailAddress='"
                + ChangeUser.EmailAddress + "' Where id=" + ChangeUser.Id + ";";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }


        //It is get the user from id information.
        public static User getUserDB(User KeyUser)
        {
            string sqlQuery = "select * from user where name='" + KeyUser.Name+"'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
        
            if (rows.Count == 0)
            { 
                return null;
            }

        // It is choosing first user.
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
