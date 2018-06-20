namespace XRD_Tool
{
    partial class FormDeviceInit
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
            this.buttonSkip = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.textBoxSavePath = new System.Windows.Forms.TextBox();
            this.buttonSavePath = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonTargetType2 = new System.Windows.Forms.RadioButton();
            this.radioButtonTargetType1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.radioButtonDetectorType2 = new System.Windows.Forms.RadioButton();
            this.radioButtonDetectorType1 = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonSkip
            // 
            this.buttonSkip.FlatAppearance.BorderSize = 0;
            this.buttonSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSkip.Image = global::XRD_Tool.Properties.Resources.暂时跳过;
            this.buttonSkip.Location = new System.Drawing.Point(373, 391);
            this.buttonSkip.Name = "buttonSkip";
            this.buttonSkip.Size = new System.Drawing.Size(172, 53);
            this.buttonSkip.TabIndex = 8;
            this.buttonSkip.UseVisualStyleBackColor = true;
            this.buttonSkip.Click += new System.EventHandler(this.buttonSkip_Click);
            // 
            // buttonDone
            // 
            this.buttonDone.FlatAppearance.BorderSize = 0;
            this.buttonDone.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDone.Image = global::XRD_Tool.Properties.Resources.初始化完成1态;
            this.buttonDone.Location = new System.Drawing.Point(714, 391);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(172, 53);
            this.buttonDone.TabIndex = 9;
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // textBoxSavePath
            // 
            this.textBoxSavePath.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBoxSavePath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxSavePath.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSavePath.Location = new System.Drawing.Point(52, 93);
            this.textBoxSavePath.Multiline = true;
            this.textBoxSavePath.Name = "textBoxSavePath";
            this.textBoxSavePath.Size = new System.Drawing.Size(196, 54);
            this.textBoxSavePath.TabIndex = 10;
            // 
            // buttonSavePath
            // 
            this.buttonSavePath.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSavePath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonSavePath.FlatAppearance.BorderSize = 0;
            this.buttonSavePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSavePath.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonSavePath.Image = global::XRD_Tool.Properties.Resources.选中框2态;
            this.buttonSavePath.Location = new System.Drawing.Point(6, 92);
            this.buttonSavePath.Name = "buttonSavePath";
            this.buttonSavePath.Size = new System.Drawing.Size(33, 29);
            this.buttonSavePath.TabIndex = 11;
            this.buttonSavePath.UseVisualStyleBackColor = false;
            this.buttonSavePath.Click += new System.EventHandler(this.buttonSavePath_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.BackgroundImage = global::XRD_Tool.Properties.Resources.靶材选择底图;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.radioButtonTargetType2);
            this.panel1.Controls.Add(this.radioButtonTargetType1);
            this.panel1.Location = new System.Drawing.Point(86, 162);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 168);
            this.panel1.TabIndex = 13;
            // 
            // radioButtonTargetType2
            // 
            this.radioButtonTargetType2.AutoSize = true;
            this.radioButtonTargetType2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonTargetType2.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonTargetType2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.radioButtonTargetType2.Location = new System.Drawing.Point(36, 124);
            this.radioButtonTargetType2.Name = "radioButtonTargetType2";
            this.radioButtonTargetType2.Size = new System.Drawing.Size(49, 23);
            this.radioButtonTargetType2.TabIndex = 1;
            this.radioButtonTargetType2.TabStop = true;
            this.radioButtonTargetType2.Text = "Cu";
            this.radioButtonTargetType2.UseVisualStyleBackColor = false;
            // 
            // radioButtonTargetType1
            // 
            this.radioButtonTargetType1.AutoSize = true;
            this.radioButtonTargetType1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonTargetType1.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonTargetType1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.radioButtonTargetType1.Location = new System.Drawing.Point(36, 68);
            this.radioButtonTargetType1.Name = "radioButtonTargetType1";
            this.radioButtonTargetType1.Size = new System.Drawing.Size(49, 23);
            this.radioButtonTargetType1.TabIndex = 0;
            this.radioButtonTargetType1.TabStop = true;
            this.radioButtonTargetType1.Text = "Cr";
            this.radioButtonTargetType1.UseVisualStyleBackColor = false;
            // 
            // panel2
            // 
            this.panel2.BackgroundImage = global::XRD_Tool.Properties.Resources.探测器选择底图;
            this.panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel2.Controls.Add(this.radioButtonDetectorType2);
            this.panel2.Controls.Add(this.radioButtonDetectorType1);
            this.panel2.Location = new System.Drawing.Point(487, 162);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(267, 168);
            this.panel2.TabIndex = 14;
            // 
            // radioButtonDetectorType2
            // 
            this.radioButtonDetectorType2.AutoSize = true;
            this.radioButtonDetectorType2.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonDetectorType2.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonDetectorType2.ForeColor = System.Drawing.SystemColors.Highlight;
            this.radioButtonDetectorType2.Location = new System.Drawing.Point(39, 124);
            this.radioButtonDetectorType2.Name = "radioButtonDetectorType2";
            this.radioButtonDetectorType2.Size = new System.Drawing.Size(147, 23);
            this.radioButtonDetectorType2.TabIndex = 3;
            this.radioButtonDetectorType2.TabStop = true;
            this.radioButtonDetectorType2.Text = "线阵列探测器";
            this.radioButtonDetectorType2.UseVisualStyleBackColor = false;
            // 
            // radioButtonDetectorType1
            // 
            this.radioButtonDetectorType1.AutoSize = true;
            this.radioButtonDetectorType1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonDetectorType1.Font = new System.Drawing.Font("SimSun", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.radioButtonDetectorType1.ForeColor = System.Drawing.SystemColors.Highlight;
            this.radioButtonDetectorType1.Location = new System.Drawing.Point(39, 68);
            this.radioButtonDetectorType1.Name = "radioButtonDetectorType1";
            this.radioButtonDetectorType1.Size = new System.Drawing.Size(131, 23);
            this.radioButtonDetectorType1.TabIndex = 2;
            this.radioButtonDetectorType1.TabStop = true;
            this.radioButtonDetectorType1.Text = "SDD 探测器";
            this.radioButtonDetectorType1.UseVisualStyleBackColor = false;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::XRD_Tool.Properties.Resources.文件目录选择;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Controls.Add(this.textBoxSavePath);
            this.panel3.Controls.Add(this.buttonSavePath);
            this.panel3.Location = new System.Drawing.Point(886, 162);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(267, 168);
            this.panel3.TabIndex = 14;
            // 
            // FormDeviceInit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::XRD_Tool.Properties.Resources.底图1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.buttonSkip);
            this.Name = "FormDeviceInit";
            this.Text = "浩元仪器测量软件（Ver1.0）";
            this.Activated += new System.EventHandler(this.FormDeviceInit_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDeviceInit_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormDeviceInit_FormClosed);
            this.Load += new System.EventHandler(this.DeviceInit_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonSkip;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Button buttonSavePath;
        public System.Windows.Forms.TextBox textBoxSavePath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioButtonTargetType2;
        private System.Windows.Forms.RadioButton radioButtonTargetType1;
        private System.Windows.Forms.RadioButton radioButtonDetectorType2;
        private System.Windows.Forms.RadioButton radioButtonDetectorType1;
    }
}