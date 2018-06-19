namespace XRD_Tool
{
    partial class FormChildTexture
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelSetting = new System.Windows.Forms.Panel();
            this.groupBoxSettings = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.checkBoxExpert = new System.Windows.Forms.CheckBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxScanMethod = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSampleName = new System.Windows.Forms.TextBox();
            this.textBoxPeakDegree = new System.Windows.Forms.TextBox();
            this.textBoxSampleSn = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBoxPsiStep = new System.Windows.Forms.TextBox();
            this.textBoxFaceExp = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxTubeVoltage = new System.Windows.Forms.TextBox();
            this.textBoxTubeCurrent = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBoxBM = new System.Windows.Forms.TextBox();
            this.labelScanMethod = new System.Windows.Forms.Label();
            this.textBoxPsiStartAngle = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.textBoxPsiStopAngle = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.textBoxPhiSpeed = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBoxMeasureTime = new System.Windows.Forms.TextBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.panelRealTimeChart = new System.Windows.Forms.Panel();
            this.panelDataResultTable = new System.Windows.Forms.Panel();
            this.chartRealTime = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridViewCalcResult = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelSetting.SuspendLayout();
            this.groupBoxSettings.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelRealTimeChart.SuspendLayout();
            this.panelDataResultTable.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartRealTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalcResult)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.Controls.Add(this.panelSetting, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelRealTimeChart, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelDataResultTable, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1264, 681);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelSetting
            // 
            this.panelSetting.BackgroundImage = global::XRD_Tool.Properties.Resources.参数设置底图;
            this.panelSetting.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelSetting.Controls.Add(this.groupBoxSettings);
            this.panelSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSetting.Location = new System.Drawing.Point(3, 3);
            this.panelSetting.Name = "panelSetting";
            this.tableLayoutPanel1.SetRowSpan(this.panelSetting, 2);
            this.panelSetting.Size = new System.Drawing.Size(373, 675);
            this.panelSetting.TabIndex = 0;
            // 
            // groupBoxSettings
            // 
            this.groupBoxSettings.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.groupBoxSettings.Controls.Add(this.tableLayoutPanel2);
            this.groupBoxSettings.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBoxSettings.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBoxSettings.Location = new System.Drawing.Point(0, 43);
            this.groupBoxSettings.Name = "groupBoxSettings";
            this.groupBoxSettings.Size = new System.Drawing.Size(373, 632);
            this.groupBoxSettings.TabIndex = 50;
            this.groupBoxSettings.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
            this.tableLayoutPanel2.Controls.Add(this.checkBoxExpert, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.buttonStart, 0, 16);
            this.tableLayoutPanel2.Controls.Add(this.label1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.label2, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.comboBoxScanMethod, 1, 8);
            this.tableLayoutPanel2.Controls.Add(this.label4, 0, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxSampleName, 1, 1);
            this.tableLayoutPanel2.Controls.Add(this.textBoxPeakDegree, 1, 4);
            this.tableLayoutPanel2.Controls.Add(this.textBoxSampleSn, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.label11, 0, 3);
            this.tableLayoutPanel2.Controls.Add(this.textBoxPsiStep, 1, 9);
            this.tableLayoutPanel2.Controls.Add(this.textBoxFaceExp, 1, 3);
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 9);
            this.tableLayoutPanel2.Controls.Add(this.label9, 0, 5);
            this.tableLayoutPanel2.Controls.Add(this.label10, 0, 6);
            this.tableLayoutPanel2.Controls.Add(this.textBoxTubeVoltage, 1, 5);
            this.tableLayoutPanel2.Controls.Add(this.textBoxTubeCurrent, 1, 6);
            this.tableLayoutPanel2.Controls.Add(this.label12, 0, 7);
            this.tableLayoutPanel2.Controls.Add(this.textBoxBM, 1, 7);
            this.tableLayoutPanel2.Controls.Add(this.labelScanMethod, 0, 8);
            this.tableLayoutPanel2.Controls.Add(this.textBoxPsiStartAngle, 1, 10);
            this.tableLayoutPanel2.Controls.Add(this.label14, 0, 10);
            this.tableLayoutPanel2.Controls.Add(this.textBoxPsiStopAngle, 1, 11);
            this.tableLayoutPanel2.Controls.Add(this.label15, 0, 11);
            this.tableLayoutPanel2.Controls.Add(this.textBoxPhiSpeed, 1, 12);
            this.tableLayoutPanel2.Controls.Add(this.label16, 0, 12);
            this.tableLayoutPanel2.Controls.Add(this.label17, 0, 13);
            this.tableLayoutPanel2.Controls.Add(this.textBoxMeasureTime, 1, 13);
            this.tableLayoutPanel2.Controls.Add(this.buttonStop, 1, 16);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.Blue;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 22);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 18;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(367, 607);
            this.tableLayoutPanel2.TabIndex = 51;
            // 
            // checkBoxExpert
            // 
            this.checkBoxExpert.Dock = System.Windows.Forms.DockStyle.Fill;
            this.checkBoxExpert.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBoxExpert.ForeColor = System.Drawing.Color.Blue;
            this.checkBoxExpert.Location = new System.Drawing.Point(149, 3);
            this.checkBoxExpert.Name = "checkBoxExpert";
            this.checkBoxExpert.Size = new System.Drawing.Size(215, 24);
            this.checkBoxExpert.TabIndex = 51;
            this.checkBoxExpert.Text = "高级模式";
            this.checkBoxExpert.UseVisualStyleBackColor = true;
            this.checkBoxExpert.CheckedChanged += new System.EventHandler(this.checkBoxExpert_CheckedChanged);
            // 
            // buttonStart
            // 
            this.buttonStart.FlatAppearance.BorderSize = 0;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Image = global::XRD_Tool.Properties.Resources.开始按钮1态;
            this.buttonStart.Location = new System.Drawing.Point(3, 483);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(115, 34);
            this.buttonStart.TabIndex = 43;
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(3, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 30);
            this.label1.TabIndex = 0;
            this.label1.Text = "样品名称：";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 30);
            this.label2.TabIndex = 1;
            this.label2.Text = "样品编号：";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBoxScanMethod
            // 
            this.comboBoxScanMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBoxScanMethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxScanMethod.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBoxScanMethod.FormattingEnabled = true;
            this.comboBoxScanMethod.Items.AddRange(new object[] {
            "常规扫描",
            "快速扫描",
            "高强度扫描"});
            this.comboBoxScanMethod.Location = new System.Drawing.Point(149, 243);
            this.comboBoxScanMethod.Name = "comboBoxScanMethod";
            this.comboBoxScanMethod.Size = new System.Drawing.Size(215, 28);
            this.comboBoxScanMethod.TabIndex = 41;
            this.comboBoxScanMethod.SelectedIndexChanged += new System.EventHandler(this.comboBoxScanMethod_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 30);
            this.label4.TabIndex = 3;
            this.label4.Text = "峰位角度：";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxSampleName
            // 
            this.textBoxSampleName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSampleName.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSampleName.Location = new System.Drawing.Point(149, 33);
            this.textBoxSampleName.Name = "textBoxSampleName";
            this.textBoxSampleName.Size = new System.Drawing.Size(215, 26);
            this.textBoxSampleName.TabIndex = 10;
            this.textBoxSampleName.Text = "name";
            // 
            // textBoxPeakDegree
            // 
            this.textBoxPeakDegree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPeakDegree.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPeakDegree.Location = new System.Drawing.Point(149, 123);
            this.textBoxPeakDegree.Name = "textBoxPeakDegree";
            this.textBoxPeakDegree.Size = new System.Drawing.Size(215, 26);
            this.textBoxPeakDegree.TabIndex = 12;
            this.textBoxPeakDegree.Text = "0";
            // 
            // textBoxSampleSn
            // 
            this.textBoxSampleSn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxSampleSn.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxSampleSn.Location = new System.Drawing.Point(149, 63);
            this.textBoxSampleSn.Name = "textBoxSampleSn";
            this.textBoxSampleSn.Size = new System.Drawing.Size(215, 26);
            this.textBoxSampleSn.TabIndex = 11;
            this.textBoxSampleSn.Text = "sn";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label11.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label11.Location = new System.Drawing.Point(3, 90);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(140, 30);
            this.label11.TabIndex = 43;
            this.label11.Text = "晶面指数：";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPsiStep
            // 
            this.textBoxPsiStep.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPsiStep.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPsiStep.Location = new System.Drawing.Point(149, 273);
            this.textBoxPsiStep.Name = "textBoxPsiStep";
            this.textBoxPsiStep.Size = new System.Drawing.Size(215, 26);
            this.textBoxPsiStep.TabIndex = 14;
            this.textBoxPsiStep.Text = "0";
            // 
            // textBoxFaceExp
            // 
            this.textBoxFaceExp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxFaceExp.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxFaceExp.Location = new System.Drawing.Point(149, 93);
            this.textBoxFaceExp.Name = "textBoxFaceExp";
            this.textBoxFaceExp.Size = new System.Drawing.Size(215, 26);
            this.textBoxFaceExp.TabIndex = 44;
            this.textBoxFaceExp.Text = "sn";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 30);
            this.label6.TabIndex = 5;
            this.label6.Text = "ψ 步宽：";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label9.Location = new System.Drawing.Point(3, 150);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(140, 30);
            this.label9.TabIndex = 8;
            this.label9.Text = "管电压：";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label10.Location = new System.Drawing.Point(3, 180);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(140, 30);
            this.label10.TabIndex = 9;
            this.label10.Text = "管电流：";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxTubeVoltage
            // 
            this.textBoxTubeVoltage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTubeVoltage.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTubeVoltage.Location = new System.Drawing.Point(149, 153);
            this.textBoxTubeVoltage.Name = "textBoxTubeVoltage";
            this.textBoxTubeVoltage.Size = new System.Drawing.Size(215, 26);
            this.textBoxTubeVoltage.TabIndex = 17;
            // 
            // textBoxTubeCurrent
            // 
            this.textBoxTubeCurrent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxTubeCurrent.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxTubeCurrent.Location = new System.Drawing.Point(149, 183);
            this.textBoxTubeCurrent.Name = "textBoxTubeCurrent";
            this.textBoxTubeCurrent.Size = new System.Drawing.Size(215, 26);
            this.textBoxTubeCurrent.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label12.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label12.Location = new System.Drawing.Point(3, 210);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(140, 30);
            this.label12.TabIndex = 45;
            this.label12.Text = "B/M选择：";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxBM
            // 
            this.textBoxBM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxBM.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxBM.Location = new System.Drawing.Point(149, 213);
            this.textBoxBM.Name = "textBoxBM";
            this.textBoxBM.Size = new System.Drawing.Size(215, 26);
            this.textBoxBM.TabIndex = 46;
            // 
            // labelScanMethod
            // 
            this.labelScanMethod.AutoSize = true;
            this.labelScanMethod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelScanMethod.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelScanMethod.Location = new System.Drawing.Point(3, 240);
            this.labelScanMethod.Name = "labelScanMethod";
            this.labelScanMethod.Size = new System.Drawing.Size(140, 30);
            this.labelScanMethod.TabIndex = 47;
            this.labelScanMethod.Text = "扫描方法：";
            this.labelScanMethod.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPsiStartAngle
            // 
            this.textBoxPsiStartAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPsiStartAngle.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPsiStartAngle.Location = new System.Drawing.Point(149, 303);
            this.textBoxPsiStartAngle.Name = "textBoxPsiStartAngle";
            this.textBoxPsiStartAngle.Size = new System.Drawing.Size(215, 26);
            this.textBoxPsiStartAngle.TabIndex = 49;
            this.textBoxPsiStartAngle.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label14.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label14.Location = new System.Drawing.Point(3, 300);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(140, 30);
            this.label14.TabIndex = 48;
            this.label14.Text = "ψ 起始角度：";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPsiStopAngle
            // 
            this.textBoxPsiStopAngle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPsiStopAngle.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPsiStopAngle.Location = new System.Drawing.Point(149, 333);
            this.textBoxPsiStopAngle.Name = "textBoxPsiStopAngle";
            this.textBoxPsiStopAngle.Size = new System.Drawing.Size(215, 26);
            this.textBoxPsiStopAngle.TabIndex = 51;
            this.textBoxPsiStopAngle.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label15.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label15.Location = new System.Drawing.Point(3, 330);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(140, 30);
            this.label15.TabIndex = 50;
            this.label15.Text = "ψ 终止角度：";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxPhiSpeed
            // 
            this.textBoxPhiSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxPhiSpeed.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxPhiSpeed.Location = new System.Drawing.Point(149, 363);
            this.textBoxPhiSpeed.Name = "textBoxPhiSpeed";
            this.textBoxPhiSpeed.Size = new System.Drawing.Size(215, 26);
            this.textBoxPhiSpeed.TabIndex = 53;
            this.textBoxPhiSpeed.Text = "0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label16.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label16.Location = new System.Drawing.Point(3, 360);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(140, 30);
            this.label16.TabIndex = 52;
            this.label16.Text = "Φ 测量速度：";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label17.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label17.Location = new System.Drawing.Point(3, 390);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(140, 30);
            this.label17.TabIndex = 54;
            this.label17.Text = "测量时间：";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxMeasureTime
            // 
            this.textBoxMeasureTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBoxMeasureTime.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBoxMeasureTime.Location = new System.Drawing.Point(149, 393);
            this.textBoxMeasureTime.Name = "textBoxMeasureTime";
            this.textBoxMeasureTime.Size = new System.Drawing.Size(215, 26);
            this.textBoxMeasureTime.TabIndex = 55;
            this.textBoxMeasureTime.Text = "0";
            // 
            // buttonStop
            // 
            this.buttonStop.FlatAppearance.BorderSize = 0;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Image = global::XRD_Tool.Properties.Resources.终止按钮1;
            this.buttonStop.Location = new System.Drawing.Point(149, 483);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(115, 34);
            this.buttonStop.TabIndex = 44;
            this.buttonStop.UseVisualStyleBackColor = true;
            // 
            // panelRealTimeChart
            // 
            this.panelRealTimeChart.BackgroundImage = global::XRD_Tool.Properties.Resources.实时绘图底图;
            this.panelRealTimeChart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelRealTimeChart.Controls.Add(this.chartRealTime);
            this.panelRealTimeChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRealTimeChart.Location = new System.Drawing.Point(382, 3);
            this.panelRealTimeChart.Name = "panelRealTimeChart";
            this.panelRealTimeChart.Size = new System.Drawing.Size(879, 334);
            this.panelRealTimeChart.TabIndex = 1;
            // 
            // panelDataResultTable
            // 
            this.panelDataResultTable.BackgroundImage = global::XRD_Tool.Properties.Resources.数据结果底图;
            this.panelDataResultTable.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelDataResultTable.Controls.Add(this.dataGridViewCalcResult);
            this.panelDataResultTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDataResultTable.Location = new System.Drawing.Point(382, 343);
            this.panelDataResultTable.Name = "panelDataResultTable";
            this.panelDataResultTable.Size = new System.Drawing.Size(879, 335);
            this.panelDataResultTable.TabIndex = 2;
            // 
            // chartRealTime
            // 
            chartArea2.BackColor = System.Drawing.Color.White;
            chartArea2.Name = "ChartArea1";
            this.chartRealTime.ChartAreas.Add(chartArea2);
            this.chartRealTime.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.DockedToChartArea = "ChartArea1";
            legend2.Name = "Legend1";
            this.chartRealTime.Legends.Add(legend2);
            this.chartRealTime.Location = new System.Drawing.Point(0, 0);
            this.chartRealTime.Name = "chartRealTime";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "射线强度";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Int32;
            this.chartRealTime.Series.Add(series2);
            this.chartRealTime.Size = new System.Drawing.Size(879, 334);
            this.chartRealTime.TabIndex = 1;
            // 
            // dataGridViewCalcResult
            // 
            this.dataGridViewCalcResult.AllowUserToAddRows = false;
            this.dataGridViewCalcResult.AllowUserToDeleteRows = false;
            this.dataGridViewCalcResult.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewCalcResult.BackgroundColor = System.Drawing.SystemColors.ButtonHighlight;
            this.dataGridViewCalcResult.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridViewCalcResult.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.NullValue = "0.00";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewCalcResult.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewCalcResult.Location = new System.Drawing.Point(3, 28);
            this.dataGridViewCalcResult.Name = "dataGridViewCalcResult";
            this.dataGridViewCalcResult.ReadOnly = true;
            this.dataGridViewCalcResult.RowTemplate.Height = 23;
            this.dataGridViewCalcResult.Size = new System.Drawing.Size(853, 245);
            this.dataGridViewCalcResult.TabIndex = 1;
            // 
            // FormChildTexture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 681);
            this.ControlBox = false;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormChildTexture";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormChildTexture_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormChildTexture_FormClosed);
            this.Load += new System.EventHandler(this.FormChildTexture_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelSetting.ResumeLayout(false);
            this.groupBoxSettings.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.panelRealTimeChart.ResumeLayout(false);
            this.panelDataResultTable.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartRealTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCalcResult)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelSetting;
        private System.Windows.Forms.Panel panelRealTimeChart;
        private System.Windows.Forms.Panel panelDataResultTable;
        private System.Windows.Forms.GroupBox groupBoxSettings;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.TextBox textBoxTubeCurrent;
        public System.Windows.Forms.ComboBox comboBoxScanMethod;
        public System.Windows.Forms.TextBox textBoxTubeVoltage;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.TextBox textBoxPsiStep;
        public System.Windows.Forms.TextBox textBoxSampleName;
        public System.Windows.Forms.TextBox textBoxPeakDegree;
        public System.Windows.Forms.TextBox textBoxSampleSn;
        private System.Windows.Forms.CheckBox checkBoxExpert;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.TextBox textBoxFaceExp;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.TextBox textBoxBM;
        private System.Windows.Forms.Label labelScanMethod;
        public System.Windows.Forms.TextBox textBoxPsiStartAngle;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.TextBox textBoxPsiStopAngle;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.TextBox textBoxPhiSpeed;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.TextBox textBoxMeasureTime;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonStop;
        public System.Windows.Forms.DataVisualization.Charting.Chart chartRealTime;
        public System.Windows.Forms.DataGridView dataGridViewCalcResult;
    }
}