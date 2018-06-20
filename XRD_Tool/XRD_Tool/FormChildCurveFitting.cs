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
        public int CurveFitFunctionIndex;   // 0:PearsonVII 1：PearsonVII参考Jade6 2：PearsonVII参考Jade6 with 3 steps
        public bool CustomExpEnabled;       // true: 指定exponent  false: 不指定exponent
        public double PearsonExponent;
        public double PearsonSkewness;
        public int BackgroudType;           // 0:不扣除背景 1：85%扣除背景 2：曲线水平拟合扣除背景


        public FormChildCurveFitting()
        {
            InitializeComponent();
        }

        private void FormChildCurveFitting_Load(object sender, EventArgs e)
        {
            radioButton_PearsonVII.Checked = true;
            radioButton_DefaultBG.Checked = true;
            checkBox_CustomExp.Checked = false;
        }

        private void button_Ok_Click(object sender, EventArgs e)
        {
            try 
            {
                if (radioButton_PearsonVII.Checked)
                {
                    CurveFitFunctionIndex = 0;
                }
                else if (radioButton_PearsonVIIJade.Checked)
                {
                    CurveFitFunctionIndex = 1;
                }
                else if (radioButton_PearsonVIIJade3Step.Checked)
                {
                    CurveFitFunctionIndex = 2;
                }
                else
                {
                    CurveFitFunctionIndex = 0;
                }

                if (CustomExpEnabled)
                {
                    PearsonExponent = Convert.ToDouble(textBox_Exp.Text);
                    PearsonSkewness = Convert.ToDouble(textBox_S.Text);
                }

                if (radioButton_DefaultBG.Checked)
                {
                    BackgroudType = 0;
                }
                else if (radioButton_85BG.Checked)
                {
                    BackgroudType = 1;
                }
                else if (radioButton_CurveFitBG.Checked)
                {
                    BackgroudType = 2;
                }
                else
                {
                    BackgroudType = 0;
                }

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
                CustomExpEnabled = true;

                label_Exp.Visible = true;
                label_S.Visible = true;
                textBox_Exp.Visible = true;
                textBox_S.Visible = true;
            }
            else
            {
                CustomExpEnabled = false;

                label_Exp.Visible = false;
                label_S.Visible = false;
                textBox_Exp.Visible = false;
                textBox_S.Visible = false;
            }
        }
    }
}
