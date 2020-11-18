﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FORBES.KEY_LOGGER_NAMESPACE;
using FORBES.TABLE_PROCESSOR_NAMESPACE;

namespace TyrannosaurusPlex
{
    [System.ComponentModel.DesignerCategory("")] //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
    public class DUMMY_PARTIAL_KEYLOGGER { } //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
    public partial class FORM_MAIN
    {
        //Objects
        List<string> SEQUENCE_LIST = new List<string> { };
        DataTable SEQUENCE_DATATABLE = new DataTable();
        private bool REPLAY_SEQUENCE_ACTIVE = false;
        DataTable INJECTION_TABLE = BACKEND.CREATE_INJECTION_TABLE();


        //Methods

        private void KEY_LOGGER_SETUP() //Pseudo-contstructor. Be sure to call at initialization.
        {
            KEY_LOGGER.KEY_PRESSED += new EventHandler(KEY_LOGGER_KEY_PRESSED_EVENT);
            SEQUENCE_DATATABLE.Columns.Add("Keys");
        }
        private void KEY_LOGGER_KEY_PRESSED_EVENT(object sender, EventArgs e) //Event setup in KEY_LOGGER_SETUP.
        {
            if (REPLAY_SEQUENCE_ACTIVE) //Check if the replay sequence is active...
            {
                if ((string)sender == "{INSERT}")
                    REPLAY(SEQUENCE_LIST, INJECTION_TABLE);
                if ((string)sender == "{ESC}")
                    REPLAY_SEQUENCE_STOP(null, null);
            }
        }
        private void KEY_LOGGER_START(object sender, EventArgs e) //Starts the keylogger.
        {
            KEY_LOGGER.START_KEY_LOGGER();
        }
        private void KEY_LOGGER_STOP(object sender, EventArgs e) //Not currently used.
        {
            KEY_LOGGER.STOP_KEY_LOGGER();
        }
        private void KEY_LOGGER_CLEAR(object sender, EventArgs e) //Not currently used.
        {
            KEY_LOGGER.CLEAR_KEY_LOGGER();
        }
        private void REPLAY_SEQUENCE_START(object sender, EventArgs e)
        {
            KEY_LOGGER_START(null, null); //Start the keylogger.
            REPLAY_SEQUENCE_ACTIVE = true;
        }
        private void REPLAY_SEQUENCE_STOP(object sender, EventArgs e)
        {
            KEY_LOGGER_STOP(null, null); //Stop the keylogger.
            REPLAY_SEQUENCE_ACTIVE = false;
        }
        private void REPLAY(List<string> PASSED_SEQUENCE, DataTable TABLE_TO_INJECT)
        {
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
            REPLAY_SEQUENCE_ACTIVE = false; //Indicate the sequence is done.
        }


        private void button1_Click(object sender, EventArgs e)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");

            //First instruct the user how to use the injector.
            string MESSAGE = "Navigate to the first field and press the insert key.";
            MessageBox.Show(MESSAGE, "Instructions", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Get the currently selected recipe data.
            RECIPE_DATA DATA = new RECIPE_DATA();
            GET_CURRENTLY_SELECTED_RECIPE_DATA(out DATA);

            //Process the key sequence from the recipe into a list and table.
            SEQUENCE_LIST.Clear();
            SEQUENCE_DATATABLE.Clear();
            BACKEND.LOAD_IN_KEY_SEQUENCE(DATA.key_sequence, ref SEQUENCE_LIST, ref SEQUENCE_DATATABLE);

            //Get data from file.
            TABLE_PROCESSOR KEYENCE_PROCESSOR = new TABLE_PROCESSOR();
            DataTable TABLE = new DataTable();
            DataTable INSTRUCTIONS = INSTRUCTION_SET.CREATE_INSTRUCTION_TABLE();
            KEYENCE_PROCESSOR.PROCESS_INSTRUCTIONS(ref TABLE, ref INSTRUCTIONS, DATA.csv_location, ',');


            //Fill out the injection table.
            BACKEND.MOVE_CSV_DATA_INTO_INJECTION_TABLE(TABLE, INJECTION_TABLE);

            //Start the keylogger.
            REPLAY_SEQUENCE_START(null, null);

            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");

        }
    }
}
