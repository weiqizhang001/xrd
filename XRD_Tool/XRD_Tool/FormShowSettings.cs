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
    public partial class FormShowSettings : Form
    {
        public XrdConfig myConfig;
        public FormChildMeasure myParentForm;

        public FormShowSettings(FormChildMeasure form)
        {
            myParentForm = form;
            myConfig = form.myConfig;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
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
                if (0 == myConfig.TargetType)
                {
                    labelTargetType.Text = "Cr";
                }
                else if (1 == myConfig.TargetType)
                {
                    labelTargetType.Text = "Cu";
                }
                else if (2 == myConfig.TargetType)
                {
                    labelTargetType.Text = "Mn";
                }

                if (0 == myConfig.DetectorType)
                {
                    labelDetectType.Text = "SDD探测器";
                }
                else
                {
                    labelDetectType.Text = "线阵探测器";
                }

                labelSampleName.Text = myParentForm.textBoxSampleName.Text;
                labelSampleSn.Text = myParentForm.textBoxSampleSn.Text;
                labelMeasureMethod.Text = myParentForm.comboBoxMeaMethod.Text;

                labelPeakDegree.Text = myParentForm.textBox_StartDegree.Text;
                labelDegreeRange.Text = myParentForm.textBox_StopDegree.Text;
                labelMeasureStep.Text = myParentForm.textBox_MeaStep.Text;
                labelMeasureTime.Text = myParentForm.textBoxMeaTime.Text;
                labelTubeVoltage.Text = myParentForm.textBoxTubeVoltage.Text;
                labelTubeCurrent.Text = myParentForm.textBoxTubeCurrent.Text;

                labelPsi.Text = myParentForm.textBoxAnglePsi.Text;
                labelX.Text = myParentForm.textBoxAngleX.Text;
                labelY.Text = myParentForm.textBoxAngleY.Text;
                labelZ.Text = myParentForm.textBoxAngleZ.Text;
                labelBeta.Text = myParentForm.textBoxAngleBeta.Text;
                labelAlpha.Text = myParentForm.textBoxAngleAlpha.Text;

                if (myParentForm.checkBoxFiveAxis.CheckState == CheckState.Unchecked)
                {
                    panelWhite.Visible = true;
                    panelWhite.Location = new System.Drawing.Point(319, 257);
                    panelWhite.Size = new System.Drawing.Size(263, 244);
                }
                else
                {
                    panelWhite.Visible = false;
                    if (myParentForm.comboBoxMeaMethod.SelectedIndex == 0)
                    {
                        panelWhite.Visible = true;
                        panelWhite.Location = new System.Drawing.Point(319, 453);
                        panelWhite.Size = new System.Drawing.Size(263, 51);
                    }
                    else
                    {
                        panelWhite.Visible = false;
                    }
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        private void FormShowSettings_FormClosed(object sender, FormClosedEventArgs e)
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
