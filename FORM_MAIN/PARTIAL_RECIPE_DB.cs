﻿using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FORBES;

namespace TyrannosaurusPlex
{
    [System.ComponentModel.DesignerCategory("")] //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
    public class DUMMY_PARTIAL_RECIPEDB { } //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
    public partial class FORM_MAIN
    {
        DataTable LOCAL_TABLE_COPY = new DataTable();

        MYSQL_COMS DB = new MYSQL_COMS(Properties.DB.Default.HOST, 
                                       Properties.DB.Default.DATABASE,
                                       Properties.DB.Default.USER,
                                       Properties.DB.Default.PASSWORD); //Instance of the database connection.
        private void DB_SETUP()
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            
            DB.CONNECTION_CHANGED += new EventHandler(CONNECTION_CHANGED); //Add a form level event handler for connection state change.
            CONNECTION_CHANGED(null, new MYSQL_COMS.CONNECTION_CHANGED_EVENT_ARGS(false)); //Call the connection state change function but force a disconnected state.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void CONNECTION_CHANGED(object sender, EventArgs e) //Called when the connection state changes.
        {
            var E = (MYSQL_COMS.CONNECTION_CHANGED_EVENT_ARGS)e;
            //Controls to enable on connection.
            BTN_ADD_RECIPE.Enabled = E.FULLY_CONNECTED;
            BTN_DISCONNECT.Enabled = E.FULLY_CONNECTED;
            BTN_DELETE_RECIPE.Enabled = E.FULLY_CONNECTED;
            DGV_RECIPE_TABLE.Enabled = E.FULLY_CONNECTED;

            //Controls to disable on connection.
            BTN_CONNECT.Enabled = !E.FULLY_CONNECTED;

            if(E.FULLY_CONNECTED)
            {
                DGV_RECIPE_TABLE.DefaultCellStyle.BackColor = Color.White;
                DGV_RECIPE_TABLE.DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                DGV_RECIPE_TABLE.DefaultCellStyle.BackColor = Color.LightGray;
                DGV_RECIPE_TABLE.DefaultCellStyle.ForeColor = Color.DarkGray;
            }
        }
        private void CONNECT_TO_DB(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            int RESULT = DB.CONNECT_TO_DB();
            if (RESULT == 0)
                UPDATE_TABLE(null, null);
            else
            {
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void DISCONNECT_FROM_DB(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            DB.DISCONNECT_FROM_DB();
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void ADD_EDIT_RECIPE(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            bool ALREADY_OPEN = false; //Create a flag to indicate if the form is already open.
            FormCollection COLLECTION = Application.OpenForms; //Get a collection of all the open forms.
            foreach (Form FORM in COLLECTION) //Go through each open form...
            {
                if (FORM.Name == "FORM_ADD_RECIPE") //If the evaluated form has the same name as the form we're about to open...
                {
                    ALREADY_OPEN = true; //Indicate the form is already open.
                    FORM.BringToFront(); //Bring the form to the front.
                }
            }

            if(ALREADY_OPEN) //If the form is already open...
            {
                EVENTS.LOG_MESSAGE(2, "Form already open.");
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return; //Exit.
            }
            DataTable CHECKSHEET_DATA = DB.GET_TABLE(Properties.DB.Default.CHECKSHEET_TABLE, Properties.DB.Default.CHECKSHEET_TABLE_COLUMN_SCHEMA); //Get the checksheet data listing to pass over to the form.
            var ADD_FORM = new FORM_ADD_RECIPE(CHECKSHEET_DATA); //No matter what, make a form.
            if ((Button)sender == BTN_EDIT) //However, if the sender is the recipe edit button, tweak what we send.
            {
                ADD_FORM = new FORM_ADD_RECIPE(CHECKSHEET_DATA, GET_CURRENTLY_SELECTED_RECIPE_DATA());
            }
            ADD_FORM.Show();
            ADD_FORM.DATA_READY += new EventHandler(PROCESS_ADD_RECIPE_DATA); //Add handler for when the form has its data all set.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private RECIPE_DATA GET_CURRENTLY_SELECTED_RECIPE_DATA()
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            int SELECTED_INDEX = DGV_RECIPE_TABLE.SelectedRows[0].Index;
            RECIPE_DATA DATA = new RECIPE_DATA();
            DATA.part_number = LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["part_number"].ToString();
            DATA.checksheet_type = LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["checksheet_type"].ToString();
            DATA.csv_location = LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["csv_location"].ToString();
            DATA.key_sequence = LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["key_sequence"].ToString();
            DATA.a_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["a_col"].ToString());
            DATA.b_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["b_col"].ToString());
            DATA.c_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["c_col"].ToString());
            DATA.d_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["d_col"].ToString());
            DATA.e_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["e_col"].ToString());
            DATA.f_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["f_col"].ToString());
            DATA.g_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["g_col"].ToString());
            DATA.h_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["h_col"].ToString());
            DATA.i_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["i_col"].ToString());
            DATA.j_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["j_col"].ToString());
            DATA.k_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["k_col"].ToString());
            DATA.l_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["l_col"].ToString());
            DATA.m_col = int.Parse(LOCAL_TABLE_COPY.Rows[SELECTED_INDEX]["m_col"].ToString());
            Console.WriteLine(DATA.csv_location);
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            return DATA;
        }
        private void UPDATE_TABLE(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            LOCAL_TABLE_COPY = DB.GET_TABLE(Properties.DB.Default.RECIPE_TABLE, Properties.DB.Default.RECIPE_TABLE_COLUMN_SCHEMA);
            DGV_RECIPE_TABLE.DataSource = LOCAL_TABLE_COPY;
            DGV_RECIPE_TABLE.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DGV_RECIPE_TABLE.MultiSelect = false;
            DGV_RECIPE_TABLE.RowHeadersVisible = false;
            DGV_RECIPE_TABLE.AllowUserToAddRows = false;
            DGV_RECIPE_TABLE.AllowUserToDeleteRows = false;
            DGV_RECIPE_TABLE.AllowUserToResizeRows = false;
            DGV_RECIPE_TABLE.AllowUserToOrderColumns = false;
            DGV_RECIPE_TABLE.AllowUserToResizeColumns = false;
            DGV_RECIPE_TABLE.ReadOnly = true;
            
            try
            {
                foreach (DataGridViewColumn COLUMN in DGV_RECIPE_TABLE.Columns) //Go through each column...
                    COLUMN.Visible = false; //Make the column invisible.

                DGV_RECIPE_TABLE.Columns["part_number"].HeaderText = "Part Number";
                DGV_RECIPE_TABLE.Columns["part_number"].Visible = true; //Turn the visibility of this column back on.
                DGV_RECIPE_TABLE.Columns["part_number"].Width = 70;
                DGV_RECIPE_TABLE.Columns["part_number"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
                DGV_RECIPE_TABLE.Columns["part_number"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                DGV_RECIPE_TABLE.Columns["checksheet_type"].HeaderText = "Check Type";
                DGV_RECIPE_TABLE.Columns["checksheet_type"].Visible = true; //Turn the visibility of this column back on.
                DGV_RECIPE_TABLE.Columns["checksheet_type"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                DGV_RECIPE_TABLE.Columns["checksheet_type"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomCenter;
            }
            catch(Exception EX)
            {
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", EX.Message);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
            }
            
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void PROCESS_ADD_RECIPE_DATA(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            //Extract data out of arguments and build query string.
            var E = (RECIPE_DATA)e; //Specify the exact type of EventArgs.

            //Create the parameter array we're going to pass to the EXECUTE_COMMAND function.
            SQL_PARAMETER[] PARAMS = new SQL_PARAMETER[18];
            for (int i = 0; i < PARAMS.Length; i++) //Since this is an array of a custom type, loop through and initialize each element...
                PARAMS[i] = new SQL_PARAMETER();

            //Define where we're going to insert data.
            string INSERT_LOCATION = "INSERT INTO " + Properties.DB.Default.RECIPE_TABLE + " (" +
                                    "part_number," +
                                    "checksheet_type," +
                                    "key_sequence," +
                                    "csv_location," +
                                    "timestamp_col," +
                                    "a_col," +
                                    "b_col," +
                                    "c_col," +
                                    "d_col," +
                                    "e_col," +
                                    "f_col," +
                                    "g_col," +
                                    "h_col," +
                                    "i_col," +
                                    "j_col," +
                                    "k_col," +
                                    "l_col," +
                                    "m_col" +
                                    ") ";

            //Define the escape strings. These are the strings that will subsitute in data in the COMMAND_STRING.
            PARAMS[0].ESCAPE_STRING = "?part_number";
            PARAMS[1].ESCAPE_STRING = "?checksheet_type";
            PARAMS[2].ESCAPE_STRING = "?key_sequence";
            PARAMS[3].ESCAPE_STRING = "?csv_location";
            PARAMS[4].ESCAPE_STRING = "?timestamp_col";
            PARAMS[5].ESCAPE_STRING = "?a_col";
            PARAMS[6].ESCAPE_STRING = "?b_col";
            PARAMS[7].ESCAPE_STRING = "?c_col";
            PARAMS[8].ESCAPE_STRING = "?d_col";
            PARAMS[9].ESCAPE_STRING = "?e_col";
            PARAMS[10].ESCAPE_STRING = "?f_col";
            PARAMS[11].ESCAPE_STRING = "?g_col";
            PARAMS[12].ESCAPE_STRING = "?h_col";
            PARAMS[13].ESCAPE_STRING = "?i_col";
            PARAMS[14].ESCAPE_STRING = "?j_col";
            PARAMS[15].ESCAPE_STRING = "?k_col";
            PARAMS[16].ESCAPE_STRING = "?l_col";
            PARAMS[17].ESCAPE_STRING = "?m_col";

            //Create the second part of the command string.
            string INSERT_DATA = "VALUES(";
            for (int i = 0; i < PARAMS.Length; i++)
                INSERT_DATA += PARAMS[i].ESCAPE_STRING + ",";
            INSERT_DATA = INSERT_DATA.TrimEnd(',');
            INSERT_DATA += ")";

            //Define all the data that ties to the escape strings.
            PARAMS[0].STRING_TO_INSERT = E.part_number;
            PARAMS[1].STRING_TO_INSERT = E.checksheet_type;
            PARAMS[2].STRING_TO_INSERT = E.key_sequence;
            PARAMS[3].STRING_TO_INSERT = E.csv_location;
            PARAMS[4].STRING_TO_INSERT = E.timestamp_col.ToString();
            PARAMS[5].STRING_TO_INSERT = E.a_col.ToString();
            PARAMS[6].STRING_TO_INSERT = E.b_col.ToString();
            PARAMS[7].STRING_TO_INSERT = E.c_col.ToString();
            PARAMS[8].STRING_TO_INSERT = E.d_col.ToString();
            PARAMS[9].STRING_TO_INSERT = E.e_col.ToString();
            PARAMS[10].STRING_TO_INSERT = E.f_col.ToString();
            PARAMS[11].STRING_TO_INSERT = E.g_col.ToString();
            PARAMS[12].STRING_TO_INSERT = E.h_col.ToString();
            PARAMS[13].STRING_TO_INSERT = E.i_col.ToString();
            PARAMS[14].STRING_TO_INSERT = E.j_col.ToString();
            PARAMS[15].STRING_TO_INSERT = E.k_col.ToString();
            PARAMS[16].STRING_TO_INSERT = E.l_col.ToString();
            PARAMS[17].STRING_TO_INSERT = E.m_col.ToString();

            //Check to make sure there is no current entry that matches for part number and 
            foreach (DataRow RECORD in LOCAL_TABLE_COPY.Rows)
            {
                if (RECORD["part_number"].ToString() == E.part_number &&
                    RECORD["checksheet_type"].ToString() == E.checksheet_type) //If there is a record with the same part number and checksheet...
                {
                    string MESSAGE;
                    MESSAGE = "A recipe for the part number and checksheet type already exists.";
                    MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    EVENTS.LOG_MESSAGE(2, MESSAGE);
                    EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                    return;
                }
            }

            //Issue the command.
            string COMMAND_STRING = INSERT_LOCATION + INSERT_DATA; //Combine the strings together.
            int RESULT = DB.EXECUTE_COMMAND(COMMAND_STRING, PARAMS);

            //If a negative 1 is returned, this means failure.
            if(RESULT == -1 )
            {
                string MESSAGE;
                MESSAGE = "EXECUTE_COMMAND method failed. Record was not added. See log for details.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            UPDATE_TABLE(null,null);
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void DELETE_RECORD(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            DataGridViewRow ROW = new DataGridViewRow();
            try
            {
                ROW = DGV_RECIPE_TABLE.SelectedRows[0];
            }
            catch (Exception EX)
            {
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", EX.Message);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            DialogResult WARNING = MessageBox.Show("Are you sure you wish to delete the selected record? This action cannot be undone.", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (WARNING == DialogResult.No)
            {
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            int ROW_ID;
            bool SUCCESS = int.TryParse(ROW.Cells["id"].Value.ToString(), out ROW_ID);
            if (!SUCCESS)
            {
                EVENTS.LOG_MESSAGE(2, "Could not parse out row ID.");
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            string COMMAND_STRING = "DELETE FROM " + Properties.DB.Default.RECIPE_TABLE + " WHERE id=" + ROW_ID + ";";
            DB.EXECUTE_COMMAND(COMMAND_STRING);
            UPDATE_TABLE(null, null);

        }
    }
}
