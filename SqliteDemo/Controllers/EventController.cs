using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;

namespace SqliteDemo.Controllers
{
    public class EventController : Controller
    {

        public ActionResult ListEvents()
        {
            Events[] event1 = EventManager.GetAllEvents();
            return View(event1);
        }

        /*
		 * Handle a GET request for the Add Book form.
         */
        [HttpGet]
        public ActionResult AddEvent()
        {
            return View(new Events());
        }
        [HttpGet]
        public ActionResult DeleteEvent()
        {

            Events event1 = new Events();
            return View(event1);
        }

        [HttpPost]
        public ActionResult DeleteEvent(Events newEvent)
        {

            bool result = EventManager.DeleteEvent(newEvent);
            if (result)
            {
                ViewBag.message = "Event Deleted";
            }
            else
            {
                ViewBag.message = "That event could not be deleted";
            }
            Events[] events = EventManager.GetAllEvents();
            return View("ListEvents", events);
        }
        /*
         * Handle the POST request from the Add Book form. The form parameters
         * are encapsulated in a Book object.
         */
        [HttpPost]
        public ActionResult AddEvent(Events newEvent)
        {
            // Validate book data from the transaction
            if (newEvent.EventName == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again with choosing a name";
                return View(new Events());
            }
            if (newEvent == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again";
                return View(new Events());
            }
            if (newEvent.eventId.ToString().Length == 0)
            {
                ViewBag.message = "Error: An Id is required";
                return View(newEvent);
            }
            if (newEvent.userId.ToString().Length == 0)
            {
                ViewBag.message = "Error: An User Id is required";
                return View(newEvent);
            }

            // Add the book
            bool result = EventManager.AddNewEvent(newEvent);
            if (result)
            {
                ViewBag.message = "Event added";
            }
            else
            {
                ViewBag.message = "That event could not be added";
            }

            Events[] events = EventManager.GetAllEvents();
            return View("ListEvents", events);
        }


        /*
         * This method checks if the newbook is null after that it checks
         * if the ISBN number is 0 after that if the result is true
         * it gives the message of success.
         */
        [HttpGet]
        public ActionResult UpdateEvent()
        {

            return View(new Events());
        }
        [HttpPost]
        public ActionResult UpdateEvent(Events newEvent)
        {

            if (newEvent == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again";
                return View(new Events());
            }

            if (newEvent.eventId.ToString().Length == 0)
            {
                ViewBag.message = "Error: An Id is required";
                return View(newEvent);
            }


            bool result = EventManager.ChangeEvent(newEvent);
            if (result)
            {
                ViewBag.message = "Event Updated";
            }
            else
            {
                ViewBag.message = "That event could not be Updated";
            }
            Events[] events = EventManager.GetAllEvents();
            return View("ListEvents", events);
        }
    }
}