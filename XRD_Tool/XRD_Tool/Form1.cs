using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace XRD_Tool
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < 100; i++)
            {
                listX.Add(0.0 + i);
                listY.Add(Math.Sin((0.0 + i) / 180 * Math.PI));  
                
            }


            // 数据源至界面
            chart1.Series[0].Points.DataBindXY(listX, listY);
            chart1.Series[0].Points.DataBindY(listY);
            // 去除数据绑定
            //chart1.Series.Clear();
            // X轴不从0开始
            chart1.ChartAreas[0].AxisX.IsStartedFromZero = false;
            // X轴允许子栅格
            chart1.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
            // 设置游标的可偏移量
            chart1.ChartAreas[0].CursorX.Interval = 0.01D;
            chart1.ChartAreas[0].CursorX.IntervalOffset = 0.01D;
            // 游标可用、可选
            chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            // 填充
            chart1.Dock = DockStyle.Fill;
            // 线的Y轴依附于次Y轴
            chart1.Series[0].YAxisType = AxisType.Secondary;
            // 设置线型
            chart1.Series[0].ChartType = SeriesChartType.Line;
            // 设置线的颜色
            chart1.Series[0].Color = Color.Black;
            // 设置线宽
            chart1.Series[0].CustomProperties = "PointWidth=0.01";
            // 游标位置改变时触发的事件
            chart1.CursorPositionChanged += (sender, args) => { };
            // 游标的位置
            var position = chart1.ChartAreas[0].CursorX.Position;
            // X轴的最小值
            var mininum = chart1.ChartAreas[0].AxisX.Minimum;
            // Y轴可用性
            chart1.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            // 游标所在轴
            chart1.ChartAreas[0].CursorY.AxisType = AxisType.Secondary;
            // 线可用性
            chart1.Series[0].Enabled = true;
            // 轴坐标的形式
            chart1.ChartAreas[0].AxisX.LabelStyle.Format = "0.00";
            // 清楚图例
            chart1.Legends.Clear();
            // 轴不可缩放
            chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = false;
            // 选择区域改变时的事件
            chart1.SelectionRangeChanged +=
                (sender, args) =>
                {
                    if (args.Axis.AxisName == AxisName.X)
                    {
                        chart1.ChartAreas[0].AxisX.Minimum = 0;
                        chart1.ChartAreas[0].AxisX.Maximum = 100;
                    }
                    else if (args.Axis.AxisName == AxisName.Y)
                    {
                        chart1.ChartAreas[0].AxisY.Minimum = 0;
                        chart1.ChartAreas[0].AxisY.Maximum = 100;
                    }
                };
        }

        private List<double> listX = new List<double>();
        private List<double> listY = new List<double>();

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
