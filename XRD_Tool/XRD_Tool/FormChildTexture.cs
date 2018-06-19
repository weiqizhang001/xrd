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
    public partial class FormChildTexture : Form
    {
        public FormMDIParent myParentForm;
        public SerialPortCommon myUart;
        public XrdConfig myConfig;
        public MeasureApi myApi;
        public IniEdit iniE = new IniEdit();

        public FormChildTexture(FormMDIParent form)
        {
            myParentForm = form;
            myUart = form.myUart;
            myConfig = form.myConfig;
            myApi = form.myApi;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            
        }

        private void FormChildTexture_Load(object sender, EventArgs e)
        {
            try
            {
                myApi.MeasureMethod = myConfig.MeasureMethod;


                iniE.IniEditConfig("ZG_Config.ini");//建立连接
                checkBoxExpert.Checked = false;
                //设置专家模式或通用模式
                checkBoxExpert_CheckedChanged(null, null);
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }   
        }

        private void FormChildTexture_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormChildTexture_FormClosed(object sender, FormClosedEventArgs e)
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

        

        private void checkBoxExpert_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LoadConfig(checkBoxExpert.Checked);//加载配置文件

                if (checkBoxExpert.CheckState == CheckState.Checked)
                {
                    labelScanMethod.Visible = false;
                    comboBoxScanMethod.Visible = false;

                    textBoxPsiStep.ReadOnly = false;
                    textBoxPsiStartAngle.ReadOnly = false;
                    textBoxPsiStoptAngle.ReadOnly = false;
                    textBoxPhiSpeed.ReadOnly = false;
                    textBoxMeasureTime.ReadOnly = false;
                }
                else
                {
                    comboBoxScanMethod.SelectedIndex = 0;//默认选择常规扫描

                    labelScanMethod.Visible = true;
                    comboBoxScanMethod.Visible = true;

                    textBoxPsiStep.ReadOnly = true;
                    textBoxPsiStartAngle.ReadOnly = true;
                    textBoxPsiStoptAngle.ReadOnly = true;
                    textBoxPhiSpeed.ReadOnly = true;
                    textBoxMeasureTime.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="value">是否是高级模式</param>
        public void LoadConfig(bool value)
        {
            if (value)
            {
                textBoxSampleSn.Text = iniE.GetVlueWithKey("Expert", "SampeSn");
                textBoxFaceExp.Text = iniE.GetVlueWithKey("Expert", "FaceExp");
                textBoxPeakDegree.Text = iniE.GetVlueWithKey("Expert", "PeakDegree");
                textBoxTubeVoltage.Text = iniE.GetVlueWithKey("Expert", "TubeVoltage");
                textBoxTubeCurrent.Text = iniE.GetVlueWithKey("Expert", "TubeCurrent");
                textBox1.Text = iniE.GetVlueWithKey("Expert", "Mode");
                textBoxPsiStep.Text = iniE.GetVlueWithKey("Expert", "PsiStep");
                textBoxPsiStartAngle.Text = iniE.GetVlueWithKey("Expert", "PsiStartAngle");
                textBoxPsiStoptAngle.Text = iniE.GetVlueWithKey("Expert", "PsiStoptAngle");
                textBoxPhiSpeed.Text = iniE.GetVlueWithKey("Expert", "PhiSpeed");
                textBoxMeasureTime.Text = iniE.GetVlueWithKey("Expert", "MeasureTime");
            }
            else
            {
                textBoxSampleSn.Text = iniE.GetVlueWithKey("General", "SampeSn");
                textBoxFaceExp.Text = iniE.GetVlueWithKey("General", "FaceExp");
                textBoxPeakDegree.Text = iniE.GetVlueWithKey("General", "PeakDegree");
                textBoxTubeVoltage.Text = iniE.GetVlueWithKey("General", "TubeVoltage");
                textBoxTubeCurrent.Text = iniE.GetVlueWithKey("General", "TubeCurrent");
                textBox1.Text = iniE.GetVlueWithKey("General", "Mode");
                textBoxPsiStep.Text = iniE.GetVlueWithKey("General", "PsiStep");
                textBoxPsiStartAngle.Text = iniE.GetVlueWithKey("General", "PsiStartAngle");
                textBoxPsiStoptAngle.Text = iniE.GetVlueWithKey("General", "PsiStoptAngle");
                textBoxPhiSpeed.Text = iniE.GetVlueWithKey("General", "PhiSpeed");
                textBoxMeasureTime.Text = iniE.GetVlueWithKey("General", "MeasureTime");
            }
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="value">是否是高级模式</param>
        public void SaveConfig(bool value)
        {
            if (value)
            {
                iniE.SaveValueWithKey("Expert", "SampeSn", textBoxSampleSn.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "FaceExp", textBoxFaceExp.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "PeakDegree", textBoxPeakDegree.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "TubeVoltage", textBoxTubeVoltage.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "TubeCurrent", textBoxTubeCurrent.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "Mode", textBox1.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "PsiStep", textBoxPsiStep.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "PsiStartAngle", textBoxPsiStartAngle.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "PsiStoptAngle", textBoxPsiStoptAngle.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "textBoxPhiSpeed", textBoxPhiSpeed.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "MeasureTime", textBoxMeasureTime.Text.ToString().Trim());
            }
            else
            {
                iniE.SaveValueWithKey("General", "SampeSn", textBoxSampleSn.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "FaceExp", textBoxFaceExp.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "PeakDegree", textBoxPeakDegree.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "TubeVoltage", textBoxTubeVoltage.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "TubeCurrent", textBoxTubeCurrent.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "Mode", textBox1.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "PsiStep", textBoxPsiStep.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "PsiStartAngle", textBoxPsiStartAngle.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "PsiStoptAngle", textBoxPsiStoptAngle.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "textBoxPhiSpeed", textBoxPhiSpeed.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "MeasureTime", textBoxMeasureTime.Text.ToString().Trim());
            }
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckValue())//校验
                {
                    SaveConfig(checkBoxExpert.Checked);//保存参数
                }
                else
                {
                    MessageBox.Show("参数校验失败，请重新调整");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 校验数据规则
        /// 1.所有数据不能为空值
        /// 2.组数与样品编号组数一致
        /// 3.高级模式大于2组   通用模式只有一组数据
        /// </summary>
        /// <returns>校验结果</returns>
        public bool CheckValue()
        {
            //不能有空值
            if (textBoxSampleSn.Text=="")//样品编号为空代表没有数据
                return false;
            if (textBoxFaceExp.Text == "")//晶面
                return false;
            if (textBoxPeakDegree.Text=="")//峰位角度
                return false;
            if (textBoxTubeVoltage.Text=="")//管电压
                return false;
            if (textBoxTubeCurrent.Text == "")//管电流
                return false;
            if (textBox1.Text == "")//B/M
                return false;
            if (textBoxPsiStep.Text == "")//步宽
                return false;
            if (textBoxPsiStartAngle.Text == "")//起始角度
                return false;
            if (textBoxPsiStoptAngle.Text == "")//终止角度
                return false;
            if (textBoxPhiSpeed.Text == "")//测量速度
                return false;
            if (textBoxMeasureTime.Text == "")//测量时间
                return false;

            //获取组数
            string SampleSn=textBoxSampleSn.Text.ToString();//样品编号
            string[] strdata = SampleSn.Split(',');

           
            //组数与样品编号保持一致
            if (textBoxFaceExp.Text.Split(',').Length != strdata.Length)//晶面
                return false;
            if (textBoxPeakDegree.Text.Split(',').Length != strdata.Length)//峰位角度
                return false;
            if (textBoxTubeVoltage.Text.Split(',').Length != strdata.Length)//管电压
                return false;
            if (textBoxTubeCurrent.Text.Split(',').Length != strdata.Length)//管电流
                return false;
            if (textBox1.Text.Split(',').Length != strdata.Length)//B/M
                return false;
            if (textBoxPsiStep.Text.Split(',').Length != strdata.Length)//步宽
                return false;
            if (textBoxPsiStartAngle.Text.Split(',').Length != strdata.Length)//起始角度
                return false;
            if (textBoxPsiStoptAngle.Text.Split(',').Length != strdata.Length)//终止角度
                return false;
            if (textBoxPhiSpeed.Text.Split(',').Length != strdata.Length)//测量速度
                return false;
            if (textBoxMeasureTime.Text.Split(',').Length != strdata.Length)//测量时间
                return false;

            //两个模式之分
            if (checkBoxExpert.Checked == true)//高级模式
            {
                if (strdata.Length > 0)
                {

                }
                else//高级模式下至少是两组数据
                {
                    return false;
                }
            }
            else//通用模式
            {
                if (strdata.Length > 1)//通用模式下只能有一组数据
                {
                    return false;
                }
                else
                {

                }
            }
            

            return true;
        }
        /// <summary>
        /// 通用模式选择扫描方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBoxScanMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (comboBoxScanMethod.SelectedIndex)
                {
                    case 0://常规扫描
                        textBoxPsiStep.Text = "5";
                        textBoxPsiStartAngle.Text = "0";
                        textBoxPsiStoptAngle.Text = "70";
                        textBoxPhiSpeed.Text = "5";
                        textBoxMeasureTime.Text = "1";
                        break;
                    case 1://快速扫描
                        textBoxPsiStep.Text = "5";
                        textBoxPsiStartAngle.Text = "0";
                        textBoxPsiStoptAngle.Text = "70";
                        textBoxPhiSpeed.Text = "10";
                        textBoxMeasureTime.Text = "0.5";
                        break;
                    case 2://高强度扫描
                        textBoxPsiStep.Text = "5";
                        textBoxPsiStartAngle.Text = "0";
                        textBoxPsiStoptAngle.Text = "70";
                        textBoxPhiSpeed.Text = "1";
                        textBoxMeasureTime.Text = "5";
                        break;
                    default:
                        textBoxPsiStep.Text = "0";
                        textBoxPsiStartAngle.Text = "0";
                        textBoxPsiStoptAngle.Text = "0";
                        textBoxPhiSpeed.Text = "0";
                        textBoxMeasureTime.Text = "0";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


    }
}
