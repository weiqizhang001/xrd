namespace XRD_Tool
{
    partial class FormShortcutAutoFocus
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
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.textBoxAxisZ = new System.Windows.Forms.TextBox();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // buttonOK
            // 
            this.buttonOK.FlatAppearance.BorderSize = 0;
            this.buttonOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOK.Image = global::XRD_Tool.Properties.Resources.开始对焦;
            this.buttonOK.Location = new System.Drawing.Point(127, 210);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(108, 33);
            this.buttonOK.TabIndex = 9;
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // textBoxAxisZ
            // 
            this.textBoxAxisZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAxisZ.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxAxisZ.Location = new System.Drawing.Point(222, 151);
            this.textBoxAxisZ.Name = "textBoxAxisZ";
            this.textBoxAxisZ.ReadOnly = true;
            this.textBoxAxisZ.Size = new System.Drawing.Size(235, 22);
            this.textBoxAxisZ.TabIndex = 10;
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Image = global::XRD_Tool.Properties.Resources.停止对焦;
            this.buttonCancel.Location = new System.Drawing.Point(273, 210);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 33);
            this.buttonCancel.TabIndex = 11;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // FormShortcutAutoFocus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::XRD_Tool.Properties.Resources.自动对焦底图;
            this.ClientSize = new System.Drawing.Size(532, 279);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.textBoxAxisZ);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.buttonExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormShortcutAutoFocus";
            this.Text = "FormShortcutAutoFocus";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormShortcutAutoFocus_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormShortcutAutoFocus_FormClosed);
            this.Load += new System.EventHandler(this.FormShortcutAutoFocus_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.TextBox textBoxAxisZ;
        private System.Windows.Forms.Button buttonCancel;
    }
}