using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteDemo.Models.Repository
{
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
            return (decimal)rows.Count;
        }
        public static Events GetEvent(int eventId)
        {
            foreach (Events Event in events)
            {
                if (eventId == Event.eventId)
                {
                    return Event;
                }
            }
            return null;
        }

        public static List<Events> GetAllEvents()
        {
            List<Events> events = new List<Events>();

            string sqlQuery = "select * from events";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            foreach (object[] dataRow in rows)
            {

                Events evnt = new Events
                {
                    eventId = (decimal)dataRow[0],
                    userId = (decimal)dataRow[1],
                    EventName = (string)dataRow[2]

                };
                events.Add(evnt);
            }

            return events;
        }

        public static bool AddEvent(Events Event)
        {

            Event.eventId = Countt();
            string sql = "INSERT INTO events(eventId, userId, EventName) VALUES ("
           + Event.eventId + ", "
           + Event.userId + ", '"
           + Event.EventName + "' );";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }

        /*
         * Update a book that is in the database, replacing all field values except
         * the key field.
         * Return false if the book is not found, based on key field match.
         */

       /* public static bool CheckEventname(User keyEvent)
        {
            string sqlQuery = "select * from events where name='" + keyEvent. + "'";
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);

            if (rows.Count != 0)
            {
                return false;

            }
            else
                return true;
        }*/

        public static bool UpdateEventName(Events changeName)
        {
            /*
            * This method use a SQL format (update) to update the 
            * book's title with using it's ISBN number
            */
            string sql = "Update events Set EventName='"
                + changeName.EventName + "' Where eventId=" + changeName.eventId + ";";
            RepositoryManager.Repository.DoCommand(sql);
            return true;
        }


        public static Events getEvent(Events keyEvent)
        {
            string sqlQuery = "select * from events where eventId=" + keyEvent.eventId;
            List<object[]> rows = RepositoryManager.Repository.DoQuery(sqlQuery);
            //System.Console.WriteLine("$$rows: " + rows.Count);
            if (rows.Count == 0)
            {
                return null;
            }

            // Use the data from the first returned row (should be the only one) to create a Book.
            object[] dataRow = rows[0];

            Events Event = new Events { eventId = (decimal)dataRow[0], userId = (decimal)dataRow[1], EventName = (string)dataRow[2] };
            return Event;
        }
        /*
         * Get one user from the repository, identified by userId 
         */

        public static bool DeleteEvent(Events delEvent)
        {
            string sql = "delete from events where eventId=" + delEvent.eventId;
            if (RepositoryManager.Repository.DoCommand(sql) == 1)
            {
                return true;
            }
            return false;
        }
    }
}