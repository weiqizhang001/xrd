using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Threading;

namespace XRD_Tool
{
    public partial class FormDeviceInit : Form
    {
        public SerialPortCommon myUart;    
        public XrdConfig myConfig;
        public TCPClient myTCP;
        public MeasureApi myApi;
        public byte deviceStateBackup;

        public string ServerIP = "192.168.0.90";
        public string ServerPort = "1031";

        public System.Timers.Timer timerUartRecv;
        public System.Timers.Timer timerTCPRecv;
        public int TCPReturnCode = 0x7FFFFFFF;

        public FormDeviceInit()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        public FormDeviceInit(FormUserLogin form)
        {
            myUart = form.myUart;
            myConfig = form.myConfig;
            myTCP = new TCPClient(myUart, null, ServerIP, ServerPort);
            myApi = new MeasureApi(this);
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
                myUart.DeviceState = DEVICE_STATE.DEVICE_INIT;
                myUart.RegControl(this, UartRecv_DeviceInit, DEVICE_STATE.DEVICE_INIT);

                myTCP.DeviceState = DEVICE_STATE.DEVICE_INIT;
                myTCP.RegControl(this, TCPRecv_DeviceInit, DEVICE_STATE.DEVICE_INIT); 
            }
            else
            {

            }
        }

        public FormDeviceInit(FormMDIParent form)
        {
            myUart = form.myUart;
            myConfig = form.myConfig;
            myTCP = new TCPClient(myUart, null, ServerIP, ServerPort);
            myApi = new MeasureApi(this);
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

            if (myUart.port_UART.IsOpen)
            {
                deviceStateBackup = myUart.DeviceState;
                myUart.DeviceState = DEVICE_STATE.DEVICE_INIT;
                myUart.RegControl(this, UartRecv_DeviceInit, DEVICE_STATE.DEVICE_INIT);

                    myTCP.DeviceState = DEVICE_STATE.DEVICE_INIT;
                myTCP.RegControl(this, TCPRecv_DeviceInit, DEVICE_STATE.DEVICE_INIT);
            }
            else
            {

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

        private void DeviceInit_Load(object sender, EventArgs e)
        {
            try
            {
                ReadFromConfig();

                if (myApi.TargetType == 0)
                {
                    radioButtonTargetType1.Checked = true;
                }
                else if (myApi.TargetType == 1)
                {
                    radioButtonTargetType2.Checked = true;
                }
                else
                {

                }
                if (myApi.DetectorType == 0)
                {
                    radioButtonDetectorType1.Checked = true;
                }
                else if (myApi.DetectorType == 1)
                {
                    radioButtonDetectorType2.Checked = true;
                }
                else
                {
                    
                }

                if (!Directory.Exists(myApi.SavePath))
                {
                    myApi.SavePath = System.IO.Directory.GetCurrentDirectory();
                }
                textBoxSavePath.Text = myApi.SavePath;
                buttonDone.Enabled = false;

                if (myUart.port_UART.IsOpen)
                { 
                    DetectorInitStart();
                }
                else
                {
                    myUart.Pack_Debug_out(null, "[DeviceInit] Init faild, Uart Port not open");
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            } 
        }

        private void FormDeviceInit_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void buttonSkip_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonTargetType1.Checked)
                {
                    myApi.TargetType = 0;
                }
                else if (radioButtonTargetType2.Checked)
                {
                    myApi.TargetType = 1;
                }
                else
                {
                    myApi.TargetType = 0;
                }

                if (radioButtonDetectorType1.Checked)
                {
                    myApi.DetectorType = 0;
                }
                else if (radioButtonDetectorType2.Checked)
                {
                    myApi.DetectorType = 1;
                }
                else
                {
                    myApi.DetectorType = 0;
                }

                SaveToConfig();

                myConfig.SaveDeviceInit();

                myUart.Pack_Debug_out(null, "[DeviceInit] Init Skip");

                this.Hide();

                FormMDIParent mdi = new FormMDIParent(this);
                mdi.StartPosition = FormStartPosition.CenterScreen;
                //mdi.ShowDialog();
                mdi.Show();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioButtonTargetType1.Checked)
                {
                    myApi.TargetType = 0;
                }
                else if (radioButtonTargetType2.Checked)
                {
                    myApi.TargetType = 1;
                }
                else
                {
                    myApi.TargetType = 0;
                }

                if (radioButtonDetectorType1.Checked)
                {
                    myApi.DetectorType = 0;
                }
                else if (radioButtonDetectorType2.Checked)
                {
                    myApi.DetectorType = 1; 
                }
                else
                {
                    myApi.DetectorType = 0;
                }

                SaveToConfig();

                myConfig.SaveDeviceInit();

                if (WarmUpCheck())
                {
                    if (!WarmUpConfirm())
                    {
                        WarmUpSkip();
                    }
                }
                else
                {
                    WarmUpSkip();
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void DetectorInitStart()
        {
            try
            {
                myUart.Pack_Debug_out(null, "[DeviceInit] Init Start");
                myApi.SendDevicePause();
                timerUartRecv.Interval = 1000 * 40;
                timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void DetectorInitFinish()
        {
            try
            {
                myUart.Pack_Debug_out(null, "[DeviceInit] Init Finish");

                this.Hide();

                FormMDIParent mdi = new FormMDIParent(this);
                mdi.StartPosition = FormStartPosition.CenterScreen;
               // mdi.ShowDialog(); 
                mdi.Show();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void UartRecv_DeviceInit(byte[] text, int PackageLen)
        {   
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;

            try
            {
                string strRecvData = System.Text.Encoding.Default.GetString(text);
                if (strRecvData.IndexOf("**") >= 0)
                {
                    myUart.DeviceStateBackup = myUart.DeviceState;
                    myUart.DeviceState = DEVICE_STATE.DEVICE_ERROR;
                    timerUartRecv.Enabled = false;

                    myApi.SendDevicePause();
                    timerUartRecv.Interval = 1000 * 5;
                    timerUartRecv.Enabled = true;
                    //showErrorMessageBox(strRecvData);
                    myUart.Pack_Debug_out(text, "Recv Device Error" + "[" + strRecvData + "]");
                }
                else
                {
                    if (DEVICE_STATE.DEVICE_ERROR == myUart.DeviceState)
                    {
                        if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
                        {
                            result = myApi.RecvDeviceReady(text);
                            if (result)
                            {
                                timerUartRecv.Enabled = false;

                                myApi.SendCloseShutter();
                                timerUartRecv.Interval = 1000 * 5;
                                timerUartRecv.Enabled = true;
                            }
                        }
                        else if (DEVICE_CMD_ID.CLOSE_LIGHT_SHUTTER == LastSendCmd)
                        {
                            result = myApi.RecvDeviceReady(text);
                            if (result)
                            {
                                timerUartRecv.Enabled = false;

                                myApi.SendHighVoltageDown(myApi.DefaultVotage, myApi.DefaultCurrent);
                                timerUartRecv.Interval = 1000 * 10;
                                timerUartRecv.Enabled = true;
                            }
                        }
                        else if (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_DOWN == LastSendCmd)
                        {
                            result = myApi.RecvDeviceReady(text);
                            if (result)
                            {
                                timerUartRecv.Enabled = false;

                                myUart.DeviceState = myUart.DeviceStateBackup;
                            }
                        }
                        else
                        {

                        }

                        return;
                    }
                }



                //if (DEVICE_CMD_ID.CHECK_DEV_READY == LastSendCmd)
                if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                        myApi.CheckHostAttachStatus();
                        timerUartRecv.Interval = 1000 * 5;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.CHECK_HOST_ATTACH_STATUS == LastSendCmd)
                {
                    result = myApi.RecvHostAttachStatus(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                        myApi.SendGetHighVoltage();
                        timerUartRecv.Interval = 1000 * 5;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.GET_HIGH_VOLTAGE == LastSendCmd)
                {
                    result = myApi.RecvGetHighVoltage(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                    }
                }
                else if (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_UP == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                    }
                }
                else if (DEVICE_CMD_ID.GET_DEV_ZERO_POINT == LastSendCmd)
                {
                    result = myApi.RecvGetDeviceZeroPoint(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                        myApi.FiveAxis_GetZeroPoint();
                        timerUartRecv.Interval = 1000 * 5;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.GET_5_AXIS_ZERO_POINT == LastSendCmd)
                {
                    result = myApi.RecvFiveAxis_GetZeroPoint(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                    }
                }
                else if (DEVICE_CMD_ID.SET_SDP == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                    }
                }
                else
                {
                    timerUartRecv.Enabled = false;
                }

                if (result)
                {
                    if (DEVICE_CMD_ID.GET_HIGH_VOLTAGE == LastSendCmd)
                    {
                        buttonDone.Enabled = true;
                    }
                    else if (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_UP == LastSendCmd)
                    {
                        WarmUpWait();
                    }
                    else if (DEVICE_CMD_ID.GET_5_AXIS_ZERO_POINT == LastSendCmd)
                    {

                        myApi.SendSetSdp(myApi.DetectorType + 1);
                        timerUartRecv.Interval = 1000 * 10;
                        timerUartRecv.Enabled = true;

                        //if (ZeroPointCheck())
                        //{
                        //    if (ZeroPointModifyConfirm())
                        //    {
                        //        ZeroPointModifyProc();
                        //    }
                        //    else
                        //    {

                        //    }
                        //}
                        //else
                        //{
                            
                        //}
                    }
                    else if (DEVICE_CMD_ID.SET_SDP == LastSendCmd)
                    {
                        if (0 == myApi.DetectorType)
                        {
                            // 如果SDD，通信间隔超过60min，等待3min
                            DetectorInitFinish();
                        }
                        else if (1 == myApi.DetectorType)
                        {
                            TCPRecv_DeviceInit();

                        }
                        else
                        {

                        }  
                    }
                    else
                    {

                    }
                }
                else
                {
                    //string strRecvData = System.Text.Encoding.Default.GetString(text);
                    //showErrorMessageBox(strRecvData);
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }   
        }

        //public void UartRecv_DeviceError(byte[] text, int PackageLen)
        //{
        //    bool result = false;
        //    int LastSendCmd = myApi.CurrentSendCmd;

        //    try
        //    {
        //        string strRecvData = System.Text.Encoding.Default.GetString(text);
        //        if (strRecvData.IndexOf("*") >= 0)
        //        {
        //            myUart.DeviceStateBackup = myUart.DeviceState;
        //            myUart.DeviceState = DEVICE_STATE.DEVICE_ERROR;
        //            timerUartRecv.Enabled = false;

        //            myApi.SendDevicePause();
        //            timerUartRecv.Interval = 1000 * 5;
        //            timerUartRecv.Enabled = true;
        //            //showErrorMessageBox(strRecvData);
        //            myUart.Pack_Debug_out(text, "Recv Device Error" + "[" + strRecvData + "]");
        //        }
        //        else
        //        {
        //            if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
        //            {
        //                result = myApi.RecvDeviceReady(text);
        //                if (result)
        //                {
        //                    timerUartRecv.Enabled = false;

        //                    myApi.SendCloseShutter();
        //                    timerUartRecv.Interval = 1000 * 5;
        //                    timerUartRecv.Enabled = true;
        //                }
        //            }
        //            else if (DEVICE_CMD_ID.CLOSE_LIGHT_SHUTTER == LastSendCmd)
        //            {
        //                result = myApi.RecvDeviceReady(text);
        //                if (result)
        //                {
        //                    timerUartRecv.Enabled = false;

        //                    myApi.SendHighVoltageDown(myApi.DefaultVotage, myApi.DefaultCurrent);
        //                    timerUartRecv.Interval = 1000 * 10;
        //                    timerUartRecv.Enabled = true;
        //                }
        //            }
        //            else if (DEVICE_CMD_ID.SET_HIGH_VOLTAGE_DOWN == LastSendCmd)
        //            {
        //                result = myApi.RecvDeviceReady(text);
        //                if (result)
        //                {
        //                    timerUartRecv.Enabled = false;

        //                    myUart.DeviceState = myUart.DeviceStateBackup;
        //                }
        //            }
        //            else
        //            {

        //            }
        //        } 
        //    }
        //    catch (Exception ex)
        //    {
        //        myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
        //    }            
        //}

        private bool WarmUpCheck()
        {
            bool needconfirm = false;

            try
            {
                if ((null == myConfig.WarmUpLastTime) || (" " == myConfig.WarmUpLastTime))
                {
                    needconfirm = true;

                }
                else
                {
                    DateTime now = DateTime.Now;
                    TimeSpan ts = now.Subtract(Convert.ToDateTime(myConfig.WarmUpLastTime));
                    if (ts.Hours > myConfig.WarmUpIntervalMinute)
                    {
                        needconfirm = true;
                    }
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            myUart.Pack_Debug_out(null, "[DeviceInit] warmup check=" + needconfirm.ToString());

            return needconfirm;
        }

        private bool WarmUpConfirm()
        {
            bool confirm = false;

            try
            {
                FormWarmUpConfirm form = new FormWarmUpConfirm();
                
                form.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    myApi.SendWarmUp(myConfig.WarmUpVoltage, myConfig.WarmUpCurrent);
                    timerUartRecv.Interval = 1000 * 40;
                    timerUartRecv.Enabled = true;
                    confirm = true;
                }
                else
                {
                    confirm = false;
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            myUart.Pack_Debug_out(null, "[DeviceInit] warmup confirm=" + confirm.ToString());

            return confirm;
        }

        private void WarmUpWait()
        {
            try
            {
                myUart.Pack_Debug_out(null, "[DeviceInit] warmup progress start");

                FormWarmUpProgress form = new FormWarmUpProgress(this);

                form.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = form.ShowDialog();
                myUart.Pack_Debug_out(null, "[DeviceInit] warmup progress finish");

                myApi.GetDeviceZeroPoint();
                timerUartRecv.Interval = 1000 * 5;
                timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }   
        }

        private void WarmUpSkip()
        {
            try
            {
                myUart.Pack_Debug_out(null, "[DeviceInit] warmup skip, GetDeviceZeroPoint");

                myApi.GetDeviceZeroPoint();
                timerUartRecv.Interval = 1000 * 5;
                timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private bool ZeroPointCheck()
        {
            bool NeedConfirm = false;

            try
            {
                if ((myConfig.ZeroPointMotorTS[myConfig.DetectorType] != myApi.DetectorZeroPoint[0])
                    || (myConfig.ZeroPointMotorTD[myConfig.DetectorType] != myApi.DetectorZeroPoint[1])
                    || (myConfig.ZeroPointAlpha[myConfig.DetectorType] != myApi.DetectorZeroPoint[2])
                    || (myConfig.ZeroPointBeta[myConfig.DetectorType] != myApi.DetectorZeroPoint[3])
                    || (myConfig.ZeroPointX[myConfig.DetectorType] != myApi.DetectorZeroPoint[4])
                    || (myConfig.ZeroPointY[myConfig.DetectorType] != myApi.DetectorZeroPoint[5])
                    || (myConfig.ZeroPointZ[myConfig.DetectorType] != myApi.DetectorZeroPoint[6]))
                {
                    NeedConfirm = true;
                }
                else
                {
                    NeedConfirm = false;
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
            
            return NeedConfirm;
        }

        private bool ZeroPointModifyConfirm()
        {
            bool confirm = false;

            try
            {
                FormZeroPointConfirm form = new FormZeroPointConfirm(this);
                
                form.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    confirm = true;
                }
                else
                {
                    confirm = false;
                }

            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
            
            return confirm;
        }

        private bool ZeroPointModifyProc()
        {
            bool confirm = false;

            // T.B.D
            try
            {

            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return confirm;
        }      

        private void buttonSavePath_Click(object sender, EventArgs e)
        {
            try
            {
                FolderBrowserDialog dialog = new FolderBrowserDialog();

                dialog.Description = "请选择文件路径";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    myApi.SavePath = dialog.SelectedPath;
                    textBoxSavePath.Text = dialog.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            } 
        }      

        public static void showErrorMessageBox(string text)
        {
            try
            {
                MessageBoxButtons box_button = MessageBoxButtons.OK;
                MessageBoxIcon box_icon = MessageBoxIcon.Error;
                MessageBoxDefaultButton def_button = MessageBoxDefaultButton.Button1;

                DialogResult result2 = MessageBox.Show(text, "WARNING", box_button, box_icon, def_button);
                if (result2 == DialogResult.OK)
                {

                    return;
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //DetectorErrorRecvData(System.Text.Encoding.ASCII.GetBytes("**12 DOOR OPENED FAULT"), 0);
                //myApi.UartSendCmd(System.Text.Encoding.ASCII.GetBytes("SSS\r"));
                myApi.SendGetHighVoltage();
                timerUartRecv.Interval = 1000 * 5;
                timerUartRecv.Enabled = true;
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
                myApi.SavePath = myConfig.SavePath;
                myApi.TargetType = myConfig.TargetType;
                myApi.DetectorType = myConfig.DetectorType;
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
                myConfig.SavePath = myApi.SavePath;
                myConfig.TargetType = myApi.TargetType;
                myConfig.DetectorType = myApi.DetectorType;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void FormDeviceInit_FormClosing(object sender, FormClosingEventArgs e)
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


        public void TCPRecv_DeviceInit(byte[] data, int PackageLen)
        {
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;

            try 
            {
                if (DEVICE_CMD_ID.RESET == LastSendCmd)
                {
                    result = myApi.RecvTCPCmdResultCheck(data, out TCPReturnCode);
                    if (result)
                    {
                        timerTCPRecv.Enabled = false;
                        string name;
                        if (0 == myApi.TargetType)
                        {
                            name = "Cr";
                        }
                        else
                        {
                            name = "Cu";
                        }

                        myApi.SetTarget(name);
                    }
                }
                else if (DEVICE_CMD_ID.SET_TARGET == LastSendCmd)
                {
                    result = myApi.RecvTCPCmdResultCheck(data, out TCPReturnCode);
                    if (result)
                    {
                        timerTCPRecv.Enabled = false;
                        // init end
                        DetectorInitFinish();
                    }
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }  
        }

        public void TCPRecv_DeviceInit()
        {
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;

            try
            {
                while (!myApi.Connect())
                {
                    Thread.Sleep(1000);
                }

                if (DEVICE_STATE.DEVICE_INIT == myUart.DeviceState)
                {
                    if (DEVICE_CMD_ID.RESET == myApi.CurrentSendCmd)
                    {
                        result = myApi.RecvTCPCmdResultCheck(myApi.TCPRecvBytes, out TCPReturnCode);
                        if (result)
                        {
                            timerTCPRecv.Enabled = false;
                            string name;
                            if (0 == myApi.TargetType)
                            {
                                name = "Cr";
                            }
                            else
                            {
                                name = "Cu";
                            }

                            myApi.SetTarget(name);
                            result = myApi.RecvTCPCmdResultCheck(myApi.TCPRecvBytes, out TCPReturnCode);
                            if (result)
                            {
                                timerTCPRecv.Enabled = false;
                                // init end
                                DetectorInitFinish();
                            }
                        }
                    }
                    //else if (DEVICE_CMD_ID.SET_TARGET == myApi.CurrentSendCmd)
                    //{
                    //    result = myApi.RecvTCPCmdResultCheck(myApi.TCPRecvBytes, out TCPReturnCode);
                    //    if (result)
                    //    {
                    //        timerTCPRecv.Enabled = false;
                    //        // init end
                    //        DetectorInitFinish();
                    //    }
                    //}
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

        private void FormDeviceInit_Activated(object sender, EventArgs e)
        {
            //if (myUart.port_UART.IsOpen)
            //{
            //    myUart.DeviceState = DEVICE_STATE.DEVICE_INIT;
            //    myUart.RegControl(this, UartRecv_DeviceInit, DEVICE_STATE.DEVICE_INIT);
            //    //myUart.RegControl(this, UartRecv_DeviceError, DEVICE_STATE.DEVICE_ERROR);

            //    DetectorInitStart();
            //}
        }  

        
    }
}
