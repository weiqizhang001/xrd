using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllLibrary
{
    public class SDDDetector
    {
        public struct DeviceZeroPoint { 
            
        
        
        }

        /// <summary>
        /// 0x0D: 发送回车（0X0D），等待上位机返回“C=”通讯字符。
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] CheckDeviceReady()
        {
            string Str = "\r";

            byte[] Cmd = null;

            try
            {
                Cmd = System.Text.Encoding.ASCII.GetBytes(Str);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }           

            return Cmd;
        }

        /// <summary>
        /// 0x0D: 返回Ready结果，true代表已经ready，false代表没有ready
        /// </summary>
        /// <returns></returns>
        public bool RecvDeviceReady(byte[] data)
        {
            //if ("C=" == data.ToString())
            //int c = data.ToString().IndexOf("C=");
            //string str1 = data.ToString();
            //string str2 = Convert.ToString(data);
            //string str = System.Text.Encoding.Default.GetString(data);

            int index = System.Text.Encoding.Default.GetString(data).IndexOf("C=");

            if (index >= 0)
            {
                return true;
            }
            else {
                return false;
            }        
        }

        /// <summary>
        /// RFF: 查询当前主机附件状态（例如返回“+101”代表五轴电机附件在线。）
        /// </summary>
        /// <returns></returns>
        public byte[] CheckHostAttachStatus()
        {
            string Str = "RFF" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// RFF: 返回当前主机附件状态
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool RecvHostAttachStatus(byte[] data)
        {
            int index = System.Text.Encoding.Default.GetString(data).IndexOf("++101");

            if (index >= 0)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }

        /// <summary>
        /// /: 暂停命令。（暂停当前动作）
        /// </summary>
        /// <returns></returns>
        public byte[] SetDevicePause()
        {
            string Str = "/" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// //: 复位命令。（启动看门狗）
        /// </summary>
        /// <returns></returns>
        public byte[] SetDeviceReset()
        {
            string Str = "//" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// PAR: 查询仪器零点（测角仪零点，返回结果与设置界面TS、TD进行比对）
        /// PAR：查看主机参数。
        /// </summary>
        /// <returns></returns>
        public byte[] GetDeviceZeroPoint()
        {
            string Str = "PAR" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// PAR: 返回仪器零点
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool RecvDeviceZeroPoint(byte[] data, ref double[] zeropoint)
        {
            string str1 = System.Text.Encoding.Default.GetString(data);
            int index1 = str1.IndexOf("TSZ");
            int index2 = str1.IndexOf("TDZ");

            if ((index1 >= 0) && (index2 >= 0))
            {
                string str2 = str1.Remove(0, index1 + "TSZ".Length);
                str2 = str2.Replace("\r\n", " ");
                str2 = str2.Replace("\nC=", " ");
                string[] str_array = str2.Split(new string[] { "\r \r", "TDZ" }, StringSplitOptions.RemoveEmptyEntries);

                zeropoint[0] = Convert.ToDouble(str_array[0]);
                zeropoint[1] = Convert.ToDouble(str_array[1]);

                return true;
            }
            else
            {
                zeropoint[0] = 0.0;
                zeropoint[1] = 0.0;

                return false;
            }
        }

        /// <summary>
        /// SDP
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public byte[] SetSDP(int mode)
        {
            string Str = "SDP" + mode.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// 设置电机工作方式:1,2,3
        /// MODx：设定电机工作方式：MOD1为TS电机转动、MOD2为TD电机转动、MOD3为TS、TD同时转动。
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public byte[] SetMoterMode(int mode)
        {
            string Str = "MOD" + mode.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SANxxx：（0~+170）控制两个电机（TS、TD）同时转动到xxx角度，（xxx角度=TS转动角度+TD转动角度）。
        /// 例如SAN20，则TS转动10度、TD转动10度，他们的共转动20度范围。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] MoveMoterTSAndTD(double degree)
        {
            string Str = "SAN" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// STHxxx：（0~+85）控制TS电机转动到xxx角度。（xxx角度=TS角度）
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] MoveMoterTS(double degree)
        {
            string Str = "STH" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// STTxxx：（0~+85）控制TD电机转动到xxx角度。（xxx角度=TD角度）
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] MoveMoterTD(double degree)
        {
            string Str = "STT" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// DPLX：向上位机返回TS+TD当前角度和以INT时间为单位的脉冲计数值。
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoterDegreeOfTSAndTD()
        {
            string Str = "DPLX" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// DPLT：向上位机返回TS当前角度和以INT时间为单位的脉冲计数值。
        /// </summary>
        /// <returns></returns>
        public byte[] GetMoterDegreeOfTS()
        {
            string Str = "DPLT" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// CTHxxx：设置TS电机电器零点。
        /// </summary>
        /// <returns></returns>
        public byte[] SetMoterTSZeroPoint(int degree)
        {
            string Str = "CTH" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// CTTxxx：设置TD电机电器零点。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] SetMoterTDZeroPoint(int degree)
        {
            string Str = "CTT" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// 设定脉冲计数时间（设置测量时间）
        /// INTxxx：（0.05S~10S）设定脉冲计数时间，单位：秒。
        /// 
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public byte[] SetPulseCountSecond(double sec)
        {
            string Str = "INT" + sec.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// 设定扫描步宽距离（设置测量步宽）
        /// STPxxx：（0.0002~2）设定扫描步宽距离，单位：度/步。
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        public byte[] SetScanStepDistance(double degree)
        {
            string Str = "STP" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SSCxxx：（MOD1：0-85 MOD2：0-85 MOD3：0-170）步进扫描方式，以STP为步宽、INT为脉冲计数时间进行扫描。
        ///（脉冲记录次数为：N=（xxx÷STP）+1。（加1为起始点开始记录）每运动一次电机停止后等待INT时间脉冲计数完成，然后进行下一次运动。
        ///a)MOD1工作方式：TS电机转动，步宽为STP。
        ///b)MOD2工作方式：TD电机转动，步宽为STP。
        ///c)MOD3工作方式：TS、TD两个电机同时以不同方向转动，步宽为“STP÷2”。例如：SSC10、TS转动5°范围，步宽为STP÷2。TD与TS相同。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] SetStepScanMode(double degreeRange)
        {
            string Str = "SSC" + degreeRange.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// CSBxxx：
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public byte[] SetCycleScanMode(double degree)
        {
            string Str = "CSB" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// CSCxxx：（MOD1：0-85 MOD2：0-85 MOD3：0-170）连续扫描方式，以SPES为电机速度、INT为脉冲计数时间进行扫描。工作方式与“SSC”的MOD1、2、3相同
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public byte[] SetContinuousScanMode(int count)
        {
            string Str = "CSC" + count.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// PATxxx：（0~+45）使用侧倾法时设置Ψ角的偏移角度。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] SetPsiAngle(double degree)
        {
            string Str = "PAT" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// 设置起始测量位置（峰位角度-测量范围）
        /// SATxxx：（0<xxx/2-Ψ、xxx/2+Ψ<170）使用侧倾法测量时根据PAT命令设置的Ψ角转动电机。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] SetStartMeasurePos(double degree)
        {
            string Str = "SAT" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// 设置高压（千伏、毫安）
        /// SKMxxx：升降压至xxx。SKM格式“SKM千伏 毫安”中间使用空格分开，千伏范围：10-60、毫安范围：5-80。
        /// </summary>
        /// <param name="Vol"></param>
        /// <param name="cur"></param>
        /// <returns></returns>
        public byte[] SetHighVoltage(double Vol, double cur)
        {
            string Str = "SKM" + Vol.ToString() + " " + cur.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// RKM：读取当前高压值。
        /// </summary>
        /// <param name="Vol"></param>
        /// <param name="cur"></param>
        /// <returns></returns>
        public byte[] GetHighVoltage()
        {
            string Str = "RKM" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// 返回高压电流
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool RecvHighVoltage(byte[] data, out double voltage, out double current)
        {
            string str1 = System.Text.Encoding.Default.GetString(data);
            int index = str1.IndexOf("RKM\r");

            voltage = 0.0;
            current = 0.0;

            if (index >= 0)
            {
                string str2 = str1.Remove(0, index + "RKM\r".Length);
                //string[] str_array = str2.Split(' ');
                str2 = str2.Replace("\r\nC=", " ");
                string[] str_array = str2.Split(new string[] { "\r \r" }, StringSplitOptions.RemoveEmptyEntries);

                if (str_array.Length > 1)
                {
                    voltage = Convert.ToDouble(str_array[0]);
                    current = Convert.ToDouble(str_array[1]);

                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// SHO：控制X射线光闸打开。
        /// </summary>
        /// <returns></returns>
        public byte[] OpenLightShutter()
        {
            string Str = "SHO" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SHC：控制X射线光闸关闭。
        /// </summary>
        /// <returns></returns>
        public byte[] CloseLightShutter()
        {
            string Str = "SHC" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// STAxxx.xxxx：（-30~+75）控制A电机转动到指定位置，转动结束后向上位机发送结束指令。
        /// </summary>
        /// <returns></returns>
        public byte[] FiveAxis_MoveMotorA(double degree)
        {
            string Str = "STA" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// STBxxx.xxxx：（0~360）控制B电机转动到指定位置，转动结束后向上位机发送结束指令。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_MoveMotorB(double degree)
        {
            string Str = "STB" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// STXxxx.xxxx：（-30~+30）控制X电机转动到指定位置，转动结束后向上位机发送结束指令。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_MoveMotorX(double degree)
        {
            string Str = "STX" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// STYxxx.xxxx：（-30~+30）控制Y电机转动到指定位置，转动结束后向上位机发送结束指令。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_MoveMotorY(double degree)
        {
            string Str = "STY" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// STZxxx.xxxx：（-2~+48）控制Z电机转动到指定位置，转动结束后向上位机发送结束指令。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_MoveMotorZ(double degree)
        {
            string Str = "STZ" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SSM角度参数A，角度参数B，角度参数X，角度参数Y，角度参数Z（角度参数n=xxx.xxxx）：
        /// 同时控制5个电机转动到指定位置，转动结束后向上位机发送结束指令。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_RotateMotorABXYZ(double[] degree)
        {
            string Str = "SSM" + degree[0].ToString() + "A," + degree[1].ToString() + "B," + degree[2].ToString() + "X," + degree[3].ToString() + "Y," + degree[4].ToString() + "Z" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// RTS：向上位机发送当前A、B、X、Y、Z电机位置。回送格式：（角度A，角度B，角度X，角度Y，角度Z）
        /// </summary>
        /// <returns></returns>
        public byte[] FiveAxis_GetMotorPosOfABXYZ()
        {
            string Str = "RTS" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// PAR: 返回仪器零点
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool FiveAxis_RecvMotorPosOfABXYZ(byte[] data, ref double[] pos)
        {
            string str1 = System.Text.Encoding.Default.GetString(data);
            int index1 = str1.IndexOf("RTS\r");
            str1 = str1.Replace("+", " ");
            str1 = str1.Replace("\r", " ");
            str1 = str1.Replace("\n", " ");
            str1 = str1.Replace("C=", " ");

            string str2 = str1.Remove(0, index1 + "RTS\r".Length);
            string[] str_array = str2.Split(new string[] { "A", "B", "X", "Y", "Z", }, StringSplitOptions.RemoveEmptyEntries);
            if (str_array.Length >= 5)
            {
                pos[0] = Convert.ToDouble(str_array[0]);
                pos[1] = Convert.ToDouble(str_array[1]);
                pos[2] = Convert.ToDouble(str_array[2]);
                pos[3] = Convert.ToDouble(str_array[3]);
                pos[4] = Convert.ToDouble(str_array[4]);

                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// SSAxxx.xxxx：（0~100）控制电机A以“步宽距离”正向转动（xxx.xxxx÷步宽距离）次，且每转动一次等待上位机应答后再转动下一次直到结束，
        /// 如果接收到停止命令则停止当前动作并记录当前角度，动作完成后向上位机发送结束指令。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_RotateMotorA(double degree)
        {
            string Str = "SSA" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SSBxxx.xxxx：（0~360）控制电机B，方式同上。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_RotateMotorB(double degree)
        {
            string Str = "SSB" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SSXxxx.xxxx：（0~60）控制电机X，方式同上。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_RotateMotorX(double degree)
        {
            string Str = "SSX" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SSYxxx.xxxx：（0~60）控制电机Y，方式同上。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_RotateMotorY(double degree)
        {
            string Str = "SSY" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SSZxxx.xxxx：（0~6）控制电机 Z，方式同上。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_RotateMotorZ(double degree)
        {
            string Str = "SSZ" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// **Laser On/Off
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool FiveAxis_RecvLaser(byte[] data, out bool isEnable)
        {
            int indexOn = System.Text.Encoding.Default.GetString(data).IndexOf("**Laser On");
            int indexOff = System.Text.Encoding.Default.GetString(data).IndexOf("**Laser Off");

            isEnable = false;

            if ((indexOn >= 0) || (indexOff >= 0))
            {
                if (indexOff >= 0)
                {
                    isEnable = true;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// ZTA0：设置A电机零点位置（现在的设置为只允许使用参数零）
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_SetZeroPointOfMotorA(double degree)
        {
            string Str = "ZTA0" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// ZTB0：设置B电机零点位置
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_SetZeroPointOfMotorB(double degree)
        {
            string Str = "ZTB0" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// ZTX0：设置X电机零点位置
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_SetZeroPointOfMotorX(double degree)
        {
            string Str = "ZTX0" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// ZTY0：设置Y电机零点位置
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_SetZeroPointOfMotorY(double degree)
        {
            string Str = "ZTY0" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// ZTZ0：设置Z电机零点位置
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_SetZeroPointOfMotorZ(double degree)
        {
            string Str = "ZTZ0" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// STPxx.xxxx：（0.0002~5）设置步宽距离。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_SetScanStepDistance(double degree)
        {
            string Str = "STP" + degree.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// SPExxx：（1~12）以xxx速度驱动B电机转动。单位“度/秒”，1度=10000脉冲
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public byte[] FiveAxis_SetSpeedMoterB(double speed)
        {
            string Str = "SPE" + speed.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// INTxx.xxx：（0.05~10）设置计数时间。
        /// </summary>
        /// <param name="sec"></param>
        /// <returns></returns>
        public byte[] FiveAxis_SetPulseCountSecond(double sec)
        {
            string Str = "INT" + sec.ToString() + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// /：（反斜杠）停止当前动作。
        /// </summary>
        /// <returns></returns>
        public byte[] FiveAxis_SetDevicePause()
        {
            string Str = "/" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        /// RST：复位命令，控制器及所有电机复位（电机复位：向小角度方向转动碰下限开关）。
        /// </summary>
        /// <returns></returns>
        public byte[] FiveAxis_SetDeviceReset()
        {
            string Str = "RST" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        /// <summary>
        ///  PAF：查看附件参数命令。
        /// </summary>
        /// <returns></returns>
        public byte[] FiveAxis_GetZeroPoint()
        {
            string Str = "PAF" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }

        public bool FiveAxis_RecvZeroPoint(byte[] data, ref double[] zeropoint)
        {
            string str1 = System.Text.Encoding.Default.GetString(data);
            int index1 = str1.IndexOf("ZTA");

            if (index1 >= 0)
            {
                string str2 = str1.Remove(0, index1 + "ZTA".Length);
                int index2 = str2.IndexOf("STP");
                str2 = str2.Remove(index2, str2.Length - index2);
                str2 = str2.Replace("\r\n\n", " ");
                string[] str_array = str2.Split(new string[] { "ZTB", "ZTX", "ZTY", "ZTZ"}, StringSplitOptions.RemoveEmptyEntries);

                zeropoint[2] = Convert.ToDouble(str_array[0]);
                zeropoint[3] = Convert.ToDouble(str_array[1]);
                zeropoint[4] = Convert.ToDouble(str_array[2]);
                zeropoint[5] = Convert.ToDouble(str_array[3]);
                zeropoint[6] = Convert.ToDouble(str_array[4]);

                return true;
            }
            else
            {
                zeropoint[2] = 0.0;
                zeropoint[3] = 0.0;
                zeropoint[4] = 0.0;
                zeropoint[5] = 0.0;
                zeropoint[6] = 0.0;

                return false;
            }
        }
        /// <summary>
        /// STZ48：激光定位自动寻水平零点。
        /// </summary>
        /// <param name="degree"></param>
        /// <returns></returns>
        public byte[] FiveAxis_LaserAutoPosHorizonZeroPoint(double degree)
        {
            string Str = "STZ48" + "\r";

            byte[] Cmd = System.Text.Encoding.ASCII.GetBytes(Str);

            return Cmd;
        }







    }
}
