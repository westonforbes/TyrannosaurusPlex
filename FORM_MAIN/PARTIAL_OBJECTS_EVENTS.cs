using System;
using System.Data;
using FORBES;

//VISUAL STUDIO DESIGNER BLOCKER-------------------------------------------------------------------------------------------------------------
[System.ComponentModel.DesignerCategory("")] //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
public class DUMMY_CLASS_1 { } //Prevents Visual Studio from trying to open this file in Forms Designer. Not needed at runtime.
//VISUAL STUDIO DESIGNER BLOCKER-------------------------------------------------------------------------------------------------------------

namespace TyrannosaurusPlex
{
    public partial class FORM_MAIN
    {
        //Objects
        DataTable RECIPE_TABLE_LOCAL = new DataTable(); //Holds a local copy of the recipe table.

        MYSQL_COMS DB = new MYSQL_COMS(Properties.DB.Default.HOST, 
                                       Properties.DB.Default.DATABASE,
                                       Properties.DB.Default.USER,
                                       Properties.DB.Default.PASSWORD); //Instance of the database connection.
                                                                        
        //Events
        public static event EventHandler OK_TO_CLOSE_ADD_RECIPE_FORM; //Event used to notify the FORM_ADD_RECIPE that its ok to close.
        //This is needed because FORM_MAIN does some processing on form close and there is an opprotunity where the user can cancel the
        //form closing. Therefor FORM_MAIN needs to call the shots.
    }

}

