using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    public class events
    {
        public decimal eventId { get; set; }
        public decimal userId { get; set; }
        public string EventName { get; set; }

        public events()
        {

        }

        public events(int eventId1, int userId1, string EventName1)
        {
            eventId = eventId1;
            EventName = EventName1;
            userId = userId1;
        }
    }
}