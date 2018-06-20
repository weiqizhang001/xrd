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
    public partial class FormUserLogin : Form
    {
        // 登陆校验后的用户ID
        private string userId = null;

        // 登陆校验后的用户名
        private string userName = null;

        public SerialPortCommon myUart;
        public XrdConfig myConfig; 

        public FormUserLogin()
        {
            myUart = new SerialPortCommon();
            myConfig = new XrdConfig();

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }
        private void UserLogin_Load(object sender, EventArgs e)
        {
            try
            {
                // read ini file
                myConfig.ReadIniFile();
                // update controls
                textBoxUserName.Text = myConfig.UserName;
                textBoxPwd.Text = myConfig.UserPwd;
                comboBoxLang.SelectedIndex = myConfig.Language;
                // get serial port
                GetSerialPort_info();

                buttonLogin.Focus();

                myUart.Pack_Debug_Init();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }
        private void FormUserLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                myUart.Pack_Debug_End();
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            try
            {
                myConfig.UserName = textBoxUserName.Text;
                myConfig.UserPwd = textBoxPwd.Text;

                // 校验用户名和密码是否填写
                //if (string.IsNullOrEmpty(textBoxUserName.Text) || string.IsNullOrEmpty(textBoxPwd.Text))
                //{
                //    MessageBox.Show("用户名和密码必须要输入!");
                //    return;
                //}

                // 校验密码是否正确
                if (!checkUser())
                {
                    MessageBox.Show("输入的密码有问题!");
                    myUart.Pack_Debug_out(null, "[UserLogin] login failed!user=" + myConfig.UserName + ",pwd=" + myConfig.UserPwd);

                    return;
                }

                myUart.Pack_Debug_out(null, "[UserLogin] login succeed!user=" + myConfig.UserName + ",pwd=" + myConfig.UserPwd);

                myConfig.SaveLogin();

                // 校验成功跳转页面
                if (SerialPort_Open())
                {
                    myConfig.SaveCommunication();
                }
                else
                {
                    //MessageBox.Show("串口打开错误，登录失败");     
                }

                this.textBoxUserName.Text = string.Empty;
                this.textBoxPwd.Text = string.Empty;
                this.Hide();

                FormDeviceInit dev = new FormDeviceInit(this);
                dev.StartPosition = FormStartPosition.CenterScreen;
                //dev.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private bool checkUser()
        {

            return true;

            //bool ret = false;
            //string id = tbID.Text;
            //string password = tbPassword.Text;

            //string executeSQL = string.Empty;

            //executeSQL += " SELECT *  ";
            //executeSQL += " FROM USERS";
            //executeSQL += " WHERE USERCODE = '" + id + "'";
            //executeSQL += " AND   PASSWORD = '" + password + "';";

            //DataTable user = Uart_Tools_Config.DbHelperACE.Query(executeSQL).Tables[0];

            ////如果有任意一条数据存在的话则登陆成功
            //if (user.Rows.Count > 0)
            //{
            //    this.userId = user.Rows[0]["USERID"].ToString();
            //    this.userName = user.Rows[0]["USERNAME"].ToString();

            //    ret = true;
            //}

            //return ret;
        }
        

        /// <summary>
        /// 获取并初始化SerialPort信息
        /// </summary>
        private void GetSerialPort_info()
        {
            try
            {
                string[] ports = SerialPort.GetPortNames();
                //Array.Sort<int>(ports);
                comboBoxSerialPort.Items.Clear();
                comboBoxSerialPort.Text = Properties.Settings.Default.COM;
                comboBoxSerialPort.Enabled = true;
                if (ports.Length > 0)
                {
                    foreach (string portName in ports)
                    {
                        comboBoxSerialPort.Items.Add(portName);
                        //设置选择的index
                        if (portName == Properties.Settings.Default.COM)
                        {
                            comboBoxSerialPort.SelectedIndex = comboBoxSerialPort.Items.Count - 1;
                        }
                    }
                    if (comboBoxSerialPort.SelectedIndex == -1)
                    {
                        comboBoxSerialPort.SelectedIndex = 0;
                    }
                }
                else
                {
                    comboBoxSerialPort.Items.Add(" ");
                    comboBoxSerialPort.SelectedIndex = 0;
                    comboBoxSerialPort.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        private bool SerialPort_Open()
        {
            try
            {
                if (myUart.port_UART.IsOpen)
                    myUart.port_UART.Close();

                if (myUart.OpenPortTer(comboBoxSerialPort.Text))
                {
                    Properties.Settings.Default.COM = comboBoxSerialPort.Text;
                    Properties.Settings.Default.Save();
                    myUart.Recstatic = 0;
                }
                else
                {
                    //MessageBox.Show("串口打开失败");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("com口设置异常:" + ex.ToString());
            }

            return myUart.port_UART.IsOpen;
        }


        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                GetSerialPort_info();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void comboBoxLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                myConfig.Language = comboBoxLang.SelectedIndex;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
