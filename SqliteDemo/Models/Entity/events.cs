﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    public class Events
    {
        public decimal EventId { get; set; }
        public decimal UserId { get; set; }
        public string EventName { get; set; }

        public Events()
        {

        }

        public Events(int EventId1, int UserId1, string EventName1)
        {
            EventId = EventId1;
            EventName = EventName1;
            UserId = UserId1;
        }
    }
}