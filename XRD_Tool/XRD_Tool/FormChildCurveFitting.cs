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
    public partial class FormChildCurveFitting : Form
    {
        public FormMDIParent myParentForm;
        public XrdConfig myConfig;

        public FormChildCurveFitting(FormMDIParent form)
        {
            myParentForm = form;
            myConfig = form.myConfig;


            InitializeComponent();
        }

        private void FormChildCurveFitting_Load(object sender, EventArgs e)
        {
            myConfig.ReadPearson();

            if (0 == myConfig.FunctionIndex)
            {
                radioButton_PearsonVII.Checked = true;
            }
            else if (1 == myConfig.FunctionIndex)
            {
                radioButton_PearsonVIIJade.Checked = true;
            }
            else if (2 == myConfig.FunctionIndex)
            {
                radioButton_PearsonVIIJade3Step.Checked = true;
            }
            else
            {
                radioButton_PearsonVII.Checked = true;
                myConfig.FunctionIndex = 0;
            }

            if (1 == myConfig.CustomExpEnabled)
            {
                checkBox_CustomExp.Checked = true;
                textBox_Exp.Text = myConfig.n.ToString();
                textBox_S.Text = myConfig.s.ToString();
            }
            else
            {
                checkBox_CustomExp.Checked = false;
            }

            if (0 == myConfig.BackgroudType)
            {
                radioButton_DefaultBG.Checked = true;
            }
            else if (1 == myConfig.BackgroudType)
            {
                radioButton_85BG.Checked = true;
            }
            else if (2 == myConfig.BackgroudType)
            {
                radioButton_CurveFitBG.Checked = true;
            }
            else
            {
                radioButton_DefaultBG.Checked = true;
                myConfig.BackgroudType = 0;
            }
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            try 
            {
                if (radioButton_PearsonVII.Checked)
                {
                    myConfig.FunctionIndex = 0;
                }
                else if (radioButton_PearsonVIIJade.Checked)
                {
                    myConfig.FunctionIndex = 1;
                }
                else if (radioButton_PearsonVIIJade3Step.Checked)
                {
                    myConfig.FunctionIndex = 2;
                }
                else
                {
                    myConfig.FunctionIndex = 0;
                }

                if (1 == myConfig.CustomExpEnabled)
                {
                    myConfig.n = Convert.ToDouble(textBox_Exp.Text);
                    myConfig.s = Convert.ToDouble(textBox_S.Text);
                }

                if (radioButton_DefaultBG.Checked)
                {
                    myConfig.BackgroudType = 0;
                }
                else if (radioButton_85BG.Checked)
                {
                    myConfig.BackgroudType = 1;
                }
                else if (radioButton_CurveFitBG.Checked)
                {
                    myConfig.BackgroudType = 2;
                }
                else
                {
                    myConfig.BackgroudType = 0;
                }

                myConfig.SavePearson();

                this.Hide();
            }
            catch (Exception ex)
            {
                //myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void checkBox_CustomExp_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_CustomExp.Checked)
            {
                myConfig.CustomExpEnabled = 1;

                label_Exp.Visible = true;
                label_S.Visible = true;
                textBox_Exp.Visible = true;
                textBox_S.Visible = true;
            }
            else
            {
                myConfig.CustomExpEnabled = 0;

                label_Exp.Visible = false;
                label_S.Visible = false;
                textBox_Exp.Visible = false;
                textBox_S.Visible = false;
            }
        }
    }
}
