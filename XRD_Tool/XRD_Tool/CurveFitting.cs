using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace XRD_Tool
{
    public class CurveFitting
    {
        private XrdConfig myConfig;
        public static string strCallPyCmd;

        public CurveFitting(XrdConfig config)
        {
            myConfig = config;
        }


        public static bool Calc2Theta(string dataFile, ref double[] calcResult, int functionIndex)
        {
            bool result = false;

            string CurrentPath = System.IO.Directory.GetCurrentDirectory();
            //string ScriptFileName = CurrentPath + "\\Resources\\CurveFittingPearson_20180526.py";
            string ScriptFileName = CurrentPath + "\\Resources\\CurveFittingPearson_20180622.py";
            string ResultFileName = CurrentPath + "\\Resources\\CalcResult.py";


            strCallPyCmd = "python";
            strCallPyCmd += " " + ScriptFileName;    // script name
            strCallPyCmd += " " + functionIndex.ToString();      // api index
            strCallPyCmd += " " + dataFile; // api param1
            strCallPyCmd += " " + "0";      // api param2,pattern

            strCallPyCmd += " > " + ResultFileName;   // output file

            Process p = new Process();

            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            //p.StartInfo.RedirectStandardOutput = true;          
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;

            try
            {
                p.Start();

                p.StandardInput.WriteLine(strCallPyCmd + "&exit");
                p.StandardInput.AutoFlush = true;
                //string strOuput = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
                p.Close();

                //print(x0, H, D, omega, sigma, sep=',', end="\r\n")
                StreamReader sr = new StreamReader(ResultFileName, Encoding.Default);
                //if (sr.ReadToEnd().IndexOf(',') > 0)
                //{
                    string[] nodes = sr.ReadToEnd().Split(',');
                    calcResult = Array.ConvertAll(nodes, double.Parse);

                    result = true;
                //} 
                    sr.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("error:" + ex.ToString() + p.StandardError.ReadToEnd());
            }

            return result;
        }

        public static bool PearsonVII_Calc_Y(double x, double x0, double H, double omega, double sigma, out double y)
        {
            bool result = false;

            y = 0.0;

            try
            {
                y = H / (Math.Pow(1 + Math.Pow(((2 * (x - x0) * Math.Sqrt(Math.Pow(2, (1 / omega)) - 1)) / sigma), 2), omega));
                result = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("error:" + ex.ToString());
            }

            return result;
        }

        public static bool PearsonVII_Jade_Calc_Y(double x, double x0, double H, double n, double D, out double y)
        {
            bool result = false;

            y = 0.0;

            try
            {
                y = H / (Math.Pow((1 + (4 * (Math.Pow(2, 1 / n) -1) / (Math.Pow(D, 2))) * (Math.Pow(((x - x0)), 2))), n));

                result = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("error:" + ex.ToString());
            }

            return result;
        }

        public static bool Calc_Sin2_Psi(double PsiAngle, out double value)
        {
            bool result = false;

            value = 0.0;

            try
            {
                value = Math.Pow((Math.Sin(PsiAngle / 180 * Math.PI)), 2);
                result = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("error:" + ex.ToString());
            }

            return result;
        }

        public static bool Calc_d(double Theta, double xRay, out double value)
        {
            bool result = false;

            value = 0.0;

            try
            {
                value = xRay / (2 * Math.Sin(Theta / 180 * Math.PI));
                result = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("error:" + ex.ToString());
            }

            return result;
        }

        public static bool LeastSquare(double[] x, double[] y, out double B, out double A)
        {
            bool result = false;

            B = 0.0;
            A = 0.0;

            try
            {
                double xMean = 0.0;
                for (int i=0; i<x.Length; i++)
                {
                    xMean += x[i];
                }
                xMean = xMean / x.Length;

                double yMean = 0.0;
                for (int i = 0; i < y.Length; i++)
                {
                    yMean += y[i];
                }
                yMean = yMean / y.Length;

                double xSum = 0.0;
                double ySum = 0.0;

                for (int i = 0; i < x.Length; i++)
                {
                    xSum += (x[i] - xMean) * (y[i] - yMean);
                    ySum += Math.Pow((x[i] - xMean), 2);
                }

                B = xSum / ySum;
                A = yMean - B * xMean;

                //xMean = sum(x) / len(x)   
                //yMean = sum(y) / len(y)   
                //xSum = 0.0
                //ySum = 0.0
                //for i in range(len(x)):
                //    xSum += (x[i] - xMean) * (y[i] - yMean)
                //    ySum += (x[i] - xMean)**2
                //k = xSum / ySum
                //b = yMean - k * xMean

                result = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("error:" + ex.ToString());
            }

            return result;
        }

        public static bool Calc_Stress(double[] x, double[] y, double E, double V, out double Stress, out double A, out double B)
        {
            bool result = false;

            Stress = 0.0;
            A = 0.0;
            B = 0.0;

            try
            {
                
                LeastSquare(x, y, out B, out A);

                Stress = B * E / (1 + V);

                result = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("error:" + ex.ToString());
            }

            return result;
        }

        public static void txt_parse(string file_path, out double[] dataX, out double[] dataY)
        {
            dataX = null;
            dataY = null;

            try
            {
                StreamReader sr = new StreamReader(file_path, Encoding.Default);
                string txt = sr.ReadToEnd();
                txt = txt.Replace("\r\n", "-");
                txt = txt.Replace("\r", " ");
                txt = txt.Replace("\n", " ");
                txt = txt.Replace("C=", " ");

                string[] nodes = txt.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                double[] x = new double[nodes.Length];
                double[] y = new double[nodes.Length];

                for (int i = 0; i < nodes.Length; i++)
                {
                    //string[] strs = Regex.Split(nodes[i], "   ", RegexOptions.IgnoreCase);
                    //x[i] = Convert.ToDouble(strs[0]);
                    //y[i] = Convert.ToInt32(strs[1]);
                    nodes[i] = nodes[i].Trim();
                    nodes[i] = nodes[i].Replace(" ", "");
                    byte[] array = System.Text.Encoding.ASCII.GetBytes(nodes[i]);
                    if (array.Length > 8)
                    {
                        byte[] array_x = new byte[8];
                        byte[] array_y = new byte[8];
                        System.Array.Copy(array, array_x, array_x.Length);
                        System.Array.Copy(array, array_x.Length, array_y, 0, array.Length - array_x.Length);

                        x[i] = double.Parse(System.Text.Encoding.Default.GetString(array_x));
                        y[i] = double.Parse(System.Text.Encoding.Default.GetString(array_y));
                    }
                    else
                    {
                        //string str = SerialPortCommon.ToHexString(array);
                        //MessageBox.Show("数据错误: " + str);

                        //return;
                    }

                }

                dataX = x;
                dataY = y;
            }
            catch (Exception ex)
            {
                //myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public static void txt_parse(string file_path, out double[] dataX, out int[] dataY)
        {
            dataX = null;
            dataY = null;

            try
            {
                StreamReader sr = new StreamReader(file_path, Encoding.Default);
                string txt = sr.ReadToEnd();
                txt = txt.Replace("\r\n", "-");
                txt = txt.Replace("\r", " ");
                txt = txt.Replace("\n", " ");
                txt = txt.Replace("C=", " ");

                string[] nodes = txt.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

                double[] x = new double[nodes.Length];
                int[] y = new int[nodes.Length];

                for (int i = 0; i < nodes.Length; i++)
                {
                    //string[] strs = Regex.Split(nodes[i], "   ", RegexOptions.IgnoreCase);
                    //x[i] = Convert.ToDouble(strs[0]);
                    //y[i] = Convert.ToInt32(strs[1]);
                    nodes[i] = nodes[i].Trim();
                    nodes[i] = nodes[i].Replace(" ", "");
                    byte[] array = System.Text.Encoding.ASCII.GetBytes(nodes[i]);
                    if (array.Length > 8)
                    {
                        byte[] array_x = new byte[8];
                        byte[] array_y = new byte[8];
                        System.Array.Copy(array, array_x, array_x.Length);
                        System.Array.Copy(array, array_x.Length, array_y, 0, array.Length - array_x.Length);

                        x[i] = double.Parse(System.Text.Encoding.Default.GetString(array_x));
                        y[i] = int.Parse(System.Text.Encoding.Default.GetString(array_y));
                    }
                    else
                    {
                        //string str = SerialPortCommon.ToHexString(array);
                        //MessageBox.Show("数据错误: " + str);

                        //return;
                    }

                }

                dataX = x;
                dataY = y;
            }
            catch (Exception ex)
            {
                //myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }
    }
}
