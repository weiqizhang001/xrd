using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace XRD_Tool
{
    public partial class FormShortcutHighVoltage : Form
    {
        public FormMDIParent myParentForm;
        public SerialPortCommon myUart;
        public System.Timers.Timer timerUartRecv;
        public MeasureApi myApi;
        public XrdConfig myConfig;
        public double showVoltage;
        public double showCurrent;
        public double sendVoltage;
        public double sendCurrent;
        public byte deviceStateBackup;

        public FormShortcutHighVoltage(FormMDIParent form)
        {
            myParentForm = form;
            myUart = form.myUart;
            myConfig = form.myConfig;
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
                myUart.DeviceState = DEVICE_STATE.SHORTCUT_HIGH_VOLTAGE;
                myUart.RegControl(this, UartRecv_HighVoltage, DEVICE_STATE.SHORTCUT_HIGH_VOLTAGE);
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
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void FormShortcutHighVoltage_Load(object sender, EventArgs e)
        {
            try
            {
                buttonCancel.Enabled = false;
                buttonOK.Enabled = false;

                if (myUart.port_UART.IsOpen)
                {
                    myApi.SendGetHighVoltage();
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

        private void FormShortcutHighVoltage_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FormShortcutHighVoltage_FormClosed(object sender, FormClosedEventArgs e)
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[HighVoltage] voltage close");

                sendVoltage = 0;
                sendCurrent = 0;
                myApi.SendHighVoltageDown(sendVoltage, sendCurrent);
                timerUartRecv.Interval = 1000 * 10;
                timerUartRecv.Enabled = true;
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
                sendVoltage = Convert.ToDouble(textBoxHighVoltage.Text);
                sendCurrent = Convert.ToDouble(textBoxCurrent.Text);

                myUart.Pack_Debug_out(null, "[HighVoltage] voltage open,v=" + sendVoltage.ToString() + "c=" + sendCurrent.ToString());

                bool checkResult = FormChildMeasure.checkHighVoltageRange(myApi.TargetType, sendVoltage, sendCurrent);
                if (checkResult)
                {
                    myApi.SendHighVoltageUp(sendVoltage, sendCurrent);
                    timerUartRecv.Interval = 1000 * 40;
                    timerUartRecv.Enabled = true;
                }

                //string pattern = @"^[0-9]*$";
                //Match m1 = Regex.Match(textBoxHighVoltage.Text, pattern);   // 匹配正则表达式
                //Match m2 = Regex.Match(textBoxCurrent.Text, pattern); 
                //if ((!m1.Success) || (!m2.Success))
                //{
                //    FormDeviceInit.showErrorMessageBox("只能输入整数");

                //    return;
                //}
                
                //int voltageMin = 10;
                //int voltageMax = 25;
                //int currentMin = 5;
                //int currentMax = 40;

                //if (myParent.myConfig.TargetType == 0)
                //{ 
                //    voltageMin = 10;
                //    voltageMax = 25;
                //    currentMin = 5;
                //    currentMax = 40;
                //}
                //else if (myParent.myConfig.TargetType == 1)
                //{
                //    voltageMin = 10;
                //    voltageMax = 40;
                //    currentMin = 5;
                //    currentMax = 40;      
                //}
                //else
                //{

                //}

                //int voltage = Convert.ToInt32(textBoxHighVoltage.Text);
                //int current = Convert.ToInt32(textBoxCurrent.Text);
                //if ((voltage >= voltageMin) && (voltage <= voltageMax)
                //    && (current >= currentMin) && (current <= currentMax))
                //{
                //    myParent.ShortcutHighVoltage_SetValue[0] = voltage;
                //    myParent.ShortcutHighVoltage_SetValue[1] = current;

                //    this.DialogResult = DialogResult.OK;
                //    this.Close();
                //}
                //else
                //{
                //    string str = "输入范围错误！电压：";
                //    str += voltageMin.ToString();
                //    str += "~";
                //    str += voltageMax.ToString();
                //    str += "  电流：";
                //    str += currentMin.ToString();
                //    str += "~";
                //    str += currentMax.ToString();

                //    FormDeviceInit.showErrorMessageBox(str);
                //}    
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void UartRecv_HighVoltage(byte[] text, int PackageLen)
        {
            bool result = false;
            int LastSendCmd = myApi.CurrentSendCmd;

            try
            {
                if (DEVICE_CMD_ID.GET_HIGH_VOLTAGE == LastSendCmd)
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
                else
                {

                }

                if (result)
                {
                    if (LastSendCmd == DEVICE_CMD_ID.GET_HIGH_VOLTAGE)
                    {
                        showVoltage = myApi.recvVoltage;
                        showCurrent = myApi.recvCurrent;

                        textBoxHighVoltage.Text = showVoltage.ToString();
                        textBoxCurrent.Text = showCurrent.ToString();
                        buttonCancel.Enabled = true;
                        buttonOK.Enabled = true;
                        myParentForm.statusBar_HighVoltageUpdate(myApi.sendVoltage, myApi.sendCurrent);
                    }
                    else if (LastSendCmd == DEVICE_CMD_ID.SET_HIGH_VOLTAGE_UP)
                    {
                        if ((sendVoltage > 0) || (sendCurrent > 0))
                        {
                            myConfig.SaveWarmUpTime();
                        }

                        MessageBox.Show("开启高压成功");
                        myParentForm.statusBar_HighVoltageUpdate(myApi.sendVoltage, myApi.sendCurrent);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else if (LastSendCmd == DEVICE_CMD_ID.SET_HIGH_VOLTAGE_DOWN)
                    {
                        if ((sendVoltage > 0) || (sendCurrent > 0))
                        {
                            myConfig.SaveWarmUpTime();
                        }

                        myParentForm.statusBar_HighVoltageUpdate(myApi.sendVoltage, myApi.sendCurrent);

                        MessageBox.Show("关闭高压成功");

                        this.DialogResult = DialogResult.OK;
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
    }
}
