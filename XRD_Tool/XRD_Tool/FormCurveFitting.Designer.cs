namespace XRD_Tool
{
    partial class FormCurveFitting
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
            this.textBoxDataResut = new System.Windows.Forms.TextBox();
            this.buttonSetDataResut = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.buttonRunScript = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPy = new System.Windows.Forms.TextBox();
            this.buttonSetScript = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxDataResut
            // 
            this.textBoxDataResut.Location = new System.Drawing.Point(80, 68);
            this.textBoxDataResut.Name = "textBoxDataResut";
            this.textBoxDataResut.Size = new System.Drawing.Size(486, 21);
            this.textBoxDataResut.TabIndex = 0;
            // 
            // buttonSetDataResut
            // 
            this.buttonSetDataResut.Location = new System.Drawing.Point(583, 68);
            this.buttonSetDataResut.Name = "buttonSetDataResut";
            this.buttonSetDataResut.Size = new System.Drawing.Size(31, 23);
            this.buttonSetDataResut.TabIndex = 1;
            this.buttonSetDataResut.Text = "...";
            this.buttonSetDataResut.UseVisualStyleBackColor = true;
            this.buttonSetDataResut.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(80, 185);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "计算结果";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(80, 290);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(116, 23);
            this.button3.TabIndex = 3;
            this.button3.Text = ".txt DataTable";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonRunScript
            // 
            this.buttonRunScript.Location = new System.Drawing.Point(196, 185);
            this.buttonRunScript.Name = "buttonRunScript";
            this.buttonRunScript.Size = new System.Drawing.Size(116, 23);
            this.buttonRunScript.TabIndex = 4;
            this.buttonRunScript.Text = "运行脚本";
            this.buttonRunScript.UseVisualStyleBackColor = true;
            this.buttonRunScript.Click += new System.EventHandler(this.button4_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(78, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "数据文件：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "脚本文件：";
            // 
            // textBoxPy
            // 
            this.textBoxPy.Location = new System.Drawing.Point(80, 130);
            this.textBoxPy.Name = "textBoxPy";
            this.textBoxPy.Size = new System.Drawing.Size(486, 21);
            this.textBoxPy.TabIndex = 7;
            // 
            // buttonSetScript
            // 
            this.buttonSetScript.Location = new System.Drawing.Point(583, 128);
            this.buttonSetScript.Name = "buttonSetScript";
            this.buttonSetScript.Size = new System.Drawing.Size(31, 23);
            this.buttonSetScript.TabIndex = 8;
            this.buttonSetScript.Text = "...";
            this.buttonSetScript.UseVisualStyleBackColor = true;
            this.buttonSetScript.Click += new System.EventHandler(this.button5_Click);
            // 
            // FormCurveFitting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 501);
            this.Controls.Add(this.buttonSetScript);
            this.Controls.Add(this.textBoxPy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRunScript);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.buttonSetDataResut);
            this.Controls.Add(this.textBoxDataResut);
            this.Name = "FormCurveFitting";
            this.Text = "CurveFitting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCurveFitting_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCurveFitting_FormClosed);
            this.Load += new System.EventHandler(this.FormCurveFitting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxDataResut;
        private System.Windows.Forms.Button buttonSetDataResut;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button buttonRunScript;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPy;
        private System.Windows.Forms.Button buttonSetScript;
    }
}