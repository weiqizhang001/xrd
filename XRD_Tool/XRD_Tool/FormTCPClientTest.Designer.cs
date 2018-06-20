namespace XRD_Tool
{
    partial class FormTCPClientTest
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.textBoxIPServer = new System.Windows.Forms.TextBox();
            this.textBoxPortServer = new System.Windows.Forms.TextBox();
            this.textBox_Send = new System.Windows.Forms.TextBox();
            this.buttonSend = new System.Windows.Forms.Button();
            this.richTextBox_RecvDataShow = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(81, 13);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "连接服务器";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // textBoxIPServer
            // 
            this.textBoxIPServer.Location = new System.Drawing.Point(171, 13);
            this.textBoxIPServer.Name = "textBoxIPServer";
            this.textBoxIPServer.Size = new System.Drawing.Size(148, 21);
            this.textBoxIPServer.TabIndex = 1;
            // 
            // textBoxPortServer
            // 
            this.textBoxPortServer.Location = new System.Drawing.Point(345, 12);
            this.textBoxPortServer.Name = "textBoxPortServer";
            this.textBoxPortServer.Size = new System.Drawing.Size(100, 21);
            this.textBoxPortServer.TabIndex = 1;
            // 
            // textBox_Send
            // 
            this.textBox_Send.Location = new System.Drawing.Point(81, 312);
            this.textBox_Send.Multiline = true;
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.Size = new System.Drawing.Size(364, 123);
            this.textBox_Send.TabIndex = 1;
            // 
            // buttonSend
            // 
            this.buttonSend.Location = new System.Drawing.Point(473, 412);
            this.buttonSend.Name = "buttonSend";
            this.buttonSend.Size = new System.Drawing.Size(75, 23);
            this.buttonSend.TabIndex = 0;
            this.buttonSend.Text = "发送";
            this.buttonSend.UseVisualStyleBackColor = true;
            this.buttonSend.Click += new System.EventHandler(this.buttonSend_Click);
            // 
            // richTextBox_RecvDataShow
            // 
            this.richTextBox_RecvDataShow.Location = new System.Drawing.Point(81, 73);
            this.richTextBox_RecvDataShow.Name = "richTextBox_RecvDataShow";
            this.richTextBox_RecvDataShow.Size = new System.Drawing.Size(364, 233);
            this.richTextBox_RecvDataShow.TabIndex = 2;
            this.richTextBox_RecvDataShow.Text = "";
            // 
            // FormTCPClientTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(617, 474);
            this.Controls.Add(this.richTextBox_RecvDataShow);
            this.Controls.Add(this.textBox_Send);
            this.Controls.Add(this.textBoxPortServer);
            this.Controls.Add(this.textBoxIPServer);
            this.Controls.Add(this.buttonSend);
            this.Controls.Add(this.buttonConnect);
            this.Name = "FormTCPClientTest";
            this.Text = "FormTCPClientTest";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormTCPClientTest_FormClosed);
            this.Load += new System.EventHandler(this.FormTCPClientTest_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.TextBox textBoxIPServer;
        private System.Windows.Forms.TextBox textBoxPortServer;
        private System.Windows.Forms.TextBox textBox_Send;
        private System.Windows.Forms.Button buttonSend;
        private System.Windows.Forms.RichTextBox richTextBox_RecvDataShow;
    }
}