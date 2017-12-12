using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SqliteDemo.Models.Entity
{
    /*
     * This class represents one Book.
     */
    public class Book
    {
        public string Title { get; set; }
        public decimal ISBN { get; set; }
        public DateTime DateAdded { get; set; }

        /*
         * Default constructor - no initialization.
         */
        public Book()
        {
            DateAdded = new DateTime();
        }

        /*
         * Parameterized constructor
         */
        public Book(String title, int isbn, DateTime dateAdded)
        {
            Title = title;
            ISBN = isbn;
            DateAdded = dateAdded;
        }
    }
}