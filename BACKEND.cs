using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using FORBES;

namespace TyrannosaurusPlex
{
    public static class BACKEND
    {
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
        /// This method moves data from a DataTable created in the image of the source CSV file into a injection table.
        /// </summary>
        /// <param name="SOURCE_CSV_TABLE"> This DataTable should be in the format of the CSV file but should have marked column names.</param>
        /// <param name="SOURCE_INJECTION_TABLE">This table must have a column named "Letter" and a column named "Value".
        /// The "Letter" column must be populated with all letters A thru Z all uppercase. One letter per row.</param>
        public static void MOVE_CSV_DATA_INTO_INJECTION_TABLE(DataTable SOURCE_CSV_TABLE, DataTable SOURCE_INJECTION_TABLE)
        {
            EVENTS.LOG_MESSAGE(1, "ENTER");
            foreach (DataColumn COLUMN in SOURCE_CSV_TABLE.Columns) //For each column...
            {
                for (int i = (char)'A'; i <= (char)'Z'; i++) //Go through each letter...
                {
                    char i_AS_CHAR = (char)i; //Convert incrementer to a character.
                    string i_AS_STRING = i_AS_CHAR.ToString(); //Convert incrementer character to string.
                    if (COLUMN.ColumnName == i_AS_STRING) //Check if the incrementer letter code is the same as the current column header text...
                    {
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


    }
}
