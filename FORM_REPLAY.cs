using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TyrannosaurusPlex
{
    public partial class FORM_REPLAY : Form
    {
        public FORM_REPLAY()
        {
            InitializeComponent();
            FORM_MAIN.DONE_REPLAYING += new EventHandler(CLOSE);
        }

        private void FORM_REPLAY_Load(object sender, EventArgs e)
        {
            Point LOCATION = new Point();
            LOCATION.X = 0;
            Rectangle RESOLUTION = Screen.PrimaryScreen.Bounds;
            Rectangle THIS_FORM = this.Bounds;
            LOCATION.Y = RESOLUTION.Height - THIS_FORM.Height - 50;
            this.DesktopLocation = LOCATION;
        }
        private void CLOSE(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
