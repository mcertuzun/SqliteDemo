using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    /*
   * This class represents one Event.
   */
    public class Events
    {
        public decimal EventId { get; set; }
        public decimal UserId { get; set; }
        public string EventName { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string Information { get; set; }
        public string PhotoURL { get; set; }
        public string Comment { get; set; }

        /*
         * Default constructor - no initialization.
        */
        public Events()
        {

        }

        /*
        * Parameterized constructor
        */
        public Events(int EventId1, int UserId1, string EventName1)
        {
            EventId = EventId1;
            EventName = EventName1;
            UserId = UserId1;
        }
    }
}