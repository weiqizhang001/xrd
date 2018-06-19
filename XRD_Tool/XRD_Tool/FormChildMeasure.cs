using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading;
using System.Text.RegularExpressions;


namespace XRD_Tool
{
    public partial class FormChildMeasure : Form
    {
        public FormMDIParent myParentForm;
        public SerialPortCommon myUart;
        public TCPClient myTCP;
        public XrdConfig myConfig;
        public MeasureApi myApi;
        public DataResultTable myResultTable;
        public CurveFitting myCurveFit;
        public byte deviceStateBackup;

        public int ChartRealTimeShowIndex = 0;  // 实时绘图的当前数据的行号的索引

        private System.Timers.Timer timerHighVoltage_10min; // 11.测量完成后，如果10分钟没有对高压进行操作，则关闭高压SKM0 0
        public System.Timers.Timer timerUartRecv;
        public System.Timers.Timer timerTCPRecv;
        public int TCPReturnCode = 0x7FFFFFFF;

        public ToolTip tooltip;
        public Color[] colorArray = { Color.Blue, Color.Orange, Color.Red, Color.LightBlue, Color.Green, Color.Purple, Color.Pink };
        public volatile int MeasureTimeCount = 0;
        public volatile int MeasureTimeCountMax = 0;

        public volatile bool MeasureStopFlag = false;
        public volatile bool RecvDataFinishFlag = false;
        public volatile int FiveAxisInUse = 0;
        public volatile int MeasureIndex_Psi = 0;
        public volatile int MeasureIndex_XYZ = 0;
        public volatile string ChartFittingImageName;
      
#region 测量范围
        public double[] START_DEGREE_MIN = { 95.0, 95.0, 95.0, 95.0 };
        public double[] START_DEGREE_MAX = { 167.0, 167.0, 155.0, 155.0 };

        public double[] STOP_DEGREE_MIN = { 95.0, 95.0, 95.0, 95.0 };
        public double[] STOP_DEGREE_MAX = { 167.0, 167.0, 155.0, 155.0 };

        public double[] MEASURE_STEP_MIN = { 0.0002, 0.0002, 0.005, 0.005 };
        public double[] MEASURE_STEP_MAX = { 1.0, 1.0, 1.0, 1.0 };

        public double[] MEASURE_TIME_MIN = { 0.05, 0.05, 0.001, 0.001 };
        public double[] MEASURE_TIME_MAX = { 30.0, 30.0, 3600.0, 3600.0 };

        public double[] ANGLE_PSI_MIN = { 0, 0, 0, 0 };
        public double[] ANGLE_PSI_MAX = { 60.0, 70.0, 60.0, 70.0 };

        public int[] ANGLE_PSI_COUNT_MIN = { 1, 1, 1, 1 };
        public int[] ANGLE_PSI_COUNT_MAX = { 20, 20, 20, 20 };

        public double[] ANGLE_A_MIN = { 0, 0, 0, 0 };
        public double[] ANGLE_A_MAX = { 70.0, 70.0, 70.0, 70.0 };

        public double[] ANGLE_A_COUNT_MIN = { 1, 1, 1, 1 };
        public double[] ANGLE_A_COUNT_MAX = { 20, 20, 20, 20 };

        public double[] ANGLE_B_MIN = { 0, 0, 0, 0 };
        public double[] ANGLE_B_MAX = { 360.0, 360.0, 360.0, 360.0 };

        public double[] ANGLE_X_MIN = { -50.0, -50.0, -50.0, -50.0 };
        public double[] ANGLE_X_MAX = { 50.0, 50.0, 50.0, 50.0 };

        public double[] ANGLE_Y_MIN = { -50.0, -50.0, -50.0, -50.0 };
        public double[] ANGLE_Y_MAX = { 50.0, 50.0, 50.0, 50.0 };

        public double[] ANGLE_Z_MIN = { 0, 0, 0, 0 };
        public double[] ANGLE_Z_MAX = { 48.0, 48.0, 48.0, 48.0 };

        public double[] VOLTAGE_Cr_MIN = { 10.0, 5.0 };
        public double[] VOLTAGE_Cr_MAX = { 25.0, 40.0 };

        public double[] VOLTAGE_Cu_MIN = { 10.0, 5.0 };
        public double[] VOLTAGE_Cu_MAX = { 40.0, 40.0 };
#endregion

