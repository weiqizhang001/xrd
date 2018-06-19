using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace XRD_Tool
{
    public partial class FormMDIParent : Form
    {
        public FormChildLogo myFormLogo;
        public FormChildMeasure myFormMeasure;
        public FormChildDeviceParam myFormDevParam;
        public FormChildTexture myFormTexture;
        public FormChildDeviceCtrl myFormDevCtrl;
        public FormChildAccessPort myFormAccessPort;

        public FormDeviceInit myFormInit;
        public FormShortcutHighVoltage myFormShortHighVoltge;
        public FormShortcutFiveAxis myFormShortFiveAxis;
        public FormShortcutAutoFocus myFormShortAutoFocus;

        public SerialPortCommon myUart;
        public TCPClient myTCP;
        public XrdConfig myConfig;
        public MeasureApi myApi;
        public CurveFitting myCurveFit;
        public double showVoltage;
        public double showCurrent;
        
        public bool OpenDataFileFlag = false;

        public FormMDIParent(FormDeviceInit form)
        {
            myFormInit = form;
            myUart = form.myUart;
            myTCP = form.myTCP;
            myConfig = form.myConfig;
            myApi = form.myApi;

            myCurveFit = new CurveFitting(myConfig);

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void MDIParent_Load(object sender, EventArgs e)
        {
            try
            {
                myFormLogo = new FormChildLogo();
                myFormLogo.MdiParent = this;
                myFormLogo.WindowState = FormWindowState.Maximized;
                myFormLogo.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                myFormLogo.Text = "";
                myFormLogo.ControlBox = false;
                myFormLogo.Show();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void FormMDIParent_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                myUart.Pack_Debug_End();
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void buttonMenuExit_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_End();
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }
        private bool HaveOpened(Form ParentForm, string childName)
        {
            // 查看窗口是否已经被打开
            bool bReturn = true;

            try
            {
                for (int i = 0; i < ParentForm.MdiChildren.Length; i++)
                {
                    if (ParentForm.MdiChildren[i].Name == childName)
                    {
                        //ParentForm.MdiChildren[i].BringToFront();
                        ParentForm.MdiChildren[i].Close();
                        bReturn = false;
                        break;
                    }
                }

            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
            
            return bReturn;
        }

        private void buttonMenuMea_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Menu Measure");

                if (!HaveOpened(this, "FormChildMeasure"))
                {
                    //return; 
                }

                myFormMeasure = new FormChildMeasure(this);
                myFormMeasure.MdiParent = this;
                myFormMeasure.WindowState = FormWindowState.Maximized;
                myFormMeasure.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                myFormMeasure.Text = "";
                myFormMeasure.ControlBox = false;
                myFormMeasure.Dock = DockStyle.Fill;
                //myFormMeasure.Show();             
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }  
        }

        private void buttonMenuDev_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Menu Setup");

                if (!HaveOpened(this, "FormChildDevParam"))
                {
                    //return;
                }

                myFormDevParam = new FormChildDeviceParam(this);
                myFormDevParam.MdiParent = this;
                myFormDevParam.WindowState = FormWindowState.Maximized;
                myFormDevParam.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                myFormDevParam.Text = "";
                myFormDevParam.ControlBox = false;
                myFormDevParam.Show();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void buttonMenuTexture_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Menu Texture");

                if (!HaveOpened(this, "FormChildTexture"))
                {
                    //return;
                }

                myFormTexture = new FormChildTexture(this);
                myFormTexture.MdiParent = this;
                myFormTexture.WindowState = FormWindowState.Maximized;
                myFormTexture.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                myFormTexture.Text = "";
                myFormTexture.ControlBox = false;
                myFormTexture.Show();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        private void buttonMenuCtrl_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Menu DevCtrl");

                if (!HaveOpened(this, "FormChildDevCtrl"))
                {
                    //return;
                }

                myFormDevCtrl = new FormChildDeviceCtrl();
                myFormDevCtrl.MdiParent = this;
                myFormDevCtrl.MdiParent = this;
                myFormDevCtrl.WindowState = FormWindowState.Maximized;
                myFormDevCtrl.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                myFormDevCtrl.Text = "";
                myFormDevCtrl.ControlBox = false;
                myFormDevCtrl.Show();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            } 
        }

        private void buttonShortAbout_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Shortcut About");

                if (!HaveOpened(this, "FormChildAccessPort"))
                {
                    //return;
                }

                myFormAccessPort = new FormChildAccessPort(this);
                myFormAccessPort.MdiParent = this;
                myFormAccessPort.WindowState = FormWindowState.Maximized;
                myFormAccessPort.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                myFormAccessPort.Text = "";
                myFormAccessPort.ControlBox = false;
                myFormAccessPort.Show();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            } 
        }

        private void buttonShortOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDlg = new OpenFileDialog();

            myUart.Pack_Debug_out(null, "[Parent] Shortcut Calc");


            if ((null == myConfig.OpenDataFileName) || (" " == myConfig.OpenDataFileName))
            {
                fileDlg.InitialDirectory = "c:\\";
            }
            else
	        {
                fileDlg.InitialDirectory = System.IO.Path.GetDirectoryName(myConfig.OpenDataFileName);
	        }
            
            fileDlg.Filter = "所有文件(*.*)|*.*"; // 如果需要筛选txt文件（"Files (*.txt)|*.txt"）
            fileDlg.RestoreDirectory = true;
            fileDlg.FilterIndex = 1;
            //fileDlg.Multiselect = true;

            try {
                if (fileDlg.ShowDialog() == DialogResult.OK)
                {
                    myConfig.OpenDataFileName = fileDlg.FileName;
                    OpenDataFileFlag = true;

                    buttonMenuMea_Click(this, null);
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

        }

        private void buttonShortHighVol_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Shortcut HighVoltage");

                myFormShortHighVoltge = new FormShortcutHighVoltage(this);
                myFormShortHighVoltge.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = myFormShortHighVoltge.ShowDialog();
                if (result == DialogResult.OK)
                {

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

        private void buttonShortSave_Click(object sender, EventArgs e)
        {

        }

        private void buttonShortInit_Click(object sender, EventArgs e)
        {
            myUart.Pack_Debug_out(null, "[Parent] Shortcut Init");

            if ((myFormInit == null) || (myFormInit.IsDisposed))
            {
                myFormInit = new FormDeviceInit(this);
                myFormInit.StartPosition = FormStartPosition.CenterScreen;
                myFormInit.Show();
            }
            else
            {
                this.Hide();
                myFormInit.BringToFront();
                myFormInit.Show();
                myUart.DeviceState = DEVICE_STATE.DEVICE_INIT;
            }
        }

        private void buttonShortFive_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Shortcut 5 Axis");

                myFormShortFiveAxis = new FormShortcutFiveAxis(this);
                myFormShortFiveAxis.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = myFormShortFiveAxis.ShowDialog();
                if (result == DialogResult.OK)
                {

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

        private void buttonShortFocus_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Shortcut AutoFocus");

                myFormShortAutoFocus = new FormShortcutAutoFocus(this);
                myFormShortAutoFocus.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = myFormShortAutoFocus.ShowDialog();
                if (result == DialogResult.OK)
                {

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

        private void buttonShortHelp_Click(object sender, EventArgs e)
        {
            myUart.Pack_Debug_out(null, "[Parent] Shortcut Help");
        }

        private void FormMDIParent_FormClosing(object sender, FormClosingEventArgs e)
        {
            myUart.Pack_Debug_out(null, "[Parent] closing");

            if ((showVoltage > 0) || (showCurrent > 0)) 
            {
                FormHighVoltageConfirm form = new FormHighVoltageConfirm(showVoltage, showCurrent);

                form.StartPosition = FormStartPosition.CenterScreen;
                DialogResult result = form.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //e.Cancel = true;
                    myApi.SendHighVoltageDown(0, 0);
                }
                else
                {

                }
            }
            else
            {

            } 
        }

        public void statusBar_HighVoltageUpdate(double v, double c)
        {
            myUart.Pack_Debug_out(null, "[Parent] closing");

            showVoltage = v;
            showCurrent = c;

            toolStripStatusLabelHighVoltage.Text = "当前电压：" + showVoltage.ToString() + "    当前电流：" + showCurrent.ToString();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            try
            {
                myUart.Pack_Debug_out(null, "[Parent] Menu exit");

                //this.DialogResult = DialogResult.Cancel;
                //this.Close();
                myUart.Pack_Debug_End();
                System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        




    }
}
