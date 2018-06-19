namespace XRD_Tool
{
    partial class FormShortcutHighVoltage
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.textBoxHighVoltage = new System.Windows.Forms.TextBox();
            this.textBoxCurrent = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Image = global::XRD_Tool.Properties.Resources.开启高压1态;
            this.buttonOK.Location = new System.Drawing.Point(288, 254);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(108, 33);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Image = global::XRD_Tool.Properties.Resources.关闭高压;
            this.buttonCancel.Location = new System.Drawing.Point(129, 254);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 33);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Image = global::XRD_Tool.Properties.Resources._004;
            this.buttonExit.Location = new System.Drawing.Point(476, 22);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(35, 34);
            this.buttonExit.TabIndex = 8;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // textBoxHighVoltage
            // 
            this.textBoxHighVoltage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxHighVoltage.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxHighVoltage.Location = new System.Drawing.Point(172, 149);
            this.textBoxHighVoltage.Name = "textBoxHighVoltage";
            this.textBoxHighVoltage.Size = new System.Drawing.Size(267, 22);
            this.textBoxHighVoltage.TabIndex = 9;
            // 
            // textBoxCurrent
            // 
            this.textBoxCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCurrent.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxCurrent.Location = new System.Drawing.Point(172, 200);
            this.textBoxCurrent.Name = "textBoxCurrent";
            this.textBoxCurrent.Size = new System.Drawing.Size(267, 22);
            this.textBoxCurrent.TabIndex = 10;
            // 
            // FormShortcutHighVoltage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::XRD_Tool.Properties.Resources.高压开关底图;
            this.ClientSize = new System.Drawing.Size(532, 338);
            this.Controls.Add(this.textBoxCurrent);
            this.Controls.Add(this.textBoxHighVoltage);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormShortcutHighVoltage";
            this.Text = "FormShortcutHighVoltage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormShortcutHighVoltage_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormShortcutHighVoltage_FormClosed);
            this.Load += new System.EventHandler(this.FormShortcutHighVoltage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TextBox textBoxHighVoltage;
        private System.Windows.Forms.TextBox textBoxCurrent;
    }
}