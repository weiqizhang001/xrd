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
    public partial class FormChildDeviceCtrl : Form
    {
        public FormChildDeviceCtrl()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

        private void FormChildDevCtrl_Load(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormChildDevCtrl_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormChildDevCtrl_FormClosed(object sender, FormClosedEventArgs e)
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
