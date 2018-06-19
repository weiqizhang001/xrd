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
    public partial class FormChildDevParam : Form
    {
        public FormMDIParent myParentForm;
        public SerialPortCommon myUart;  
        public XrdConfig myConfig;
        public MeasureApi myApi;

        public FormChildDevParam(FormMDIParent form)
        {
            myParentForm = form;
            myUart = form.myUart;
            myConfig = form.myConfig;
            myApi = form.myApi;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

        private void FormChildDev_Load(object sender, EventArgs e)
        {
            try
            {
                textBoxZeroPointTS.Text = myConfig.ZeroPointMotorTS[myConfig.DetectorType].ToString();
                textBoxZeroPointTD.Text = myConfig.ZeroPointMotorTD[myConfig.DetectorType].ToString();

                textBoxZeroPointA.Text = myConfig.ZeroPointAlpha[myConfig.DetectorType].ToString();
                textBoxZeroPointB.Text = myConfig.ZeroPointBeta[myConfig.DetectorType].ToString();
                textBoxZeroPointX.Text = myConfig.ZeroPointX[myConfig.DetectorType].ToString();
                textBoxZeroPointY.Text = myConfig.ZeroPointY[myConfig.DetectorType].ToString();
                textBoxZeroPointZ.Text = myConfig.ZeroPointZ[myConfig.DetectorType].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }   
        }

        private void textBoxZeroPointTS_TextChanged(object sender, EventArgs e)
        {
            try
            {
                myConfig.ZeroPointMotorTS[myConfig.DetectorType] = Convert.ToDouble(textBoxZeroPointTS.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxZeroPointTD_TextChanged(object sender, EventArgs e)
        {
            try
            {
                myConfig.ZeroPointMotorTD[myConfig.DetectorType] = Convert.ToDouble(textBoxZeroPointTD.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxZeroPointA_TextChanged(object sender, EventArgs e)
        {
            try
            {
                myConfig.ZeroPointAlpha[myConfig.DetectorType] = Convert.ToDouble(textBoxZeroPointA.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxZeroPointB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                myConfig.ZeroPointBeta[myConfig.DetectorType] = Convert.ToDouble(textBoxZeroPointB.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxZeroPointX_TextChanged(object sender, EventArgs e)
        {
            try
            {
                myConfig.ZeroPointX[myConfig.DetectorType] = Convert.ToDouble(textBoxZeroPointX.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxZeroPointY_TextChanged(object sender, EventArgs e)
        {
            try
            {
                myConfig.ZeroPointY[myConfig.DetectorType] = Convert.ToDouble(textBoxZeroPointY.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxZeroPointZ_TextChanged(object sender, EventArgs e)
        {
            try
            {
                myConfig.ZeroPointZ[myConfig.DetectorType] = Convert.ToDouble(textBoxZeroPointZ.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void FormChildDevParam_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormChildDevParam_FormClosed(object sender, FormClosedEventArgs e)
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
