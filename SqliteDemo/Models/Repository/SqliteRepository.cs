using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace SqliteTest.Models.Repository
{
    /*
     * This class creates and accesses an SQLite database.
     */
    public class SqliteRepository : IRepository
    {
        // Location of the database file 
        private string databaseFile = "C:\\Users\\EFE\\MyDatabase.sqlite";

        private SQLiteConnection dbConnection;

        public bool IsOpen { get { return isOpen; } }
        private bool isOpen = false;

        /*
         * When the Repository shuts down, it should close the DB if it's open.
         */
        ~SqliteRepository() {
            if (IsOpen) {
                Close();
            }
        }

        /*
         * Open the database. Return true iff the open succeeds, or it was
         * already open.
         */
        public bool Open()
        {
            if (IsOpen) {
                return true;
            }
            dbConnection =
                new SQLiteConnection("Data Source=" + databaseFile + ";Version=3;");
            if (dbConnection == null) { return false; }
            dbConnection.Open();
            isOpen = true;
            return true;
        }

        /*
         * Close the database, if it's open.
         */
        public void Close()
        {
            if (!IsOpen) {
                return;
            }
            isOpen = false;
            dbConnection.Close();
        }

        /*
         * Execute an SQL command. 
         * The return value is the number of rows affected by the command.
         */
        public int DoCommand(string sqlCommand)
        {
            if (!IsOpen) 
            {
                return -1;
            }
            SQLiteCommand command = new SQLiteCommand(sqlCommand, dbConnection);
            int result = command.ExecuteNonQuery();
            return result;
        }

        /*
         * Execute an SQL query. 
         * The return value is a List of object arrays, in which each array 
         * represents one row of data returned.
         */
        public List<object[]> DoQuery(string sqlQuery)
        {
            if (!IsOpen)
            {
                return null;
            }
            List<object[]> rows = new List<object[]>();
            SQLiteCommand command = new SQLiteCommand(sqlQuery, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                object[] row = new object[reader.FieldCount];
                reader.GetValues(row);
                rows.Add(row);
            }
            return rows;
        }

        /*
         * Recreate and reinitialize the database.
         * The return value is true iff the initialization succeeds.
         */
        public bool Initialize()
        {
            bool success = true;

            Close();

            try
            {
                SQLiteConnection.CreateFile(databaseFile);
            }
            catch (IOException e)
            {
                success = false;
            }

            bool openResult = Open();
            if (success & openResult)
            {
                string sql = "CREATE TABLE user (id DECIMAL, name VARCHAR(25),EmailAddress VARCHAR(50),salt VARCHAR(150), HashedPassword VARCHAR(150), IsAdmin Integer, Status Integer, PRIMARY KEY(id))";
                DoCommand(sql);
               

                string events = "CREATE TABLE events (eventId DECIMAL PRIMARY KEY, userId DECIMAL NOT NULL, EventName VARCHAR(25),FOREIGN KEY(userId) REFERENCES user(id))";
                DoCommand(events);
              

                string comment = "CREATE TABLE comment (CommentId Integer, EventId Integer ,Text VARCHAR(500), PRIMARY KEY(CommentId),FOREIGN KEY(EventId) REFERENCES events(eventId))";
                DoCommand(comment);

              string sqlAdmin = "INSERT INTO user(id, name, EmailAddress, salt, HashedPassword, IsAdmin, Status )VALUES("
                 + 0 + ", '"
                 + "admin" + "', '"
                 + "admin@ad.com" + "', '"
                 + "fjkljwurýwjnçzop" + "', '"
                 + "asdfasdfsdfsdfsadf" + "', "
                 + 1 + ", "
                 + 1 + ");";
                DoCommand(sqlAdmin);
            }

            return success;
        }
    }
}
