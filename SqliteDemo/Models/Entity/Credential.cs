using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    public class Credential
    {

        public int UserId { get; set; }
        public String Password { get; set; }
        public Credential()
        {

        }
    }
}