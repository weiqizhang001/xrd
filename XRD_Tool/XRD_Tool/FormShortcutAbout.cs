using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XRD_Tool
{
    public partial class FormShortcutAbout : Form
    {
        public FormShortcutAbout()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

        private void FormShortcutAbout_Load(object sender, EventArgs e)
        {

        }

        private void FormShortcutAbout_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormShortcutAbout_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
