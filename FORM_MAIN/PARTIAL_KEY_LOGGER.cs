using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FORBES;

namespace TyrannosaurusPlex
{
    [System.ComponentModel.DesignerCategory("")] //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
    public class DUMMY_PARTIAL_KEYLOGGER { } //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
    public partial class FORM_MAIN
    {
        //Objects
        List<string> SEQUENCE_LIST = new List<string> { };
        DataTable SEQUENCE_DATATABLE = new DataTable();
        private bool RECORD_SEQUENCE_ACTIVE = false;
        private bool REPLAY_SEQUENCE_ACTIVE = false;
        DataTable INJECTION_TABLE = new DataTable();

        //Methods

        private void KEY_LOGGER_SETUP() //Pseudo-contstructor. Be sure to call at initialization.
        {
            KEY_LOGGER.KEY_PRESSED += new EventHandler(KEY_LOGGER_KEY_PRESSED_EVENT);
            SEQUENCE_DATATABLE.Columns.Add("Keys");
            BTN_RECORD_STOP.Enabled = false;
        }
        private void INJECTION_TABLE_SETUP() //Create the table that we'll pull values from.
        {
            INJECTION_TABLE.Columns.Add("Letter", typeof(string));
            INJECTION_TABLE.Columns.Add("Value", typeof(string));
            DGV_INJECTION_TABLE.DataSource = INJECTION_TABLE;
            for (int i = 65; i <= 90; i++)
            {
                INJECTION_TABLE.Rows.Add((char)i, "");
            }
        }
        private void KEY_LOGGER_KEY_PRESSED_EVENT(object sender, EventArgs e) //Event setup in KEY_LOGGER_SETUP.
        {
            if (RECORD_SEQUENCE_ACTIVE) //If the record sequence is active...
            {
                SEQUENCE_LIST.Add((string)sender); //Add the key to the sequence.
                SEQUENCE_DATATABLE.Rows.Add((string)sender); //Add the key to the sequence.
            }
            else if (REPLAY_SEQUENCE_ACTIVE) //Check if the replay sequence is active (and record is not active due to "else").
            {
                if((string) sender == "{INSERT}")
                    REPLAY(SEQUENCE_LIST,INJECTION_TABLE);
                if((string) sender == "{ESC}")
                    REPLAY_SEQUENCE_STOP(null, null);
            }
        }
        private void KEY_LOGGER_START(object sender, EventArgs e) //Starts the keylogger.
        {
            KEY_LOGGER.START_KEY_LOGGER();
        }
        private void KEY_LOGGER_PAUSE(object sender, EventArgs e) //Not currently used.
        {
            KEY_LOGGER.STOP_KEY_LOGGER();
        }
        private void KEY_LOGGER_CLEAR(object sender, EventArgs e) //Not currently used.
        {
            KEY_LOGGER.CLEAR_KEY_LOGGER();
        }
        private void RECORD_SEQUENCE_START(object sender, EventArgs e) //Records keys to SEQUENCE_LIST/DATATABLE.
        {
            KEY_LOGGER.START_KEY_LOGGER();
            BTN_RECORD_START.Enabled = false;
            BTN_RECORD_STOP.Enabled = true;
            SEQUENCE_LIST.Clear();
            SEQUENCE_DATATABLE.Clear();
            RECORD_SEQUENCE_ACTIVE = true;
        }
        private void RECORD_SEQUENCE_STOP(object sender, EventArgs e) //Stops recording.
        {
            BTN_RECORD_START.Enabled = true;
            BTN_RECORD_STOP.Enabled = false;
            RECORD_SEQUENCE_ACTIVE = false;
            KEY_LOGGER.STOP_KEY_LOGGER();
        }
        private void REPLAY_SEQUENCE_START(object sender, EventArgs e)
        {
            KEY_LOGGER_START(null,null); //Start the keylogger.
            REPLAY_SEQUENCE_ACTIVE = true;
        }
        private void REPLAY_SEQUENCE_STOP(object sender, EventArgs e)
        {
            KEY_LOGGER_PAUSE(null, null); //Stop the keylogger.
            REPLAY_SEQUENCE_ACTIVE = false;
        }
        private void REPLAY(List<string> PASSED_SEQUENCE, DataTable TABLE_TO_INJECT)
        {
            RECORD_SEQUENCE_STOP(null, null); //Ensure the recording has stopped.
            SendKeys.Flush(); //Wait for the buffer to empty.
            foreach (string KEY in PASSED_SEQUENCE) //For each key in the recorded sequence...
            {
                bool IS_CHAR = Char.IsLetterOrDigit(KEY,0); //Check if its a letter.
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
        
    }
}
