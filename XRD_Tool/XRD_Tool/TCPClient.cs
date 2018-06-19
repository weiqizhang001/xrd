using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace XRD_Tool
{
    public class TCPClient
    {
        public delegate void HandleInterfaceUpdateDelegate(byte[] buffer, int PackageLen);  // 委托，此为重点
        private List<Win_Control> Win_Controls = new List<Win_Control>();

        public string ipServer = "192.168.0.90";   // 服务器端ip  
        public int portServer = 1031;                // 服务器端口  
        public Socket socket;
        public Print print;                     // 运行时的信息输出方法  
        public bool connected = false;          // 标识当前是否连接到服务器  
        public string localIpPort = "";         // 记录本地ip端口信息  
        public SerialPortCommon myUart;
        public byte[] socketRecieveBuffer = new byte[1024 * 1024];
        public byte DeviceState = DEVICE_STATE.DEVICE_INIT;
        public byte DeviceStateBackup = DEVICE_STATE.DEVICE_INIT;

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

        public TCPClient(Print print = null, string ipServer = null, string portServer = "-1")
        {
            this.print = print;
            if (ipServer != null) this.ipServer = ipServer;

            int port_int = Int32.Parse(portServer);
            if (port_int >= 0) this.portServer = port_int;
        }

        public TCPClient(SerialPortCommon uart, Print print = null, string ipServer = null, string portServer = "-1")
        {
            myUart = uart;

            this.print = print;
            if (ipServer != null) this.ipServer = ipServer;

            int port_int = Int32.Parse(portServer);
            if (port_int >= 0) this.portServer = port_int;
        }

        /// <summary>  
        /// Print用于输出Server的输出信息  
        /// </summary>  
        public delegate void Print(byte[] data, int PackageLen);

        /// <summary>  
        /// 启动客户端，连接至服务器  
        /// </summary>  
        public void start()
        {
            //设定服务器IP地址    
            IPAddress ip = IPAddress.Parse(ipServer);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                socket.Connect(new IPEndPoint(ip, portServer));   // 连接服务器  

                myUart.Pack_Debug_out(null, "连接服务器" + socket.RemoteEndPoint.ToString() + "完成\n");

                localIpPort = socket.LocalEndPoint.ToString();
                connected = true;

                Thread thread = new Thread(receiveData);
                thread.Name = "TCPClientRecv";
                thread.IsBackground = true;
                thread.Start(socket);      // 在新的线程中接收服务器信息  

            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "连接服务器失败 " + ex.ToString());

                connected = false;
            }
        }

        /// <summary>  
        /// 结束客户端  
        /// </summary>  
        public bool stop()
        {
            connected = false;

            if (socket == null)
            {
                return false;
            }
            try
            {
                if (!socket.Connected)
                {
                    socket = null;

                    return true;
                }

                socket.Shutdown(SocketShutdown.Both);
                socket.Disconnect(true);
                socket.Close();
                socket = null;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>  
        /// 发送信息  
        /// </summary>  
        public void Send(string info)
        {
            try
            {
                Send(socket, info);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "服务器端已断开，" + socket.RemoteEndPoint.ToString() + "\n");
                stop();
            }
        }

        /// <summary>  
        /// 通过socket发送数据data  
        /// </summary>  
        private void Send(Socket socket, string data)
        {
            if (socket != null && data != null && !data.Equals(""))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(data);   // 将data转化为byte数组  

                myUart.Pack_Debug_out(bytes, "TCP Send: " + "[" + data + "]");
                socket.Send(bytes);
            }
        }

        /// <summary>  
        /// 接收通过socket发送过来的数据  
        /// </summary>  
        private void receiveData(object socket)
        {
            Socket ortherSocket = (Socket)socket;

            while (true)
            {
                try
                {
                    byte[] recvBytes = Receive(ortherSocket);       // 接收客户端发送的信息  

                    if (recvBytes != null)
                    {
                        UpdateDelegate(recvBytes, recvBytes.Length, DeviceState);

                        string showString = Encoding.Unicode.GetString(recvBytes);
                        myUart.Pack_Debug_out(recvBytes, "TCP Recv: " + "[" + showString + "]");
                    }
                }
                catch (Exception ex)
                {
                    myUart.Pack_Debug_out(null, "连接已自动断开，" + ex.Message + "\n");

                    ortherSocket.Shutdown(SocketShutdown.Both);
                    ortherSocket.Close();
                    connected = false;
                    break;
                }

                Thread.Sleep(20);      // 延时0.2后处理接收到的信息  
                if (!connected)
                {
                    continue;
                }
            }
        }

        /// <summary>  
        /// 从socket接收数据  
        /// </summary>  
        private byte[] Receive(Socket socket)
        {
            byte[] bytes = null;
            int len = socket.Available;
            int recvCount = 0;

            if (len > 0)
            {
                bytes = new byte[len];
                recvCount = socket.Receive(bytes);

                byte[] recvBytes = new byte[recvCount];

                System.Array.Copy(bytes, 0, recvBytes, 0, recvBytes.Length);

                return recvBytes;
            }

            

            return null;
        }

        public byte[] ReceiveDataFromSocketPort()
        {
            if (socket == null) return null;

            byte[] result = null;

            try//尝试接收数据
            {

                socket.Blocking = true;
                int recieveCount = socket.Receive(socketRecieveBuffer);
                if (recieveCount > 0)
                {
                    result = new byte[recieveCount];
                    System.Array.Copy(socketRecieveBuffer, result, recieveCount);


                    return result;
                }

            }
            catch (SocketException ex)
            {

                if (!ex.NativeErrorCode.Equals(10060))
                {

                }
                else
                {
                    Thread.Sleep(100);
                }

            }
            catch (ObjectDisposedException e)
            {

            }
            catch (Exception ex)
            {

            }

            return null;
        }








    }
}
