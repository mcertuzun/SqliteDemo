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


            bool eventChecker = EventPersistence.CheckEventname(newEvent);

            Events oldEvent = EventPersistence.getEvent(newEvent);
          
            if (eventChecker == true)
            {
                return EventPersistence.AddEvent(newEvent);
            }
  
            return false;
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
            return EventPersistence.UpdateEvent(changeEvent);
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