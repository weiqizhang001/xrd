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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // richTextBoxUartRecv
            // 
            this.richTextBoxUartRecv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxUartRecv.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxUartRecv.Name = "richTextBoxUartRecv";
            this.richTextBoxUartRecv.Size = new System.Drawing.Size(1066, 497);
            this.richTextBoxUartRecv.TabIndex = 3;
            this.richTextBoxUartRecv.Text = "";
            // 
            // button_ClearSend
            // 
            this.button_ClearSend.Location = new System.Drawing.Point(189, 11);
            this.button_ClearSend.Name = "button_ClearSend";
            this.button_ClearSend.Size = new System.Drawing.Size(75, 23);
            this.button_ClearSend.TabIndex = 6;
            this.button_ClearSend.Text = "清除";
            this.button_ClearSend.UseVisualStyleBackColor = true;
            this.button_ClearSend.Click += new System.EventHandler(this.button_ClearSend_Click);
            // 
            // textBox_Send
            // 
            this.textBox_Send.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Send.Location = new System.Drawing.Point(3, 13);
            this.textBox_Send.Name = "textBox_Send";
            this.textBox_Send.Size = new System.Drawing.Size(774, 21);
            this.textBox_Send.TabIndex = 4;
            // 
            // button_Send
            // 
            this.button_Send.Location = new System.Drawing.Point(95, 11);
            this.button_Send.Name = "button_Send";
            this.button_Send.Size = new System.Drawing.Size(75, 23);
            this.button_Send.TabIndex = 5;
            this.button_Send.Text = "发送";
            this.button_Send.UseVisualStyleBackColor = true;
            this.button_Send.Click += new System.EventHandler(this.buttonUartSend_Click);
            // 
            // button_ClearScreen
            // 
            this.button_ClearScreen.Location = new System.Drawing.Point(3, 11);
            this.button_ClearScreen.Name = "button_ClearScreen";
            this.button_ClearScreen.Size = new System.Drawing.Size(75, 23);
            this.button_ClearScreen.TabIndex = 6;
            this.button_ClearScreen.Text = "清屏";
            this.button_ClearScreen.UseVisualStyleBackColor = true;
            this.button_ClearScreen.Click += new System.EventHandler(this.button_ClearScreen_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1318, 697);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(123, 63);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1072, 571);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "返回的信息";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 514);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1066, 54);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.richTextBoxUartRecv);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1066, 497);
            this.panel2.TabIndex = 8;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button_ClearScreen);
            this.panel3.Controls.Add(this.button_Send);
            this.panel3.Controls.Add(this.button_ClearSend);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(799, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(267, 54);
            this.panel3.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.textBox_Send);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(799, 54);
            this.panel4.TabIndex = 4;
            // 
            // FormChildAccessPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1318, 697);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChildAccessPort";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormChildAccessPort_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormChildAccessPort_FormClosed);
            this.Load += new System.EventHandler(this.FormChildAccessPort_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Send;
        private System.Windows.Forms.Button button_ClearSend;
        public System.Windows.Forms.TextBox textBox_Send;
        private System.Windows.Forms.Button button_ClearScreen;
        public System.Windows.Forms.RichTextBox richTextBoxUartRecv;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel3;

    }
}