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
    public partial class FormWarmUpProgress : Form
    {
        public XrdConfig myConfig;        
        public SerialPortCommon myUart;
        public MeasureApi myApi;
        public int timerCount = 0;

        public FormWarmUpProgress(FormDeviceInit form)
        {
            this.myUart = form.myUart;
            this.myConfig = form.myConfig;
            this.myApi = form.myApi;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                timerWarmUp.Enabled = false;

                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormWarmUpProgress_Load(object sender, EventArgs e)
        {
            try
            {
                timerWarmUp.Interval = 1000;
                timerWarmUp.Enabled = true;
                label2.Text = myConfig.WarmUpWaitSec.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void timerWarmUp_Tick(object sender, EventArgs e)
        {
            try
            {
                timerCount++;
                label2.Text = (myConfig.WarmUpWaitSec - timerCount).ToString();
                if (timerCount >= myConfig.WarmUpWaitSec)
                {
                    timerWarmUp.Enabled = false;
                    this.Close();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormWarmUpProgress_FormClosed(object sender, FormClosedEventArgs e)
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

        private void buttonExit_Click(object sender, EventArgs e)
        {
            try
            {
                timerWarmUp.Enabled = false;

                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
