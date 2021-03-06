﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FORBES.LOGGER_NAMESPACE;
using FORBES.KEY_LOGGER_NAMESPACE;

namespace TyrannosaurusPlex
{
    public static class BACKEND
    {
        /// <summary>
        /// This function attempts to safely cast a DataGridViews source into a DataTable.
        /// </summary>
        /// <param name="DGV">The DGV to get the data from.</param>
        /// <param name="DATA_TABLE">A DataTable to bind to.</param>
        /// <returns>True on success, false on failure.</returns>
        public static bool EXTRACT_DATA_TABLE_FROM_DGV(ref DataGridView DGV, ref DataTable DATA_TABLE)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            try
            {
                DATA_TABLE = (DataTable)DGV.DataSource;
            }
            catch (InvalidCastException EX)
            {
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", EX.Message);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return false;
            }
            EVENTS.LOG_MESSAGE(2, "EXIT_SUCCESS");
            return true;
        }

        /// <summary>
        /// Event log for all backend methods.
        /// </summary>
        public static LOGGER EVENTS = new LOGGER("Backend Methods Log");

        /// <summary>
        /// This method checks that the part number entered in TXT_BOX_PART_NUM is a valid ASPC part number.
        /// </summary>
        /// <returns>
        /// <para>0: Part number is valid.</para>
        /// <para>1: Part number is not 6 characters long.</para>
        /// <para>2: A non-alpha character was detected in the first two characters.</para>
        /// <para>3: A non-numeric character was detected in the last four characters.</para>
        /// </returns>
        public static int VALIDATE_PART_NUMBER(ref string PART_NUMBER)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            string TEXT = PART_NUMBER.ToUpper(); //Capitalize the text.
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
            PART_NUMBER = TEXT; //Reinsert the capitalized version of the part number into the text field.
            return 0;

        }

        /// <summary>
        /// This method moves data from a DataTable created in the image of the source CSV file into a injection table. It sorts through column labels (A thru Z)
        /// in the source table to figure out what goes where.
        /// </summary>
        /// <param name="SOURCE_CSV_TABLE"> This DataTable should be in the format of the CSV file but should have marked column names.</param>
        /// <param name="SOURCE_INJECTION_TABLE">This table must have a column named "Letter" and a column named "Value".
        /// The "Letter" column must be populated with all letters A thru Z all uppercase. One letter per row.</param>
        public static void MOVE_CSV_DATA_INTO_INJECTION_TABLE(DataTable SOURCE_CSV_TABLE, DataTable SOURCE_INJECTION_TABLE)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            foreach (DataColumn COLUMN in SOURCE_CSV_TABLE.Columns) //For each column...
            {
                EVENTS.LOG_MESSAGE(3, string.Format("Evaluating source column {0}", COLUMN.Ordinal.ToString()));
                EVENTS.LOG_MESSAGE(3, string.Format("Column name: {0}", COLUMN.ColumnName));
                for (int i = (char)'A'; i <= (char)'Z'; i++) //Go through each letter...
                {
                    char i_AS_CHAR = (char)i; //Convert incrementer to a character.
                    string i_AS_STRING = i_AS_CHAR.ToString(); //Convert incrementer character to string.
                    if (COLUMN.ColumnName == i_AS_STRING) //Check if the incrementer letter code is the same as the current column header text...
                    {
                        EVENTS.LOG_MESSAGE(3, string.Format("Match between column {0} and iterator {1}.", COLUMN.Ordinal.ToString(), (char)i));
                        //Now that the proper column in the from the CSV data is selected, we need to find the last populated row.
                        //The last populated row should be the most recent entry.
                        for (int j = SOURCE_CSV_TABLE.Rows.Count - 1; j >= 0; j--) //Scan rows from last to first...
                        {
                            var CELL_VAL = SOURCE_CSV_TABLE.Rows[j][COLUMN.Ordinal]; //Get the value of the cell.
                            if (CELL_VAL != null) //If the cell is not null. This must be checked before doing a ToString to prevent a NoRef exception.
                            {
                                if (CELL_VAL.ToString() != "") //Also check to make sure its not a non-null blank...
                                                               //Nest this check rather than doing a compound as a 
                                                               //null reference may occur this will mess up the ToString method.
                                {
                                    //Now we have the most recent value from the SOURCE_CSV_TABLE, we need to add it to the SOURCE_INJECTION_TABLE.
                                    foreach (DataRow ROW in SOURCE_INJECTION_TABLE.Rows) //Find the appropriate row in the SOURCE_INJECTION_TABLE...
                                    {
                                        if (ROW["Letter"].ToString() == i_AS_STRING) //If the row letter is the same as the column header...
                                        {
                                            ROW["Value"] = CELL_VAL.ToString(); //Move the data over.
                                            break;  //Break out as there will only be one letter row.
                                        }
                                    }
                                    break; //Break out, otherwise this will recurse all the way up to the oldest cell.
                                }
                            }
                        }
                    }
                }
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        /// <summary>
        /// This method checks if the file at specified path exists. Its really just a cleaner wrapper for a common call.
        /// </summary>
        /// <param name="PATH">The filepath to check.</param>
        /// <returns>True on valid filepath. False on invalid path.</returns>
        public static bool VALIDATE_FILE(string PATH)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            if (!System.IO.File.Exists(PATH)) //If the file does not exist.
            {
                string MESSAGE = "The file does not exist. Plese check filepath.";
                MessageBox.Show(MESSAGE, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EVENTS.LOG_MESSAGE(2, "EXCEPTION", MESSAGE);
                EVENTS.LOG_MESSAGE(1, "EXIT_FAIL");
                return false;
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            return true;
        }

        /// <summary>
        /// This method creates a properly formatted injection table.
        /// </summary>
        /// <returns>A DataTable with column 0 called "Letter" and column 1 called "Value". Each row contains a uppercase letter in the column "Letter".</returns>
        public static DataTable CREATE_INJECTION_TABLE()
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            DataTable INJECTION_TABLE = new DataTable();
            INJECTION_TABLE.Columns.Add("Letter", typeof(string));
            INJECTION_TABLE.Columns.Add("Value", typeof(string));
            for (int i = (char)'A'; i <= (char)'Z'; i++)
            {
                INJECTION_TABLE.Rows.Add((char)i, "");
            }
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
            return INJECTION_TABLE;
        }

        /// <summary>
        /// This method simulates a keyboard user and replays the keys sent to it. This method substitutes letters for data in TABLE_TO_INJECT.
        /// For instance, if there is a "A" character in the PASSED_SEQUENCE, the method will look for row "A" in the DataTable and output the value into the keystream.
        /// Be sure to stop recording before running this method.
        /// </summary>
        /// <param name="PASSED_SEQUENCE">Each string in this list needs to be a valid keycode.</param>
        /// <param name="TABLE_TO_INJECT">This table must be formatted properly, use the function CREATE_INJECTON_TABLE to make a proper table.</param>
        public static void REPLAY(List<string> PASSED_SEQUENCE, DataTable TABLE_TO_INJECT)
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
        }

        /// <summary>
        /// This method loads a string key sequence into a list and table.
        /// </summary>
        /// <param name="KEY_SEQ">The comma delimited string to process.</param>
        /// <param name="SEQUENCE_LIST">A list of strings to put the results in.</param>
        /// <param name="SEQUENCE_DATATABLE">A single column data table to put the results in.</param>
        public static void LOAD_IN_KEY_SEQUENCE(string KEY_SEQ, ref List<string> SEQUENCE_LIST, ref DataTable SEQUENCE_DATATABLE)
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
            EVENTS.LOG_MESSAGE(1, "EXIT_SUCCESS");
        }

        /// <summary>
        /// This method labels all the columns in TABLE in accordance with DATA.
        /// </summary>
        /// <param name="TABLE">The table to label.</param>
        /// <param name="DATA">The structure that holds the labelling.</param>
        public static void LABEL_DATATABLE_COLUMNS(ref DataTable TABLE, RECIPE_DATA DATA)
        {
            if (DATA.a_col >= 0)
                TABLE.Columns[DATA.a_col].ColumnName = "A";
            if (DATA.b_col >= 0)
                TABLE.Columns[DATA.b_col].ColumnName = "B";
            if (DATA.c_col >= 0)
                TABLE.Columns[DATA.c_col].ColumnName = "C";
            if (DATA.d_col >= 0)
                TABLE.Columns[DATA.d_col].ColumnName = "D";
            if (DATA.e_col >= 0)
                TABLE.Columns[DATA.e_col].ColumnName = "E";
            if (DATA.f_col >= 0)
                TABLE.Columns[DATA.f_col].ColumnName = "F";
            if (DATA.g_col >= 0)
                TABLE.Columns[DATA.g_col].ColumnName = "G";
            if (DATA.h_col >= 0)
                TABLE.Columns[DATA.h_col].ColumnName = "H";
            if (DATA.i_col >= 0)
                TABLE.Columns[DATA.i_col].ColumnName = "I";
            if (DATA.j_col >= 0)
                TABLE.Columns[DATA.j_col].ColumnName = "J";
            if (DATA.k_col >= 0)
                TABLE.Columns[DATA.k_col].ColumnName = "K";
            if (DATA.l_col >= 0)
                TABLE.Columns[DATA.l_col].ColumnName = "L";
            if (DATA.m_col >= 0)
                TABLE.Columns[DATA.m_col].ColumnName = "M";
            if (DATA.timestamp_col >= 0)
                TABLE.Columns[DATA.timestamp_col].ColumnName = "TimeStamp";
        }
    }
}
