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
    public partial class FormChildLogo : Form
    {
        public FormChildLogo()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

        private void FormChildLogo_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormChildLogo_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormChildLogo_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
               // System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
