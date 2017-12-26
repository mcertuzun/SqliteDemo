using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Credential()
        {

        }
    }
}