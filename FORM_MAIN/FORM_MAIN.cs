using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FORBES.LOGGER_NAMESPACE;
using FORBES.KEY_LOGGER_NAMESPACE;
using System.Diagnostics;


namespace TyrannosaurusPlex
{
    public partial class FORM_MAIN : Form
    {
        LOGGER EVENTS = new LOGGER("Form Log");
        public FORM_MAIN()
        {
            EVENTS.LOG_MESSAGE(1, "INITIALIZE");
            InitializeComponent();
            KEY_LOGGER_SETUP();
            DB_SETUP();
        }
        private void MENU_ITEM_TOOL_STRIP_Click(object sender, EventArgs e)
        {
            bool FORM_EXISTS = false;
            FormCollection OPEN_FORMS = Application.OpenForms;

            foreach (Form FORM in OPEN_FORMS)
            {
                if (FORM.Name == "FORM_LOGS")
                {
                    FORM.BringToFront();
                    FORM_EXISTS = true;
                    break;
                }
            }
            if(!FORM_EXISTS)
            {
                FORM_LOGS LOG_FORM = new FORM_LOGS();
                LOG_FORM.Show();
            }
        }

        private void FORM_MAIN_Load(object sender, EventArgs e)
        {
            CONNECT_TO_DB(null, null);
        }
    }
}
