using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FORBES.LOGGER_NAMESPACE;
using FORBES.KEY_LOGGER_NAMESPACE;
using FORBES.TABLE_PROCESSOR_NAMESPACE;

namespace TyrannosaurusPlex
{
    public partial class FORM_ADD_RECIPE : Form
    {
        //Events, Delegates, Handlers
        public event EventHandler DATA_READY; //Create a EventHandler Delegate that we can invoke.
        readonly List<string> SEQUENCE_LIST = new List<string> { }; //Key data in list.
        readonly DataTable SEQUENCE_DATATABLE = new DataTable(); //Key data DataTable.
        private bool RECORD_SEQUENCE_ACTIVE = false;
        private bool REPLAY_SEQUENCE_ACTIVE = false;
        DataTable INJECTION_TABLE = new DataTable();

        //Objects
        readonly private LOGGER EVENTS = new LOGGER("Add Recipe Form Log");
        
        //Constructor
        public FORM_ADD_RECIPE(DataTable CHECKSHEET_TYPE_DATA_TABLE, RECIPE_DATA CURRENT_RECIPE_DATA = null) //Logged and documented.
        {
            InitializeComponent();
            EVENTS.LOG_MESSAGE(1, "INITIALIZE");

            SEQUENCE_DATATABLE.Columns.Add("Keys");
            //For each row in the datatable passed on initialization, add the data to the listbox.
            foreach (DataRow ROW in CHECKSHEET_TYPE_DATA_TABLE.Rows)
                LISTBOX_CHECKSHEET_TYPE.Items.Add(ROW.ItemArray[0].ToString());
            EVENTS.LOG_MESSAGE(3, "Checksheet types loaded.");

            //Format all the color assigners.
            GRP_BOX_COLUMN_ASSIGNERS.Enabled = false;
            EVENTS.LOG_MESSAGE(3, "Assigner controls disabled.");

            BTN_A.BackColor = ColorTranslator.FromHtml("#CC99C9");
            BTN_B.BackColor = ColorTranslator.FromHtml("#9EC1CF");
            BTN_C.BackColor = ColorTranslator.FromHtml("#9EE09E");
            BTN_D.BackColor = ColorTranslator.FromHtml("#FDFD97");
            BTN_E.BackColor = ColorTranslator.FromHtml("#FEB144");
            BTN_F.BackColor = ColorTranslator.FromHtml("#FF6663");
            BTN_G.BackColor = ColorTranslator.FromHtml("#CC99C9");

            BTN_H.BackColor = ColorTranslator.FromHtml("#9EC1CF");
            BTN_I.BackColor = ColorTranslator.FromHtml("#9EE09E");
            BTN_J.BackColor = ColorTranslator.FromHtml("#FDFD97");
            BTN_K.BackColor = ColorTranslator.FromHtml("#FEB144");
            BTN_L.BackColor = ColorTranslator.FromHtml("#FF6663");
            BTN_M.BackColor = ColorTranslator.FromHtml("#CC99C9");
            BTN_TIMESTAMP.BackColor = Color.Pink;
            EVENTS.LOG_MESSAGE(3, "Assigner colors set.");

            //If this form is being created as a edit recipe, rather than a new recipe...
            if (CURRENT_RECIPE_DATA != null)
            {
                EVENTS.LOG_MESSAGE(3, "Form call is an edit request.");
                LOAD_IN_EDIT_DATA(CURRENT_RECIPE_DATA);
                TXT_BOX_PART_NUM.Enabled = false;
                LISTBOX_CHECKSHEET_TYPE.Enabled = false;
            }
            else
                EVENTS.LOG_MESSAGE(3, "Form call is a new request.");
            KEY_LOGGER_SETUP();
            FORM_MAIN.OK_TO_CLOSE_ADD_RECIPE_FORM += new EventHandler(CLOSE);
            EVENTS.LOG_MESSAGE(3, "Close EventHandler installed.");
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        //Methods

        /// <summary>
        /// This method is called when the form loads and it is determined to be a "edit recipe" call rather than a "new recipe call".
        /// The method loads in passed data into all the form objects.
        /// </summary>
        /// <param name="CURRENT_RECIPE_DATA">Recipe data to load into the form fields.</param>
        private void LOAD_IN_EDIT_DATA(RECIPE_DATA CURRENT_RECIPE_DATA) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            TXT_BOX_PART_NUM.Text = CURRENT_RECIPE_DATA.part_number; //Load in part number.
            EVENTS.LOG_MESSAGE(3, "Part number loaded.");
            LISTBOX_CHECKSHEET_TYPE.SelectedItem = CURRENT_RECIPE_DATA.checksheet_type; //Load in checksheet type.
            EVENTS.LOG_MESSAGE(3, "Checksheet type loaded.");
            string MODIFIED_PATH = CURRENT_RECIPE_DATA.csv_location; //Load in CSV file location.
            MODIFIED_PATH.Replace(@"\", @"\\"); //The SQL db was squirrelly with storing slashes, it seems to be fine
            //now and not entirely needed but I don't think its hurting. I'm gonna keep it.
            TXT_BOX_KEYENCE.Text = MODIFIED_PATH;  //Assign the value to the textbox.
            EVENTS.LOG_MESSAGE(3, "Filepath loaded.");
            //Load CSV file into viewer.
            LOAD_CSV_DATA(null, null);

            DataTable TABLE = new DataTable();
            bool SUCCESS = BACKEND.EXTRACT_DATA_TABLE_FROM_DGV(ref DGV1, ref TABLE);
            if(SUCCESS)
            {
                foreach(DataColumn COLUMN in TABLE.Columns)
                {
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.a_col)
                        COLUMN.ColumnName = "A";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.b_col)
                        COLUMN.ColumnName = "B";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.c_col)
                        COLUMN.ColumnName = "C";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.d_col)
                        COLUMN.ColumnName = "D";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.e_col)
                        COLUMN.ColumnName = "E";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.f_col)
                        COLUMN.ColumnName = "F";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.g_col)
                        COLUMN.ColumnName = "G";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.h_col)
                        COLUMN.ColumnName = "H";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.i_col)
                        COLUMN.ColumnName = "I";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.j_col)
                        COLUMN.ColumnName = "J";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.k_col)
                        COLUMN.ColumnName = "K";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.l_col)
                        COLUMN.ColumnName = "L";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.m_col)
                        COLUMN.ColumnName = "M";
                    if (COLUMN.Ordinal == CURRENT_RECIPE_DATA.timestamp_col)
                        COLUMN.ColumnName = "TimeStamp";
                }
            }
            EVENTS.LOG_MESSAGE(3, "Column names assigned.");
            DGV1.DataSource = TABLE;
            EVENTS.LOG_MESSAGE(3, "Table bound to DGV.");
            COLOR_DGV_COLUMNS();
            LOAD_IN_KEY_SEQUENCE(CURRENT_RECIPE_DATA.key_sequence);
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        private void LOAD_IN_KEY_SEQUENCE(string KEY_SEQ)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            string[] ARRAY = KEY_SEQ.Split(',');
            SEQUENCE_LIST.Clear(); //Be sure the list is clear.
            SEQUENCE_DATATABLE.Clear(); //Be sure the list is clear.
            foreach (string KEY in ARRAY)
            {
                if (KEY != "" && KEY != null)
                {
                    SEQUENCE_LIST.Add(KEY);
                    SEQUENCE_DATATABLE.Rows.Add(KEY);
                }
            }
            dataGridView2.DataSource = SEQUENCE_DATATABLE;
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        
        /// <summary>
        /// This method handles checking that all data is good for saving. It is called when the save button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_SAVE_CLICK(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");

            //Check the part number is good----------------------------------------------------------------------
            string TEXT = TXT_BOX_PART_NUM.Text; //Get the canidate string from the textbox.
            int RESULT = BACKEND.VALIDATE_PART_NUMBER(ref TEXT); //Check the string validity.
            TXT_BOX_PART_NUM.Text = TEXT; //Reassign the string to the textbox so its capitalized.
            if (RESULT != 0) //If VALIDATE_PART_NUMBER did not execute successfully...
            {
                string MESSAGE = null;
                switch (RESULT) //Determine exactly what happened and throw the appropriate message.
                {
                    case (1):
                        MESSAGE = "The part number is not valid. It must be six (6) characters long.";
                        MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case (2):
                        MESSAGE = "The part number is not valid. The first two characters need to be letters.";
                        MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case (3):
                        MESSAGE = "The part number is not valid. The last four characters need to be digits.";
                        MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return; //Cancel all other operations.
            }
            EVENTS.LOG_MESSAGE(3, "Part number is good.");

            //Check that a checksheet type is selected-----------------------------------------------------------
            string CHECKSHEET_TYPE = "";
            try
            {
                CHECKSHEET_TYPE = LISTBOX_CHECKSHEET_TYPE.SelectedItem.ToString();
            }
            catch (NullReferenceException)
            {
                string MESSAGE = "Please select a checksheet type.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return; //Cancel all other operations.
            }
            EVENTS.LOG_MESSAGE(3, "Sheet type is good.");

            //Check if the keyence csv path is valid-------------------------------------------------------------
            bool IS_CSV_VALID = BACKEND.VALIDATE_FILE(TXT_BOX_KEYENCE.Text);
            if (!IS_CSV_VALID)
                return;
            EVENTS.LOG_MESSAGE(3, "Path is valid.");

            //Check that data has been previewed in the viewer---------------------------------------------------
            //This bit should no longer be relevant because I changed the code to automatically load whatever
            //the filepath is, but it can't hurt to keep it in case I did not think of something.
            if (DGV1.Rows.Count <= 0)
            {
                string MESSAGE = "The Keyence CSV file has not been opened and assignments have not been made.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return; //Cancel all other operations.
            }

            //Get the DGV table for the next couple steps--------------------------------------------------------
            DataTable TABLE = new DataTable();
            bool SUCCESS = BACKEND.EXTRACT_DATA_TABLE_FROM_DGV(ref DGV1, ref TABLE);
            EVENTS.LOG_MESSAGE(3, "Retrieved DataTable.");

            //Check that a timestamp column is selected----------------------------------------------------------
            int TIMESTAMP_INDEX = -1;
            foreach (DataColumn COLUMN in TABLE.Columns)
                if (COLUMN.ColumnName == "TimeStamp")
                    TIMESTAMP_INDEX = COLUMN.Ordinal;
            if (TIMESTAMP_INDEX == -1)
            {
                string MESSAGE = "A timestamp column was not designated, this is required.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return; //Cancel all other operations.
            }

            //Generate a string of all the keystrokes------------------------------------------------------------
            string KEY_SEQUENCE = "";
            foreach (string KEY in SEQUENCE_LIST)
                KEY_SEQUENCE += KEY + ",";
            EVENTS.LOG_MESSAGE(3, "Key sequence generated.");

            //Commit the data to the returning package-----------------------------------------------------------
            RECIPE_DATA DATA = new RECIPE_DATA();
            DATA.part_number = TXT_BOX_PART_NUM.Text;
            DATA.checksheet_type = CHECKSHEET_TYPE;
            DATA.key_sequence = KEY_SEQUENCE;
            DATA.csv_location = TXT_BOX_KEYENCE.Text;
            DATA.timestamp_col = TIMESTAMP_INDEX;
            DATA.a_col = -1;
            DATA.b_col = -1;
            DATA.c_col = -1;
            DATA.d_col = -1;
            DATA.e_col = -1;
            DATA.f_col = -1;
            DATA.g_col = -1;
            DATA.h_col = -1;
            DATA.i_col = -1;
            DATA.j_col = -1;
            DATA.k_col = -1;
            DATA.l_col = -1;
            DATA.m_col = -1;
            if (SUCCESS)
            {
                foreach (DataColumn COLUMN in TABLE.Columns)
                {
                        switch (COLUMN.ColumnName)
                        {
                            case ("A"):
                                DATA.a_col = COLUMN.Ordinal;
                                break;
                            case ("B"):
                                DATA.b_col = COLUMN.Ordinal;
                                break;
                            case ("C"):
                                DATA.c_col = COLUMN.Ordinal;
                                break;
                            case ("D"):
                                DATA.d_col = COLUMN.Ordinal;
                                break;
                            case ("E"):
                                DATA.e_col = COLUMN.Ordinal;
                                break;
                            case ("F"):
                                DATA.f_col = COLUMN.Ordinal;
                                break;
                            case ("G"):
                                DATA.g_col = COLUMN.Ordinal;
                                break;
                            case ("H"):
                                DATA.h_col = COLUMN.Ordinal;
                                break;
                            case ("I"):
                                DATA.i_col = COLUMN.Ordinal;
                                break;
                            case ("J"):
                                DATA.j_col = COLUMN.Ordinal;
                                break;
                            case ("K"):
                                DATA.k_col = COLUMN.Ordinal;
                                break;
                            case ("L"):
                                DATA.l_col = COLUMN.Ordinal;
                                break;
                            case ("M"):
                                DATA.m_col = COLUMN.Ordinal;
                                break;

                    }
                }
                EVENTS.LOG_MESSAGE(3, "Columns assigned.");
            }
            else
            {
                string MESSAGE = "Failed to extract DGV Data";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
            }

            //Invoke delegate with info pack and close-----------------------------------------------------------
            DATA_READY?.Invoke(null, DATA);
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        
        /// <summary>
        /// This method closes the form. Its called through a cancel button click and through a convoluted route its called by
        /// the save button. The save button actually notifies the main form which processes the request by checking the DB for
        /// conflicts, if all is good the main form issues a delegate event which this method handles.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CLOSE(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            //Not much to do here.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            this.Close();
        } 

        /// <summary>
        /// This method is called when the user clicks on the browse button for a keyence file. It makes sure everything is valid as well
        /// (through calls to backend methods).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_BROWSE_KEYENCE_CLICK(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            OpenFileDialog FILE_PATH_BROWSER = new OpenFileDialog();
            //Set properties of the open file dialog.
            FILE_PATH_BROWSER.InitialDirectory = Properties.Misc.Default.KEYENCE_FOLDER;
            FILE_PATH_BROWSER.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            FILE_PATH_BROWSER.RestoreDirectory = true;
            EVENTS.LOG_MESSAGE(3, "Launched open file dialog.");
            if (FILE_PATH_BROWSER.ShowDialog() == DialogResult.OK) //Show the dialog. If the result is good...
                TXT_BOX_KEYENCE.Text = FILE_PATH_BROWSER.FileName; //Store the filename
            else //If the user did not select anything...
            {
                EVENTS.LOG_MESSAGE(3, "No file selected.");
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            bool IS_VALID = BACKEND.VALIDATE_FILE(TXT_BOX_KEYENCE.Text); //Check if the file is valid.
            if (IS_VALID) //If it is valid...
                LOAD_CSV_DATA(null, null); //Load in the data.
            else
            {
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return;
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        /// <summary>
        /// This method loads CSV data in from the TXT_BOX_KEYENCE.Text and binds it to DGV1. It does some controls formatting as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LOAD_CSV_DATA(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");

            //Load in data.
            bool IS_VALID = BACKEND.VALIDATE_FILE(TXT_BOX_KEYENCE.Text); //Check to make sure the filepath is valid.
            if (!IS_VALID) //If the file is not valid...
            {
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return; //Break out of method. VALIDATE_FILE method will notify user of problems.
            }
            TABLE_PROCESSOR KEYENCE_PROCESSOR = new TABLE_PROCESSOR(); //Create a table processor.
            EVENTS.LOG_MESSAGE(3, "Created a new TABLE_PROCESSOR.");
            DataTable GRID_DATA = new DataTable(); //Create a datatable that will hold all the csv data.
            DataTable INSTRUCTIONS = INSTRUCTION_SET.CREATE_INSTRUCTION_TABLE(); //We need to create a simple instruction table to load in the file.
            KEYENCE_PROCESSOR.PROCESS_INSTRUCTIONS(ref GRID_DATA, ref INSTRUCTIONS, TXT_BOX_KEYENCE.Text, ',', null, ','); //Load in the data into GRID_DATA.
            EVENTS.LOG_MESSAGE(3, "Data loaded in.");
            EVENTS.LOG_MESSAGE(3, "Naming each column.");
            foreach (DataColumn COLUMN in GRID_DATA.Columns) //Modification to prevent some null references later on.
                COLUMN.ColumnName = COLUMN.Ordinal.ToString();

            DGV1.DataSource = GRID_DATA; //Bind the DGV to the data. The DGV will be where we can pull the table from on future calls.
            EVENTS.LOG_MESSAGE(3, "Data bound to DGV.");
            //Format DGV.
            DGV1.RowHeadersVisible = false;
            DGV1.ColumnHeadersVisible = true;
            DGV1.ReadOnly = true;
            DGV1.AllowUserToOrderColumns = false;
            DGV1.AllowUserToResizeRows = false;
            DGV1.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn COLUMN in DGV1.Columns)
            {
                COLUMN.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            EVENTS.LOG_MESSAGE(3, "Formatted DGV.");
            //Enable controls for working with table.
            GRP_BOX_COLUMN_ASSIGNERS.Enabled = true;
            EVENTS.LOG_MESSAGE(3, "Assigner controls enabled.");
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        
        /// <summary>
        /// This method is called when any of the assigner buttons (letters and timestamp) are pushed. It handles assigning the name to the table column.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BTN_CLICK(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            int COLUMN_INDEX = DGV1.CurrentCell.ColumnIndex; //Get the currently selected column index.
            int ROW_INDEX = DGV1.CurrentCell.RowIndex; //Get the currently selected row index.
            EVENTS.LOG_MESSAGE(3, string.Format("Current cell selected: [C {0}, R {1}]", COLUMN_INDEX, ROW_INDEX));
            Button SENDER = (Button)sender; //Cast the generic sender into a button object.
            string TEXT = SENDER.Text; //Get the text of the button, we'll use this as the label for the column.
            DataTable TABLE = new DataTable(); //Create a blank table that we'll fill with data from the DGV.
            bool SUCCESS = BACKEND.EXTRACT_DATA_TABLE_FROM_DGV(ref DGV1, ref TABLE); //Extract the table from the DGV.
            
            foreach(DataColumn COLUMN in TABLE.Columns) //Go through each column...
            {
                if (COLUMN.ColumnName == null) //If the column has no name...
                    COLUMN.ColumnName = COLUMN.Ordinal.ToString(); //Assign it its ordinal number, this will prevent null reference exceptions.
                if (COLUMN.ColumnName == TEXT && COLUMN.Ordinal != COLUMN_INDEX) //If any column is equal to what we're currently evaluating...
                {
                    EVENTS.LOG_MESSAGE(3, string.Format("Column name already in use at ordinal {0}. Resetting column name.", COLUMN.Ordinal));
                    COLUMN.ColumnName = COLUMN.Ordinal.ToString(); //Reset it.
                }
            }

            //If the column is already named what we're trying to assign it, then that must mean the user wants to reset
            //the name of that column. So blank it out. If thats not the case, then assign it the TEXT value.
            if (TABLE.Columns[COLUMN_INDEX].ColumnName == TEXT)
            {
                EVENTS.LOG_MESSAGE(3, "Column already assigned this value. Resetting column name.");
                TABLE.Columns[COLUMN_INDEX].ColumnName = TABLE.Columns[COLUMN_INDEX].Ordinal.ToString();
            }
            else
            {
                TABLE.Columns[COLUMN_INDEX].ColumnName = TEXT;
                EVENTS.LOG_MESSAGE(3, string.Format("Column at ordinal {0} assigned value: {1}", COLUMN_INDEX, TEXT));
            }

            DGV1.DataSource = TABLE; //Rebind the DGV
            DGV1.CurrentCell = DGV1[COLUMN_INDEX, ROW_INDEX]; //Reselect the current cell.
            EVENTS.LOG_MESSAGE(3, string.Format("Data rebound to DGV, selected cell: [C {0}, R {1}]", COLUMN_INDEX, ROW_INDEX));
            COLOR_DGV_COLUMNS();
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
       
        /// <summary>
        /// This method simply colors DGV1 depending on column selections.
        /// </summary>
        private void COLOR_DGV_COLUMNS() //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            Color SELECTED_COLOR = new Color(); //Create a blank color object to hold color code.
            EVENTS.LOG_MESSAGE(3, "Coloring columns has begun.");
            foreach(DataGridViewColumn COLUMN in DGV1.Columns) //Cycle through each column in the DGV.
            {
                EVENTS.LOG_MESSAGE(3, string.Format("Processing column {0} for highlighting.", COLUMN.Index));
                switch (COLUMN.HeaderText)
                {
                    case ("A"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#CC99C9");
                        break;
                    case ("B"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#9EC1CF");
                        break;
                    case ("C"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#9EE09E");
                        break;
                    case ("D"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#FDFD97");
                        break;
                    case ("E"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#FEB144");
                        break;
                    case ("F"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#FF6663");
                        break;
                    case ("G"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#CC99C9");
                        break;
                    case ("H"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#9EC1CF");
                        break;
                    case ("I"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#9EE09E");
                        break;
                    case ("J"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#FDFD97");
                        break;
                    case ("K"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#FEB144");
                        break;
                    case ("L"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#FF6663");
                        break;
                    case ("M"):
                        SELECTED_COLOR = ColorTranslator.FromHtml("#CC99C9");
                        break;
                    case ("TimeStamp"):
                        SELECTED_COLOR = Color.Pink;
                        break;
                    default:
                        SELECTED_COLOR = Color.White;
                        break;
                }
                DGV1.Columns[COLUMN.Index].DefaultCellStyle.BackColor = SELECTED_COLOR; //Assign the color.
            }
            EVENTS.LOG_MESSAGE(3, "Coloring columns has finished.");
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }


        //Keylogger Methods

        /// <summary>
        /// This is a psuedo-constructor for setting up data for all methods related to key logging.
        /// Its broken off into its own method for organizations sake.
        /// </summary>
        private void KEY_LOGGER_SETUP() //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            KEY_LOGGER.KEY_PRESSED += new EventHandler(KEY_LOGGER_KEY_PRESSED_EVENT); //Install EventHandler.
            EVENTS.LOG_MESSAGE(3, "Key pressed EventHandler installed.");
            BTN_RECORD_STOP.Enabled = false;
            INJECTION_TABLE = BACKEND.CREATE_INJECTION_TABLE(); //Properly format the injection table. 
            BTN_REPLAY.Enabled = !RECORD_SEQUENCE_ACTIVE;
            EVENTS.LOG_MESSAGE(3, "Tables are set up.");
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        /// <summary>
        /// This method is called every time a key is pressed while the key logger is on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEY_LOGGER_KEY_PRESSED_EVENT(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            bool KEY_GOOD_TO_LOG = false;   
            if (RECORD_SEQUENCE_ACTIVE) //If the record sequence is active...
            {         
                string TEXT = (string)sender;
                if (TEXT.Length == 1) //Indicates that it's not a control key.
                {
                    char CHAR = TEXT[0]; //Get the character out of the string.
                    if (char.IsLetter(CHAR)) //Determine if the character is a letter...
                    {
                        KEY_GOOD_TO_LOG = true; //Indicate that this key press is valid to log.
                        EVENTS.LOG_MESSAGE(3, string.Format("Detected key {0} will be logged.", TEXT));
                    }
                }
                else //if the key press generated more than one character, its a control key...
                {
                    if (TEXT == "{TAB}") //Currently TAB key is the only control key we care about.
                    {
                        KEY_GOOD_TO_LOG = true; //Indicate that this key press is valid to log.
                        EVENTS.LOG_MESSAGE(3, string.Format("Detected key {0} will be logged.", TEXT));
                    }
                    else
                        EVENTS.LOG_MESSAGE(3, string.Format("Detected key {0} will NOT be logged.", TEXT));
                }
                if (KEY_GOOD_TO_LOG) //If the key is good to log...
                {
                    SEQUENCE_LIST.Add(TEXT); //Add the key to the sequence.
                    SEQUENCE_DATATABLE.Rows.Add(TEXT); //Add the key to the sequence.
                }
            }
            else if (REPLAY_SEQUENCE_ACTIVE) //Check if the replay sequence is active (and record is not active due to "else").
            {
                if ((string)sender == "{ESC}")
                {
                    EVENTS.LOG_MESSAGE(3, "ESC key detected, stopping replay.");
                    REPLAY_SEQUENCE_STOP(null, null);
                    EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
                    return;
                }
                if ((string)sender == "{INSERT}")
                {
                    EVENTS.LOG_MESSAGE(3, "INSERT key detected, starting replay.");
                    RECORD_SEQUENCE_STOP(null,null);

                    DataTable SOURCE = new DataTable();
                    bool SUCCESS = BACKEND.EXTRACT_DATA_TABLE_FROM_DGV(ref DGV1, ref SOURCE);
                    if (SUCCESS)
                        BACKEND.MOVE_CSV_DATA_INTO_INJECTION_TABLE(SOURCE, INJECTION_TABLE);

                    dataGridView1.DataSource = INJECTION_TABLE; //Temporary.

                    BACKEND.REPLAY(SEQUENCE_LIST, INJECTION_TABLE); //Where the actual replaying happens.
                    System.Threading.Thread.Sleep(75); //Delay a bit.
                    SendKeys.SendWait("{INSERT}"); //As the insert key is a latched key, we want to turn it back to its original state, so resend.
                    REPLAY_SEQUENCE_ACTIVE = false;
                }
                else
                    EVENTS.LOG_MESSAGE(3, "Program in replay mode, INSERT and ESC are the only valid keys.");
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        /// <summary>
        /// This method will start the key logger. Keys will be recorded when the KEY_PRESSED event is raised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RECORD_SEQUENCE_START(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            KEY_LOGGER.START_KEY_LOGGER(); //Start the keylogger in the FORBES library.
            EVENTS.LOG_MESSAGE(3, "Key logger started.");
            BTN_RECORD_START.Enabled = false;
            BTN_RECORD_STOP.Enabled = true;
            SEQUENCE_LIST.Clear();
            SEQUENCE_DATATABLE.Clear();
            EVENTS.LOG_MESSAGE(3, "Cleared sequence tables.");
            RECORD_SEQUENCE_ACTIVE = true;
            BTN_REPLAY.Enabled = !RECORD_SEQUENCE_ACTIVE;
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        /// <summary>
        /// This method will stop the key logger record session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RECORD_SEQUENCE_STOP(object sender, EventArgs e) //Logged and documented.

        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            BTN_RECORD_START.Enabled = true;
            BTN_RECORD_STOP.Enabled = false;
            RECORD_SEQUENCE_ACTIVE = false;
            KEY_LOGGER.STOP_KEY_LOGGER(); //Stop the keylogger in the FORBES library.
            BTN_REPLAY.Enabled = !RECORD_SEQUENCE_ACTIVE;
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        /// <summary>
        /// This method will start the replay sequence. All it does is set a flag bit,
        /// as the real startup of the replay sequence is handled by the KEY_LOGGER_KEY_PRESSED_EVENT method
        /// once the user presses the insert key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void REPLAY_SEQUENCE_START(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            KEY_LOGGER.START_KEY_LOGGER(); //Start the logger in the FORBES library.
            REPLAY_SEQUENCE_ACTIVE = true;
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        /// <summary>
        /// This method turns off the REPLAY_SEQUENCE_ACTIVE flag and stops the keylogger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void REPLAY_SEQUENCE_STOP(object sender, EventArgs e) //Logged and documented.
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            KEY_LOGGER.STOP_KEY_LOGGER(); //Stop the logger in the FORBES library.
            REPLAY_SEQUENCE_ACTIVE = false;
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

    }
}
