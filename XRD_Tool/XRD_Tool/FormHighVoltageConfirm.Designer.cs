namespace XRD_Tool
{
    partial class FormHighVoltageConfirm
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
            this.textBoxCurrent = new System.Windows.Forms.TextBox();
            this.textBoxHighVoltage = new System.Windows.Forms.TextBox();
            this.buttonOk = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxCurrent
            // 
            this.textBoxCurrent.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCurrent.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxCurrent.Location = new System.Drawing.Point(172, 200);
            this.textBoxCurrent.Name = "textBoxCurrent";
            this.textBoxCurrent.Size = new System.Drawing.Size(267, 22);
            this.textBoxCurrent.TabIndex = 12;
            // 
            // textBoxHighVoltage
            // 
            this.textBoxHighVoltage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxHighVoltage.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxHighVoltage.Location = new System.Drawing.Point(172, 149);
            this.textBoxHighVoltage.Name = "textBoxHighVoltage";
            this.textBoxHighVoltage.Size = new System.Drawing.Size(267, 22);
            this.textBoxHighVoltage.TabIndex = 11;
            // 
            // buttonOk
            // 
            this.buttonOk.FlatAppearance.BorderSize = 0;
            this.buttonOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonOk.Image = global::XRD_Tool.Properties.Resources.关闭高压;
            this.buttonOk.Location = new System.Drawing.Point(144, 264);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(108, 33);
            this.buttonOk.TabIndex = 13;
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.FlatAppearance.BorderSize = 0;
            this.buttonCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCancel.Image = global::XRD_Tool.Properties.Resources.取消按钮;
            this.buttonCancel.Location = new System.Drawing.Point(302, 264);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(108, 33);
            this.buttonCancel.TabIndex = 14;
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonExit
            // 
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Image = global::XRD_Tool.Properties.Resources._004;
            this.buttonExit.Location = new System.Drawing.Point(482, 22);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(35, 34);
            this.buttonExit.TabIndex = 15;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // FormHighVoltageConfirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::XRD_Tool.Properties.Resources.高压开关底图;
            this.ClientSize = new System.Drawing.Size(532, 338);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.textBoxCurrent);
            this.Controls.Add(this.textBoxHighVoltage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormHighVoltageConfirm";
            this.Text = "FormHighVoltageConfirm";
            this.Load += new System.EventHandler(this.FormHighVoltageConfirm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxCurrent;
        private System.Windows.Forms.TextBox textBoxHighVoltage;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonExit;
    }
}