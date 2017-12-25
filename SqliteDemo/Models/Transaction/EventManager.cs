using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Repository;

namespace SqliteDemo.Models.Transaction
{
    public class EventManager
    {
        public static bool AddNewEvent(Events newEvent)
        {
            // Verify that the book doesn't already exist
            Events oldEvent = EventPersistence.getEvent(newEvent);
            // oldBook should be null, if this is a new book
            if (oldEvent != null)
            {
                return false;
            }
            //int count;
            //newEvent.eventId = 0;
            //newEvent.eventId = newEvent.eventId + 1;
            // set tomorrow as the official date added

            return EventPersistence.AddEvent(newEvent);
        }



        /*
         * Transaction: Delete a book from the database
         * Returns true iff the book exists in the database and
         * it was successfully deleted.
         */
        public static bool DeleteEvent(Events delEvent)
        {
            if (EventPersistence.getEvent(delEvent) == null)
            {
                return false;
            }
            return EventPersistence.DeleteEvent(delEvent);
        }


        /*
         * Transaction: Update a book in the database
         * Returns true iff the book exists in the database and
         * it was successfully changed.
         */
        public static bool ChangeEvent(Events changeEvent)
        {
            if (EventPersistence.getEvent(changeEvent) == null)
            {
                return false;
            }
            return EventPersistence.UpdateEventName(changeEvent);
        }

        public static Events[] GetAllEvents()
        {
            List<Events> events = EventPersistence.GetAllEvents();
            if (events != null)
            {
                return EventPersistence.GetAllEvents().ToArray();
            }
            else
            {
                return (new Events[0]);
            }
        }
    }
}