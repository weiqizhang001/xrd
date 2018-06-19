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
    public partial class FormHighVoltageConfirm : Form
    {
        public double showVoltage;
        public double showCurrent;

        public FormHighVoltageConfirm(double v, double c)
        {
            showVoltage = v;
            showCurrent = c;

            InitializeComponent();
        }

        private void FormHighVoltageConfirm_Load(object sender, EventArgs e)
        {
            textBoxHighVoltage.Text = showVoltage.ToString();
            textBoxCurrent.Text = showCurrent.ToString();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                //myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        
    }
}
