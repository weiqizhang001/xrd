namespace XRD_Tool
{
    partial class FormShortcutFiveAxis
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
            this.buttonMoveStop = new System.Windows.Forms.Button();
            this.buttonMoveStart = new System.Windows.Forms.Button();
            this.textBoxAxisA = new System.Windows.Forms.TextBox();
            this.textBoxAxisB = new System.Windows.Forms.TextBox();
            this.textBoxAxisX = new System.Windows.Forms.TextBox();
            this.textBoxAxisY = new System.Windows.Forms.TextBox();
            this.textBoxAxisZ = new System.Windows.Forms.TextBox();
            this.buttonExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonMoveStop
            // 
            this.buttonMoveStop.FlatAppearance.BorderSize = 0;
            this.buttonMoveStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoveStop.Image = global::XRD_Tool.Properties.Resources.停止按钮;
            this.buttonMoveStop.Location = new System.Drawing.Point(113, 400);
            this.buttonMoveStop.Name = "buttonMoveStop";
            this.buttonMoveStop.Size = new System.Drawing.Size(108, 33);
            this.buttonMoveStop.TabIndex = 0;
            this.buttonMoveStop.UseVisualStyleBackColor = true;
            this.buttonMoveStop.Click += new System.EventHandler(this.buttonMoveStop_Click);
            // 
            // buttonMoveStart
            // 
            this.buttonMoveStart.FlatAppearance.BorderSize = 0;
            this.buttonMoveStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMoveStart.Image = global::XRD_Tool.Properties.Resources.移动1态;
            this.buttonMoveStart.Location = new System.Drawing.Point(272, 400);
            this.buttonMoveStart.Name = "buttonMoveStart";
            this.buttonMoveStart.Size = new System.Drawing.Size(108, 33);
            this.buttonMoveStart.TabIndex = 1;
            this.buttonMoveStart.UseVisualStyleBackColor = true;
            this.buttonMoveStart.Click += new System.EventHandler(this.buttonMoveStart_Click);
            // 
            // textBoxAxisA
            // 
            this.textBoxAxisA.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAxisA.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxAxisA.Location = new System.Drawing.Point(169, 149);
            this.textBoxAxisA.Name = "textBoxAxisA";
            this.textBoxAxisA.Size = new System.Drawing.Size(270, 22);
            this.textBoxAxisA.TabIndex = 2;
            // 
            // textBoxAxisB
            // 
            this.textBoxAxisB.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAxisB.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxAxisB.Location = new System.Drawing.Point(169, 195);
            this.textBoxAxisB.Name = "textBoxAxisB";
            this.textBoxAxisB.Size = new System.Drawing.Size(270, 22);
            this.textBoxAxisB.TabIndex = 3;
            // 
            // textBoxAxisX
            // 
            this.textBoxAxisX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAxisX.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxAxisX.Location = new System.Drawing.Point(169, 246);
            this.textBoxAxisX.Name = "textBoxAxisX";
            this.textBoxAxisX.Size = new System.Drawing.Size(270, 22);
            this.textBoxAxisX.TabIndex = 4;
            // 
            // textBoxAxisY
            // 
            this.textBoxAxisY.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAxisY.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxAxisY.Location = new System.Drawing.Point(169, 293);
            this.textBoxAxisY.Name = "textBoxAxisY";
            this.textBoxAxisY.Size = new System.Drawing.Size(270, 22);
            this.textBoxAxisY.TabIndex = 5;
            // 
            // textBoxAxisZ
            // 
            this.textBoxAxisZ.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAxisZ.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxAxisZ.Location = new System.Drawing.Point(169, 339);
            this.textBoxAxisZ.Name = "textBoxAxisZ";
            this.textBoxAxisZ.Size = new System.Drawing.Size(270, 22);
            this.textBoxAxisZ.TabIndex = 6;
            // 
            // buttonExit
            // 
            this.buttonExit.FlatAppearance.BorderSize = 0;
            this.buttonExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonExit.Image = global::XRD_Tool.Properties.Resources._004;
            this.buttonExit.Location = new System.Drawing.Point(476, 21);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(35, 34);
            this.buttonExit.TabIndex = 7;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            // 
            // FormShortcutFiveAxis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::XRD_Tool.Properties.Resources.五轴移动底图;
            this.ClientSize = new System.Drawing.Size(532, 469);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.textBoxAxisZ);
            this.Controls.Add(this.textBoxAxisY);
            this.Controls.Add(this.textBoxAxisX);
            this.Controls.Add(this.textBoxAxisB);
            this.Controls.Add(this.textBoxAxisA);
            this.Controls.Add(this.buttonMoveStart);
            this.Controls.Add(this.buttonMoveStop);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormShortcutFiveAxis";
            this.Text = "FormShortcutFiveAxis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormShortcutFiveAxis_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormShortcutFiveAxis_FormClosed);
            this.Load += new System.EventHandler(this.FormShortcutFiveAxis_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonMoveStop;
        private System.Windows.Forms.Button buttonMoveStart;
        private System.Windows.Forms.TextBox textBoxAxisA;
        private System.Windows.Forms.TextBox textBoxAxisB;
        private System.Windows.Forms.TextBox textBoxAxisX;
        private System.Windows.Forms.TextBox textBoxAxisY;
        private System.Windows.Forms.TextBox textBoxAxisZ;
        private System.Windows.Forms.Button buttonExit;
    }
}