        public FormChildMeasure(FormMDIParent form)
        {
            myParentForm = form;
            myUart = form.myUart;
            myTCP = form.myTCP;
            myConfig = form.myConfig;
            myApi = form.myApi;
            myCurveFit = form.myCurveFit;

            tooltip = new ToolTip();

            timerHighVoltage_10min = new System.Timers.Timer(1000 * 60 * 10); // 10min
            timerHighVoltage_10min.Elapsed += new System.Timers.ElapsedEventHandler(timerHighVoltage_10min_Timeout);
            timerHighVoltage_10min.AutoReset = true;
            timerHighVoltage_10min.Enabled = false;

            // timer
            timerUartRecv = new System.Timers.Timer(10000);
            timerUartRecv.Elapsed += new System.Timers.ElapsedEventHandler(timerUartRecv_Timeout);
            timerUartRecv.AutoReset = true;
            timerUartRecv.Enabled = false;

            // timer
            timerTCPRecv = new System.Timers.Timer(10000);
            timerTCPRecv.Elapsed += new System.Timers.ElapsedEventHandler(timerTCPRecv_Timeout);
            timerTCPRecv.AutoReset = true;
            timerTCPRecv.Enabled = false;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.Show();

            if (myUart.port_UART.IsOpen)
            {
                deviceStateBackup = myUart.DeviceState;
                myUart.DeviceState = DEVICE_STATE.SCAN;
                myUart.RegControl(this, UartRecv_Measure, DEVICE_STATE.SCAN);
                if (myTCP.connected)
                {
                    myTCP.DeviceState = DEVICE_STATE.SCAN;
                    myTCP.RegControl(this, TCPRecv_Measure, DEVICE_STATE.SCAN);
                }
            }
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

        private void timerTCPRecv_Timeout(object sender, EventArgs e)
        {
            try
            {
                timerTCPRecv.Enabled = false;
                string strSendData = System.Text.Encoding.Default.GetString(myApi.TCPSendBuf);

                myUart.Pack_Debug_out(myApi.TCPSendBuf, "TCP Recv Timeout: @" + myUart.DeviceState.ToString() + "[" + strSendData + "]");
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
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

        private void FormChildMea_Load(object sender, EventArgs e)
        {
            buttonTest.Visible = true;
            buttonTest.Enabled = true;

            try
            {
                myApi.DetectorType = myConfig.DetectorType;
                myApi.MeasureMethod = myConfig.MeasureMethod;
                if (0 == myApi.DetectorType)
                {
                    if (myApi.MeasureMethod > 1)
                    {
                        myApi.MeasureMethod = 0;
                    }
                }
                else
                {
                    if (myApi.MeasureMethod < 2)
                    {
                        myApi.MeasureMethod = 2;
                    }
                }

                ReadFromConfig();

                updateControls();

                //checkBoxFiveAxis_CheckedChanged(this, null);

                ChartInit();

                if (myUart.port_UART.IsOpen)
                {

                }
                else
                {
                    buttonStart.Enabled = false;
                }
                

                this.myResultTable = new DataResultTable(this.dataGridViewCalcResult);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }    
        }

        private void ReadFromConfig()
        {
            try
            {
                myApi.SDDMotorMode = myConfig.SDDMotorMode;
                myApi.DefaultVotage = myConfig.DefaultVotage;
                myApi.DefaultCurrent = myConfig.DefaultCurrent;
                myApi.SampleName = myConfig.SampleName[myApi.DetectorType];
                myApi.SampleSn = myConfig.SampleSn[myApi.DetectorType];
                
                myApi.StartDegree = myConfig.StartDegree[myApi.MeasureMethod];
                myApi.StopDegree = myConfig.StopDegree[myApi.MeasureMethod];
                myApi.MeasureStep = myConfig.MeasureStep[myApi.MeasureMethod];
                myApi.MeasureTime = myConfig.MeasureTime[myApi.MeasureMethod];
                myApi.TubeVoltage = myConfig.TubeVoltage[myApi.MeasureMethod];
                myApi.TubeCurrent = myConfig.TubeCurrent[myApi.MeasureMethod];

                FiveAxisInUse = myConfig.FiveAxisInUse[myApi.MeasureMethod];

                myApi.ZeroPointMotorTS = myConfig.ZeroPointMotorTS[myApi.DetectorType];
                myApi.ZeroPointMotorTD = myConfig.ZeroPointMotorTD[myApi.DetectorType];
                myApi.ZeroPointAlpha = myConfig.ZeroPointAlpha[myApi.DetectorType];
                myApi.ZeroPointBeta = myConfig.ZeroPointBeta[myApi.DetectorType];
                myApi.ZeroPointX = myConfig.ZeroPointX[myApi.DetectorType];
                myApi.ZeroPointY = myConfig.ZeroPointY[myApi.DetectorType];
                myApi.ZeroPointZ = myConfig.ZeroPointZ[myApi.DetectorType];

                if (0 == myApi.MeasureMethod)
                {
                    myApi.AnglePsi = myConfig.AnglePsi_Method0;
                    myApi.AngleX = myConfig.AngleX_Method0;
                    myApi.AngleY = myConfig.AngleY_Method0;
                    myApi.AngleZ = myConfig.AngleZ_Method0;
                    myApi.AngleAlpha = myConfig.AngleAlpha_Method0;
                    myApi.AngleBeta = myConfig.AngleBeta_Method0;
                }
                else if (1 == myApi.MeasureMethod)
                {
                    myApi.AnglePsi = myConfig.AnglePsi_Method1;
                    myApi.AngleX = myConfig.AngleX_Method1;
                    myApi.AngleY = myConfig.AngleY_Method1;
                    myApi.AngleZ = myConfig.AngleZ_Method1;
                    myApi.AngleAlpha = myConfig.AngleAlpha_Method1;
                    myApi.AngleBeta = myConfig.AngleBeta_Method1;
                }
                else if (2 == myApi.MeasureMethod)
                {
                    myApi.AnglePsi = myConfig.AnglePsi_Method2;
                    myApi.AngleX = myConfig.AngleX_Method2;
                    myApi.AngleY = myConfig.AngleY_Method2;
                    myApi.AngleZ = myConfig.AngleZ_Method2;
                    myApi.AngleAlpha = myConfig.AngleAlpha_Method2;
                    myApi.AngleBeta = myConfig.AngleBeta_Method2;
                }
                else if (3 == myApi.MeasureMethod)
                {
                    myApi.AnglePsi = myConfig.AnglePsi_Method3;
                    myApi.AngleX = myConfig.AngleX_Method3;
                    myApi.AngleY = myConfig.AngleY_Method3;
                    myApi.AngleZ = myConfig.AngleZ_Method3;
                    myApi.AngleAlpha = myConfig.AngleAlpha_Method3;
                    myApi.AngleBeta = myConfig.AngleBeta_Method3;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            } 
        }
      
        public void SaveToConfig()
        {
            try 
            {
                myConfig.SampleName[myApi.DetectorType] = myApi.SampleName;
                myConfig.SampleSn[myApi.DetectorType] = myApi.SampleSn;
                myConfig.MeasureMethod = myApi.MeasureMethod;
                myConfig.StartDegree[myApi.MeasureMethod] = myApi.StartDegree;
                myConfig.StopDegree[myApi.MeasureMethod] = myApi.StopDegree;
                myConfig.MeasureStep[myApi.MeasureMethod] = myApi.MeasureStep;
                myConfig.MeasureTime[myApi.MeasureMethod] = myApi.MeasureTime;
                myConfig.TubeVoltage[myApi.MeasureMethod] = myApi.TubeVoltage;
                myConfig.TubeCurrent[myApi.MeasureMethod] = myApi.TubeCurrent;

                myConfig.FiveAxisInUse[myApi.MeasureMethod] = FiveAxisInUse;
                myConfig.ZeroPointMotorTS[myApi.DetectorType] = myApi.ZeroPointMotorTS;
                myConfig.ZeroPointMotorTD[myApi.DetectorType] = myApi.ZeroPointMotorTD;
                myConfig.ZeroPointAlpha[myApi.DetectorType] = myApi.ZeroPointAlpha;
                myConfig.ZeroPointBeta[myApi.DetectorType] = myApi.ZeroPointBeta;
                myConfig.ZeroPointX[myApi.DetectorType] = myApi.ZeroPointX;
                myConfig.ZeroPointY[myApi.DetectorType] = myApi.ZeroPointY;
                myConfig.ZeroPointZ[myApi.DetectorType] = myApi.ZeroPointZ;

                if (0 == myApi.MeasureMethod)
                {
                    myConfig.AnglePsi_Method0 = myApi.AnglePsi;
                    myConfig.AngleX_Method0 = myApi.AngleX;
                    myConfig.AngleY_Method0 = myApi.AngleY;
                    myConfig.AngleZ_Method0 = myApi.AngleZ;
                    myConfig.AngleAlpha_Method0 = myApi.AngleAlpha;
                    myConfig.AngleBeta_Method0= myApi.AngleBeta;
                }
                else if (1 == myApi.MeasureMethod)
                {
                    myConfig.AnglePsi_Method1 = myApi.AnglePsi;
                    myConfig.AngleX_Method1 = myApi.AngleX;
                    myConfig.AngleY_Method1 = myApi.AngleY;
                    myConfig.AngleZ_Method1 = myApi.AngleZ;
                    myConfig.AngleAlpha_Method1 = myApi.AngleAlpha;
                    myConfig.AngleBeta_Method1 = myApi.AngleBeta;
                }
                else if (2 == myApi.MeasureMethod)
                {
                    myConfig.AnglePsi_Method2 = myApi.AnglePsi;
                    myConfig.AngleX_Method2 = myApi.AngleX;
                    myConfig.AngleY_Method2 = myApi.AngleY;
                    myConfig.AngleZ_Method2 = myApi.AngleZ;
                    myConfig.AngleAlpha_Method2 = myApi.AngleAlpha;
                    myConfig.AngleBeta_Method2 = myApi.AngleBeta;
                }
                else if (3 == myApi.MeasureMethod)
                {
                    myConfig.AnglePsi_Method3 = myApi.AnglePsi;
                    myConfig.AngleX_Method3 = myApi.AngleX;
                    myConfig.AngleY_Method3 = myApi.AngleY;
                    myConfig.AngleZ_Method3 = myApi.AngleZ;
                    myConfig.AngleAlpha_Method3 = myApi.AngleAlpha;
                    myConfig.AngleBeta_Method3= myApi.AngleBeta;
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }
        public void updateControls()
        {
            textBoxSampleName.Text = myApi.SampleName;
            textBoxSampleSn.Text = myApi.SampleSn;

            textBox_StartDegree.Text = myApi.StartDegree.ToString();
            textBox_StopDegree.Text = myApi.StopDegree.ToString();
            textBox_MeaStep.Text = myApi.MeasureStep.ToString();
            textBoxMeaTime.Text = myApi.MeasureTime.ToString();
            textBoxTubeVoltage.Text = myApi.TubeVoltage.ToString();
            textBoxTubeCurrent.Text = myApi.TubeCurrent.ToString();
            if (0 == myApi.DetectorType)
            {
                comboBoxMeaMethod.SelectedIndex = myApi.MeasureMethod;
            }
            else
            {
                comboBoxMeaMethod.SelectedIndex = myApi.MeasureMethod - 2;
            }

            textBoxAnglePsi.Text = string.Join(",", myApi.AnglePsi);
            textBoxAngleX.Text = string.Join(",", myApi.AngleX);
            textBoxAngleY.Text = string.Join(",", myApi.AngleY);
            textBoxAngleZ.Text = string.Join(",", myApi.AngleZ);
            textBoxAngleAlpha.Text = string.Join(",", myApi.AngleAlpha);
            textBoxAngleBeta.Text = string.Join(",", myApi.AngleBeta);

            if (1 == FiveAxisInUse)
            {
                checkBoxFiveAxis.CheckState = CheckState.Checked;
            }
            else
            {
                checkBoxFiveAxis.CheckState = CheckState.Unchecked;
            }
            checkBoxFiveAxis_CheckedChanged(this, null);

            buttonStop.Enabled = false;


            if (1 == myApi.DetectorType)
            {
                label_StartDegree.Text = "峰位角度";
                
                label_StopDegree.Text = "测量范围";
                textBox_StopDegree.Text = "24";
                textBox_StopDegree.ReadOnly = true;
     
                textBox_MeaStep.Text = "0.04";
                textBox_MeaStep.ReadOnly = true;
            }
            else
            {
                label_StartDegree.Text = "起始角度";

                label_StopDegree.Text = "终止角度";
                textBox_StopDegree.ReadOnly = false;

                textBox_MeaStep.ReadOnly = false;
            }
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Measure] ButtonStart");

                if (!ReadSettingsFromControl())
                {
                    return;
                }

                if (!checkSettingsRange())
                {
                    return;
                }

                FormShowSettings ShowForm = new FormShowSettings(this);
                ShowForm.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = ShowForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    buttonStart.Enabled = false;
                    buttonStop.Enabled = true;
                    groupBoxFiveAxis.Enabled = false;
                    groupBoxSettings.Enabled = false;
                    checkBoxFiveAxis.Enabled = false;
                    myParentForm.tableLayoutPanel1.Enabled = false;
                    myParentForm.tableLayoutPanel2.Enabled = false;

                    this.SaveToConfig();
                    myConfig.SaveMeasureSettings();

                    //myApi.SetMeasureSettings(myConfig);
               
                    ChartRealTimeShowIndex = 0;
                    chartRealTime.Series["射线强度"].Points.Clear();
                    chartFitting.Series.Clear();
                    StressViewClear();
                    dataGridViewClear();

                    MeasureIndex_Psi = 0;
                    MeasureIndex_XYZ = 0;
                    RecvDataFinishFlag = false;
                    MeasureStopFlag = false;
                    myParentForm.toolStripStatusLabelMeasureStatus.Text = "测量开始";

                    ScanSetupStart();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }     
        }

        public bool ReadSettingsFromControl()
        {
            bool result = false;

            try
            {
                myApi.SampleName = textBoxSampleName.Text;
                myApi.SampleSn = textBoxSampleSn.Text;
                myApi.StartDegree = Convert.ToDouble(textBox_StartDegree.Text);
                myApi.StopDegree = Convert.ToDouble(textBox_StopDegree.Text);
                myApi.MeasureStep = Convert.ToDouble(textBox_MeaStep.Text);
                myApi.MeasureTime = Convert.ToDouble(textBoxMeaTime.Text);
                myApi.AnglePsi = Array.ConvertAll(textBoxAnglePsi.Text.Split(','), double.Parse);
                myApi.TubeVoltage = Convert.ToDouble(textBoxTubeVoltage.Text);
                myApi.TubeCurrent = Convert.ToDouble(textBoxTubeCurrent.Text);

                if (checkBoxFiveAxis.CheckState == CheckState.Checked)
                {
                    FiveAxisInUse = 1;
                    myApi.AngleX = Array.ConvertAll(textBoxAngleX.Text.Split(','), double.Parse);
                    myApi.AngleY = Array.ConvertAll(textBoxAngleY.Text.Split(','), double.Parse);
                    myApi.AngleZ = Array.ConvertAll(textBoxAngleZ.Text.Split(','), double.Parse);
                    myApi.AngleBeta = Array.ConvertAll(textBoxAngleBeta.Text.Split(','), double.Parse);
                    myApi.AngleAlpha = Array.ConvertAll(textBoxAngleAlpha.Text.Split(','), double.Parse);

                }
                else
                {
                    FiveAxisInUse = 0;
                }

                result = true;
            }
            catch (Exception ex)
            {
                FormDeviceInit.showErrorMessageBox("输入格式错误！");
            }

            return result;
        }

        #region 检查用户输入值范围
        public bool checkSettingsRange()
        {
            bool result = false;
            double min = 0;
            double max = 0;

            try 
            {
                // 判断起始角度，终止角度范围
                min = START_DEGREE_MIN[myApi.MeasureMethod];
                max = START_DEGREE_MAX[myApi.MeasureMethod];
                if ((myApi.StartDegree >= min) && (myApi.StopDegree <= max) && (myApi.StopDegree > myApi.StartDegree))
                {
                    // 角度OK
                }
                else
                {
                    FormDeviceInit.showErrorMessageBox("起始/终止角数据范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");
                    result = false;

                    return result;
                }

                // 判断测量步宽范围
                min = MEASURE_STEP_MIN[myApi.MeasureMethod];
                max = MEASURE_STEP_MAX[myApi.MeasureMethod];
                if ((myApi.MeasureStep >= min) && (myApi.MeasureStep <= max))
                {
                    // 角度OK
                    if ((2 == myApi.MeasureMethod) || (3 == myApi.MeasureMethod))
                    {
                        if (myApi.MeasureStep / 0.005 == 0)
                        { 
                        
                        }
                        else
                        {
                            FormDeviceInit.showErrorMessageBox("测量步宽不是0.005的整数倍!");
                            result = false;

                            return result;
                        }
                    }
                }
                else
                {
                    FormDeviceInit.showErrorMessageBox("测量步宽范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");
                    result = false;

                    return result;
                }

                // 判断测量时间范围
                min = MEASURE_TIME_MIN[myApi.MeasureMethod];
                max = MEASURE_TIME_MAX[myApi.MeasureMethod];
                if ((myApi.MeasureTime >= min) && (myApi.MeasureTime <= max))
                {

                }
                else
                {
                    FormDeviceInit.showErrorMessageBox("测量时间范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");
                    result = false;

                    return result;
                }

                // 判断Psi轴范围
                min = ANGLE_PSI_MIN[myApi.MeasureMethod];
                max = ANGLE_PSI_MAX[myApi.MeasureMethod];
                result = checkFiveAxisAngleRange(myApi.AnglePsi, min, max);
                if (!result)
                {
                    FormDeviceInit.showErrorMessageBox("ψ轴数据范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");
                    return result;
                }

                // 判断高压
                result = checkHighVoltageRange(myApi.TargetType, myApi.TubeVoltage, myApi.TubeCurrent);
                if (!result)
                {
                    return result;
                }

                // 判断五轴范围
                if (1 == FiveAxisInUse)
                {
                    // 判断四轴个数是否相等
                    if ((myApi.AngleBeta.Length == myApi.AngleX.Length)
                        && (myApi.AngleBeta.Length == myApi.AngleY.Length)
                        && (myApi.AngleBeta.Length == myApi.AngleZ.Length))
                    {

                    }
                    else
                    {
                        FormDeviceInit.showErrorMessageBox("β，X，Y，Z轴个数不一致!");
                        result = false;

                        return result;
                    }
                    // 判断α轴个数
                    if ((1 == myApi.MeasureMethod) || (3 == myApi.MeasureMethod))
                    {
                        if (myApi.AngleAlpha.Length == myApi.AngleBeta.Length)
                        { 
                        
                        }
                        else
                        {
                            FormDeviceInit.showErrorMessageBox("α轴个数错误!");
                            result = false;

                            return result;
                        }
                    }

                    // 判断五轴个数：1~20
                    if ((myApi.AngleBeta.Length > 0) && (myApi.AngleBeta.Length <= 20))
                    {

                    }
                    else
                    {
                        FormDeviceInit.showErrorMessageBox("五轴个数错误!" + myApi.AngleBeta.Length.ToString());
                        result = false;

                        return result;
                    }

                    // 判断Alpha轴范围
                    if ((1 == myApi.MeasureMethod) || (3 == myApi.MeasureMethod))
                    {
                        min = ANGLE_A_MIN[myApi.MeasureMethod];
                        max = ANGLE_A_MAX[myApi.MeasureMethod];
                        result = checkFiveAxisAngleRange(myApi.AngleAlpha, min, max);
                        if (!result)
                        {
                            FormDeviceInit.showErrorMessageBox("α轴数据范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");
                            return result;
                        }
                    }

                    // 判断Beta轴范围
                    min = ANGLE_B_MIN[myApi.MeasureMethod];
                    max = ANGLE_B_MAX[myApi.MeasureMethod];
                    result = checkFiveAxisAngleRange(myApi.AngleBeta, min, max);
                    if (!result)
                    {
                        FormDeviceInit.showErrorMessageBox("β轴数据范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");

                        return result;
                    }
                    // 判断X轴范围
                    min = ANGLE_X_MIN[myApi.MeasureMethod];
                    max = ANGLE_X_MAX[myApi.MeasureMethod];
                    result = checkFiveAxisAngleRange(myApi.AngleX, min, max);
                    if (!result)
                    {
                        FormDeviceInit.showErrorMessageBox("X轴数据范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");

                        return result;
                    }
                    // 判断Y轴范围
                    min = ANGLE_Y_MIN[myApi.MeasureMethod];
                    max = ANGLE_Y_MAX[myApi.MeasureMethod];
                    result = checkFiveAxisAngleRange(myApi.AngleY, min, max);
                    if (!result)
                    {
                        FormDeviceInit.showErrorMessageBox("Y轴数据范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");

                        return result;
                    }
                    // 判断Z轴范围
                    min = ANGLE_Z_MIN[myApi.MeasureMethod];
                    max = ANGLE_Z_MAX[myApi.MeasureMethod];
                    result = checkFiveAxisAngleRange(myApi.AngleZ, min, max);
                    if (!result)
                    {
                        FormDeviceInit.showErrorMessageBox("Z轴数据范围错误!（" + min.ToString() + " ~ " + max.ToString() + "）");

                        return result;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                FormDeviceInit.showErrorMessageBox("数值范围检查失败！" + ex.ToString());
            }

            return result;
        }

        public static bool checkFiveAxisAngleRange(double[] angleArray, double min, double max)
        {
            bool result = false;

            for (int i = 0; i < angleArray.Length; i++)
            {
                if ((angleArray[i] >= min) && (angleArray[i] <= max))
                {

                }
                else
                {
                    result = false;

                    return result;
                }
            }

            result = true;

            return result;
        }

        public static bool checkFiveAxisAngleRange_Single(double angle, double min, double max)
        {
            bool result = false;

            if ((angle >= min) && (angle <= max))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public static bool checkHighVoltageRange(int target, double voltage, double current)
        {
            bool result = false;

            if (target == 0)  // Cr
            {
                if ((voltage >= 10) && (voltage <= 25))
                {
                    if ((current >= 5) && (current <= 40))
                    {
                        result = true;
                    }
                    else
                    {
                        FormDeviceInit.showErrorMessageBox("靶材=Cr，电流范围错误!（5~40）");
                        result = false;
                    }
                }
                else
                {
                    FormDeviceInit.showErrorMessageBox("靶材=Cr，电压范围错误!（10~25）");
                    result = false;
                }
            }
            else if (target == 1) // Cu
            {
                if ((voltage >= 10) && (voltage <= 40))
                {
                    if ((current >= 5) && (current <= 40))
                    {
                        result = true;
                    }
                    else
                    {
                        FormDeviceInit.showErrorMessageBox("靶材=Cu，电流范围错误!（5~40）");
                        result = false;
                    }
                }
                else
                {
                    FormDeviceInit.showErrorMessageBox("靶材=Cu，电压范围错误!（510~40）");
                    result = false;
                }
            }
            else
            {
                FormDeviceInit.showErrorMessageBox("靶材错误!" + target.ToString());
                result = false;
            }


            return result;
        }
#endregion

        private void ScanSetupStart()
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Measure] Measure Start");

                myApi.SendDevicePause();
                timerUartRecv.Interval = 1000 * 5;
                timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void comboBoxMeaMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (0 == comboBoxMeaMethod.SelectedIndex)
                {
                    labelAngleAlpha.Visible = false;
                    textBoxAngleAlpha.Visible = false;
                }
                else
                {
                    labelAngleAlpha.Visible = true;
                    textBoxAngleAlpha.Visible = true;
                }

                int selectMethod = 0;
                if (0 == myApi.DetectorType)
                {
                    selectMethod = comboBoxMeaMethod.SelectedIndex;
                }
                else
                {
                    selectMethod = comboBoxMeaMethod.SelectedIndex + 2;
                }

                if (myApi.MeasureMethod != selectMethod)
                {
                    myUart.Pack_Debug_out(null, "[Measure] Measure Methord changed" + selectMethod.ToString());

                    myApi.MeasureMethod = selectMethod;
                    ReadFromConfig();

                    updateControls();
                }   
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }   
        }

        private void checkBoxFiveAxis_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (checkBoxFiveAxis.CheckState == CheckState.Checked)
                {
                    groupBoxFiveAxis.Enabled = true;
                }
                else
                {
                    groupBoxFiveAxis.Enabled = false;
                }

                myUart.Pack_Debug_out(null, "[Measure] check 5 Axis=" + checkBoxFiveAxis.CheckState.ToString());
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            } 
        }

        #region 图表
        private void ChartInit()
        {
            try
            {
                chartRealTime.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                chartRealTime.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
                //chartRealTime.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
                //chartRealTime.ChartAreas["ChartArea1"].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
                //chartRealTime.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = 10;
                //chartRealTime.ChartAreas["ChartArea1"].AxisY.LabelStyle.Interval = 10;

                chartFitting.ChartAreas["ChartArea1"].AxisX.MajorGrid.Enabled = false;
                chartFitting.ChartAreas["ChartArea1"].AxisY.MajorGrid.Enabled = false;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        
        public void ChartRealTimeUpdate()
        {
            try 
            {
                for (int i = ChartRealTimeShowIndex; i < myApi.RecvDataTable.Rows.Count; i++)
                {
                    double x = Convert.ToDouble(myApi.RecvDataTable.Rows[i][0]);
                    int y = Convert.ToInt32(myApi.RecvDataTable.Rows[i][1]);
                    if ((x == 0.0) || (y == 0))
                    {
                        continue;
                    }
                    chartRealTime.Series["射线强度"].Points.AddXY(x, y);
                }
                ChartRealTimeShowIndex = myApi.RecvDataTable.Rows.Count - 1;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void ChartRealTimeUpdate(double[] x, double[] y)
        {
            try
            {
                chartRealTime.Series["射线强度"].Points.Clear();
                chartRealTime.ChartAreas["ChartArea1"].AxisX.Maximum = x.Max();
                chartRealTime.ChartAreas["ChartArea1"].AxisX.Minimum = x.Min() ;
                //chartRealTime.ChartAreas["ChartArea1"].AxisY.Maximum = y.Max();
                //chartRealTime.ChartAreas["ChartArea1"].AxisY.Minimum = y.Min();

                chartFitting.ChartAreas["ChartArea1"].AxisX.Maximum = x.Max();
                chartFitting.ChartAreas["ChartArea1"].AxisX.Minimum = x.Min();
                //chartFitting.ChartAreas["ChartArea1"].AxisY.Maximum = y.Max();
                //chartFitting.ChartAreas["ChartArea1"].AxisY.Minimum = y.Min();


                //chartRealTime.ChartAreas["ChartArea1"].AxisX.Interval = x[1]-x[0]; 

                //chartRealTime.ChartAreas["ChartArea1"].AxisX.LabelAutoFitStyle = LabelAutoFitStyles.None;
                //chartRealTime.ChartAreas["ChartArea1"].AxisY.LabelAutoFitStyle = LabelAutoFitStyles.None;
                //chartRealTime.ChartAreas["ChartArea1"].AxisX.LabelStyle.Interval = x[1] - x[0];


                for (int i = 0; i < x.Length; i++)
                {
                    if ((x[i] == 0.0) || (y[i] == 0.0))
                    {
                        continue;
                    }

                    chartRealTime.Series["射线强度"].Points.AddXY(x[i], y[i]);
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void ChartFittingUpdate(string seriesName, string dataFile, double[] x)
        {
            double[] CalcResult = new double[5];

            try
            {
                myUart.Pack_Debug_out(null, "[Measure] Chart Fit Update,series=" + seriesName + " file=" + dataFile);

                // 拟合曲线
                //chartFitting.Series.Clear();

                //Legend myLegend = new Legend();
                //myLegend.DockedToChartArea = "ChartArea1";
                //myLegend.Name = "Legend1";
                Series mySeries = new Series();
                mySeries.ChartArea = "ChartArea1";
                mySeries.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                mySeries.Legend = "Legend1";

                if (chartFitting.Series.IsUniqueName(seriesName))
                {
                    mySeries.Name = seriesName;
                }
                else
                {
                    mySeries.Name = seriesName + "_" + MeasureIndex_Psi.ToString();
                    myUart.Pack_Debug_out(null, "SeriesNameError" + "[" + seriesName + ","+ mySeries.Name + "]");
                }
                int colorIndex = MeasureIndex_Psi % colorArray.Length;
                mySeries.Color = colorArray[colorIndex];
                
                chartFitting.Series.Add(mySeries);
                
                // 实时曲线
                //Series mySeries2 = new Series();
                //mySeries2.ChartArea = "ChartArea1";
                //mySeries2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                //mySeries2.Legend = "Legend1";
                //mySeries2.Name = seriesName;
                //mySeries2.Color = Color.Red;
                //chartRealTime.Series.Add(mySeries2);
              
                if (CurveFitting.Calc2Theta(dataFile, ref CalcResult))
                {
                    double x0 = CalcResult[0];  // 波峰中心
                    double H = CalcResult[1];   // 波峰中心高度
                    double D = CalcResult[2];   // 半峰宽
                    double omega = CalcResult[3];
                    double sigma = CalcResult[4];
                    double n = 1.5;

                    double[] yCalc = new double[x.Length];

                    for (int i = 0; i < x.Length; i++)
                    {
                        // 拟合后的y值
                        CurveFitting.PearsonVII_Calc_Y(x[i], x0, H, omega, sigma, out yCalc[i]);
                        //CurveFitting.PearsonVII_Jade_Calc_Y(x[i], x0, H, n, D, out yCalc[i]);
                        
                        mySeries.Points.AddXY(x[i], yCalc[i]);

                        //mySeries2.Points.AddXY(x[i], yCalc[i]);
                    }

                    //double[] dataGridView = new double[9];
                    //dataGridView[0] = 0.0;  // "Beta"
                    //dataGridView[1] = 0.0;  // "Psi"
                    //dataGridView[2] = 0.0;  // "Sin^psi"
                    //dataGridView[3] = 0.0;  // "DSpacing"
                    //dataGridView[4] = x0;   // "2Theta"
                    //dataGridView[5] = 0.0;  // "Strain*E3"
                    //dataGridView[6] = D;    // "FWHM"
                    //dataGridView[7] = 0.0;  // "Breadth"
                    //dataGridView[8] = 0.0;  // "Intensity"

                    double[] dataGridView = new double[8];
                    dataGridView[0] = myApi.AnglePsi[MeasureIndex_Psi];  // "Psi"
                    double temp = 0.0;
                    CurveFitting.Calc_Sin2_Psi(dataGridView[0], out temp);
                    dataGridView[1] = temp;  // "Sin^psi"
                    CurveFitting.Calc_d(x0 / 2, FIX_VALUE.X_RAY_LENGTH_Cr_Ka, out temp);
                    dataGridView[2] = temp; // "DSpacing"
                    dataGridView[3] = x0;   // "2Theta"
                    dataGridView[4] = D;    // "FWHM"
                    dataGridView[5] = H;    // "Intensity"
                    dataGridView[6] = 0.0;  // "Strain*E3"
                    dataGridView[7] = 0.0;  // "%(d-d0)/d0"

                    dataGridViewUpdate(dataGridView);
                }
                else
                {
                    MessageBox.Show("参数错误！计算失败。");
                }

            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            myUart.Pack_Debug_out(null, "Python Cmd: [" + CurveFitting.strCallPyCmd + "]");
            myUart.Pack_Debug_out(null, "Python Result: [" + string.Join(",", CalcResult) + "]");
        }

        public void ChartFittingExport(string fileName)
        {
            myUart.Pack_Debug_out(null, "[Measure] export bmp=" + fileName);

            chartFitting.SaveImage(fileName, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Bmp);
        }

        public void StressViewUpdate(int group)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Measure] stress calc");

                double[] x = new double[group];
                double[] y = new double[group];

                for (int i = 0; i < x.Length; i++)
                {
                    x[i] = (double)myResultTable.myDataTable.Rows[i][1];
                    y[i] = (double)myResultTable.myDataTable.Rows[i][7];
                }

                double E = 216.0;
                double V = 0.28;
                double A = 0.0;
                double B = 0.0;
                double stress = 0.0;

                CurveFitting.Calc_Stress(x, y, E, V, out stress, out A, out B);

                stress = stress * 10;
                stress = Math.Round(stress, 3);
                A = Math.Round(A, 3);
                B = Math.Round(B, 3);

                labelStress.Text = stress.ToString();
                labelB.Text = B.ToString() + "%";
                labelA.Text = A.ToString() + "%";
                labelE.Text = E.ToString() + " * 10^3";
                labelV.Text = V.ToString();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void StressViewClear()
        {
            try
            {
                double E = 216.0;
                double V = 0.28;
                double A = 0.0;
                double B = 0.0;
                double stress = 0.0;
                
                stress = Math.Round(stress, 3);
                A = Math.Round(A, 3);
                B = Math.Round(B, 3);

                labelStress.Text = stress.ToString();
                labelB.Text = B.ToString() + "%";
                labelA.Text = A.ToString() + "%";
                labelE.Text = E.ToString() + " * 10^3";
                labelV.Text = V.ToString();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void dataGridViewUpdate(double[] data)
        {
            try
            {
                myResultTable.UpdateDataResultTable(data);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void dataGridViewClear()
        {
            try
            {
                myResultTable.ClearDataResultTable();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }
        #endregion

        private void FormChildMeasure_Activated(object sender, EventArgs e)
        {
            try
            {
                if (myParentForm.OpenDataFileFlag)
                {
                    myParentForm.OpenDataFileFlag = false;
                    double[] x;
                    double[] y;
                    chartFitting.Series.Clear();
                    StressViewClear();
                    dataGridViewClear();

                    CurveFitting.txt_parse(myConfig.OpenDataFileName, out x, out y);

                    ChartRealTimeUpdate(x, y);

                    string seriesName = System.IO.Path.GetFileName(myConfig.OpenDataFileName);
                    ChartFittingUpdate(seriesName, myConfig.OpenDataFileName, x);
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void FormChildMeasure_FormClosing(object sender, FormClosingEventArgs e)
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

        private void button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDlg = new OpenFileDialog();
            fileDlg.Filter = "所有文件(*.*)|*.*"; // 如果需要筛选txt文件（"Files (*.txt)|*.txt"）
            fileDlg.RestoreDirectory = true;
            fileDlg.FilterIndex = 1;
            fileDlg.Multiselect = true;

            double[] x1;
            int[] y1;

            if (fileDlg.ShowDialog() == DialogResult.OK)
            {
                CurveFitting.txt_parse(fileDlg.FileName, out x1, out y1);
                System.Array.Copy(y1, 0, myApi.TCP_RecvIntensityArray, 0, 600);
            }
            else
            {
                x1 = null;
                y1 = null;
            }
            

            double[] TCP_PsiAngleArray = new double[640];
            // 起始角度=峰位角度 - 测量范围/2
            double startDegree = myApi.StartDegree - 25.6 / 2;
            for (int i = 0; i < TCP_PsiAngleArray.Length; i++)
            {
                TCP_PsiAngleArray[i] = startDegree + i * 25.6 / 640;
            }

            double[] x;
            double[] y;
            double[] smooth_y;
            // debug
            System.Array.Copy(x1, 0, TCP_PsiAngleArray, 0, 600);
            myApi.TCP_RecvDataSplit(TCP_PsiAngleArray, myApi.TCP_RecvIntensityArray, 8, out x, out y);

            myApi.TCP_RecvDataSmooth(y, out smooth_y);

            double[] y2 = new double[4800];
            System.Array.Copy(smooth_y, 160, y2, 0, 4800);

            Array.Clear(myApi.SSC_RecvDataArray, 0, myApi.SSC_RecvDataArray.Length);
            myApi.SSC_RecvDataReadIndex = 0;
            myApi.SSC_RecvDataWriteIndex = 0;

            myApi.RecvDataTableSaveFileEnd();

            ChartRealTimeUpdate();

            PsiMeasureFinish();


            //byte[] yArray = new byte[640 * 4]; 
            //for (int i = 0; i < 640 * 4; )
            //{
            //    yArray[i] = 0x12;
            //    yArray[i + 1] = 0x34;
            //    yArray[i + 2] = 0;
            //    yArray[i + 3] = 0;
                
            //    i = i + 4;
            //}

            //double[] xArray = new double[640];
            //for (int i = 0; i < xArray.Length; i++)
            //{
            //    xArray[i] = 155.0 + 25.6 / 640 * i;
            //}

            //myApi.TCP_RecvDataAnalyze(yArray);

            //myApi.RecvDataTableSaveFileStart(33, 0);

            //myApi.RecvDataTableUpdate(xArray, myApi.TCP_RecvIntensityArray);
            //myApi.RecvDataTableSaveFileProc(xArray, myApi.TCP_RecvIntensityArray);

            //myApi.RecvDataTableSaveFileEnd();

            //ChartRealTimeUpdate();

            //return;
            

            //OpenFileDialog fileDlg = new OpenFileDialog();
            //fileDlg.Filter = "所有文件(*.*)|*.*"; // 如果需要筛选txt文件（"Files (*.txt)|*.txt"）
            //fileDlg.RestoreDirectory = true;
            //fileDlg.FilterIndex = 1;
            //fileDlg.Multiselect = true;

            //try
            //{
            //    if (fileDlg.ShowDialog() == DialogResult.OK)
            //    {
            //        myApi.AnglePsi = new double[4];
            //        myApi.AnglePsi[0] = 0;
            //        myApi.AnglePsi[1] = 15;
            //        myApi.AnglePsi[2] = 30;
            //        myApi.AnglePsi[3] = 45;
            //        MeasureIndex_Psi = 0;
            //        chartFitting.Series.Clear();
            //        StressViewClear();
            //        dataGridViewClear();

            //        for (int i = 0; i < fileDlg.FileNames.Length; i++)
            //        {
            //            double[] x;
            //            double[] y;
            //            CurveFitting.txt_parse(fileDlg.FileNames[i], out x, out y);

            //            ChartRealTimeUpdate(x, y);

            //            string seriesName = System.IO.Path.GetFileName(fileDlg.FileNames[i]);
            //            ChartFittingUpdate(seriesName, fileDlg.FileNames[i], x);
            //            MeasureIndex_Psi++; // test
            //        }

            //        StressViewUpdate(fileDlg.FileNames.Length);                      
            //    }
            //}
            //catch (Exception ex)
            //{
            //    myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            //} 
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Measure] Measure Stop");
                MeasureStopFlag = true;
                timerUartRecv.Enabled = false;

                myApi.SendDevicePause();
                timerUartRecv.Interval = 1000 * 5;
                timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }     
        }

        private void FormChildMeasure_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        #region tip
        private void textBoxAnglePsi_MouseEnter(object sender, EventArgs e)
        {
            showTooltip(textBoxAnglePsi);
        }

        private void textBoxAngleX_MouseEnter(object sender, EventArgs e)
        {
            showTooltip(textBoxAngleX);
        }

        private void textBoxAngleY_MouseEnter(object sender, EventArgs e)
        {
            showTooltip(textBoxAngleY);
        }

        private void textBoxAngleZ_MouseEnter(object sender, EventArgs e)
        {
            showTooltip(textBoxAngleZ);
        }

        private void textBoxAngleBeta_MouseEnter(object sender, EventArgs e)
        {
            showTooltip(textBoxAngleBeta);
        }

        private void textBoxAngleAlpha_MouseEnter(object sender, EventArgs e)
        {
            showTooltip(textBoxAngleAlpha);
        }

        private void showTooltip(TextBox tbox)
        {
            tooltip.Dispose();
            tooltip = new ToolTip();
            tooltip.Show(tbox.Text, tbox);
        }
        #endregion

        private void timerMeasureTimeCountDown_Tick(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "CountDown Run" + "[" + MeasureTimeCount.ToString() + "," + MeasureTimeCountMax.ToString() + "]");

                MeasureTimeCount++;

                //myParentForm.toolStripStatusLabelCountDown.Text = "剩余测量时间：" + (MeasureTimeCountMax - MeasureTimeCount).ToString() + " 秒";
                if (MeasureTimeCount >= MeasureTimeCountMax)
                {
                    timerMeasureTimeCountDown.Enabled = false;
                    myUart.Pack_Debug_out(null, "CountDown stop" + "[" + MeasureTimeCount.ToString() + "," + MeasureTimeCountMax.ToString() + "]");
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }


        public void UartRecv_Measure(byte[] text, int PackageLen)
        {
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;

            try
            {
                #region SDD 探测器
                if ((0 == myApi.MeasureMethod) || (1 == myApi.MeasureMethod))
                {
                    if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;
                            if (!MeasureStopFlag)
                            {
                                myApi.SendMotorMode();
                                timerUartRecv.Interval = 1000 * 5;
                                timerUartRecv.Enabled = true;
                            }
                            else
                            {
                                myApi.SendCloseShutter();
                            }
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_MOTOR_MODE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;
                            myApi.SendMeasureTime(myApi.MeasureTime);
                            timerUartRecv.Interval = 1000 * 5;
                            timerUartRecv.Enabled = true;
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_MES_TIME == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;
                            myApi.SendMeasureStep();
                            timerUartRecv.Interval = 1000 * 5;
                            timerUartRecv.Enabled = true;
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_SCAN_STEP_DISTANCE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            if (1 == FiveAxisInUse)
                            {
                                myApi.SendAngleABXYZ(MeasureIndex_XYZ, MeasureIndex_Psi);
                                timerUartRecv.Interval = 1000 * 60;
                                timerUartRecv.Enabled = true;
                            }
                            else
                            {
                                if (0 == myApi.MeasureMethod)
                                {
                                    myApi.SendAnglePsi(myApi.AnglePsi[MeasureIndex_Psi]);
                                    timerUartRecv.Interval = 1000 * 10;
                                    timerUartRecv.Enabled = true;
                                }
                                else if (1 == myApi.MeasureMethod)
                                {
                                    myApi.SendAngleA(0); // 20180525,STA0 first
                                    timerUartRecv.Interval = 1000 * 30;
                                    timerUartRecv.Enabled = true;
                                }
                                else
                                {

                                }
                            }
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_ABXYZ_ANGLE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            myApi.SendAnglePsi(myApi.AnglePsi[MeasureIndex_Psi]);
                            timerUartRecv.Interval = 1000 * 10;
                            timerUartRecv.Enabled = true;
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_PSI_ANGLE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            myApi.SendStartMeaurePostion();
                            timerUartRecv.Interval = 1000 * 30;
                            timerUartRecv.Enabled = true;
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_A_ANGLE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;
                            myApi.SendAnglePsi(myApi.AnglePsi[MeasureIndex_Psi]);
                            timerUartRecv.Interval = 1000 * 10;
                            timerUartRecv.Enabled = true;
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_START_MEA_POS == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            myApi.SendHighVoltageUp(myApi.TubeVoltage, myApi.TubeCurrent);
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

                            myApi.RecvDataCount = 0;
                            myApi.RecvDataTable.Rows.Clear();
                            myApi.TotalDataCount = (int)((myApi.StopDegree - myApi.StartDegree) / myApi.MeasureStep) + 1;
                            myUart.Pack_Debug_out(null, "[Measure] totalcount=" + myApi.TotalDataCount.ToString());
                            myApi.SSC_RecvDataReadIndex = 0;
                            myApi.SSC_RecvDataWriteIndex = 0;
                            myApi.SSC_AnalyzeDataCount = 0;

                            myApi.RecvDataTableSaveFileStart(myApi.AnglePsi[MeasureIndex_Psi], MeasureIndex_Psi);

                            if (MeasureIndex_Psi == 0)
                            {
                                myApi.SendOpenShutter();
                                timerUartRecv.Interval = 1000 * 5;
                                timerUartRecv.Enabled = true;
                            }
                            else
                            {
                                myApi.SendStartScan();
                                timerUartRecv.Interval = 1000 * 5;
                                timerUartRecv.Enabled = true;

                                MeasureTimeCount = 0;
                                MeasureTimeCountMax = (int)(myApi.TotalDataCount * (myApi.MeasureTime + 0.12));
                                timerMeasureTimeCountDown.Enabled = true;
                                timerMeasureTimeCountDown.Interval = 1000;
                                myUart.Pack_Debug_out(null, "CountDown start" + "[" + MeasureTimeCount.ToString() + "," + MeasureTimeCountMax.ToString() + "]");
                            }
                        }
                    }
                    else if (DEVICE_CMD_ID.OPEN_LIGHT_SHUTTER == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            myApi.SendStartScan();
                            timerUartRecv.Interval = 1000 * 5;
                            timerUartRecv.Enabled = true;

                            MeasureTimeCount = 0;
                            MeasureTimeCountMax = (int)(myApi.TotalDataCount * (myApi.MeasureTime + 0.12));
                            timerMeasureTimeCountDown.Enabled = true;
                            timerMeasureTimeCountDown.Interval = 1000;
                            myUart.Pack_Debug_out(null, "CountDown start" + "[" + MeasureTimeCount.ToString() + "," + MeasureTimeCountMax.ToString() + "]");
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
                        myUart.Pack_Debug_out(null, "[Measure] analyzecount=" + (myApi.SSC_AnalyzeDataCount / 20).ToString() + "recvcount=" + myApi.RecvDataCount.ToString());

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
                }
                #endregion
                
                #region LDA 侧倾法
                else if ((2 == myApi.MeasureMethod) || (3 == myApi.MeasureMethod))
                {
                    if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;
                            if (!MeasureStopFlag)
                            {
                                myApi.SendMeasureTime(myApi.MeasureTime);

                                timerTCPRecv.Interval = 1000 * 5;
                                timerTCPRecv.Enabled = true;
                            }
                            else
                            {
                                myApi.SendCloseShutter();
                            }
                        }
                    } 
                    //else if (DEVICE_CMD_ID.SET_SCAN_STEP_DISTANCE == LastSendCmd)
                    //{
                    //    if (myApi.RecvDeviceReady(text))
                    //    {
                    //        timerUartRecv.Enabled = false;

                    //        if (1 == FiveAxisInUse)
                    //        {
                    //            myApi.SendAngleABXYZ(MeasureIndex_XYZ, MeasureIndex_Psi);
                    //            timerUartRecv.Interval = 1000 * 60;
                    //            timerUartRecv.Enabled = true;
                    //        }
                    //        else
                    //        {
                    //            if (0 == myApi.MeasureMethod)
                    //            {
                    //                myApi.SendAnglePsi(myApi.AnglePsi[MeasureIndex_Psi]);
                    //                timerUartRecv.Interval = 1000 * 10;
                    //                timerUartRecv.Enabled = true;
                    //            }
                    //            else if (1 == myApi.MeasureMethod)
                    //            {
                    //                myApi.SendAngleA(0); // 20180525,STA0 first
                    //                timerUartRecv.Interval = 1000 * 30;
                    //                timerUartRecv.Enabled = true;
                    //            }
                    //            else
                    //            {

                    //            }
                    //        }
                    //    }
                    //}
                    else if (DEVICE_CMD_ID.SET_ABXYZ_ANGLE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            myApi.SendAnglePsi(myApi.AnglePsi[MeasureIndex_Psi]);
                            timerUartRecv.Interval = 1000 * 10;
                            timerUartRecv.Enabled = true;
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_PSI_ANGLE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            myApi.SendStartMeaurePostion();
                            timerUartRecv.Interval = 1000 * 30;
                            timerUartRecv.Enabled = true;
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_A_ANGLE == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;
                            myApi.SendAnglePsi(myApi.AnglePsi[MeasureIndex_Psi]);
                            timerUartRecv.Interval = 1000 * 10;
                            timerUartRecv.Enabled = true;
                        }
                    }
                    else if (DEVICE_CMD_ID.SET_START_MEA_POS == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            myApi.SendHighVoltageUp(myApi.TubeVoltage, myApi.TubeCurrent);
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

                            myApi.RecvDataCount = 0;
                            myApi.RecvDataTable.Rows.Clear();
                            myApi.TotalDataCount = (int)((myApi.StopDegree - myApi.StartDegree) / myApi.MeasureStep) + 1;
                            myUart.Pack_Debug_out(null, "[Measure] totalcount=" + myApi.TotalDataCount.ToString());
                            myApi.SSC_RecvDataReadIndex = 0;
                            myApi.SSC_RecvDataWriteIndex = 0;
                            myApi.SSC_AnalyzeDataCount = 0;

                            myApi.RecvDataTableSaveFileStart(myApi.AnglePsi[MeasureIndex_Psi], MeasureIndex_Psi);

                            if (MeasureIndex_Psi == 0)
                            {
                                myApi.SendOpenShutter();
                                timerUartRecv.Interval = 1000 * 5;
                                timerUartRecv.Enabled = true;
                            }
                            else
                            {
                                myApi.SendStartScan();
                                timerUartRecv.Interval = 1000 * 5;
                                timerUartRecv.Enabled = true;

                                MeasureTimeCount = 0;
                                MeasureTimeCountMax = (int)(myApi.TotalDataCount * (myApi.MeasureTime + 0.12));
                                timerMeasureTimeCountDown.Enabled = true;
                                timerMeasureTimeCountDown.Interval = 1000;
                                myUart.Pack_Debug_out(null, "CountDown start" + "[" + MeasureTimeCount.ToString() + "," + MeasureTimeCountMax.ToString() + "]");
                            }
                        }
                    }
                    else if (DEVICE_CMD_ID.OPEN_LIGHT_SHUTTER == LastSendCmd)
                    {
                        if (myApi.RecvDeviceReady(text))
                        {
                            timerUartRecv.Enabled = false;

                            myApi.SendStartScan();
                            timerUartRecv.Interval = 1000 * 5;
                            timerUartRecv.Enabled = true;

                            MeasureTimeCount = 0;
                            MeasureTimeCountMax = (int)(myApi.TotalDataCount * (myApi.MeasureTime + 0.12));
                            timerMeasureTimeCountDown.Enabled = true;
                            timerMeasureTimeCountDown.Interval = 1000;
                            myUart.Pack_Debug_out(null, "CountDown start" + "[" + MeasureTimeCount.ToString() + "," + MeasureTimeCountMax.ToString() + "]");
                        }
                    }
                    //else if (DEVICE_CMD_ID.START_SCAN == LastSendCmd)
                    //{
                    //    // save data
                    //    timerUartRecv.Enabled = false;
                    //    myApi.SSC_RecvDataAnalyze(text);
                    //    myApi.RecvDataTableUpdate(myApi.SSC_AnalyzeDataArray, myApi.SSC_AnalyzeDataCount);
                    //    myApi.RecvDataTableSaveFileProc(myApi.SSC_AnalyzeDataArray, myApi.SSC_AnalyzeDataCount);

                    //    myApi.RecvDataCount += (myApi.SSC_AnalyzeDataCount / 20);
                    //    myUart.Pack_Debug_out(null, "[Measure] analyzecount=" + (myApi.SSC_AnalyzeDataCount / 20).ToString() + "recvcount=" + myApi.RecvDataCount.ToString());

                    //    myApi.SSC_AnalyzeDataCount = 0;
                    //    Array.Clear(myApi.SSC_AnalyzeDataArray, 0, myApi.SSC_AnalyzeDataArray.Length);

                    //    if (myApi.RecvDataCount >= myApi.TotalDataCount)
                    //    {
                    //        Array.Clear(myApi.SSC_RecvDataArray, 0, myApi.SSC_RecvDataArray.Length);
                    //        myApi.SSC_RecvDataReadIndex = 0;
                    //        myApi.SSC_RecvDataWriteIndex = 0;

                    //        myApi.RecvDataTableSaveFileEnd();
                    //        RecvDataFinishFlag = true;
                    //    }
                    //}
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
                }
                #endregion
               
                else
                {

                }



                if (DEVICE_CMD_ID.GET_HIGH_VOLTAGE == LastSendCmd)
                {
                    myParentForm.statusBar_HighVoltageUpdate(myApi.recvVoltage, myApi.recvCurrent);

                    ChartRealTimeShowIndex = 0;
                    chartRealTime.Series["射线强度"].Points.Clear();

                    double xMin;
                    double xMax;

                    if (0 == myApi.DetectorType)
                    {
                        xMin = myApi.StartDegree;
                        xMax = myApi.StopDegree;
                    }
                    else
                    {
                        xMin = myApi.StartDegree - myApi.StopDegree / 2;
                        xMax = myApi.StartDegree + myApi.StopDegree / 2;
                    }
                    
                    
                    chartRealTime.ChartAreas["ChartArea1"].AxisX.Maximum = xMax;
                    chartRealTime.ChartAreas["ChartArea1"].AxisX.Minimum = xMin;

                    chartFitting.ChartAreas["ChartArea1"].AxisX.Maximum = xMax;
                    chartFitting.ChartAreas["ChartArea1"].AxisX.Minimum = xMin;

                    if (MeasureIndex_Psi == 0)
                    {
                        chartFitting.Series.Clear();
                        StressViewClear();
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
                            checkBoxFiveAxis.Enabled = true;
                            checkBoxFiveAxis_CheckedChanged(this, null);
                            timerMeasureTimeCountDown.Enabled = false;
                            myUart.Pack_Debug_out(null, "CountDown stop" + "[" + MeasureTimeCount.ToString() + "," + MeasureTimeCountMax.ToString() + "]");
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
                    ChartRealTimeUpdate();
                }
                else
                {

                }
                // 接收数据完成
                if (RecvDataFinishFlag)
                {
                    RecvDataFinishFlag = false;

                    PsiMeasureFinish();
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void PsiMeasureFinish()
        {
            // 更新拟合图像
            string seriesName = myApi.AnglePsi[MeasureIndex_Psi].ToString();
            string fileName = myApi.RecvDataFileName;
            double[] x;
            double[] y;
            // 解析txt文件
            myUart.Pack_Debug_out(null, "[Measure] parse txt=" + fileName);
            CurveFitting.txt_parse(fileName, out x, out y);
            // 更新拟合图像
            ChartFittingUpdate(seriesName, fileName, x);

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
                // 更新应力计算结果
                StressViewUpdate(myApi.AnglePsi.Length);
                // 生成RSC文件
                ToolStripMenuItemRSC_Click(this, null);
                // 生成xls文件
                ChartFittingImageName = myApi.RecvDataFileNameArray[0].Replace("txt", "bmp");
                ChartFittingExport(ChartFittingImageName);
                //ToolStripMenuItemMsExcel_Click(this, null);

                if (1 == FiveAxisInUse)
                {
                    if (MeasureIndex_XYZ < myApi.AngleBeta.Length - 1)
                    {
                        MeasureIndex_XYZ++;
                        myApi.SendAngleABXYZ(MeasureIndex_XYZ, MeasureIndex_Psi);
                        timerUartRecv.Interval = 1000 * 60;
                        timerUartRecv.Enabled = true;
                    }
                    else
                    {
                        myApi.SendCloseShutter();
                        timerUartRecv.Interval = 1000 * 5;
                        timerUartRecv.Enabled = true;
                    }
                }
                else
                {
                    myApi.SendCloseShutter();
                    timerUartRecv.Interval = 1000 * 5;
                    timerUartRecv.Enabled = true;
                }
            }
        }

        public void TCPRecv_Measure(byte[] data, int PackageLen)
        {
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;

            try
            {
                if (DEVICE_CMD_ID.SET_MES_TIME == LastSendCmd)
                {
                    result = myApi.RecvTCPCmdResultCheck(data, out TCPReturnCode);
                    if (result)
                    {
                        timerTCPRecv.Enabled = false;
                        // T.B.D
                        // 计算步宽
                        //myApi.SendHighVoltageUp(myApi.TubeVoltage, myApi.TubeCurrent);
                        //timerUartRecv.Interval = 1000 * 40;
                        //timerUartRecv.Enabled = true;

                        //return;





                        // 设置四轴/Psi
                        if (1 == FiveAxisInUse)
                        {
                            myApi.SendAngleABXYZ(MeasureIndex_XYZ, MeasureIndex_Psi);
                            timerUartRecv.Interval = 1000 * 60;
                            timerUartRecv.Enabled = true;
                        }
                        else
                        {
                            if (2 == myApi.MeasureMethod)
                            {
                                myApi.SendAnglePsi(myApi.AnglePsi[MeasureIndex_Psi]);
                                timerUartRecv.Interval = 1000 * 10;
                                timerUartRecv.Enabled = true;
                            }
                            else if (3 == myApi.MeasureMethod)
                            {
                                myApi.SendAngleA(0); // 20180525,STA0 first
                                timerUartRecv.Interval = 1000 * 30;
                                timerUartRecv.Enabled = true;
                            }
                            else
                            {

                            }
                        }
                    }
                }
                else if (DEVICE_CMD_ID.START_SCAN == LastSendCmd)
                {
                    result = myApi.RecvTCPCmdResultCheck(data, out TCPReturnCode);
                    if (result)
                    {
                        timerTCPRecv.Enabled = false;
                        int time = (int)(myApi.MeasureTime * 1000);
                        Thread.Sleep(time);
                        myApi.ReadFrames(1);
                        timerTCPRecv.Interval = 1000 * 5;
                        timerTCPRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.READ_FRAME == LastSendCmd)
                {
                    // save data
                    timerUartRecv.Enabled = false;

                    if (myApi.TCP_RecvDataAnalyze(data))
                    {
                        double[] TCP_PsiAngleArray = new double[640];
                        // 起始角度=峰位角度 - 测量范围/2
                        double startDegree = myApi.StartDegree - 25.6 / 2;
                        for (int i = 0; i < TCP_PsiAngleArray.Length; i++)
                        {
                            TCP_PsiAngleArray[i] = startDegree + i * 25.6 / 640;
                        }

                        double[] x;
                        double[] y;
                        double[] smooth_y;
                        myApi.TCP_RecvDataSplit(TCP_PsiAngleArray, myApi.TCP_RecvIntensityArray, 8, out x, out y);

                        myApi.TCP_RecvDataSmooth(y, out smooth_y);

                        double[] y2 = new double[4800];
                        System.Array.Copy(smooth_y, 160, y2, 0, 4800);


                        //// 截取有效数据
                        //double[] TCP_PsiAngleArray = new double[600];
                        //// 起始角度=峰位角度 - 测量范围/2
                        //double startDegree = myApi.StartDegree - myApi.StopDegree / 2;
                        //for (int i = 0; i < TCP_PsiAngleArray.Length; i++)
                        //{
                        //    TCP_PsiAngleArray[i] = startDegree + i * myApi.StopDegree / 600;
                        //}

                        //myApi.RecvDataTableUpdate(TCP_PsiAngleArray, myApi.TCP_RecvIntensityArray);
                        //myApi.RecvDataTableSaveFileProc(TCP_PsiAngleArray, myApi.TCP_RecvIntensityArray);

                        Array.Clear(myApi.SSC_RecvDataArray, 0, myApi.SSC_RecvDataArray.Length);
                        myApi.SSC_RecvDataReadIndex = 0;
                        myApi.SSC_RecvDataWriteIndex = 0;

                        myApi.RecvDataTableSaveFileEnd();

                        ChartRealTimeUpdate();

                        PsiMeasureFinish();
                    }
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }









        private void ToolStripMenuItemMsWord_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItemMsExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Microsoft.Office.Interop.Excel.Application myExcel = new Microsoft.Office.Interop.Excel.Application();
                //myExcel.DisplayAlerts = false;
                //string path = Application.StartupPath;
                //myExcel.Workbooks.Add(path + "\\Resources\\" + "应力数据报告（侧倾法）.xls");

                ////图片
                //float PicLeft, PicTop, PicWidth, PicHeight;

                //Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)myExcel.Sheets[1];
                //Microsoft.Office.Interop.Excel.Range rangePic = ws.get_Range("B5:I20", Type.Missing);
                //PicLeft = Convert.ToSingle(rangePic.Left);
                //PicTop = Convert.ToSingle(rangePic.Top);
                //PicWidth = Convert.ToSingle(rangePic.Width);
                //PicHeight = Convert.ToSingle(rangePic.Height);
                //ws.Shapes.AddPicture(ChartFittingImageName, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoTrue, PicLeft, PicTop, PicWidth, PicHeight);


                //ws.Cells[22, 3] = DateTime.Now.ToShortDateString();
                //ws.Cells[22, 5] = "";//文件名
                //ws.Cells[22, 8] = textBoxSampleSn.Text.ToString();
                //ws.Cells[23, 3] = "admin";//用户名
                //ws.Cells[24, 3] = "同倾法";
                //ws.Cells[24, 5] = "Pearson VII 85%";

                //ws.Cells[26, 2] = myApi.StartDegree.ToString("0.0000");
                //ws.Cells[26, 3] = myApi.StopDegree.ToString("0.0000");
                //ws.Cells[26, 4] = textBoxStopDegree.Text.ToString();
                //ws.Cells[26, 5] = textBoxMeaStep.Text.ToString();
                //ws.Cells[26, 6] = textBoxMeaTime.Text.ToString();
                //ws.Cells[26, 7] = textBoxTubeVoltage.Text.ToString();
                //ws.Cells[26, 8] = textBoxTubeCurrent.Text.ToString();

                //ws.Cells[29, 2] = myApi.AngleX[MeasureIndex_XYZ].ToString("0.0000");
                //ws.Cells[29, 3] = myApi.AngleY[MeasureIndex_XYZ].ToString("0.0000");
                //ws.Cells[29, 4] = myApi.AngleZ[MeasureIndex_XYZ].ToString("0.0000");
                //ws.Cells[29, 5] = "";//PHI位置

                //int dtRowCount = 0;
                //if (myResultTable.myDataTable.Rows.Count != 0)
                //{
                //    for (int i = 0; i < myResultTable.myDataTable.Rows.Count; i++)
                //    {
                //        Microsoft.Office.Interop.Excel.Range range = (Microsoft.Office.Interop.Excel.Range)myExcel.Sheets[1].Rows[32 + i, Type.Missing];
                //        // range.EntireRow.Insert(Microsoft.Office.Interop.Excel.XlDirection.xlDown,Microsoft.Office.Interop.Excel.XlInsertFormatOrigin.xlFormatFromLeftOrAbove);
                //        range.EntireRow.Insert(0);

                //        ws.Cells[32 + i, 2] = myResultTable.myDataTable.Rows[i][0].ToString();
                //        ws.Cells[32 + i, 3] = myResultTable.myDataTable.Rows[i][1].ToString();
                //        ws.Cells[32 + i, 4] = myResultTable.myDataTable.Rows[i][2].ToString();
                //        ws.Cells[32 + i, 5] = myResultTable.myDataTable.Rows[i][3].ToString();
                //        ws.Cells[32 + i, 6] = myResultTable.myDataTable.Rows[i][4].ToString();
                //        ws.Cells[32 + i, 7] = myResultTable.myDataTable.Rows[i][5].ToString();
                //        ws.Cells[32 + i, 7] = myResultTable.myDataTable.Rows[i][6].ToString();


                //        dtRowCount = i;
                //    }
                //}

                //ws.Cells[35 + dtRowCount, 3] = "DX-2700BH";
                //ws.Cells[36 + dtRowCount, 3] = "SDD探测器（或线阵探测器）";
                //ws.Cells[37 + dtRowCount, 3] = "Cr-2.291";
                //ws.Cells[38 + dtRowCount, 3] = "";//接收狭缝
                //ws.Cells[38 + dtRowCount, 5] = "";//发散狭缝
                //ws.Cells[38 + dtRowCount, 7] = "";//散射狭缝
                //ws.Cells[39 + dtRowCount, 3] = "";//材料
                //ws.Cells[40 + dtRowCount, 3] = "DX-2700BH-Ver1.0";
                //ws.Cells[41 + dtRowCount, 3] = "B1.17";

                //string fileName = myApi.RecvDataFileNameArray[0].Replace("txt", "xls"); //要保存的excel文件名
                //myUart.Pack_Debug_out(null, "[Measure] Create xls=" + fileName);

                //myExcel.Workbooks[1].SaveCopyAs(fileName);
                //myExcel.Application.Workbooks.Close();
                //myExcel.Quit();
                //System.Runtime.InteropServices.Marshal.ReleaseComObject(myExcel);
                //myExcel = null;
                //GC.Collect();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ToolStripMenuItemPdf_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItemPageSetup_Click(object sender, EventArgs e)
        {
            PageSetupDialog pageSetupDialog = new PageSetupDialog();
            pageSetupDialog.Document = printDocumentGridView;
            pageSetupDialog.ShowDialog();
        }

        private void ToolStripMenuItemPrintSetup_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocumentGridView;
            printDialog.ShowDialog(); 
        }

        private void ToolStripMenuItemPrintPreview_Click(object sender, EventArgs e)
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocumentGridView;
            //lineReader = new StringReader(stb.ToString());
            try
            {
                printPreviewDialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "打印出错", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }  
        }

        private void ToolStripMenuItemPrintStart_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItemRSC_Click(object sender, EventArgs e)
        {
            //DataTable myDt = new DataTable("dt");
            //DataColumnCollection columns = myDt.Columns;
            //string[] myColumnNames = { "x", "y" };
            //DataColumn column = null;
            //int lineBytesCount = 77;
            //myApi.RecvDataFileNameArray = new string[myApi.AnglePsi.Length];
            //myApi.RecvDataFileNameArray[0] = "D:\\0528\\SDD_sample_name_001_Psi-0_20180528-074414.txt";
            //myApi.RecvDataFileNameArray[1] = "D:\\0528\\SDD_sample_name_001_Psi-15_20180528-074458.txt";
            //myApi.RecvDataFileNameArray[2] = "D:\\0528\\SDD_sample_name_001_Psi-20_20180528-074540.txt";
            //myApi.RecvDataFileNameArray[3] = "D:\\0528\\SDD_sample_name_001_Psi-30_20180528-074623.txt";

            try
            {
                string RscFileName = myApi.RecvDataFileNameArray[0].Replace("txt", "RSC");
                myUart.Pack_Debug_out(null, "[Measure] Create RSC=" + RscFileName);
                FileStream fs = new FileStream(RscFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.ASCII);

                //for (int i = 0; i < myColumnNames.Length; i++)
                //{
                //    column = columns.Add(myColumnNames[i], typeof(double));
                //}

                string lineStr = null;
                for (int i = 0; i < myApi.RecvDataFileNameArray.Length; i++)
                {
                    double[] x;
                    double[] y;

                    CurveFitting.txt_parse(myApi.RecvDataFileNameArray[i], out x, out y);

                    //for (int j = 0; (j < x.Length); j++)
                    //{
                    //    DataRow row = myDt.NewRow();
                    //    row[0] = x[j];
                    //    row[1] = y[j];
                    //    myDt.Rows.Add(row);
                    //}
                    if (i == 0)
                    {
                        lineStr = "#.DATE. " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "                                                  .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.GTkV. " + myApi.TubeVoltage.ToString() + "                                                                   .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.GCmA. " + myApi.TubeCurrent.ToString() + "                                                                   .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.WLa1. " + "2.2907" + "                                                              .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.ANFG. " + myApi.StartDegree.ToString() + "                                                                  .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.ENDE. " + myApi.StopDegree.ToString() + "                                                                  .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.STEP. " + myApi.MeasureStep.ToString() + "                                                                 .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.t/St. " + myApi.MeasureTime.ToString() + "                                                                 .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.nScn. " + (myApi.AnglePsi.Length).ToString() + "                                                             .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.MaxI. " + y.Max().ToString() + "                                                .";
                        sw.WriteLine(lineStr);
                        lineStr = "#.    .                                                                      .";
                        sw.WriteLine(lineStr);
                    }
 
                    lineStr = "#.iScn. " + (i+1).ToString() + "                                                .";
                    sw.WriteLine(lineStr);
                    lineStr = "#.STPi. " + myApi.MeasureStep.ToString() + "                                                .";
                    sw.WriteLine(lineStr);
                    lineStr = "#.PSIi. " + myApi.AnglePsi[i].ToString() + "                                                .";
                    sw.WriteLine(lineStr);
                    lineStr = "#.FETA. -1                                                                   .";
                    sw.WriteLine(lineStr);

                    int lineCount = y.Length / 10;
                    int k = 0;
                    for (int j = 0; j < lineCount; j++)
                    {
                        lineStr = SerialPortCommon.RightAlign_InSeven(x[k].ToString("#0.000"));
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 0].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 1].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 2].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 3].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 4].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 5].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 6].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 7].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 8].ToString());
                        lineStr += SerialPortCommon.RightAlign_InSeven(y[k + 9].ToString());
                        sw.WriteLine(lineStr);
                        k += 10;
                    }
                    if (y.Length % 10 != 0)
                    {
                        lineStr = SerialPortCommon.RightAlign_InSeven(x[k].ToString("#0.000"));
                        for (int j = 0; j < y.Length % 10; j++)
                        {
                            lineStr += SerialPortCommon.RightAlign_InSeven(y[k].ToString());
                            k++;
                        }
                        for (int j = 0; j < (10 - y.Length % 10); j++)
                        {
                            lineStr += SerialPortCommon.RightAlign_InSeven(0.ToString());
                        }
                        sw.WriteLine(lineStr);
                    }
                }

                lineStr = "#.EOFm.                                                                      .";
                sw.WriteLine(lineStr);

                sw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            } 
        }





    }
}
