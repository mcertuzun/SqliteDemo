using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    /*
    * This class represents one Credential.
    */
    public class Credential
    {
        public string UserName { get; set; }
        public string Password { get; set; }
       
       /*
      * Default constructor - no initialization.
      */
        public Credential()
        {

        }
    }
}