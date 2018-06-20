using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace XRD_Tool
{
    public class SerialPortCommon
    {
        public delegate void HandleInterfaceUpdateDelegate(byte[] buffer, int PackageLen);  // 委托，此为重点
        private List<Win_Control> Win_Controls = new List<Win_Control>();
        public SerialPort port_UART;
        public byte DeviceState = DEVICE_STATE.DEVICE_INIT;
        public byte DeviceStateBackup = DEVICE_STATE.DEVICE_INIT;
        public byte MeasureMethod = 0;

        public string strLastSend;

        private int recstatic;

        public int Recstatic
        {
            get { return recstatic; }
            set { recstatic = value; }
        }

        public string LogError;
        Stream LogStream;
        StreamWriter LogSw;

        #region 委托
        public struct Win_Control
        {
            public Control WinCon;
            public HandleInterfaceUpdateDelegate UpdateHandle;
            public byte CommID;

        }
        public void RegControl(Control winControl, HandleInterfaceUpdateDelegate updateHandle, byte CommID)
        {
            RemoveControl(winControl);

            Win_Control win = new Win_Control();
            win.WinCon = winControl;
            win.UpdateHandle = updateHandle;
            win.CommID = CommID;
            Win_Controls.Add(win);

        }
        public void RemoveControl(Control winControl)
        {
            for (int i = Win_Controls.Count - 1; i >= 0; i--)
            {
                if (Win_Controls[i].WinCon.Handle.Equals(winControl.Handle))
                    Win_Controls.Remove(Win_Controls[i]);
            }
        }
        public void UpdateDelegate(Byte[] buff, int PackageLen, byte CommID)
        {
            foreach (Win_Control win in Win_Controls)
            {
                if (win.WinCon != null 
                    && win.UpdateHandle != null
                    && win.CommID == CommID)
                {
                    win.WinCon.BeginInvoke(new Action<byte[], int>(win.UpdateHandle), buff, PackageLen);

                }
            }
        }
        #endregion

        public SerialPortCommon()
        {
            port_UART = new SerialPort();
            
        }

        /// <summary>
        /// 打开com口
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public bool OpenPortTer(string com)
        {
            if (port_UART.IsOpen) {
                port_UART.Close();         
            }

            port_UART.PortName = com;
            port_UART.BaudRate = 38400;//19200;
            port_UART.Parity = Parity.None; //Parity.Odd;
            port_UART.StopBits = StopBits.One; //StopBits.Two;
            port_UART.DataBits = 8;

            port_UART.RtsEnable = true;
            port_UART.DtrEnable = true;

            port_UART.DataReceived += Port_DataReceived;
            port_UART.ReceivedBytesThreshold = 1;
            //ErrorReceived 

            try
            {
                port_UART.Open();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            return port_UART.IsOpen;
        }

        /// <summary>
        /// 串口读取数据事件  工具接收数据处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Port_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try 
            {
                Thread.Sleep(120);//暂停120毫秒等一条完整的命令进入缓冲区
                byte[] readBuffer = new byte[port_UART.BytesToRead];
                port_UART.Read(readBuffer, 0, readBuffer.Length);

                string strRecvData = System.Text.Encoding.Default.GetString(readBuffer);

                Pack_Debug_out(readBuffer, "UART Recv: @" + DeviceState.ToString() + "[" + strRecvData + "]");

                // 串口调试模式
                if (DEVICE_STATE.DEVICE_DEBUG == DeviceState)
                {
                    UpdateDelegate(readBuffer, readBuffer.Length, DeviceState);

                    return;
                }
                // 如果返回的数据跟发送的数据相同，说明接收的是回显字符，丢弃
                if (strRecvData == strLastSend)
                {
                    return;
                }
                else
                {
                    UpdateDelegate(readBuffer, readBuffer.Length, DeviceState);
                }
            }
            catch (Exception ex)
            {
                Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }   
        }
 
        // 串口发送
        public byte[] SendCmmdData(byte[] CommID)
        {
            // 发送数据
            port_UART.Write(CommID, 0, CommID.Length);
            // 返回结果
            byte[] destData = new byte[CommID.Length];
            System.Array.Copy(CommID, destData, CommID.Length);

            strLastSend = System.Text.Encoding.Default.GetString(CommID);

            Pack_Debug_out(CommID, "UART Send: " + "[" + strLastSend + "]");

            return destData;
        }

        public void Pack_Debug_Init()
        {
            try
            {
                LogStream = new FileStream(XrdConfig.LogFileName, FileMode.OpenOrCreate | FileMode.Append);
                LogSw = new StreamWriter(LogStream, System.Text.Encoding.GetEncoding("gb2312")); // System.Text.Encoding.ASCII
                LogSw.AutoFlush = true;
                
                LogSw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss fff]") + "Log Start******************************************************");
                //LogSw.FlushAsync();
            }
            catch (Exception ex)
            {
                LogError += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss fff]") + ex.ToString() + "\n";
            }
            finally
            {

            }
        }

        public void Pack_Debug_out(byte[] readBuffer, string hint)
        {
            try
            {
                string ReceivedStr = ToHexString(readBuffer);

                LogSw.WriteLine(ReceivedStr);
                LogSw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss fff]") + " " + hint);
                if (LogError != null)
                {
                    LogSw.WriteLine(LogError);
                    LogError = null;
                }
                //LogSw.FlushAsync();
            }
            catch (Exception ex)
            {
                LogError += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss fff]") + ex.ToString() + "\n";
            }
            finally
            {

            }
        }

        public void Pack_Debug_End()
        {
            try
            {
                LogSw.WriteLine(DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss fff]") + "Log End******************************************************");
                if (LogError != null)
                {
                    LogSw.WriteLine(LogError);
                    LogError = null;
                }

                LogSw.Close();
                LogStream.Close();
            }
            catch (Exception ex)
            {
                LogError += DateTime.Now.ToString("[yyyy-MM-dd HH:mm:ss fff]") + ex.ToString() + "\n";
            }
            finally
            {

            }
        }

        // 16进制字符串转字节数组   格式为 string sendMessage = "00 01 00 00 00 06 FF 05 00 64 00 00";
        public static byte[] HexStrTobyte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2).Trim(), 16);
            return returnBytes;
        }

        public static string ToHexString(byte[] bytes) // 0xae00cf => "AE00CF "
        {
            string hexString = string.Empty;

            try
            {
                if (bytes != null)
                {
                    StringBuilder strB = new StringBuilder();

                    for (int i = 0; i < bytes.Length; i++)
                    {
                        strB.Append(bytes[i].ToString("X2"));
                    }
                    hexString = strB.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return hexString;
        }

        public static Boolean FileIsUsed(String fileFullName)
        {
            Boolean result = false;
            //判断文件是否存在，如果不存在，直接返回 false
            if (!System.IO.File.Exists(fileFullName))
            {
                result = false;
            }//end: 如果文件不存在的处理逻辑
            else
            {//如果文件存在，则继续判断文件是否已被其它程序使用
                //逻辑：尝试执行打开文件的操作，如果文件已经被其它程序使用，则打开失败，抛出异常，根据此类异常可以判断文件是否已被其它程序使用。
                System.IO.FileStream fileStream = null;
                try
                {
                    fileStream = System.IO.File.Open(fileFullName, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);
                    result = false;
                }
                catch (System.IO.IOException ioEx)
                {
                    result = true;
                }
                catch (System.Exception ex)
                {
                    result = true;
                }
                finally
                {
                    if (fileStream != null)
                    {
                        fileStream.Close();
                    }
                }
            }//end: 如果文件存在的处理逻辑
            //返回指示文件是否已被其它程序使用的值
            return result;
        }//end method FileIsUsed

        //public static bool IsNumber(String strNumber)
        //{
        //    Regex objNotNumberPattern = new Regex("[^0-9.-]");
        //    Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
        //    Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");
        //    String strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
        //    String strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
        //    Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

        //    return !objNotNumberPattern.IsMatch(strNumber) &&
        //    !objTwoDotPattern.IsMatch(strNumber) &&
        //    !objTwoMinusPattern.IsMatch(strNumber) &&
        //    objNumberPattern.IsMatch(strNumber);
        //}


        public static string RightAlign_InSeven(string str)
        {
            if (str.Length == 0)
            {
                return "       ";
            }
            else if (str.Length == 1)
            {
                return "      " + str;
            }
            else if (str.Length == 2)
            {
                return "     " + str;
            }
            else if (str.Length == 3)
            {
                return "    " + str;
            }
            else if (str.Length == 4)
            {
                return "   " + str;
            }
            else if (str.Length == 5)
            {
                return "  " + str;
            }
            else if (str.Length == 6)
            {
                return " " + str;
            }
            else if (str.Length >= 7)
            {
                return str;
            }
            else
            {
                return str;
            }
        }

        public static string RightAlign_Eight(string str)
        {
            if (str.Length == 0)
            {
                return "        ";
            }
            else if (str.Length == 1)
            {
                return "       " + str;
            }
            else if (str.Length == 2)
            {
                return "      " + str;
            }
            else if (str.Length == 3)
            {
                return "     " + str;
            }
            else if (str.Length == 4)
            {
                return "    " + str;
            }
            else if (str.Length == 5)
            {
                return "   " + str;
            }
            else if (str.Length == 6)
            {
                return "  " + str;
            }
            else if (str.Length == 7)
            {
                return " " + str;
            }
            else if (str.Length >= 8)
            {
                return str;
            }
            else
            {
                return str;
            }
        }
    }
}
