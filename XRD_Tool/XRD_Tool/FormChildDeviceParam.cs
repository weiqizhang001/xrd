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
    public partial class FormChildDeviceParam : Form
    {
        public FormMDIParent myParentForm;
        public SerialPortCommon myUart;  
        public XrdConfig myConfig;
        public MeasureApi myApi;
        public IniEdit iniE = new IniEdit();
        public IniEdit iniE1 = new IniEdit();

        public FormChildDeviceParam(FormMDIParent form)
        {
            myParentForm = form;
            myUart = form.myUart;
            myConfig = form.myConfig;
            myApi = form.myApi;

            iniE.IniEditConfig("数据字典.ini");
            iniE1.IniEditConfig("参数设置.ini");
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

        /// <summary>
        /// load combox
        /// </summary>
        public void LoadEV()
        {
            bool bl = false;
            comboBox1.Items.Clear();
            string[] strname = iniE.GetSections();
            int i = 0;
            for (i = 0; i < strname.Length; i++)
            {
                comboBox1.Items.Add(strname[i].ToString());
                if (strname[i].ToString() == iniE1.GetVlueWithKey("参数设置", "name"))
                {
                    bl = true;
                    comboBox1.SelectedIndex = i;
                }
            }


            if (bl)
            {
                
            }
            else
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// load参数
        /// </summary>
        public void LoadCS()
        {
            textBoxZeroPointTS.Text = iniE1.GetVlueWithKey("参数设置", "TS");
            textBoxZeroPointTD.Text = iniE1.GetVlueWithKey("参数设置", "TD");
            textBoxZeroPointA.Text = iniE1.GetVlueWithKey("参数设置", "A");
            textBoxZeroPointB.Text = iniE1.GetVlueWithKey("参数设置", "B");
            textBoxZeroPointX.Text = iniE1.GetVlueWithKey("参数设置", "X");
            textBoxZeroPointY.Text = iniE1.GetVlueWithKey("参数设置", "Y");
            textBoxZeroPointZ.Text = iniE1.GetVlueWithKey("参数设置", "Z");
            textBoxE.Text = iniE1.GetVlueWithKey("参数设置", "E");
            textBoxV.Text = iniE1.GetVlueWithKey("参数设置", "V");
            comboBox1.Text = iniE1.GetVlueWithKey("参数设置", "name");
        }
        private void FormChildDev_Load(object sender, EventArgs e)
        {
            try
            {
                //textBoxZeroPointTS.Text = myConfig.ZeroPointMotorTS[myConfig.DetectorType].ToString();
                //textBoxZeroPointTD.Text = myConfig.ZeroPointMotorTD[myConfig.DetectorType].ToString();

                //textBoxZeroPointA.Text = myConfig.ZeroPointAlpha[myConfig.DetectorType].ToString();
                //textBoxZeroPointB.Text = myConfig.ZeroPointBeta[myConfig.DetectorType].ToString();
                //textBoxZeroPointX.Text = myConfig.ZeroPointX[myConfig.DetectorType].ToString();
                //textBoxZeroPointY.Text = myConfig.ZeroPointY[myConfig.DetectorType].ToString();
                //textBoxZeroPointZ.Text = myConfig.ZeroPointZ[myConfig.DetectorType].ToString();

                LoadEV();

                LoadCS();
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
                iniE1.SaveValueWithKey("参数设置", "TS", textBoxZeroPointTS.Text);
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
                iniE1.SaveValueWithKey("参数设置", "TD", textBoxZeroPointTD.Text);
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
                iniE1.SaveValueWithKey("参数设置", "A", textBoxZeroPointA.Text);
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
                iniE1.SaveValueWithKey("参数设置", "B", textBoxZeroPointB.Text);
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
                iniE1.SaveValueWithKey("参数设置", "X", textBoxZeroPointX.Text);
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
                iniE1.SaveValueWithKey("参数设置", "Y", textBoxZeroPointY.Text);
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
                iniE1.SaveValueWithKey("参数设置", "Z", textBoxZeroPointZ.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 联动  E/V
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBox1.Text!="")
                {
                    textBoxE.Text = iniE.GetVlueWithKey(comboBox1.Text, "E");
                    textBoxV.Text = iniE.GetVlueWithKey(comboBox1.Text, "v");
                }
                textBoxE.ReadOnly = true;
                textBoxV.ReadOnly = true;
                iniE1.SaveValueWithKey("参数设置", "name", comboBox1.Text);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("联动数据报错");
            }
        }

        private void textBoxE_TextChanged(object sender, EventArgs e)
        {
            try
            {
                iniE1.SaveValueWithKey("参数设置", "E", textBoxE.Text);
                
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
        }

        private void textBoxV_TextChanged(object sender, EventArgs e)
        {
            try
            {
                
                iniE1.SaveValueWithKey("参数设置", "V", textBoxV.Text);
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.ToString());
            }
        }

        private void linkLabel删除_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormAddCL fl = new FormAddCL(1, comboBox1.Text);
                fl.StartPosition = FormStartPosition.CenterScreen;
                fl.ShowDialog();
                LoadEV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void linkLabelEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormAddCL f1 = new FormAddCL(2, comboBox1.Text);
                f1.StartPosition = FormStartPosition.CenterScreen;
                f1.ShowDialog();
                LoadEV();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxE_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    iniE.SaveValueWithKey(comboBox1.Text, "E", textBoxE.Text);
                    textBoxE.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void textBoxV_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)13)
                {
                    iniE.SaveValueWithKey(comboBox1.Text, "V", textBoxV.Text);
                    textBoxV.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void linkLabel新增_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                FormAddCL fadd = new FormAddCL(0,"");
                fadd.StartPosition = FormStartPosition.CenterScreen;
                fadd.ShowDialog();
                LoadEV();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

    }
}
