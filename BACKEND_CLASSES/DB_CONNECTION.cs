﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Data;

namespace TyrannosaurusPlex
{
    public class DB_CONNECTION
    {
        
        //Objects
        private readonly string HOST;
        private readonly string DATABASE;
        private readonly string USER;
        private readonly string PASSWORD;
        MySqlConnection CONNECTION = new MySqlConnection();
        readonly LOGGER EVENTS = new LOGGER("Database Connection Log");

        //Properties
        public bool FULLY_CONNECTED { get; private set; }

        //Events
        public event EventHandler CONNECTION_CHANGED; //Create a EventHandler Delegate that we can invoke.
        public class CONNECTION_CHANGED_EVENT_ARGS : EventArgs
        {
            public CONNECTION_CHANGED_EVENT_ARGS(bool VALUE)
            {
                this.FULLY_CONNECTED = VALUE;
            }
            public bool FULLY_CONNECTED { get; private set; }
        } //Create a new EventArgument structure.

        //Constructor
        public DB_CONNECTION(string HOST, string DATABASE, string USER, string PASSWORD)
        {
            EVENTS.LOG_MESSAGE(1, "INITIALIZE");
            this.HOST = HOST;
            this.DATABASE = DATABASE;
            this.USER = USER;
            this.PASSWORD = PASSWORD;
            CONNECTION.StateChange += new StateChangeEventHandler(CONNECTION_STATE_CHANGED); //Setup an internal handler.
        }

        //Methods
        public int CONNECT_TO_DB()
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            try
            {
                string CONNECTION_STRING = "server=" + HOST + ";Database=" + DATABASE + ";User ID=" + USER + ";Password=" + PASSWORD;
                EVENTS.LOG_MESSAGE(3, string.Format("Connection string constructed: {0}", CONNECTION_STRING));
                CONNECTION.ConnectionString = CONNECTION_STRING;
                CONNECTION.Open(); 
            }
            catch (Exception EX)
            {
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", EX.Message);
                if (EX.InnerException is MySqlException)
                {
                    EVENTS.LOG_MESSAGE(2, "A connection-level error occurred while opening the connection.");
                    EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                    return 1;
                }
                else if (EX.InnerException is InvalidOperationException)
                {
                    EVENTS.LOG_MESSAGE(2, "Cannot open a connection without specifying a data source or server.");
                    EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                    return 2;
                }
                else
                {
                    EVENTS.LOG_MESSAGE(2, "Unhandled exception type.");
                    EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                    return 3;
                }
                
            }
            EVENTS.LOG_MESSAGE(3, "Connection success.");
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            return 0;
        }
        public void DISCONNECT_FROM_DB()
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            EVENTS.LOG_MESSAGE(3, "Closing connection.");
            CONNECTION.Close(); //No exceptions are generated by these method calls.
            CONNECTION.Dispose(); //So nothing advanced needs to happen here.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void CONNECTION_STATE_CHANGED(object SENDER, EventArgs E)
        {
            System.Data.ConnectionState STATE = CONNECTION.State; //Get the current connection status.
            EVENTS.LOG_MESSAGE(1, string.Format("Connection state change detected. Current state : {0}", STATE.ToString())); //Write to log.
            //Set a simple property FULLY_CONNECTED so that other classes can easily check.
            if (STATE == System.Data.ConnectionState.Open)
                FULLY_CONNECTED = true;
            else
                FULLY_CONNECTED = false;
            var ARGS = new CONNECTION_CHANGED_EVENT_ARGS(FULLY_CONNECTED); //Insert FULLY_CONNECTED state into the arguments that the delegate will carry out.
            CONNECTION_CHANGED?.Invoke(SENDER, ARGS); //Send out the delegate to notify other classes.
        }
        public DataTable GET_DATA(string TABLE, string COLUMN_STRING)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            DataTable DATA_TABLE = new DataTable(); //Create an empty DataTable to return.
            try
            {
                //Setup the query.
                MySqlDataReader READER; //Create a reader object.
                string QUERY = string.Format("SELECT * FROM {0}", TABLE); //Construct the query string.
                READER = EXECUTE_READER(QUERY); //Execute the query.

                //Setup the DataTable that'll hold results.
                string[] COLUMNS = COLUMN_STRING.Split(','); //Figure out the column listing from passed string.
                foreach (string ELEMENT in COLUMNS) //For each column in the listing... 
                    DATA_TABLE.Columns.Add(ELEMENT); //Sdd a column in the DataTable.

                //Fill the DataTable with results from the query.
                if (READER.HasRows) //If data was returned...
                {
                    while (READER.Read())//While there is data in the buffer...
                    {
                        DataRow ROW = DATA_TABLE.NewRow();
                        foreach (string ELEMENT in COLUMNS) //Read each element from the row and add it to the string.
                            ROW[ELEMENT] = READER[ELEMENT];
                        DATA_TABLE.Rows.Add(ROW);
                    }
                    READER.Close();
                }
                else //If no data was returned...
                {
                    EVENTS.LOG_MESSAGE(2, "No data returned from query.");
                    EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                    READER.Close();
                    return new DataTable();
                }

            }
            catch (Exception EX)
            {
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", EX.Message);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return new DataTable();
            }
   
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            return DATA_TABLE;
        }
        public MySqlDataReader EXECUTE_READER(string QUERY)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            try
            {
                MySqlDataReader READER;
                MySqlCommand COMMAND = new MySqlCommand(QUERY, CONNECTION);
                READER = COMMAND.ExecuteReader();
                EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
                return READER;
            }
            catch (Exception EX)
            {
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", EX.Message);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
            }
            return null;
        }
        public int EXECUTE_COMMAND(string COMMAND_STRING)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            try
            {
                int NUMBER_OF_ROWS_AFFECTED;
                MySqlTransaction TRANSACTION = CONNECTION.BeginTransaction();
                MySqlCommand COMMAND = CONNECTION.CreateCommand();
                COMMAND.CommandText = COMMAND_STRING;
                NUMBER_OF_ROWS_AFFECTED = COMMAND.ExecuteNonQuery();
                TRANSACTION.Commit();
                EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
                return NUMBER_OF_ROWS_AFFECTED;
            }
            catch (Exception EX)
            {
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", EX.Message);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
            }
            return -1;
        }
    }
}