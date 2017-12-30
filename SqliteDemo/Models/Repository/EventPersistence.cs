using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteDemo.Models.Repository
{
    /*
    * This class manages CRUD (create, retrieve, update, delete) operations
    * for events.
    */
    public class EventPersistence
    {
        private static List<Events> events;

        static EventPersistence()
        {
            events = new List<Events>();

        }

        public static decimal Countt()
        {
            string sqlQuery = "select * from events";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            return rows.Count;
        }
        public static decimal CountComment()
        {
            string sqlQuery = "select * from comment";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            return rows.Count;
        }
        /*
       * Adds comment to the event
       */
        public static bool AddComment(Comment value)
        {
            /*
            * This method use a SQL format (İnsert İnto) to insert the event
            */
            string sql = "INSERT INTO comment(CommentId, EventId, Text) VALUES ("
           + CountComment() + ", "
           +value.EventId+ ", '"
           +value.Text + "' );";
            RepositoryManager.Repository.DoCommand(sql);
            return true;

        }
        
    /*
     * Retrieve from the database the book matching the EventId field of
     * the parameter.
     * Return null if the event can't be found.
     */
    public static Events GetEvent(int eventId)
        {
            foreach (Events Event in events)
            {
                if (eventId == Event.EventId)
                {
                    return Event;
                }
            }
            return null;
        }
        /*
       * Get all event data from the database and return an array of Events.
       */
        public static List<Events> GetAllEvents()
        {
            List<Events> events = new List<Events>();

            string sqlQuery = "select * from events";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {

                Events evnt = new Events
                {
                    EventId = (decimal)dataRow[0],
                    UserId = (decimal)dataRow[1],
                    EventName = (string)dataRow[2],
                    Category = (string)dataRow[3],
                    Date = (string)dataRow[4],
                    Information = (string)dataRow[5],
                    PhotoURL = (string)dataRow[6]

                };
               
            }

            return events;
        }
        
        public static Events getEvent(Events keyEvent)
        {
            string sqlQuery = "select * from events where eventId=" + keyEvent.EventId;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            if (rows.Count == 0)
            {
                return null;
            }

          
            object[] dataRow = rows[0];

            Events Event = new Events
            {
                EventId = (decimal)dataRow[0],
                UserId = (decimal)dataRow[1],
                EventName = (string)dataRow[2]
            };
            return Event;
        }

        public static Events getEvent(int keyEvent)
        {
            string sqlQuery = "select * from events where eventId=" + keyEvent;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }


            object[] dataRow = rows[0];

            Events Event = new Events
            {
                EventId = (decimal)dataRow[0],
                UserId = (decimal)dataRow[1],
                EventName = (string)dataRow[2]
            };
            return Event;
        }
        /*
        * Add an event to the database.
        * Return true if the add succeeds.
        */
        public static bool AddEvent(Events Event)
        {
            /*
           * This method use a SQL format (insert into) to insert an event
           */
            Event.EventId = Countt();
           string sql = "INSERT INTO events(eventId, userId, EventName,Category, Date, Information,PhotoURL) VALUES ("
           + Event.EventId + ", "
           + Event.UserId + ", '"
           + Event.EventName + "' ,'"
           + Event.Category + "' ,'"
           + Event.Date + "' ,'"
           + Event.Information + "' ,'"
           + Event.PhotoURL + "' );";
            RepositoryManager.Repository.DoCommand(sql);
            
            return true;
        }

        

       public static bool CheckEventname(Events keyEvent)
        {
            
            string sqlQuery = "select * from events where EventName='" + keyEvent.EventName + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            if (rows.Count != 0)
            {
                return false;

            }
            else
                return true;
        }

        /*
         * Update an event that is in the database, replacing all field values except
         * the key field.
         * Return false if the event is not found, based on key field match.
         */

        public static bool UpdateEvent(Events change)
        {
            /*
            * This method use a SQL format (update) to update the 
            * event's name with using it's eventId
            */
            string sql = "Update events Set EventName='"
                + change.EventName +"userId="+ change.UserId+"' Where eventId=" + change.EventId + ";";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }


       
        /*
         * This method deletes an event
         */

        public static bool DeleteEvent(Events delEvent)
        {
            /*
            * This method use a SQL format (delete) to delete the 
            * event with using it's eventId
            */
            string sql = "delete from events where eventId=" + delEvent.EventId;
            if (RepositoryManager.Repository.DoCommand(sql) == 1)
            {
                return true;
            }
            return false;
        }
    }
}