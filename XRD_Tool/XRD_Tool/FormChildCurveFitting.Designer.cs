namespace XRD_Tool
{
    partial class FormChildCurveFitting
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
            this.groupBox_ProfileParam = new System.Windows.Forms.GroupBox();
            this.radioButton_PearsonVII = new System.Windows.Forms.RadioButton();
            this.checkBox_CustomExp = new System.Windows.Forms.CheckBox();
            this.label_Exp = new System.Windows.Forms.Label();
            this.textBox_Exp = new System.Windows.Forms.TextBox();
            this.label_S = new System.Windows.Forms.Label();
            this.textBox_S = new System.Windows.Forms.TextBox();
            this.radioButton_PearsonVIIJade = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_85BG = new System.Windows.Forms.RadioButton();
            this.radioButton_DefaultBG = new System.Windows.Forms.RadioButton();
            this.radioButton_CurveFitBG = new System.Windows.Forms.RadioButton();
            this.radioButton_PearsonVIIJade3Step = new System.Windows.Forms.RadioButton();
            this.button_Ok = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.groupBox_ProfileParam.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_ProfileParam
            // 
            this.groupBox_ProfileParam.Controls.Add(this.textBox_S);
            this.groupBox_ProfileParam.Controls.Add(this.label_S);
            this.groupBox_ProfileParam.Controls.Add(this.textBox_Exp);
            this.groupBox_ProfileParam.Controls.Add(this.label_Exp);
            this.groupBox_ProfileParam.Controls.Add(this.checkBox_CustomExp);
            this.groupBox_ProfileParam.Controls.Add(this.radioButton_PearsonVIIJade3Step);
            this.groupBox_ProfileParam.Controls.Add(this.radioButton_PearsonVIIJade);
            this.groupBox_ProfileParam.Controls.Add(this.radioButton_PearsonVII);
            this.groupBox_ProfileParam.Location = new System.Drawing.Point(332, 64);
            this.groupBox_ProfileParam.Name = "groupBox_ProfileParam";
            this.groupBox_ProfileParam.Size = new System.Drawing.Size(602, 193);
            this.groupBox_ProfileParam.TabIndex = 0;
            this.groupBox_ProfileParam.TabStop = false;
            this.groupBox_ProfileParam.Text = "峰值定位方法";
            // 
            // radioButton_PearsonVII
            // 
            this.radioButton_PearsonVII.AutoSize = true;
            this.radioButton_PearsonVII.Location = new System.Drawing.Point(20, 32);
            this.radioButton_PearsonVII.Name = "radioButton_PearsonVII";
            this.radioButton_PearsonVII.Size = new System.Drawing.Size(89, 16);
            this.radioButton_PearsonVII.TabIndex = 0;
            this.radioButton_PearsonVII.TabStop = true;
            this.radioButton_PearsonVII.Text = "Pearson-VII";
            this.radioButton_PearsonVII.UseVisualStyleBackColor = true;
            // 
            // checkBox_CustomExp
            // 
            this.checkBox_CustomExp.AutoSize = true;
            this.checkBox_CustomExp.Location = new System.Drawing.Point(306, 33);
            this.checkBox_CustomExp.Name = "checkBox_CustomExp";
            this.checkBox_CustomExp.Size = new System.Drawing.Size(114, 16);
            this.checkBox_CustomExp.TabIndex = 1;
            this.checkBox_CustomExp.Text = "Custom Exponent";
            this.checkBox_CustomExp.UseVisualStyleBackColor = true;
            this.checkBox_CustomExp.CheckedChanged += new System.EventHandler(this.checkBox_CustomExp_CheckedChanged);
            // 
            // label_Exp
            // 
            this.label_Exp.AutoSize = true;
            this.label_Exp.Location = new System.Drawing.Point(451, 32);
            this.label_Exp.Name = "label_Exp";
            this.label_Exp.Size = new System.Drawing.Size(65, 12);
            this.label_Exp.TabIndex = 2;
            this.label_Exp.Text = "Exponent =";
            this.label_Exp.Visible = false;
            // 
            // textBox_Exp
            // 
            this.textBox_Exp.Location = new System.Drawing.Point(523, 26);
            this.textBox_Exp.Name = "textBox_Exp";
            this.textBox_Exp.Size = new System.Drawing.Size(45, 21);
            this.textBox_Exp.TabIndex = 3;
            this.textBox_Exp.Visible = false;
            // 
            // label_S
            // 
            this.label_S.AutoSize = true;
            this.label_S.Location = new System.Drawing.Point(451, 66);
            this.label_S.Name = "label_S";
            this.label_S.Size = new System.Drawing.Size(65, 12);
            this.label_S.TabIndex = 2;
            this.label_S.Text = "Skewness =";
            this.label_S.Visible = false;
            // 
            // textBox_S
            // 
            this.textBox_S.Location = new System.Drawing.Point(523, 60);
            this.textBox_S.Name = "textBox_S";
            this.textBox_S.Size = new System.Drawing.Size(45, 21);
            this.textBox_S.TabIndex = 3;
            this.textBox_S.Visible = false;
            // 
            // radioButton_PearsonVIIJade
            // 
            this.radioButton_PearsonVIIJade.AutoSize = true;
            this.radioButton_PearsonVIIJade.Location = new System.Drawing.Point(20, 66);
            this.radioButton_PearsonVIIJade.Name = "radioButton_PearsonVIIJade";
            this.radioButton_PearsonVIIJade.Size = new System.Drawing.Size(137, 16);
            this.radioButton_PearsonVIIJade.TabIndex = 0;
            this.radioButton_PearsonVIIJade.TabStop = true;
            this.radioButton_PearsonVIIJade.Text = "Pearson-VII (Jade6)";
            this.radioButton_PearsonVIIJade.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_CurveFitBG);
            this.groupBox1.Controls.Add(this.radioButton_85BG);
            this.groupBox1.Controls.Add(this.radioButton_DefaultBG);
            this.groupBox1.Location = new System.Drawing.Point(332, 294);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(602, 165);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "背景扣减";
            // 
            // radioButton_85BG
            // 
            this.radioButton_85BG.AutoSize = true;
            this.radioButton_85BG.Location = new System.Drawing.Point(20, 66);
            this.radioButton_85BG.Name = "radioButton_85BG";
            this.radioButton_85BG.Size = new System.Drawing.Size(101, 16);
            this.radioButton_85BG.TabIndex = 0;
            this.radioButton_85BG.TabStop = true;
            this.radioButton_85BG.Text = "85%法扣除背景";
            this.radioButton_85BG.UseVisualStyleBackColor = true;
            // 
            // radioButton_DefaultBG
            // 
            this.radioButton_DefaultBG.AutoSize = true;
            this.radioButton_DefaultBG.Location = new System.Drawing.Point(20, 32);
            this.radioButton_DefaultBG.Name = "radioButton_DefaultBG";
            this.radioButton_DefaultBG.Size = new System.Drawing.Size(83, 16);
            this.radioButton_DefaultBG.TabIndex = 0;
            this.radioButton_DefaultBG.TabStop = true;
            this.radioButton_DefaultBG.Text = "不扣除背景";
            this.radioButton_DefaultBG.UseVisualStyleBackColor = true;
            // 
            // radioButton_CurveFitBG
            // 
            this.radioButton_CurveFitBG.AutoSize = true;
            this.radioButton_CurveFitBG.Location = new System.Drawing.Point(20, 102);
            this.radioButton_CurveFitBG.Name = "radioButton_CurveFitBG";
            this.radioButton_CurveFitBG.Size = new System.Drawing.Size(143, 16);
            this.radioButton_CurveFitBG.TabIndex = 1;
            this.radioButton_CurveFitBG.TabStop = true;
            this.radioButton_CurveFitBG.Text = "曲线水平拟合扣除背景";
            this.radioButton_CurveFitBG.UseVisualStyleBackColor = true;
            // 
            // radioButton_PearsonVIIJade3Step
            // 
            this.radioButton_PearsonVIIJade3Step.AutoSize = true;
            this.radioButton_PearsonVIIJade3Step.Location = new System.Drawing.Point(20, 103);
            this.radioButton_PearsonVIIJade3Step.Name = "radioButton_PearsonVIIJade3Step";
            this.radioButton_PearsonVIIJade3Step.Size = new System.Drawing.Size(215, 16);
            this.radioButton_PearsonVIIJade3Step.TabIndex = 0;
            this.radioButton_PearsonVIIJade3Step.TabStop = true;
            this.radioButton_PearsonVIIJade3Step.Text = "Pearson-VII (Jade6 with 3 steps)";
            this.radioButton_PearsonVIIJade3Step.UseVisualStyleBackColor = true;
            // 
            // button_Ok
            // 
            this.button_Ok.Location = new System.Drawing.Point(726, 474);
            this.button_Ok.Name = "button_Ok";
            this.button_Ok.Size = new System.Drawing.Size(75, 23);
            this.button_Ok.TabIndex = 5;
            this.button_Ok.Text = "确定";
            this.button_Ok.UseVisualStyleBackColor = true;
            this.button_Ok.Click += new System.EventHandler(this.button_Ok_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(825, 474);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.button_Cancel.TabIndex = 5;
            this.button_Cancel.Text = "取消";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // FormChildCurveFitting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1302, 658);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Ok);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox_ProfileParam);
            this.Name = "FormChildCurveFitting";
            this.Text = "FormChildCurveFitting";
            this.Load += new System.EventHandler(this.FormChildCurveFitting_Load);
            this.groupBox_ProfileParam.ResumeLayout(false);
            this.groupBox_ProfileParam.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox_ProfileParam;
        private System.Windows.Forms.CheckBox checkBox_CustomExp;
        private System.Windows.Forms.RadioButton radioButton_PearsonVII;
        private System.Windows.Forms.TextBox textBox_S;
        private System.Windows.Forms.Label label_S;
        private System.Windows.Forms.TextBox textBox_Exp;
        private System.Windows.Forms.Label label_Exp;
        private System.Windows.Forms.RadioButton radioButton_PearsonVIIJade;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton_85BG;
        private System.Windows.Forms.RadioButton radioButton_DefaultBG;
        private System.Windows.Forms.RadioButton radioButton_CurveFitBG;
        private System.Windows.Forms.RadioButton radioButton_PearsonVIIJade3Step;
        private System.Windows.Forms.Button button_Ok;
        private System.Windows.Forms.Button button_Cancel;
    }
}