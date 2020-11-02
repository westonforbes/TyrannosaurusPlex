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
    public partial class FORM_MAIN : Form
    {
        LOGGER EVENTS = new LOGGER("Form Log");
        public FORM_MAIN()
        {
            EVENTS.LOG_MESSAGE(1, "INITIALIZE");
            InitializeComponent();
            KEY_LOGGER_SETUP();
            INJECTION_TABLE_SETUP();
            DB_SETUP();
        }
    }
}
