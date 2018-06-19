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
    public partial class FormShortcutAutoFocus : Form
    {
        public SerialPortCommon myUart;
        public System.Timers.Timer timerUartRecv;

        public MeasureApi myApi;
        public byte deviceStateBackup;
        public double[] showAngle = new double[5];
        public bool load_flag = false;

        public FormShortcutAutoFocus(FormMDIParent form)
        {
            myUart = form.myUart;
            myApi = form.myApi;
            myApi.AutoFocus = form.myConfig.AutoFocus;
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
                myUart.DeviceState = DEVICE_STATE.SHORTCUT_AUTO_FOCUS;
                myUart.RegControl(this, UartRecv_AutoFocus, DEVICE_STATE.SHORTCUT_AUTO_FOCUS);
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

        private void FormShortcutAutoFocus_Load(object sender, EventArgs e)
        {
            try
            {
                buttonOK.Enabled = false;
                buttonCancel.Enabled = false;

                if (myUart.port_UART.IsOpen)
                {
                    myApi.SendGetAngleABXYZ();
                    timerUartRecv.Interval = 1000 * 5;
                    timerUartRecv.Enabled = true;
                }
                else
                {

                }
               
                load_flag = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }
        private void FormShortcutAutoFocus_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                myUart.DeviceState = deviceStateBackup;
                myUart.RemoveControl(this);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try 
            {
                myUart.Pack_Debug_out(null, "[Focus] Focus Start");

                myApi.SendMovMotorTD(myApi.AutoFocus[0]);
                timerUartRecv.Interval = 1000 * 30;
                timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Focus] Focus Stop");

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

        private void buttonExit_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }
        public void UartRecv_AutoFocus(byte[] text, int PackageLen)
        {
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;

            try
            {
                if (DEVICE_CMD_ID.MOVE_TD == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                        myApi.SendMovMotorTS(myApi.AutoFocus[1]);
                        timerUartRecv.Interval = 1000 * 30;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.MOVE_TS == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                        myApi.SendAngleAlpha(myApi.AutoFocus[2]);
                        timerUartRecv.Interval = 1000 * 30;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.SET_ALPHA_ANGLE == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                        myApi.SendAngleZ(myApi.AutoFocus[3]);
                        timerUartRecv.Interval = 1000 * 30;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.SET_Z_ANGLE == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                        myApi.SendGetAngleABXYZ();
                        timerUartRecv.Interval = 1000 * 5;
                        timerUartRecv.Enabled = true;
                    }
                }
                else if (DEVICE_CMD_ID.GET_ABXYZ_ANGLE == LastSendCmd)
                {
                    result = myApi.RecvGetAngleABXYZ(text, ref showAngle);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                    }
                }
                else if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                    }
                }
                else
                {

                }


                if (result)
                {
                    if (DEVICE_CMD_ID.GET_ABXYZ_ANGLE == LastSendCmd)
                    {
                        if (load_flag)
                        {
                            load_flag = false;
                            textBoxAxisZ.Text = showAngle[4].ToString();

                            buttonOK.Enabled = true;
                            buttonCancel.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("自动对焦成功");
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }  
                    }
                    else if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
                    {
                        MessageBox.Show("已停止对焦");
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                    else
                    {

                    }
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

        private void FormShortcutAutoFocus_FormClosed(object sender, FormClosedEventArgs e)
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

        
        
    }
}
