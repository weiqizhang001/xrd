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
    public partial class FormShowZGSettings : Form
    {
        public XrdConfig myConfig;
        public FormChildMeasure myParentForm;
        public IniEdit iniE = new IniEdit();
        bool flag = false;

        public FormShowZGSettings(FormChildMeasure form)
        {
            myParentForm = form;
            myConfig = form.myConfig;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }
        public FormShowZGSettings(bool _flag)
        {
            flag = _flag;
            InitializeComponent();
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
        private void FormShowSettings_Load(object sender, EventArgs e)
        {
            try
            {

                iniE.IniEditConfig("ZG_Config.ini");//建立连接

                if (flag)
                {
                    labelYPMC.Text = iniE.GetVlueWithKey("Expert", "SampeName");
                    labelSMFF.Text = "null";
                    labelYPBH.Text = iniE.GetVlueWithKey("Expert", "SampeSn");
                    labelJMZS.Text = iniE.GetVlueWithKey("Expert", "FaceExp");
                    labelFWJD.Text = iniE.GetVlueWithKey("Expert", "PeakDegree");
                    labelGDY.Text = iniE.GetVlueWithKey("Expert", "TubeVoltage");
                    labelGDL.Text = iniE.GetVlueWithKey("Expert", "TubeCurrent");
                    labelMS.Text = iniE.GetVlueWithKey("Expert", "Mode");
                    labelBK.Text = iniE.GetVlueWithKey("Expert", "PsiStep");
                    labelQSJD.Text = iniE.GetVlueWithKey("Expert", "PsiStartAngle");
                    labelZZJD.Text = iniE.GetVlueWithKey("Expert", "PsiStoptAngle");
                    labelCLSD.Text = iniE.GetVlueWithKey("Expert", "PhiSpeed");
                    labelCLSJ.Text = iniE.GetVlueWithKey("Expert", "MeasureTime");
                }
                else
                {
                    labelYPMC.Text = iniE.GetVlueWithKey("General", "SampeName");
                    labelSMFF.Text = iniE.GetVlueWithKey("General", "ScanMethod");
                    labelYPBH.Text = iniE.GetVlueWithKey("General", "SampeSn");
                    labelJMZS.Text = iniE.GetVlueWithKey("General", "FaceExp");
                    labelFWJD.Text = iniE.GetVlueWithKey("General", "PeakDegree");
                    labelGDY.Text = iniE.GetVlueWithKey("General", "TubeVoltage");
                    labelGDL.Text = iniE.GetVlueWithKey("General", "TubeCurrent");
                    labelMS.Text = iniE.GetVlueWithKey("General", "Mode");
                    labelBK.Text = iniE.GetVlueWithKey("General", "PsiStep");
                    labelQSJD.Text = iniE.GetVlueWithKey("General", "PsiStartAngle");
                    labelZZJD.Text = iniE.GetVlueWithKey("General", "PsiStoptAngle");
                    labelCLSD.Text = iniE.GetVlueWithKey("General", "PhiSpeed");
                    labelCLSJ.Text = iniE.GetVlueWithKey("General", "MeasureTime");
                }
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
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
