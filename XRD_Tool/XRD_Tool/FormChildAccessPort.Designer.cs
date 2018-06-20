namespace XRD_Tool
{
    partial class FormChildAccessPort
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
            this.richTextBoxUartRecv = new System.Windows.Forms.RichTextBox();
            this.button_ClearSend = new System.Windows.Forms.Button();
            this.textBox_Send = new System.Windows.Forms.TextBox();
            this.button_Send = new System.Windows.Forms.Button();
            this.button_ClearScreen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBoxUartRecv
            // 
            this.richTextBoxUartRecv.Location = new System.Drawing.Point(143, 124);
            this.richTextBoxUartRecv.Name = "richTextBoxUartRecv";
            this.richTextBoxUartRecv.Size = new System.Drawing.Size(564, 207);
            this.richTextBoxUartRecv.TabIndex = 3;
            this.richTextBoxUartRecv.Text = "";
            // 
            // button_ClearSend
            // 
            this.button_ClearSend.Location = new System.Drawing.Point(726, 443);
            this.button_ClearSend.Name = "button_ClearSend";
            this.button_ClearSend.Size = new System.Drawing.Size(75, 23);
            this.button_ClearSend.TabIndex = 6;
            this.button_ClearSend.Text = "清除";
            this.button_ClearSend.UseVisualStyleBackColor = true;
            this.button_ClearSend.Click += new System.EventHandler(this.button_ClearSend_Click);
            // 
            // textBox_Send
            // 
            this.textBox_Send.Location = new System.Drawing.Point(143, 386);
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.Size = new System.Drawing.Size(564, 21);
            this.textBox_Send.TabIndex = 4;
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(726, 403);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Send.TabIndex = 5;
            this.button_Send.Text = "发送";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.buttonUartSend_Click);
            // 
            // button_ClearScreen
            // 
            this.button_ClearScreen.Location = new System.Drawing.Point(726, 308);
            this.button_ClearScreen.Name = "button_ClearScreen";
            this.button_ClearScreen.Size = new System.Drawing.Size(75, 23);
            this.button_ClearScreen.TabIndex = 6;
            this.button_ClearScreen.Text = "清屏";
            this.button_ClearScreen.UseVisualStyleBackColor = true;
            this.button_ClearScreen.Click += new System.EventHandler(this.button_ClearScreen_Click);
            // 
            // FormChildAccessPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 697);
            this.ControlBox = false;
            this.Controls.Add(this.button_ClearScreen);
            this.Controls.Add(this.button_ClearSend);
            this.Controls.Add(this.richTextBoxUartRecv);
            this.Controls.Add(this.button_Send);
            this.Controls.Add(this.textBox_Send);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChildAccessPort";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormChildAccessPort_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormChildAccessPort_FormClosed);
            this.Load += new System.EventHandler(this.FormChildAccessPort_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_ClearSend;
        public System.Windows.Forms.TextBox textBox_Send;
        private System.Windows.Forms.Button button_ClearScreen;
        public System.Windows.Forms.RichTextBox richTextBoxUartRecv;

    }
}