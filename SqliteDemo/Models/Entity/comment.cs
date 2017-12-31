using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    /*
    * This class represents one Comment.
    */
    public class Comment
    {
        public decimal CommentId { get; set; }
        public decimal EventId { get; set; }
        public string Text { get; set; }
        public string EventName { get; set; }
       /*
       * Default constructor - no initialization.
       */
        public Comment()
        {

        }
        /*
        * Parameterized constructor
        */
        public Comment(int CommentId1, int EventId1, string Text1)
        {
            CommentId = CommentId1;
            EventId = EventId1;
            Text = Text1;
        }
    }
}