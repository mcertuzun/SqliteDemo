﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    public class User
    {
        public decimal id { get; set; }
        public string name { get; set; }
        public string EmailAddress { get; set; }
        public string HashPassword { get; set; }
        public int IsAdmin { get; set; }
        public int Status { get; set; }
        public string Salt { get; set; }
        public string password { get; set; }
        public static decimal value=0;
        public User(decimal Id, string Name, string Salt1, string Password, string EmailAddress1, int IsAdmin1, int Status1)
        {
            id = Id;
            name = Name;
            HashPassword = Password;
            EmailAddress = EmailAddress1;
            IsAdmin = IsAdmin1;
            Status = Status1;
            Salt = Salt1;

        }

        public User()
        {

        }
      
        
    }
}