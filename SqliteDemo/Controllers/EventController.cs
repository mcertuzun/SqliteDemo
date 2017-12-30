using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SqliteDemo.Models.Entity;
using SqliteDemo.Models.Transaction;
using SqliteDemo.Models.Repository;

namespace SqliteDemo.Controllers
{    
        /*
        * This class provides to add, delete, update and list events
        * Also it provides making comments for them
       */
    public class EventController : Controller
    {
        /*
        * Handle a request for a listing of events.
         */
        public ActionResult ListEvents()
        {
            List<Events> events = EventPersistence.GetAllEvents();
            if (events != null)
            {
                ViewData["eventList"] = events;
                return View(events);
            }
            else
            {
                return View("Home", "Index");
            }
           
        }

        /*
		 * Handle a GET request for the Add Book form.
         */



        [HttpGet]
        public ActionResult SearchEvent()
        {
            return View(new Events());
        }


        [HttpPost]
        public ActionResult SearchEvent(Events value)
        {
            
            List<Events> events = EventPersistence.GetAllEvents();
            if (events != null)
            {
                ViewData["events"] = events;
                return View(events.Contains(value));
            }
            else
            {
                return View("Home", "Index");
            }
        }



        [HttpGet]
        public ActionResult DeleteEvent()
        {
            return View(new Events());
        }

        /*
        * Handle the POST request from the delete event form. The form parameters
        * are encapsulated in a event object.
        */
        [HttpPost]
        public ActionResult DeleteEvent(Events newEvent)
        {

            bool result = EventManager.DeleteEvent(newEvent);
            if (result)
            {
                ViewBag.message = "Event Deleted";
                Events[] events = EventManager.GetAllEvents();

                return View("ListEvents", events);
            }
            else
            {
                ViewBag.message = "That event could not be deleted";
                return View(newEvent);
            }
         
        }

        /*
        * This mothod provides to add comments on events
        * It also protected from XSS atacks
        */
     
        [HttpPost]
        public ActionResult CommentAdd(Events textin)
        {
            string t = textin.Comment.Replace("<", "&lt");
            string t1 = t.Replace(">", "&gt");
            string t2 = t1.Replace("(", "&#40");
            string t3 = t2.Replace(")", "&#41");
            string t4 = t3.Replace("&", "&#38");
            string tfinal = t4.Replace("|", "&#124");
           
            Comment com = new Comment();
            com.EventId = textin.EventId;
            com.Text = textin.Comment;
            bool result = EventPersistence.AddComment(com);


            if (result)
            {
                ViewBag.message = "Commited";
            }
            else
            {
                ViewBag.message = "Couldnt commited";
            }
            TempData["comment"] = tfinal;
            

          
            return RedirectToAction("ListEvents","Event");

        }
       
        /*
		 * Handle a GET request for the Add event form.
         */
    
        [HttpGet]
        public ActionResult AddEvent()
        {
            return View(new Events());
        }
        /*
         * Handle the POST request from the Add event form. The form parameters
         * are encapsulated in a event object.
         */
        [HttpPost]
        public ActionResult AddEvent(Events newEvent)
        {
            // Validate event data from the transaction
            if (newEvent.EventName == null || newEvent == null)
            {
                ViewBag.message = "Error: Invalid Request - please try again with choosing a name";
                return View(new Events());
            }


            newEvent.UserId =(decimal) Session["AdderID"];


            bool result = EventManager.AddNewEvent(newEvent);
            if (result)
            {
                TempData["message"]= "Event added"; 
                ViewBag.message = "Event added";
            }
            else
            {
                ViewBag.message = "That event could not be added";
            }

            Events[] events = EventManager.GetAllEvents();
            return View(newEvent);
        }


        /*
         * This method checks if the newEvent is null after that it checks
         * if the EventId.Length is 0 after that if the result is true
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

            if (newEvent.EventId.ToString().Length == 0)
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
            if (events == null)
            {
                ViewBag.message =  "There is not event for listing.";
                return View("Home","Index");
            }else
            return View("ListEvents", events);
        }
    }
}