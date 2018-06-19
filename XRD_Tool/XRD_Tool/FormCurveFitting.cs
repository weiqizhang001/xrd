using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace XRD_Tool
{
    public partial class FormCurveFitting : Form
    {
        private string pythonFilePath;
        private string rawDataFilePath;
        public XrdConfig myConfig;

        public FormCurveFitting(XrdConfig config)
        {
            myConfig = config;

            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";

            openFileDialog.Filter = "所有文件(*.*)|*.*"; // 如果需要筛选txt文件（"Files (*.txt)|*.txt"）

            openFileDialog.RestoreDirectory = true;

            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                rawDataFilePath = openFileDialog.FileName;
                this.textBoxDataResut.Clear();
                this.textBoxDataResut.Text = rawDataFilePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ////运行python脚本
            //var engine = IronPython.Hosting.Python.CreateEngine();
            //var scope = engine.CreateScope();
            //var source = engine.CreateScriptSourceFromFile(pythonFilePath);
            //source.Execute(scope);
            ////调用无返回值函数
            //var say_hello = scope.GetVariable<Func<object>>("say_hello");
            //say_hello();
            ////调用有返回值函数
            //var get_text = scope.GetVariable<Func<object>>("get_text");
            //var text = get_text().ToString();
            //MessageBox.Show(text);
            ////调用带参函数
            //var add = scope.GetVariable<Func<object, object, object>>("add");
            //var result = add(1, 2);
            //MessageBox.Show(result.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(textBoxDataResut.Text, Encoding.Default);
            string txt = sr.ReadToEnd().Replace("\r\n", "-");
            string[] nodes = txt.Split('-');

            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Field1", typeof(double));
            dt.Columns.Add("Field2", typeof(double));

            foreach (string node in nodes)
            {
                string[] strs = node.Split(',');
                DataRow dr = dt.NewRow();

                dr["ID"] = strs[0];
                dr["Field1"] = strs[1];
                dr["Field2"] = strs[2];

                dt.Rows.Add(dr);
            }

            sr.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //cmdline_run();

            //StreamReader sr = new StreamReader(textBoxDataResut.Text, Encoding.Default);
            //string txt = sr.ReadToEnd().Replace("\r\n", "-");
            //string[] nodes = txt.Split(new char[]{'-'}, StringSplitOptions.RemoveEmptyEntries);

            //double[] x = new double[nodes.Length];
            //int[] y = new int[nodes.Length];

            //for (int i = 0; i < nodes.Length; i++) {
            //    string[] strs = Regex.Split(nodes[i], "   ", RegexOptions.IgnoreCase);
            //    x[i] = Convert.ToDouble(strs[0]);
            //    y[i] = Convert.ToInt32(strs[1]);
            //}

            //// 运行python脚本
            //var engine = IronPython.Hosting.Python.CreateEngine();
            //var scope = engine.CreateScope();
            //var source = engine.CreateScriptSourceFromFile(pythonFilePath);
            //source.Execute(scope);

            //// 调用带参函数
            //// def F_CurveFit(number, x, y, pattern):
            //var curveFit = scope.GetVariable<Func<object, object, object, object[]>>("F_CurveFit");
            //object[] result = curveFit(nodes.Length, x, y);
            //MessageBox.Show(result.ToString());
  
            //sr.Close();



        }

      

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.InitialDirectory = "c:\\";

            openFileDialog.Filter = "所有文件(*.*)|*.*"; // 如果需要筛选txt文件（"Files (*.txt)|*.txt"）

            openFileDialog.RestoreDirectory = true;

            openFileDialog.FilterIndex = 1;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                pythonFilePath = openFileDialog.FileName;
                this.textBoxPy.Clear();
                this.textBoxPy.Text = pythonFilePath;
            }
        }

        private void FormCurveFitting_Load(object sender, EventArgs e)
        {
            textBoxDataResut.Text = "D:\\work\\xrd\\01.RU\\20180326\\data2\\gang-15.txt";
            textBoxPy.Text = "D:\\work\\xrd\\05.SWTools\\python\\CurveFittingPearson_20180505.py";
        }

        private void FormCurveFitting_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void FormCurveFitting_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                //System.Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
