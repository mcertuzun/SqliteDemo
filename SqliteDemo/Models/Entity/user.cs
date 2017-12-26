﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    public class User
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string HashPassword { get; set; }
        public bool IsAdmin { get; set; }
        public bool Status { get; set; }
        public string Salt { get; set; }
        public string Password { get; set; }

        public User()
        {

        }

        public User(decimal Id, string Name, string EmailAddress1, string Salt1, string Password,  bool IsAdmin1, bool Status1)
        {
            this.Id = Id;
            this.Name = Name;
            EmailAddress = EmailAddress1;
            Salt = Salt1;
            HashPassword = Password;
            IsAdmin = IsAdmin1;
            Status = Status1;
           

        }
        
    }
}