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
        public int CommentId { get; set; }
        public int EventId { get; set; }
        public string Text { get; set; }

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