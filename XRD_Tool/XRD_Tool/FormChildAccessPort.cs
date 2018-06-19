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

namespace XRD_Tool
{
    public partial class FormChildAccessPort : Form
    {
        public SerialPortCommon myUart;
        public MeasureApi myApi;
        public System.Timers.Timer timerUartRecv;
        public byte deviceStateBackup;

        public FormChildAccessPort(FormMDIParent form)
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

            //注册事件
            if (myUart.port_UART.IsOpen)
            {
                deviceStateBackup = myUart.DeviceState;
                myUart.DeviceState = DEVICE_STATE.DEVICE_DEBUG;
                myUart.RegControl(this, UartRecv_AccessPort, DEVICE_STATE.DEVICE_DEBUG);
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

        private void FormChildAccessPort_Load(object sender, EventArgs e)
        {
            try
            {
                button_Send.Focus(); 
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void UartRecv_AccessPort(byte[] text, int PackageLen)
        {
            try
            {
                richTextBoxUartRecv.AppendText(System.Text.Encoding.ASCII.GetString(text));

                //richTextBoxUartRecv.AppendText(System.Text.Encoding.ASCII.GetString(text, 0, 1));
                //if (text[0] == 0x0D)
                //{
                //    richTextBoxUartRecv.Focus();
                //    richTextBoxUartRecv.Select(richTextBoxUartRecv.Text.Length, 0);
                //    richTextBoxUartRecv.ScrollToCaret();
                //}
            }
            catch (Exception ex)
            {
                //myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        //private void buttonUartPortClose_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        UartPort_Close();

        //        UartPort_GetInfo();

        //        comboBoxUartPortName.Enabled = true;
        //        buttonUartPortOpen.Enabled = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
        //    }
        //}

        private void buttonUartSend_Click(object sender, EventArgs e)
        {
            try
            {
                string str = textBox_Send.Text + "\r";
                byte[] sendBytes = System.Text.Encoding.ASCII.GetBytes(str);
                myApi.UartSendCmd(sendBytes);

                str = "\r\n" + str + "\r\n";
                byte[] showSendBytes = System.Text.Encoding.ASCII.GetBytes(str);
                UartRecv_AccessPort(showSendBytes, showSendBytes.Length);
            }
            catch (Exception ex)
            {
                //myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void button_ClearSend_Click(object sender, EventArgs e)
        {
            textBox_Send.Text = "";
        }

        private void button_ClearScreen_Click(object sender, EventArgs e)
        {
            textBox_Send.Text = "";
            richTextBoxUartRecv.Text = "";
        }

        private void FormChildAccessPort_FormClosing(object sender, FormClosingEventArgs e)
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

        private void FormChildAccessPort_FormClosed(object sender, FormClosedEventArgs e)
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

        


        ///// <summary>
        ///// 获取并初始化UartPort信息
        ///// </summary>
        //private void UartPort_GetInfo()
        //{
        //    try
        //    {
        //        comboBoxUartPortName.Items.Clear();

        //        if (myUart.port_UART.IsOpen)
        //        {
        //            comboBoxUartPortName.Text = myUart.port_UART.PortName;
        //            comboBoxUartPortName.Enabled = false;
        //            buttonUartPortOpen.Enabled = false;

        //            return;
        //        }

        //        string[] ports = SerialPort.GetPortNames();
        //        //comboBoxUartPortName.Text = Properties.Settings.Default.COM;
        //        comboBoxUartPortName.Enabled = true;
        //        if (ports.Length > 0)
        //        {
        //            foreach (string portName in ports)
        //            {
        //                comboBoxUartPortName.Items.Add(portName);
        //                //设置选择的index
        //                if (portName == Properties.Settings.Default.COM)
        //                {
        //                    comboBoxUartPortName.SelectedIndex = comboBoxUartPortName.Items.Count - 1;
        //                }
        //            }
        //            if (comboBoxUartPortName.SelectedIndex == -1)
        //            {
        //                comboBoxUartPortName.SelectedIndex = 0;
        //            }
        //        }
        //        else
        //        {
        //            comboBoxUartPortName.Items.Add(" ");
        //            comboBoxUartPortName.SelectedIndex = 0;
        //            comboBoxUartPortName.Enabled = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
        //    }        
        //}
            
        //private bool UartPort_Open() 
        //{
        //    try
        //    {
        //        if (myUart.port_UART.IsOpen)
        //            myUart.port_UART.Close();

        //        if (myUart.OpenPortTer(comboBoxUartPortName.Text))
        //        {
        //            //Properties.Settings.Default.COM = comboBoxUartPortName.Text;
        //            //Properties.Settings.Default.Save();
        //            myUart.Recstatic = 0;
        //        }
        //        else
        //        {
        //            MessageBox.Show("串口打开失败");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show("串口打开异常:" + ex.ToString());
        //    }

        //    return myUart.port_UART.IsOpen;
        //}

        //private bool UartPort_Close()
        //{
        //    try
        //    {
        //        if (myUart.port_UART.IsOpen)
        //            myUart.port_UART.Close();
        //    }
        //    catch (Exception ex)
        //    {

        //        MessageBox.Show("串口关闭异常:" + ex.ToString());
        //    }

        //    return !myUart.port_UART.IsOpen;
        //}

        //private void UartPort_Send()
        //{
        //    try
        //    {
        //        if (!myUart.port_UART.IsOpen)
        //        {
        //            MessageBox.Show("串口未连接");

        //            return;
        //        }

        //        string Str = textBoxUartSend.Text.ToString() + "\r";
        //        byte[] CMMID = System.Text.Encoding.ASCII.GetBytes(Str);

        //        byte[] LOG = myUart.SendCmmdData(CMMID);
        //        richTextBoxUartRecv.AppendText(System.Text.Encoding.ASCII.GetString(CMMID, 0, CMMID.Length));
        //        richTextBoxUartRecv.AppendText("\n");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("串口发送异常:" + ex.ToString());
        //    }

        //}

        

        //private void buttonUartPortOpen_Click(object sender, EventArgs e)
        //{
        //    if (UartPort_Open()) 
        //    {
        //        comboBoxUartPortName.Enabled = false;
        //        comboBoxUartBaudRate.Enabled = false;
        //        buttonUartPortOpen.Enabled = false;
        //    }
        //}

        








    }
}
