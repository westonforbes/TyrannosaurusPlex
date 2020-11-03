using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FORBES;

//VISUAL STUDIO DESIGNER BLOCKER-------------------------------------------------------------------------------------------------------------
[System.ComponentModel.DesignerCategory("")] //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
public class DUMMY_CLASS_0 { } //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
//VISUAL STUDIO DESIGNER BLOCKER-------------------------------------------------------------------------------------------------------------

namespace TyrannosaurusPlex
{
    public partial class FORM_MAIN
    {
        //PsuedoConstructor
        private void DB_SETUP() //Called in the actual form constructor, broken off for organization.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            DB.CONNECTION_CHANGED += new EventHandler(CONNECTION_CHANGED); //Add a form level event handler for connection state change.
            DGV_RECIPE_TABLE.SelectionChanged += new EventHandler(DGV_SELECTION_CHANGED); //Checks if a recipe is selected.
            CONNECTION_CHANGED(null, new MYSQL_COMS.CONNECTION_CHANGED_EVENT_ARGS(false)); //Call the connection state change function but force a disconnected state.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        //Button Driven Methods
        private void CONNECT_TO_DB(object sender, EventArgs e) //Connect to the database.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            int RESULT = DB.CONNECT_TO_DB(); //Attempt to connect to the db using the constructor parameters from initialization.
            if (RESULT == 0) //If the connection was successful...
                UPDATE_TABLE(null, null); //Update the local table and the DGV.
            else //If the connection was not successful...
            {
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void DISCONNECT_FROM_DB(object sender, EventArgs e) //Disconnect from the database.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            DB.DISCONNECT_FROM_DB(); //Disconnect from DB. There are no exceptions or errors thrown by this call. Kind of a blind shot.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void ADD_EDIT_RECIPE(object sender, EventArgs e) //Both the "Add Recipe" button and the "Edit Recipe" button call this.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            
            //Check if form is open.
            FormCollection COLLECTION = Application.OpenForms; //Get a collection of all the open forms.
            foreach (Form FORM in COLLECTION) //Go through each open form...
            {
                if (FORM.Name == "FORM_ADD_RECIPE") //If the evaluated form has the same name as the form we're about to open...
                {
                    FORM.BringToFront(); //Bring the form to the front.
                    EVENTS.LOG_MESSAGE(2, "Form already open.");
                    EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                    return; //Exit.
                }
            }

            //Get the listings of checksheet types from the SQL DB, this will be used to populate a list box on the new form.
            DataTable CHECKSHEET_DATA = DB.GET_TABLE(Properties.DB.Default.CHECKSHEET_TABLE, Properties.DB.Default.CHECKSHEET_TABLE_COLUMN_SCHEMA); //Get the checksheet data listing to pass over to the form.
            
            //Make a blank form.
            var ADD_FORM = new FORM_ADD_RECIPE(CHECKSHEET_DATA); //No matter what, make a form.

            //Determine if the edit button is what called this method. If it is, we have to get the currently selected recipe.
            if ((Button)sender == BTN_EDIT_RECIPE) //Cast the sender into a button type and check if it is BTN_EDIT. If it is...
            {
                RECIPE_DATA CURRENT_RECORD = new RECIPE_DATA(); //Create a blank RECIPE_DATA object.
                bool SUCCESS = GET_CURRENTLY_SELECTED_RECIPE_DATA(out CURRENT_RECORD); //Populate the object and store if it was a success. Function will return true on success.
                if (SUCCESS)
                {
                    ADD_FORM = new FORM_ADD_RECIPE(CHECKSHEET_DATA, CURRENT_RECORD); //Modify the form object, reinitialize with optional CURRENT_RECORD attached.
                                                                                     //FORM_ADD_RECIPE recognizes optional record attachment as a request to edit rather than add. 
                }
                else //If getting data was unsuccessful...
                {
                    EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                    return;
                }
            }
            ADD_FORM.Show(); //Show the form.
            ADD_FORM.DATA_READY += new EventHandler(PROCESS_ADD_RECIPE_DATA); //Add handler for when the form has its data all set.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void DELETE_RECORD(object sender, EventArgs e) //Deletes the selected record.
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

        //Event Driven Methods
        private void CONNECTION_CHANGED(object sender, EventArgs e) //Called when the connection state changes.
        {
            var E = (MYSQL_COMS.CONNECTION_CHANGED_EVENT_ARGS)e;
            //Controls to enable on connection.
            BTN_ADD_RECIPE.Enabled = E.FULLY_CONNECTED;
            BTN_DISCONNECT.Enabled = E.FULLY_CONNECTED;
            BTN_DELETE_RECIPE.Enabled = E.FULLY_CONNECTED;
            DGV_RECIPE_TABLE.Enabled = E.FULLY_CONNECTED;
            BTN_EDIT_RECIPE.Enabled = E.FULLY_CONNECTED;

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
        private void PROCESS_ADD_RECIPE_DATA(object sender, EventArgs e) //Called once the FORM_ADD_RECIPE is ready to submit.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            //Extract data out of arguments and build query string.
            var DATA = (RECIPE_DATA)e; //Specify the exact type of EventArgs.

            //Create an array of SQL parameters. Since its a custom type we have to recurse through it to fully initialize.
            var PARAMS = new SQL_PARAMETER[18];
            for (int i = 0; i < PARAMS.Length; i++) //Since this is an array of a custom type, loop through and initialize each element...
                PARAMS[i] = new SQL_PARAMETER();
            GENERATE_PARAMETERS(DATA, out PARAMS);

            //Build the command string.
            string COMMAND_STRING = CREATE_ADD_RECORD_STRING(DATA, PARAMS);

            //Check to make sure there is no current entry that matches for part number and checksheet type.
            foreach (DataRow RECORD in RECIPE_TABLE_LOCAL.Rows)
            {
                if (RECORD["part_number"].ToString() == DATA.part_number &&
                    RECORD["checksheet_type"].ToString() == DATA.checksheet_type) //If there is a record with the same part number and checksheet...
                {
                    string MESSAGE;
                    MESSAGE = "A recipe for the part number and checksheet type already exists. Would you like to overwrite this recipe?";
                    DialogResult CHOICE = MessageBox.Show(MESSAGE, "Warning", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    EVENTS.LOG_MESSAGE(2, MESSAGE);
                    if (CHOICE == DialogResult.No)
                    {
                        EVENTS.LOG_MESSAGE(2, "User selected NO.");
                        OK_TO_CLOSE_ADD_RECIPE_FORM?.Invoke(null, null);
                        EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                        return;
                    }
                    if (CHOICE == DialogResult.Cancel)
                    {
                        EVENTS.LOG_MESSAGE(2, "User selected CANCEL.");
                        EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                        return;
                    }
                    EVENTS.LOG_MESSAGE(2, "User selected YES.");
                    COMMAND_STRING = CREATE_MODIFY_RECORD_STRING();
                }
            }

            OK_TO_CLOSE_ADD_RECIPE_FORM?.Invoke(null, null); //notify the form that its ok to close, we're past the point where a user can cancel.

            //Issue the command.
            int RESULT = DB.EXECUTE_COMMAND(COMMAND_STRING, PARAMS);

            //If a negative 1 is returned, this means failure.
            if (RESULT == -1)
            {
                string MESSAGE;
                MESSAGE = "EXECUTE_COMMAND method failed. Record was not added. See log for details.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            UPDATE_TABLE(null, null);
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void DGV_SELECTION_CHANGED(object sender, EventArgs e) //Only enable the edit button once a selection is made.
        {
            if (DGV_RECIPE_TABLE.SelectedRows.Count > 0)
            {
                if (DB.FULLY_CONNECTED)
                    BTN_EDIT_RECIPE.Enabled = true;
            }
            else
                BTN_EDIT_RECIPE.Enabled = false;
        }

        //Helper Methods
        private bool GET_CURRENTLY_SELECTED_RECIPE_DATA(out RECIPE_DATA DATA) //Gets the record currently selected in the DGV.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            DATA = new RECIPE_DATA();
            try
            {
                int SELECTED_INDEX = DGV_RECIPE_TABLE.SelectedRows[0].Index;
                DATA.part_number = RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["part_number"].ToString();
                DATA.checksheet_type = RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["checksheet_type"].ToString();
                DATA.csv_location = RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["csv_location"].ToString();
                DATA.key_sequence = RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["key_sequence"].ToString();
                DATA.a_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["a_col"].ToString());
                DATA.b_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["b_col"].ToString());
                DATA.c_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["c_col"].ToString());
                DATA.d_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["d_col"].ToString());
                DATA.e_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["e_col"].ToString());
                DATA.f_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["f_col"].ToString());
                DATA.g_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["g_col"].ToString());
                DATA.h_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["h_col"].ToString());
                DATA.i_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["i_col"].ToString());
                DATA.j_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["j_col"].ToString());
                DATA.k_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["k_col"].ToString());
                DATA.l_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["l_col"].ToString());
                DATA.m_col = int.Parse(RECIPE_TABLE_LOCAL.Rows[SELECTED_INDEX]["m_col"].ToString());
                EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
                return true;
            }
            catch(Exception EX)
            {
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", EX.Message);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return false;
            }
            

        }
        private void UPDATE_TABLE(object sender, EventArgs e) //Updates local table and view.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            RECIPE_TABLE_LOCAL = DB.GET_TABLE(Properties.DB.Default.RECIPE_TABLE, Properties.DB.Default.RECIPE_TABLE_COLUMN_SCHEMA);
            DGV_RECIPE_TABLE.DataSource = RECIPE_TABLE_LOCAL;
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

        
        //Simple string/structure generators, moved into own functions for readability.
        private string CREATE_ADD_RECORD_STRING(RECIPE_DATA DATA, SQL_PARAMETER[] PARAMS) //Creates the command string for adding a new record.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");

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

            //Create the second part of the command string.
            string INSERT_DATA = "VALUES(";
            for (int i = 0; i < PARAMS.Length; i++)
                INSERT_DATA += PARAMS[i].ESCAPE_STRING + ",";
            INSERT_DATA = INSERT_DATA.TrimEnd(',');
            INSERT_DATA += ")";

            //Construct the command.
            string COMMAND_STRING = INSERT_LOCATION + INSERT_DATA; //Combine the strings together.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            return COMMAND_STRING;
        }
        private string CREATE_MODIFY_RECORD_STRING() //Creates the command string for modifying a record.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            string PART1 = "UPDATE " + Properties.DB.Default.RECIPE_TABLE;
            string PART2 = " SET " +
                           "part_number = ?part_number, " +
                           "checksheet_type = ?checksheet_type, " +
                           "key_sequence = ?key_sequence, " +
                           "csv_location = ?csv_location, " +
                           "timestamp_col = ?timestamp_col, " +
                           "a_col = ?a_col, " +
                           "b_col = ?b_col, " +
                           "c_col = ?c_col, " +
                           "d_col = ?d_col, " +
                           "e_col = ?e_col, " +
                           "f_col = ?f_col, " +
                           "g_col = ?g_col, " +
                           "h_col = ?h_col, " +
                           "i_col = ?i_col, " +
                           "j_col = ?j_col, " +
                           "k_col = ?k_col, " +
                           "l_col = ?l_col, " +
                           "m_col = ?m_col";
            string PART3 = " WHERE part_number = ?part_number AND checksheet_type = ?checksheet_type;";
            string COMMAND_STRING = PART1 + PART2 + PART3;
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            return COMMAND_STRING;
        }
        private void GENERATE_PARAMETERS(RECIPE_DATA DATA, out SQL_PARAMETER[] PARAMS) //Populates the parameters structure used in SQL commands.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            //Create the parameter array we're going to pass to the EXECUTE_COMMAND function.
            PARAMS = new SQL_PARAMETER[18];
            for (int i = 0; i < PARAMS.Length; i++) //Since this is an array of a custom type, loop through and initialize each element...
                PARAMS[i] = new SQL_PARAMETER();

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

            //Define all the data that ties to the escape strings.
            PARAMS[0].STRING_TO_INSERT = DATA.part_number;
            PARAMS[1].STRING_TO_INSERT = DATA.checksheet_type;
            PARAMS[2].STRING_TO_INSERT = DATA.key_sequence;
            PARAMS[3].STRING_TO_INSERT = DATA.csv_location;
            PARAMS[4].STRING_TO_INSERT = DATA.timestamp_col.ToString();
            PARAMS[5].STRING_TO_INSERT = DATA.a_col.ToString();
            PARAMS[6].STRING_TO_INSERT = DATA.b_col.ToString();
            PARAMS[7].STRING_TO_INSERT = DATA.c_col.ToString();
            PARAMS[8].STRING_TO_INSERT = DATA.d_col.ToString();
            PARAMS[9].STRING_TO_INSERT = DATA.e_col.ToString();
            PARAMS[10].STRING_TO_INSERT = DATA.f_col.ToString();
            PARAMS[11].STRING_TO_INSERT = DATA.g_col.ToString();
            PARAMS[12].STRING_TO_INSERT = DATA.h_col.ToString();
            PARAMS[13].STRING_TO_INSERT = DATA.i_col.ToString();
            PARAMS[14].STRING_TO_INSERT = DATA.j_col.ToString();
            PARAMS[15].STRING_TO_INSERT = DATA.k_col.ToString();
            PARAMS[16].STRING_TO_INSERT = DATA.l_col.ToString();
            PARAMS[17].STRING_TO_INSERT = DATA.m_col.ToString();
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");

        }
    }
}

