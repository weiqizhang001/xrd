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
    public partial class FormShortcutFiveAxis : Form
    {
        public SerialPortCommon myUart;
        public System.Timers.Timer timerUartRecv;
        public MeasureApi myApi;
        public double[] showAngle = new double[5];
        public byte deviceStateBackup;

        public FormShortcutFiveAxis(FormMDIParent form)
        {
            myUart = form.myUart;
            myApi = form.myApi;
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
                myUart.DeviceState = DEVICE_STATE.SHORTCUT_FIVE_AXIS;
                myUart.RegControl(this, UartRecv_FiveAxis, DEVICE_STATE.SHORTCUT_FIVE_AXIS);
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

        private void FormShortcutFiveAxis_Load(object sender, EventArgs e)
        {
            try
            {
                buttonMoveStart.Enabled = false;
                buttonMoveStop.Enabled = false;

                if (myUart.port_UART.IsOpen)
                {
                    myApi.SendGetAngleABXYZ();
                    timerUartRecv.Interval = 1000 * 5;
                    timerUartRecv.Enabled = true;
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

        private void FormShortcutFiveAxis_FormClosing(object sender, FormClosingEventArgs e)
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

        private void buttonMoveStart_Click(object sender, EventArgs e)
        {
            bool checkResult = false;
            double[] angle = new double[5];

            try
            {
                myUart.Pack_Debug_out(null, "[5 Axis] Move Start");

                angle[0] = Convert.ToDouble(textBoxAxisA.Text);
                angle[1] = Convert.ToDouble(textBoxAxisB.Text);
                angle[2] = Convert.ToDouble(textBoxAxisX.Text);
                angle[3] = Convert.ToDouble(textBoxAxisY.Text);
                angle[4] = Convert.ToDouble(textBoxAxisZ.Text);

                checkResult = FormChildMeasure.checkFiveAxisAngleRange_Single(angle[0], -30, 75);
                if (!checkResult)
                {
                    FormDeviceInit.showErrorMessageBox("α轴数据范围错误!（-30~75）");
                    return;
                }

                checkResult = FormChildMeasure.checkFiveAxisAngleRange_Single(angle[1], 0, 360);
                if (!checkResult)
                {
                    FormDeviceInit.showErrorMessageBox("β轴数据范围错误!（0~360）");
                    return;
                }

                checkResult = FormChildMeasure.checkFiveAxisAngleRange_Single(angle[2], -30, 30);
                if (!checkResult)
                {
                    FormDeviceInit.showErrorMessageBox("X轴数据范围错误!（-30~+30）");
                    return;
                }

                checkResult = FormChildMeasure.checkFiveAxisAngleRange_Single(angle[3], -30, 30);
                if (!checkResult)
                {
                    FormDeviceInit.showErrorMessageBox("Y轴数据范围错误!（-30~+30）");
                    return;
                }

                checkResult = FormChildMeasure.checkFiveAxisAngleRange_Single(angle[4], -2, 48);
                if (!checkResult)
                {
                    FormDeviceInit.showErrorMessageBox("Z轴数据范围错误!（-2~48）");
                    return;
                }

                myApi.SendAngleABXYZ(angle);
                timerUartRecv.Interval = 1000 * 60;
                timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            } 
        }

        private void buttonMoveStop_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[5 Axis] Move Stop");

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

        public void UartRecv_FiveAxis(byte[] text, int PackageLen)
        {
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;
     
            try
            {
                string strRecvData = System.Text.Encoding.Default.GetString(text);
                if (strRecvData.IndexOf("**") >= 0)
                {
                    bool isEnable = false;
                    if (myApi.SDDApi.FiveAxis_RecvLaser(text, out isEnable))
                    {
                        if (isEnable)
                        {
                            buttonMoveStart.Enabled = true;
                            buttonMoveStop.Enabled = true;
                            myApi.SendGetAngleABXYZ();
                            timerUartRecv.Interval = 1000 * 5;
                            timerUartRecv.Enabled = true;
                        }
                        else
                        {
                            buttonMoveStart.Enabled = false;
                            buttonMoveStop.Enabled = false;

                            timerUartRecv.Enabled = false;
                        }

                        return;
                    }
                }

                if (DEVICE_CMD_ID.GET_ABXYZ_ANGLE == LastSendCmd)
                {
                    result = myApi.RecvGetAngleABXYZ(text, ref showAngle);
                    if (result)
                    {
                        timerUartRecv.Enabled = false;
                    }
                }
                else if (DEVICE_CMD_ID.SET_ABXYZ_ANGLE == LastSendCmd)
                {
                    result = myApi.RecvDeviceReady(text);
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
                        textBoxAxisA.Text = showAngle[0].ToString();
                        textBoxAxisB.Text = showAngle[1].ToString();
                        textBoxAxisX.Text = showAngle[2].ToString();
                        textBoxAxisY.Text = showAngle[3].ToString();
                        textBoxAxisZ.Text = showAngle[4].ToString();

                        buttonMoveStart.Enabled = true;
                        buttonMoveStop.Enabled = true;
                    }
                    else if (DEVICE_CMD_ID.SET_ABXYZ_ANGLE == LastSendCmd)
                    {
                        MessageBox.Show("五轴移动成功");
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (DEVICE_CMD_ID.SET_DEV_PAUSE == LastSendCmd)
                    {
                        MessageBox.Show("已停止移动");
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

        private void FormShortcutFiveAxis_FormClosed(object sender, FormClosedEventArgs e)
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
