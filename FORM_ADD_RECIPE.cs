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
using TABLE_PROCESSOR;
using FORBES;

namespace TyrannosaurusPlex
{
    public partial class FORM_ADD_RECIPE : Form
    {
        //Events, Delegates, Handlers
        public event EventHandler DATA_READY; //Create a EventHandler Delegate that we can invoke.
        List<string> SEQUENCE_LIST = new List<string> { }; //Key data in list.
        DataTable SEQUENCE_DATATABLE = new DataTable(); //Key data DataTable.
        private bool RECORD_SEQUENCE_ACTIVE = false;
        private bool REPLAY_SEQUENCE_ACTIVE = false;
        DataTable INJECTION_TABLE = new DataTable();

        //Objects
        private LOGGER EVENTS = new LOGGER("Add Recipe Form Log");
        private string KEYENCE_CSV;

        //Constructor
        public FORM_ADD_RECIPE(DataTable CHECKSHEET_TYPE_DATA_TABLE, RECIPE_DATA CURRENT_RECIPE_DATA = null)
        {
            InitializeComponent();
            EVENTS.LOG_MESSAGE(1, "INITIALIZE");

            //For each row in the datatable passed on initialization, add the data to the listbox.
            foreach (DataRow ROW in CHECKSHEET_TYPE_DATA_TABLE.Rows)
                LISTBOX_CHECKSHEET_TYPE.Items.Add(ROW.ItemArray[0].ToString());
            
            //Format all the color assigners.
            GRP_BOX_COLUMN_ASSIGNERS.Enabled = false;

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

            //If this form is being created as a edit recipe, rather than a new recipe...
            if (CURRENT_RECIPE_DATA != null)
            {
                LOAD_IN_EDIT_DATA(CURRENT_RECIPE_DATA);
                TXT_BOX_PART_NUM.Enabled = false;
                LISTBOX_CHECKSHEET_TYPE.Enabled = false;
            }
            KEY_LOGGER_SETUP();
            FORM_MAIN.OK_TO_CLOSE_ADD_RECIPE_FORM += new EventHandler(CLOSE);
        }

        //Methods
        /// <summary>
        /// This method is called when the form loads and it is determined to be a "edit recipe" call rather than a "new recipe call".
        /// The method loads in passed data into all the form objects.
        /// </summary>
        /// <param name="CURRENT_RECIPE_DATA">Recipe data to load into the form fields.</param>
        private void LOAD_IN_EDIT_DATA(RECIPE_DATA CURRENT_RECIPE_DATA)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            //Load in part number.
            TXT_BOX_PART_NUM.Text = CURRENT_RECIPE_DATA.part_number;

            //Load in checksheet type.
            LISTBOX_CHECKSHEET_TYPE.SelectedItem = CURRENT_RECIPE_DATA.checksheet_type;

