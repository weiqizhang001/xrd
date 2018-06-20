namespace XRD_Tool
{
    partial class FormUserLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonRefresh = new System.Windows.Forms.Button();
            this.comboBoxSerialPort = new System.Windows.Forms.ComboBox();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.textBoxPwd = new System.Windows.Forms.TextBox();
            this.textBoxUserName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.comboBoxLang = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonRefresh
            // 
            this.buttonRefresh.Location = new System.Drawing.Point(692, 618);
            this.buttonRefresh.Name = "buttonRefresh";
            this.buttonRefresh.Size = new System.Drawing.Size(75, 23);
            this.buttonRefresh.TabIndex = 20;
            this.buttonRefresh.Text = "刷新";
            this.buttonRefresh.UseVisualStyleBackColor = true;
            this.buttonRefresh.Click += new System.EventHandler(this.buttonRefresh_Click);
            // 
            // comboBoxSerialPort
            // 
            this.comboBoxSerialPort.FormattingEnabled = true;
            this.comboBoxSerialPort.Location = new System.Drawing.Point(515, 621);
            this.comboBoxSerialPort.Name = "comboBoxSerialPort";
            this.comboBoxSerialPort.Size = new System.Drawing.Size(155, 20);
            this.comboBoxSerialPort.TabIndex = 19;
            // 
            // buttonLogin
            // 
            this.buttonLogin.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.buttonLogin.FlatAppearance.BorderSize = 0;
            this.buttonLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonLogin.Image = global::XRD_Tool.Properties.Resources.登陆系统1态;
            this.buttonLogin.Location = new System.Drawing.Point(515, 553);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(252, 46);
            this.buttonLogin.TabIndex = 17;
            this.buttonLogin.UseVisualStyleBackColor = false;
            this.buttonLogin.Click += new System.EventHandler(this.buttonLogin_Click);
            // 
            // textBoxPwd
            // 
            this.textBoxPwd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxPwd.Location = new System.Drawing.Point(64, 9);
            this.textBoxPwd.Name = "textBoxPwd";
            this.textBoxPwd.PasswordChar = '*';
            this.textBoxPwd.Size = new System.Drawing.Size(172, 23);
            this.textBoxPwd.TabIndex = 16;
            // 
            // textBoxUserName
            // 
            this.textBoxUserName.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textBoxUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxUserName.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxUserName.Location = new System.Drawing.Point(64, 12);
            this.textBoxUserName.Name = "textBoxUserName";
            this.textBoxUserName.Size = new System.Drawing.Size(172, 23);
            this.textBoxUserName.TabIndex = 15;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::XRD_Tool.Properties.Resources.用户名登录框;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.textBoxUserName);
            this.panel2.Location = new System.Drawing.Point(513, 372);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 46);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::XRD_Tool.Properties.Resources.登陆密码输入框;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.textBoxPwd);
            this.panel3.Font = new System.Drawing.Font("SimSun", 15F);
            this.panel3.Location = new System.Drawing.Point(513, 424);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(252, 46);
            this.panel3.TabIndex = 16;
            // 
            // panel4
            // 
            this.panel4.BackgroundImage = global::XRD_Tool.Properties.Resources.语言选择框;
            this.panel4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel4.Controls.Add(this.comboBoxLang);
            this.panel4.Font = new System.Drawing.Font("SimSun", 15F);
            this.panel4.Location = new System.Drawing.Point(515, 476);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(252, 46);
            this.panel4.TabIndex = 17;
            // 
            // comboBoxLang
            // 
            this.comboBoxLang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxLang.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold);
            this.comboBoxLang.ForeColor = System.Drawing.Color.Blue;
            this.comboBoxLang.FormattingEnabled = true;
            this.comboBoxLang.Items.AddRange(new object[] {
            "简体中文",
            "English"});
            this.comboBoxLang.Location = new System.Drawing.Point(66, 11);
            this.comboBoxLang.Name = "comboBoxLang";
            this.comboBoxLang.Size = new System.Drawing.Size(170, 28);
            this.comboBoxLang.TabIndex = 0;
            this.comboBoxLang.SelectedIndexChanged += new System.EventHandler(this.comboBoxLang_SelectedIndexChanged);
            // 
            // FormUserLogin
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::XRD_Tool.Properties.Resources.底图;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.buttonLogin);
            this.Controls.Add(this.buttonRefresh);
            this.Controls.Add(this.comboBoxSerialPort);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUserLogin";
            this.Text = "浩元仪器测量软件（Ver1.0）";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormUserLogin_FormClosed);
            this.Load += new System.EventHandler(this.UserLogin_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.ComboBox comboBoxSerialPort;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.TextBox textBoxPwd;
        private System.Windows.Forms.TextBox textBoxUserName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.ComboBox comboBoxLang;

    }
}