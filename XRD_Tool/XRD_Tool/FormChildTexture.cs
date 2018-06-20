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

        public byte deviceStateBackup;

        // 用户输入参数
        public string[] SampleSn;
        public double[] FaceExp;
        public double[] PeakDegree;
        public double[] TubeVoltage;
        public double[] TubeCurrent;
        public string[] BM;
        public double[] PsiStep;
        public double[] PsiStartAngle;
        public double[] PsiStopAngle;
        public double[] PhiSpeed;
        public double[] MeasureTime;

        public int ChartRealTimeShowIndex = 0;  // 实时绘图的当前数据的行号的索引
        public volatile int MeasureIndex_Psi = 0; // Psi索引
        public volatile int GroupIndex_Psi = 0; // Psi索引
        public volatile bool MeasureStopFlag = false;   // 测量停止flag
        public volatile bool RecvDataFinishFlag = false; // 单组测量接收数据完成flag
        public System.Timers.Timer timerUartRecv;

        private System.Timers.Timer timerHighVoltage_10min; // 11.测量完成后，如果10分钟没有对高压进行操作，则关闭高压SKM0 0


        public FormChildTexture(FormMDIParent form)
        {
            myParentForm = form;
            myUart = form.myUart;
            myConfig = form.myConfig;
            myApi = form.myApi;

            timerHighVoltage_10min = new System.Timers.Timer(1000 * 60 * 10); // 10min
            timerHighVoltage_10min.Elapsed += new System.Timers.ElapsedEventHandler(timerHighVoltage_10min_Timeout);
            timerHighVoltage_10min.AutoReset = true;
            timerHighVoltage_10min.Enabled = false;

            // timer
            timerUartRecv = new System.Timers.Timer(10000);
            timerUartRecv.Elapsed += new System.Timers.ElapsedEventHandler(timerUartRecv_Timeout);
            timerUartRecv.AutoReset = true;
            timerUartRecv.Enabled = false;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;

            if (myUart.port_UART.IsOpen)
            {
                deviceStateBackup = myUart.DeviceState;
                myUart.DeviceState = DEVICE_STATE.SCAN;
                myUart.RegControl(this, UartRecv_Texture, DEVICE_STATE.SCAN);
            }
        }

        private void timerHighVoltage_10min_Timeout(object sender, EventArgs e)
        {
            timerHighVoltage_10min.Enabled = false;
            myUart.Pack_Debug_out(null, "HV 10min timeout" + "]");

            myApi.SendHighVoltageDown(0, 0);
            timerUartRecv.Interval = 1000 * 10;
            timerUartRecv.Enabled = true;
        }

        private void timerUartRecv_Timeout(object sender, EventArgs e)
        {
            try
            {
                timerUartRecv.Enabled = false;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
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
            try
            {
                myUart.DeviceState = deviceStateBackup;
                myUart.RemoveControl(this);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
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
                    textBoxPsiStopAngle.ReadOnly = false;
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
                    textBoxPsiStopAngle.ReadOnly = true;
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
                textBoxBM.Text = iniE.GetVlueWithKey("Expert", "Mode");
                textBoxPsiStep.Text = iniE.GetVlueWithKey("Expert", "PsiStep");
                textBoxPsiStartAngle.Text = iniE.GetVlueWithKey("Expert", "PsiStartAngle");
                textBoxPsiStopAngle.Text = iniE.GetVlueWithKey("Expert", "PsiStoptAngle");
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
                textBoxBM.Text = iniE.GetVlueWithKey("General", "Mode");
                textBoxPsiStep.Text = iniE.GetVlueWithKey("General", "PsiStep");
                textBoxPsiStartAngle.Text = iniE.GetVlueWithKey("General", "PsiStartAngle");
                textBoxPsiStopAngle.Text = iniE.GetVlueWithKey("General", "PsiStoptAngle");
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
                iniE.SaveValueWithKey("Expert", "Mode", textBoxBM.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "PsiStep", textBoxPsiStep.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "PsiStartAngle", textBoxPsiStartAngle.Text.ToString().Trim());
                iniE.SaveValueWithKey("Expert", "PsiStoptAngle", textBoxPsiStopAngle.Text.ToString().Trim());
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
                iniE.SaveValueWithKey("General", "Mode", textBoxBM.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "PsiStep", textBoxPsiStep.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "PsiStartAngle", textBoxPsiStartAngle.Text.ToString().Trim());
                iniE.SaveValueWithKey("General", "PsiStoptAngle", textBoxPsiStopAngle.Text.ToString().Trim());
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


                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                    groupBoxSettings.Enabled = false;
                    myParentForm.tableLayoutPanel1.Enabled = false;
                    myParentForm.tableLayoutPanel2.Enabled = false;

                    ChartRealTimeShowIndex = 0;
                    chartRealTime.Series["射线强度"].Points.Clear();// 清除实时绘图
                    dataGridViewClear(); // 清除数据显示

                    MeasureIndex_Psi = 0;
                    GroupIndex_Psi = 0;
                    RecvDataFinishFlag = false;
                    MeasureStopFlag = false;
                    myParentForm.toolStripStatusLabelMeasureStatus.Text = "测量开始";

                    myApi.MeasureMethod = 0; // 测量方法不区分同倾法/侧倾发，固定为0
                    myApi.AnglePsi = Array.ConvertAll(textBoxPsiStartAngle.Text.Split(','), double.Parse);
                    SampleSn = textBoxSampleSn.Text.Split(',');
                    FaceExp = Array.ConvertAll(textBoxFaceExp.Text.Split(','), double.Parse);    
                    PeakDegree = Array.ConvertAll(textBoxPeakDegree.Text.Split(','), double.Parse);
                    TubeVoltage = Array.ConvertAll(textBoxTubeVoltage.Text.Split(','), double.Parse);
                    TubeCurrent = Array.ConvertAll(textBoxTubeCurrent.Text.Split(','), double.Parse);
                    BM = textBoxBM.Text.Split(',');
                    PsiStep = Array.ConvertAll(textBoxPsiStep.Text.Split(','), double.Parse);
                    PsiStartAngle = Array.ConvertAll(textBoxPsiStartAngle.Text.Split(','), double.Parse);
                    PsiStopAngle = Array.ConvertAll(textBoxPsiStopAngle.Text.Split(','), double.Parse);
                    PhiSpeed = Array.ConvertAll(textBoxPhiSpeed.Text.Split(','), double.Parse);
                    MeasureTime = Array.ConvertAll(textBoxMeasureTime.Text.Split(','), double.Parse);
                    
                    ScanSetupStart();
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
        /// 数据显示区清除
        /// </summary>
        public void dataGridViewClear()
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void ScanSetupStart()
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Texture] Measure Start");

                int PsiCount = (int)((PsiStopAngle[GroupIndex_Psi] - PsiStartAngle[GroupIndex_Psi]) / PsiStep[GroupIndex_Psi]) + 1;
                myApi.AnglePsi = new double[PsiCount];

                for (int i = 0; i < PsiCount; i++)
                {
                    myApi.AnglePsi[i] = PsiStartAngle[GroupIndex_Psi] + i * PsiStep[GroupIndex_Psi];    
                }

                myApi.SendDevicePause();
                timerUartRecv.Interval = 1000 * 5;
                timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
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
            if (textBoxBM.Text == "")//B/M
                return false;
            if (textBoxPsiStep.Text == "")//步宽
                return false;
            if (textBoxPsiStartAngle.Text == "")//起始角度
                return false;
            if (textBoxPsiStopAngle.Text == "")//终止角度
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
            if (textBoxBM.Text.Split(',').Length != strdata.Length)//B/M
                return false;
            if (textBoxPsiStep.Text.Split(',').Length != strdata.Length)//步宽
                return false;
            if (textBoxPsiStartAngle.Text.Split(',').Length != strdata.Length)//起始角度
                return false;
            if (textBoxPsiStopAngle.Text.Split(',').Length != strdata.Length)//终止角度
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
                        textBoxPsiStopAngle.Text = "70";
                        textBoxPhiSpeed.Text = "5";
                        textBoxMeasureTime.Text = "1";
                        break;
                    case 1://快速扫描
                        textBoxPsiStep.Text = "5";
                        textBoxPsiStartAngle.Text = "0";
                        textBoxPsiStopAngle.Text = "70";
                        textBoxPhiSpeed.Text = "10";
                        textBoxMeasureTime.Text = "0.5";
                        break;
                    case 2://高强度扫描
                        textBoxPsiStep.Text = "5";
                        textBoxPsiStartAngle.Text = "0";
                        textBoxPsiStopAngle.Text = "70";
                        textBoxPhiSpeed.Text = "1";
                        textBoxMeasureTime.Text = "5";
                        break;
                    default:
                        textBoxPsiStep.Text = "0";
                        textBoxPsiStartAngle.Text = "0";
                        textBoxPsiStopAngle.Text = "0";
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



        public void UartRecv_Texture(byte[] text, int PackageLen)
        {
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;

            try
            {
                if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;
                        if (!MeasureStopFlag)
                        {
                            myApi.SendStartMeaurePostion();
                            timerUartRecv.Interval = 1000 * 30;
                            timerUartRecv.Enabled = true;
                        }
                        else
                        {
                            myApi.SendCloseShutter();
                        }
                    }
                }
                else if (DEVICE_CMD_ID.SET_START_MEA_POS == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;

                        myApi.SendHighVoltageUp(TubeVoltage[GroupIndex_Psi], TubeCurrent[GroupIndex_Psi]);
                        timerUartRecv.Interval = 1000 * 40;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_UP == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;

                        myApi.SendGetHighVoltage();
                        timerUartRecv.Interval = 1000 * 5;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.GET_HIGH_VOLTAGE == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        result = myApi.RecvGetHighVoltage(text);
                        timerUartRecv.Enabled = false;

                        if (result)
                        {
                            myApi.SendMeasureTime(MeasureTime[GroupIndex_Psi]);
                            timerUartRecv.Interval = 1000 * 5;
                            timerUartRecv.Enabled = true;
                        }
                    }
                }
                else if (DEVICE_CMD_ID.SET_MES_TIME == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;
                        myApi.SendSpeedB(PhiSpeed[GroupIndex_Psi]);
                        timerUartRecv.Interval = 1000 * 5;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.SET_B_SPEED == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;
                        if (0 == GroupIndex_Psi)
                        {
                            myApi.SendOpenShutter();
                            timerUartRecv.Interval = 1000 * 5;
                            timerUartRecv.Enabled = true;
                        }
                        else
                        {
                            myApi.SendAngleA(myApi.AnglePsi[MeasureIndex_Psi]);
                            timerUartRecv.Interval = 1000 * 30;
                            timerUartRecv.Enabled = true;
                        } 
                    }
                }
                else if (DEVICE_CMD_ID.OPEN_LIGHT_SHUTTER == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;

                        myApi.SendAngleA(myApi.AnglePsi[MeasureIndex_Psi]);
                        timerUartRecv.Interval = 1000 * 30;
                        timerUartRecv.Enabled = true;
                     }
                }
                else if (DEVICE_CMD_ID.SET_A_ANGLE == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;

                        myApi.SendAngleB(0);
                        timerUartRecv.Interval = 1000 * 30;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.SET_B_ANGLE == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;

                        myApi.SendStartCycleScan(360);
                        timerUartRecv.Interval = 1000 * 5;
                        timerUartRecv.Enabled = true;

                        myApi.RecvDataCount = 0;
                        myApi.RecvDataTable.Rows.Clear();
                        myApi.TotalDataCount = (int)(360 / PhiSpeed[GroupIndex_Psi]);
                        myUart.Pack_Debug_out(null, "[Texture] totalcount=" + myApi.TotalDataCount.ToString());
                        myApi.SSC_RecvDataReadIndex = 0;
                        myApi.SSC_RecvDataWriteIndex = 0;
                        myApi.SSC_AnalyzeDataCount = 0;

                        myApi.RecvDataTableSaveFileStart(myApi.AnglePsi[MeasureIndex_Psi], MeasureIndex_Psi);
                    }
                }
                else if (DEVICE_CMD_ID.START_SCAN == LastSendCmd)
                {
                    // save data
                    timerUartRecv.Enabled = false;
                    myApi.SSC_RecvDataAnalyze(text);
                    myApi.RecvDataTableUpdate(myApi.SSC_AnalyzeDataArray, myApi.SSC_AnalyzeDataCount);
                    myApi.RecvDataTableSaveFileProc(myApi.SSC_AnalyzeDataArray, myApi.SSC_AnalyzeDataCount);

                    myApi.RecvDataCount += (myApi.SSC_AnalyzeDataCount / 20);
                    myUart.Pack_Debug_out(null, "[Texture] analyzecount=" + (myApi.SSC_AnalyzeDataCount / 20).ToString() + "recvcount=" + myApi.RecvDataCount.ToString());

                    myApi.SSC_AnalyzeDataCount = 0;
                    Array.Clear(myApi.SSC_AnalyzeDataArray, 0, myApi.SSC_AnalyzeDataArray.Length);

                    if (myApi.RecvDataCount >= myApi.TotalDataCount)
                    {
                        Array.Clear(myApi.SSC_RecvDataArray, 0, myApi.SSC_RecvDataArray.Length);
                        myApi.SSC_RecvDataReadIndex = 0;
                        myApi.SSC_RecvDataWriteIndex = 0;

                        myApi.RecvDataTableSaveFileEnd();
                        RecvDataFinishFlag = true;
                    }
                }
                else if (DEVICE_CMD_ID.CLOSE_LIGHT_SHUTTER == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;
                        myApi.SendHighVoltageDown(myApi.DefaultVotage, myApi.DefaultCurrent);
                        timerUartRecv.Interval = 1000 * 10;
                        timerUartRecv.Enabled = true;

                        timerHighVoltage_10min.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_DOWN == LastSendCmd)
                {
                    if (myApi.RecvDeviceReady(text))
                    {
                        timerUartRecv.Enabled = false;
                        // measure end
                        MeasureStopFlag = true;
                    }
                }
                else
                {

                }




                if (DEVICE_CMD_ID.GET_HIGH_VOLTAGE == LastSendCmd)
                {
                    myParentForm.statusBar_HighVoltageUpdate(myApi.recvVoltage, myApi.recvCurrent);

                    ChartRealTimeShowIndex = 0;
                    chartRealTime.Series["射线强度"].Points.Clear();

                    double xMin = 0;
                    double xMax = 360;

                    chartRealTime.ChartAreas["ChartArea1"].AxisX.Maximum = xMax;
                    chartRealTime.ChartAreas["ChartArea1"].AxisX.Minimum = xMin;

                    if (MeasureIndex_Psi == 0)
                    {
                        dataGridViewClear();
                    }
                }
                else if ((DEVICE_CMD_ID.SET_HIGH_VOLTAGE_UP == LastSendCmd)
                    || (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_DOWN == LastSendCmd))
                {
                    if ((myApi.TubeVoltage > 0) || (myApi.TubeCurrent > 0))
                    {
                        myConfig.SaveWarmUpTime();
                    }
                    else
                    {

                    }
                    if (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_UP == LastSendCmd)
                    {
                        timerHighVoltage_10min.Enabled = false;
                    }
                    else if (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_DOWN == LastSendCmd)
                    {
                        if (MeasureStopFlag)
                        {
                            MeasureStopFlag = false;
                            buttonStart.Enabled = true;
                            buttonStop.Enabled = false;
                            groupBoxSettings.Enabled = true;
                            myParentForm.tableLayoutPanel1.Enabled = true;
                            myParentForm.tableLayoutPanel2.Enabled = true;

                            //MessageBox.Show("测量结束");
                            myParentForm.toolStripStatusLabelMeasureStatus.Text = "测量结束";
                        }
                    }

                    myParentForm.statusBar_HighVoltageUpdate(myApi.sendVoltage, myApi.sendCurrent);
                }
                else if ((DEVICE_CMD_ID.START_SCAN == LastSendCmd) && (myApi.RecvDataCount > 0) && (0 == myApi.DetectorType))
                {
                    //ChartRealTimeUpdate();
                }
                else
                {

                }
                // 接收数据完成
                if (RecvDataFinishFlag)
                {
                    RecvDataFinishFlag = false;

                    PhiMeasureFinish();
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void PhiMeasureFinish()
        {
            if (MeasureIndex_Psi < myApi.AnglePsi.Length - 1)
            {
                MeasureIndex_Psi++;

                myApi.SendAngleA(myApi.AnglePsi[MeasureIndex_Psi]);
                timerUartRecv.Interval = 1000 * 10;
                timerUartRecv.Enabled = true;
            }
            else
            {
                MeasureIndex_Psi = 0;
                if ((GroupIndex_Psi < PsiStartAngle.Length - 1)
                    && (checkBoxExpert.Checked == true))
                {
                    GroupIndex_Psi++;

                    ScanSetupStart();
                }
                else
	            {
                    myApi.SendCloseShutter();
                    timerUartRecv.Interval = 1000 * 5;
                    timerUartRecv.Enabled = true;
	            }
                //// 更新应力计算结果
                //StressViewUpdate(myApi.AnglePsi.Length);
                //// 生成RSC文件
                //ToolStripMenuItemRSC_Click(this, null);
                //// 生成xls文件
                //ChartFittingImageName = myApi.RecvDataFileNameArray[0].Replace("txt", "bmp");
                //ChartFittingExport(ChartFittingImageName);
                ////ToolStripMenuItemMsExcel_Click(this, null);

                //if (1 == FiveAxisInUse)
                //{
                //    if (MeasureIndex_XYZ < myApi.AngleBeta.Length - 1)
                //    {
                //        MeasureIndex_XYZ++;
                //        myApi.SendAngleABXYZ(MeasureIndex_XYZ, MeasureIndex_Psi);
                //        timerUartRecv.Interval = 1000 * 60;
                //        timerUartRecv.Enabled = true;
                //    }
                //    else
                //    {
                //        myApi.SendCloseShutter();
                //        timerUartRecv.Interval = 1000 * 5;
                //        timerUartRecv.Enabled = true;
                //    }
                //}
                //else
                //{
                //    myApi.SendCloseShutter();
                //    timerUartRecv.Interval = 1000 * 5;
                //    timerUartRecv.Enabled = true;
                //}
            }
        }

        public void PsiMeasureFinish()
        {
            // 织构测量没有拟合

            //// 更新拟合图像
            //string seriesName = myApi.AnglePsi[MeasureIndex_Psi].ToString();
            //string fileName = myApi.RecvDataFileName;
            //double[] x;
            //double[] y;
            //// 解析txt文件
            //myUart.Pack_Debug_out(null, "[Texture] parse txt=" + fileName);
            //CurveFitting.txt_parse(fileName, out x, out y);
            //// 更新拟合图像
            //ChartFittingUpdate(seriesName, fileName, x);

            if (MeasureIndex_Psi < myApi.AnglePsi.Length - 1)
            {
                MeasureIndex_Psi++;
                //if ((0 == myApi.MeasureMethod) || (1 == myApi.MeasureMethod))
                //{
                myApi.SendAnglePsi(myApi.AnglePsi[MeasureIndex_Psi]);
                timerUartRecv.Interval = 1000 * 10;
                timerUartRecv.Enabled = true;
                //}
                //else
                //{
                //    // T.B.D
                //}
            }
            else
            {
                MeasureIndex_Psi = 0;
                //// 更新应力计算结果
                //StressViewUpdate(myApi.AnglePsi.Length);
                //// 生成RSC文件
                //ToolStripMenuItemRSC_Click(this, null);
                //// 生成xls文件
                //ChartFittingImageName = myApi.RecvDataFileNameArray[0].Replace("txt", "bmp");
                //ChartFittingExport(ChartFittingImageName);
                ////ToolStripMenuItemMsExcel_Click(this, null);

                //if (1 == FiveAxisInUse)
                //{
                //    if (MeasureIndex_XYZ < myApi.AngleBeta.Length - 1)
                //    {
                //        MeasureIndex_XYZ++;
                //        myApi.SendAngleABXYZ(MeasureIndex_XYZ, MeasureIndex_Psi);
                //        timerUartRecv.Interval = 1000 * 60;
                //        timerUartRecv.Enabled = true;
                //    }
                //    else
                //    {
                //        myApi.SendCloseShutter();
                //        timerUartRecv.Interval = 1000 * 5;
                //        timerUartRecv.Enabled = true;
                //    }
                //}
                //else
                //{
                //    myApi.SendCloseShutter();
                //    timerUartRecv.Interval = 1000 * 5;
                //    timerUartRecv.Enabled = true;
                //}
            }
        }

    }
}
