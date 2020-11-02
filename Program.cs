using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TyrannosaurusPlex
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FORM_MAIN());
        }
    }
    public class RECIPE_DATA : EventArgs
    {
        public string part_number { get; set; }
        public string checksheet_type { get; set; }
        public string key_sequence { get; set; }
        public string csv_location { get; set; }
        public int timestamp_col { get; set; }
        public int a_col { get; set; }
        public int b_col { get; set; }
        public int c_col { get; set; }
        public int d_col { get; set; }
        public int e_col { get; set; }
        public int f_col { get; set; }
        public int g_col { get; set; }
        public int h_col { get; set; }
        public int i_col { get; set; }
        public int j_col { get; set; }
        public int k_col { get; set; }
        public int l_col { get; set; }
        public int m_col { get; set; }
    } //Create a structure we'll pass back with a invokation of the DATA_READY event.

}