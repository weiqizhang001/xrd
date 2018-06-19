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
    public partial class FormTCPClientTest : Form
    {
        public TCPClient client;

        public FormTCPClientTest()
        {
            InitializeComponent();

        }

        private void FormTCPClientTest_Load(object sender, EventArgs e)
        {
            textBoxIPServer.Text = "192.168.0.90";
            textBoxPortServer.Text = "1031";
        }

        private void FormTCPClientTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (client != null && client.connected)
                client.stop();  
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                client = new TCPClient(ClientPrint, textBoxIPServer.Text, textBoxPortServer.Text);
            }

            if (client.connected)
            {
                client.stop();
            }

            client.ipServer = textBoxIPServer.Text;
            client.localIpPort = textBoxPortServer.Text;
            client.start();

            if (client != null) {
                this.Text = "客户端 " + client.localIpPort; 
            } 
        }

        // 客户端输出信息  
        private void ClientPrint(byte[] info, int PackageLen)
        {
            if (richTextBox_RecvDataShow.InvokeRequired)
            {
                TCPClient.Print F = new TCPClient.Print(ClientPrint);
                this.Invoke(F, new object[] { info });
            }
            else
            {
                if (info != null)
                {
                    richTextBox_RecvDataShow.SelectionColor = Color.Green;
                    richTextBox_RecvDataShow.AppendText(System.Text.Encoding.Default.GetString(info));
                    richTextBox_RecvDataShow.AppendText(Environment.NewLine);
                    richTextBox_RecvDataShow.ScrollToCaret();
                }
            }
        }  

        private void buttonSend_Click(object sender, EventArgs e)
        {
            if (client != null && client.connected)
            {
                string info = textBox_Send.Text;

                client.Send(info);
            } 
        }
    }
}