            //Load in CSV file location.
            string MODIFIED_PATH = CURRENT_RECIPE_DATA.csv_location;
            MODIFIED_PATH.Replace(@"\", @"\\");
            TXT_BOX_KEYENCE.Text = MODIFIED_PATH;

            //Load CSV file into viewer.
            LOAD_CSV_DATA(null, null);

            //Assign the proper headers.
            foreach(DataGridViewColumn COLUMN in DGV1.Columns)
            {
                if (COLUMN.Index == CURRENT_RECIPE_DATA.a_col)
                    COLUMN.HeaderText = "A";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.b_col)
                    COLUMN.HeaderText = "B";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.c_col)
                    COLUMN.HeaderText = "C";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.d_col)
                    COLUMN.HeaderText = "D";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.e_col)
                    COLUMN.HeaderText = "E";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.f_col)
                    COLUMN.HeaderText = "F";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.g_col)
                    COLUMN.HeaderText = "G";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.h_col)
                    COLUMN.HeaderText = "H";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.i_col)
                    COLUMN.HeaderText = "I";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.j_col)
                    COLUMN.HeaderText = "J";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.k_col)
                    COLUMN.HeaderText = "K";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.l_col)
                    COLUMN.HeaderText = "L";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.m_col)
                    COLUMN.HeaderText = "M";
                if (COLUMN.Index == CURRENT_RECIPE_DATA.timestamp_col)
                    COLUMN.HeaderText = "TimeStamp";
            }
            COLOR_DGV_COLUMNS();
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        
        /// <summary>
        /// This method checks that the part number entered in TXT_BOX_PART_NUM is a valid ASPC part number.
        /// </summary>
        /// <returns>
        /// <para>0: Part number is valid.</para>
        /// <para>1: Part number is not 6 characters long.</para>
        /// <para>2: A non-alpha character was detected in the first two characters.</para>
        /// <para>3: A non-numeric character was detected in the last four characters.</para>
        /// </returns>
        private int VALIDATE_PART_NUMBER()
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            string TEXT = TXT_BOX_PART_NUM.Text.ToUpper(); //Capitalize the text.
            if (TEXT.Length != 6) //Any ASPC part number should be six digits...
            {
                EVENTS.LOG_MESSAGE(2, "Part number needs to be 6 characters long.");
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return 1;
            }
            string COMPANY_CODE = TEXT.Substring(0, 2); //Extract the company code digits.
            string NUMERIC_CODE = TEXT.Substring(2, 4); //Extract the numeric code.
            if (!Regex.IsMatch(COMPANY_CODE, @"^[a-zA-Z]+$")) //Check that the company code is only letters, if its not...
            {
                EVENTS.LOG_MESSAGE(2, "Non-alpha character detected in company code.");
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return 2;
            }
            foreach (char CHARACTER in NUMERIC_CODE) //For each character in the numeric code...
            {
                if (CHARACTER < '0' || CHARACTER > '9') //Check its ascii value, if it lies on either side of the numeric set, the data is no good.
                {
                    EVENTS.LOG_MESSAGE(2, "Non-numeric character detected in numeric part of part number.");
                    EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                    return 3;
                }
            }
            EVENTS.LOG_MESSAGE(2, "Part number valid."); //If all the above checks passed, the part number is good.
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            TXT_BOX_PART_NUM.Text = TEXT; //Reinsert the capitalized version of the part number into the text field.
            return 0;

        }
        private void BTN_SAVE_CLICK(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");

            //Check the part number is good----------------------------------------------------------------------
            int RESULT = VALIDATE_PART_NUMBER();
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

            //Check if the keyence csv path is valid-------------------------------------------------------------
            bool IS_CSV_VALID = CHECK_IF_CSV_IS_VALID();
            if (!IS_CSV_VALID)
                return;

            //Check that data has been previewed in the viewer---------------------------------------------------
            if(DGV1.Rows.Count <= 0)
            {
                string MESSAGE = "The Keyence CSV file has not been opened and assignments have not been made.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return; //Cancel all other operations.
            }

            //Check that a timestamp column is selected----------------------------------------------------------
            int TIMESTAMP_INDEX = -1;
            foreach (DataGridViewColumn COLUMN in DGV1.Columns)
                if (COLUMN.HeaderText == "TimeStamp")
                    TIMESTAMP_INDEX = COLUMN.Index;
            if(TIMESTAMP_INDEX == -1)
            {
                string MESSAGE = "A timestamp column was not designated, this is required.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return; //Cancel all other operations.
            }

            //Commit the data to the returning package-----------------------------------------------------------
            RECIPE_DATA DATA = new RECIPE_DATA();
            DATA.part_number = TXT_BOX_PART_NUM.Text;
            DATA.checksheet_type = CHECKSHEET_TYPE;
            DATA.key_sequence = "";
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
            foreach (DataGridViewColumn COLUMN in DGV1.Columns)
            {
                switch(COLUMN.HeaderText)
                {
                    case ("A"):
                        DATA.a_col = COLUMN.Index;
                        break;
                    case ("B"):
                        DATA.b_col = COLUMN.Index;
                        break;
                    case ("C"):
                        DATA.c_col = COLUMN.Index;
                        break;
                    case ("D"):
                        DATA.d_col = COLUMN.Index;
                        break;
                    case ("E"):
                        DATA.e_col = COLUMN.Index;
                        break;
                    case ("F"):
                        DATA.f_col = COLUMN.Index;
                        break;
                    case ("G"):
                        DATA.g_col = COLUMN.Index;
                        break;
                    case ("H"):
                        DATA.h_col = COLUMN.Index;
                        break;
                    case ("I"):
                        DATA.i_col = COLUMN.Index;
                        break;
                    case ("J"):
                        DATA.j_col = COLUMN.Index;
                        break;
                    case ("K"):
                        DATA.k_col = COLUMN.Index;
                        break;
                    case ("L"):
                        DATA.l_col = COLUMN.Index;
                        break;
                    case ("M"):
                        DATA.m_col = COLUMN.Index;
                        break;

                }
            }

            //Invoke delegate with info pack and close-----------------------------------------------------------
            DATA_READY?.Invoke(null, DATA);
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void CLOSE(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            this.Close();
        }
        private void BTN_BROWSE_KEYENCE_CLICK(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            OpenFileDialog FILE_PATH_BROWSER = new OpenFileDialog();
            FILE_PATH_BROWSER.InitialDirectory = Properties.Misc.Default.KEYENCE_FOLDER;
            FILE_PATH_BROWSER.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
            FILE_PATH_BROWSER.RestoreDirectory = true;
            if (FILE_PATH_BROWSER.ShowDialog() == DialogResult.OK)
            {
                KEYENCE_CSV = FILE_PATH_BROWSER.FileName;
                TXT_BOX_KEYENCE.Text = KEYENCE_CSV;
            }
            bool IS_VALID = CHECK_IF_CSV_IS_VALID();
            if (IS_VALID)
                LOAD_CSV_DATA(null, null);
            else
            {
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void LOAD_CSV_DATA(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            bool IS_VALID = CHECK_IF_CSV_IS_VALID();
            if (!IS_VALID)
                return;
            TABLE KEYENCE_PROCESSOR = new TABLE();
            DataTable GRID_DATA = new DataTable();
            DataTable INSTRUCTIONS = new DataTable();
            INSTRUCTIONS.Columns.Add();
            INSTRUCTIONS.Rows.Add("CSV");
            KEYENCE_PROCESSOR.PROCESS_INSTRUCTIONS(ref GRID_DATA, ref INSTRUCTIONS, TXT_BOX_KEYENCE.Text, ',', null, ',');
            DGV1.DataSource = GRID_DATA;
            DGV1.RowHeadersVisible = false;
            DGV1.ColumnHeadersVisible = true;
            DGV1.ReadOnly = true;
            DGV1.AllowUserToOrderColumns = false;
            DGV1.AllowUserToResizeRows = false;
            DGV1.AllowUserToResizeColumns = false;
            foreach (DataGridViewColumn COLUMN in DGV1.Columns)
            {
                COLUMN.HeaderText = "";
                COLUMN.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            GRP_BOX_COLUMN_ASSIGNERS.Enabled = true;
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }
        private void BTN_CLICK(object sender, EventArgs e)
        {
            //This is sloppy but I'm just gonna make assignments and use the column header text to track whats what. Just like a CompSci 101 student would.
            //It'll work. It's Friday and I was up all night with the baby. I'll probably be disgusted with myself when I'm well rested but I'm doing it.
            //Backend work in a frontend class...
            EVENTS.LOG_MESSAGE(1, "ENTER");
            int INDEX = DGV1.CurrentCell.ColumnIndex; //Get the currently selected column index.
            Button SENDER = (Button)sender; //Cast the generic sender into a button object.
            int PRIOR_COLUMN = -1; //In case we need to undo a assignment.
            //First check that the assignment has not already been made. If it has, remove that assignment.
            foreach (DataGridViewColumn COLUMN in DGV1.Columns)
            {
                if (COLUMN.HeaderText == SENDER.Text)
                {
                    COLUMN.HeaderText = null;
                    COLUMN.DefaultCellStyle.BackColor = Color.White;
                    PRIOR_COLUMN = COLUMN.Index;
                }
            }
            if (PRIOR_COLUMN == INDEX)
                return; //Escape out, this will effectively invert the column to a unselected state.
            DGV1.Columns[INDEX].HeaderText = SENDER.Text;
            COLOR_DGV_COLUMNS();
        }
        private bool CHECK_IF_CSV_IS_VALID()
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            if (!System.IO.File.Exists(TXT_BOX_KEYENCE.Text)) //If the file does not exist.
            {
                string MESSAGE = "The Keyence CSV file does not exist. Plese check filepath.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return false;
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            return true;
        }
        private void COLOR_DGV_COLUMNS()
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            Color SELECTED_COLOR = new Color();
            foreach(DataGridViewColumn COLUMN in DGV1.Columns)
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
                DGV1.Columns[COLUMN.Index].DefaultCellStyle.BackColor = SELECTED_COLOR;
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }



        //Keylogger Methods

        /// <summary>
        /// This is a psuedo-constructor for setting up data for all methods related to key logging.
        /// Its broken off into its own method for organizations sake.
        /// </summary>
        private void KEY_LOGGER_SETUP()
        {
            KEY_LOGGER.KEY_PRESSED += new EventHandler(KEY_LOGGER_KEY_PRESSED_EVENT);
            SEQUENCE_DATATABLE.Columns.Add("Keys");
            BTN_RECORD_STOP.Enabled = false;
            INJECTION_TABLE.Columns.Add("Letter", typeof(string));
            INJECTION_TABLE.Columns.Add("Value", typeof(string));
            for (int i = 65; i <= 90; i++)
            {
                INJECTION_TABLE.Rows.Add((char)i, "");
            }
            dataGridView1.DataSource = INJECTION_TABLE;
            dataGridView2.DataSource = SEQUENCE_DATATABLE;
            BTN_REPLAY.Enabled = !RECORD_SEQUENCE_ACTIVE;
        }

        /// <summary>
        /// This method is called every time a key is pressed while the key logger is on.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEY_LOGGER_KEY_PRESSED_EVENT(object sender, EventArgs e)
        {
            
            if (RECORD_SEQUENCE_ACTIVE) //If the record sequence is active...
            {
                SEQUENCE_LIST.Add((string)sender); //Add the key to the sequence.
                SEQUENCE_DATATABLE.Rows.Add((string)sender); //Add the key to the sequence.
            }
            else if (REPLAY_SEQUENCE_ACTIVE) //Check if the replay sequence is active (and record is not active due to "else").
            {
                if ((string)sender == "{INSERT}")
                    REPLAY(SEQUENCE_LIST, INJECTION_TABLE);
                if ((string)sender == "{ESC}")
                    REPLAY_SEQUENCE_STOP(null, null);
            }
        }

        /// <summary>
        /// This method simulates a keyboard user and replays the keys sent to it. This method substitutes letters for data in TABLE_TO_INJECT.
        /// For instance, if there is a "A" character in the PASSED_SEQUENCE, the method will look for row "A" in the DataTable and output the value into the keystream.
        /// </summary>
        /// <param name="PASSED_SEQUENCE">Each string in this list needs to be a valid keycode.</param>
        /// <param name="TABLE_TO_INJECT">This table must have a column named "Letter" and a column named "Value".</param>
        private void REPLAY(List<string> PASSED_SEQUENCE, DataTable TABLE_TO_INJECT)
        {
            RECORD_SEQUENCE_STOP(null, null); //Ensure the recording has stopped.
            SendKeys.Flush(); //Wait for the buffer to empty.
            foreach (string KEY in PASSED_SEQUENCE) //For each key in the recorded sequence...
            {
                bool IS_CHAR = Char.IsLetterOrDigit(KEY, 0); //Check if its a letter.
                if (IS_CHAR) //If the key is a letter, this is an indication that substitution needs to happen...
                {
                    foreach (DataRow ROW in TABLE_TO_INJECT.Rows) //Cycle through the table to find the right row.
                    {
                        string ROW_LETTER = ROW["Letter"].ToString(); //Get the current row letter.
                        if (KEY.ToUpper() == ROW_LETTER) //If the row letter matches the letter from the key stream...
                        {
                            foreach (char CHARACTER in ROW["Value"].ToString()) //for each character in the value field, send the key.
                            {
                                SendKeys.SendWait(CHARACTER.ToString());
                                System.Threading.Thread.Sleep(75); //Delay a bit.
                            }
                        }
                    }
                }
                else //The key is not a letter...
                {
                    SendKeys.SendWait(KEY); //Just send the key.
                }
                System.Threading.Thread.Sleep(75); //Delay a bit.
            }
            System.Threading.Thread.Sleep(75); //Delay a bit.
            SendKeys.SendWait("{INSERT}"); //Turn INSERT back off.
            REPLAY_SEQUENCE_ACTIVE = false; //Indicate the sequence is done.
        }

        /// <summary>
        /// This method will start the key logger. Keys will be recorded when the KEY_PRESSED event is raised.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RECORD_SEQUENCE_START(object sender, EventArgs e)
        {
            KEY_LOGGER.START_KEY_LOGGER();
            BTN_RECORD_START.Enabled = false;
            BTN_RECORD_STOP.Enabled = true;
            SEQUENCE_LIST.Clear();
            SEQUENCE_DATATABLE.Clear();
            RECORD_SEQUENCE_ACTIVE = true;
            BTN_REPLAY.Enabled = !RECORD_SEQUENCE_ACTIVE;
        }

        /// <summary>
        /// This method will stop the key logger record session.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RECORD_SEQUENCE_STOP(object sender, EventArgs e)
        {
            BTN_RECORD_START.Enabled = true;
            BTN_RECORD_STOP.Enabled = false;
            RECORD_SEQUENCE_ACTIVE = false;
            KEY_LOGGER.STOP_KEY_LOGGER();
            BTN_REPLAY.Enabled = !RECORD_SEQUENCE_ACTIVE;
        }

        /// <summary>
        /// This method will start the replay sequence. All it does is set a flag bit,
        /// as the real startup of the replay sequence is handled by the KEY_LOGGER_KEY_PRESSED_EVENT method
        /// once the user presses the insert key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void REPLAY_SEQUENCE_START(object sender, EventArgs e)
        {
            KEY_LOGGER.START_KEY_LOGGER();
            REPLAY_SEQUENCE_ACTIVE = true;
        }

        /// <summary>
        /// This method turns off the REPLAY_SEQUENCE_ACTIVE flag and stops the keylogger.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void REPLAY_SEQUENCE_STOP(object sender, EventArgs e)
        {
            KEY_LOGGER.STOP_KEY_LOGGER();
            REPLAY_SEQUENCE_ACTIVE = false;
        }

    }
}
