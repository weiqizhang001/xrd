using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace XRD_Tool
{

    public struct DEVICE_CMD_ID
    {
        public const byte CONNECT = 0x00;
        public const byte CHECK_DEV_READY = 0x01;
        public const byte CHECK_HOST_ATTACH_STATUS = 0x02;
        public const byte SET_DEV_PAUSE = 0x03;
        public const byte RESET = 0x04;
        public const byte GET_DEV_ZERO_POINT = 0x05;
        public const byte GET_5_AXIS_ZERO_POINT = 0x06;
        public const byte SET_MOTOR_MODE = 0x07;
        public const byte SET_MES_TIME = 0x08;
        public const byte SET_SCAN_STEP_DISTANCE = 0x09;
        public const byte SET_START_MEA_POS = 0x0A;
        public const byte SET_PSI_ANGLE = 0x0B;
        public const byte SET_A_ANGLE = 0x0C;
        public const byte SET_B_ANGLE = 0x0D;
        public const byte SET_X_ANGLE = 0x0E;
        public const byte SET_Y_ANGLE = 0x0F;
        public const byte SET_Z_ANGLE = 0x10;
        public const byte SET_ABXYZ_ANGLE = 0x11;
        public const byte GET_ABXYZ_ANGLE = 0x12;
        public const byte SET_HIGH_VOLTAGE_UP = 0x13;
        public const byte SET_HIGH_VOLTAGE_DOWN = 0x14;
        public const byte GET_HIGH_VOLTAGE = 0x15;
        public const byte OPEN_LIGHT_SHUTTER = 0x16;
        public const byte CLOSE_LIGHT_SHUTTER = 0x17;
        public const byte START_SCAN = 0x18;
        public const byte MOVE_TS = 0x19;
        public const byte MOVE_TD = 0x20;
        public const byte SET_SDP = 0x21;
        public const byte SET_TARGET = 0x22;
        public const byte READ_FRAME = 0x23;
        public const byte STATUS = 0x24;
        public const byte SET_PSI_ANGLE_0 = 0x25;
        public const byte SET_A_ANGLE_0 = 0x26;
        public const byte SET_B_ANGLE_0 = 0x27;
        public const byte SET_B_SPEED = 0x28;

        public const byte LDA_RESET = 0x80;
        public const byte LDA_SET_TARGET = 0x81;
        public const byte LDA_SET_MEASURE_TIME = 0x82;
        public const byte LDA_SCAN_START = 0x83;
        public const byte LDA_SCAN_STOP = 0x84;
        public const byte LDA_READ_DATA = 0x85;

        public const byte INVALID = 0xFF;
    }

    public struct DEVICE_STATE
    {
        public const byte DEVICE_INIT = 0x01;
        public const byte SCAN = 0x02;
        public const byte SHORTCUT_HIGH_VOLTAGE = 0x03;
        public const byte SHORTCUT_FIVE_AXIS = 0x04;
        public const byte SHORTCUT_AUTO_FOCUS = 0x05;
        public const byte DEVICE_DEBUG = 0xFE;
        public const byte DEVICE_ERROR = 0xFF;
    }

    public struct TEXTURE_NORMAL_MODE
    {
        public const double PSI_STEP_REG = 5.0;
        public const double PSI_START_ANGLE_REG = 0.0;
        public const double PSI_STOP_ANGLE_REG = 70.0;
        public const double PHI_SPEED_REG = 5.0;
        public const double MEASURE_TIME_REG = 1.0;

        public const double PSI_STEP_FAST = 5.0;
        public const double PSI_START_ANGLE_FAST = 0.0;
        public const double PSI_STOP_ANGLE_FAST = 70.0;
        public const double PHI_SPEED_FAST = 10.0;
        public const double MEASURE_TIME_FAST = 0.5;

        public const double PSI_STEP_STRONG = 5.0;
        public const double PSI_START_ANGLE_STRONG = 0.0;
        public const double PSI_STOP_ANGLE_STRONG = 70.0;
        public const double PHI_SPEED_STRONG = 1.0;
        public const double MEASURE_TIME_STRONG = 5.0;
    }

    public struct FIX_VALUE
    {
        public const double X_RAY_LENGTH_Cr_Ka = 2.2907;
        public const double X_RAY_LENGTH_Cu_Ka = 1.5418;
        public const double STEEL_E = 216.0;
        public const double STEEL_V = 0.28;
    }





    public class XrdConfig
    {
        public string IniFileName;
        public static string LogFileName;

        public string SectionLogin = "Login";
        public string SectionComm = "Communication";
        public string SectionDevInit = "DeviceInit";
        public string SectionSDDSetting = "MeasureSettings";
        public string SectionTextureSetting = "TextureSettings";

        // login
        public string UserName = "administrator";
        public string UserPwd = "123456";
        public int Language = 0;

        // uart config
        public string UartPortName = "COM1";
        public string UartBaudrate = "2400";
        public int UartSendRetryMax = 3;


        // socket config
        public string IPAddress = "192.168.1.2";

        //
        public int TargetType = 0;         // 0=Cr 1=Cu 2=Mn
        public int WarmUpWaitSec = 6;   // seconds
        public double WarmUpVoltage = 10.0;
        public double WarmUpCurrent = 5.0;
        public string WarmUpLastTime = " ";
        public int WarmUpIntervalMinute = 0; // 48 * 60
        public int DetectorType = 0;      // 0=SDD 1=LDA
        public int SDDMotorMode = 3;
        public double DefaultVotage = 10.0;
        public double DefaultCurrent = 5.0;
        public string SavePath = " ";
        public string OpenDataFileName = " ";

        public string[] SampleName = { "SDD_sample_name", "LDA_sample_name" };
        public string[] SampleSn = { "01234", "56789" };
        public int MeasureMethod = 0;      // 0=SDD侧倾法，1=SDD同倾法，2=LDA侧倾法，3=LDA同倾法
        public double[] StartDegree = { 150.0, 150.0, 150.0, 150.0 };
        public double[] StopDegree = { 150.0, 150.0, 150.0, 150.0 };
        public double[] MeasureStep = { 0.02, 0.02, 0.02, 0.02 };
        public double[] MeasureTime = { 0.05, 0.05, 0.05, 0.05 };
        public double[] TubeVoltage = { 25.0, 25.0, 25.0, 25.0 };
        public double[] TubeCurrent = { 30.0, 30.0, 30.0, 30.0 };

        public int[] FiveAxisInUse = { 0, 0, 0, 0 };

        public double[] AnglePsi_Method0 = { 0, 15, 30, 45 };
        public double[] AngleX_Method0 = { 0, 15, 20, 25 };
        public double[] AngleY_Method0 = { 0, 15, 20, 25 };
        public double[] AngleZ_Method0 = { 0, 15, 30, 45 };
        public double[] AngleAlpha_Method0 = { 0, 15, 30, 45 };
        public double[] AngleBeta_Method0 = { 0, 15, 30, 45 };

        public double[] AnglePsi_Method1 = { 0, 15, 30, 45 };
        public double[] AngleX_Method1 = { 0, 15, 20, 25 };
        public double[] AngleY_Method1 = { 0, 15, 20, 25 };
        public double[] AngleZ_Method1 = { 0, 15, 30, 45 };
        public double[] AngleAlpha_Method1 = { 0, 15, 30, 45 };
        public double[] AngleBeta_Method1 = { 0, 15, 30, 45 };

        public double[] AnglePsi_Method2 = { 0, 15, 30, 45 };
        public double[] AngleX_Method2 = { 0, 15, 20, 25 };
        public double[] AngleY_Method2 = { 0, 15, 20, 25 };
        public double[] AngleZ_Method2 = { 0, 15, 30, 45 };
        public double[] AngleAlpha_Method2 = { 0, 15, 30, 45 };
        public double[] AngleBeta_Method2 = { 0, 15, 30, 45 };

        public double[] AnglePsi_Method3 = { 0, 15, 30, 45 };
        public double[] AngleX_Method3 = { 0, 15, 20, 25 };
        public double[] AngleY_Method3 = { 0, 15, 20, 25 };
        public double[] AngleZ_Method3 = { 0, 15, 30, 45 };
        public double[] AngleAlpha_Method3 = { 0, 15, 30, 45 };
        public double[] AngleBeta_Method3 = { 0, 15, 30, 45 };

        public double[] ZeroPointMotorTS = { 0.0, 0.0 };    // 0=SDD 1=LDA
        public double[] ZeroPointMotorTD = { 0.0, 0.0 };    
        public double[] ZeroPointAlpha = { 0.0, 0.0 };
        public double[] ZeroPointBeta = { 0.0, 0.0 };
        public double[] ZeroPointX = { 0.0, 0.0 };
        public double[] ZeroPointY = { 0.0, 0.0 };
        public double[] ZeroPointZ = { 0.0, 0.0 };
        public double[] AutoFocus = {25.0, 116.0, 0.0, 48.0}; // STT,STH,STA,STZ
        public int SaveResultAsTxt = 0;


        public string[] SampleNameTxt = { "sample_name1", "sample_name2" };
        public string[] SampleSnTxt = { "01234", "01235", "56789" };
        public double[] FaceExpTxt = { 110, 200, 210 };
        public double[] PeakDegreeTxt = { 28.4, 28.6, 56.1 };
        public double[] TubeVoltageTxt = { 25, 26, 30 };
        public double[] TubeCurrentTxt = { 25, 26, 30 };
        public int[] BMIndexTxt = { 0, 0, 1 };  //0=B，1=M
        public int ScanMethodTxt = 0;
        public double[] PsiStepTxt = { 5, 5, 1 };
        public double[] PsiStartAngleTxt = { 0, 0, 0 };
        public double[] PsiStopAngleTxt = { 70, 70, 70 };
        public double[] PhiSpeedTxt = { 5, 5, 10 };
        public double[] MeasureTimeTxt = { 1, 0.5, 1 };
        public int MeasureMethodTxt;    //0=normal，1=expert

        public XrdConfig()
        {
            // 获取应用程序的当前工作目录。
            string CurrentPath = System.IO.Directory.GetCurrentDirectory();
            if (!Directory.Exists(CurrentPath))
            {
                // 创建目录
            }

            IniFileName = CurrentPath + "\\Resources\\XRD_Tool.ini";
            //PyScriptFileName = CurrentPath + "\\CurveFittingPearson_20180505.py";
            //PyResultFileName = CurrentPath + "\\CalcResult.py";

            LogFileName = CurrentPath + "\\Resources\\" + DateTime.Now.ToString("yyyyMMdd") + "-log.txt";
        }

        public void ReadIniFile()
        {
            try
            {
                if (!File.Exists(IniFileName))
                {
                    // 创建文件
                    SaveLogin();

                    SaveCommunication();

                    SaveDeviceInit();

                    SaveMeasureSettings();
                }
                else
                {
                    ReadLogin();

                    ReadCommunication();

                    ReadDeviceInit();

                    ReadMeasureSettings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        public void SaveLogin()
        {
            try
            {
                IniHelper.INIWriteValue(IniFileName, SectionLogin, "User", UserName);
                IniHelper.INIWriteValue(IniFileName, SectionLogin, "Pwd", UserPwd);
                IniHelper.INIWriteValue(IniFileName, SectionLogin, "Language", Language.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ReadLogin()
        {
            try
            {
                if (File.Exists(IniFileName))
                {
                    UserName = IniHelper.INIGetStringValue(IniFileName, SectionLogin, "User", "administrator");
                    UserPwd = IniHelper.INIGetStringValue(IniFileName, SectionLogin, "Pwd", "123456");
                    Language = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionLogin, "Language", "0"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SaveCommunication()
        {
            try
            {
                IniHelper.INIWriteValue(IniFileName, SectionComm, "UartPort", UartPortName);
                IniHelper.INIWriteValue(IniFileName, SectionComm, "UartBaudrate", UartBaudrate);
                IniHelper.INIWriteValue(IniFileName, SectionComm, "UartSendRetryMax", UartSendRetryMax.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionComm, "IPAddress", IPAddress);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ReadCommunication()
        {
            try
            {
                if (File.Exists(IniFileName))
                {
                    UartPortName = IniHelper.INIGetStringValue(IniFileName, SectionComm, "UartPort", "COM1");
                    UartBaudrate = IniHelper.INIGetStringValue(IniFileName, SectionComm, "UartBaudrate", "2400");
                    UartSendRetryMax = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionComm, "UartSendRetryMax", "3"));
                    IPAddress = IniHelper.INIGetStringValue(IniFileName, SectionComm, "IPAddress", "192.168.1.2");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SaveDeviceInit()
        {
            try
            {
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "TargetType", TargetType.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "WarmUpWaitSec", WarmUpWaitSec.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "WarmUpVoltage", WarmUpVoltage.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "WarmUpCurrent", WarmUpCurrent.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "WarmUpLastTime", WarmUpLastTime);
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "WarmUpIntervalMinute", WarmUpIntervalMinute.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "DetectorType", DetectorType.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "SDDMotorMode", SDDMotorMode.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "DefaultVotage", DefaultVotage.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "DefaultCurrent", DefaultCurrent.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "SavePath", SavePath);
                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "OpenDataFileName", OpenDataFileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        public void ReadDeviceInit()
        {
            try
            {
                if (File.Exists(IniFileName))
                {
                    TargetType = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "TargetType", "0"));
                    WarmUpWaitSec = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "WarmUpWaitSec", "360"));
                    WarmUpVoltage = Convert.ToDouble(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "WarmUpVoltage", "10.0"));
                    WarmUpCurrent = Convert.ToDouble(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "WarmUpCurrent", "5.0"));
                    WarmUpLastTime = IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "WarmUpLastTime", " ");
                    WarmUpIntervalMinute = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "WarmUpIntervalMinute", "28800"));
                    DetectorType = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "DetectorType", "0"));
                    SDDMotorMode = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "SDDMotorMode", "3"));
                    DefaultVotage = Convert.ToDouble(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "DefaultVotage", "10"));
                    DefaultCurrent = Convert.ToDouble(IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "DefaultCurrent", "5"));
                    SavePath = IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "SavePath", " ");
                    OpenDataFileName = IniHelper.INIGetStringValue(IniFileName, SectionDevInit, "OpenDataFileName", " ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SaveMeasureSettings()
        {
            try
            {
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "SampleName", string.Join(",", SampleName));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "SampleSn", string.Join(",", SampleSn));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "MeasureMethod", MeasureMethod.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "StartDegree", string.Join(",", StartDegree));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "StopDegree", string.Join(",", StopDegree));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "MeasureStep", string.Join(",", MeasureStep));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "MeasureTime", string.Join(",", MeasureTime));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "TubeVoltage", string.Join(",", TubeVoltage));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "TubeCurrent", string.Join(",", TubeCurrent));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "FiveAxis", string.Join(",", FiveAxisInUse));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AnglePsi0", string.Join(",", AnglePsi_Method0));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleX0", string.Join(",", AngleX_Method0));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleY0", string.Join(",", AngleY_Method0));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleZ0", string.Join(",", AngleZ_Method0));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleAlpha0", string.Join(",", AngleAlpha_Method0));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleBeta0", string.Join(",", AngleBeta_Method0));

                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AnglePsi1", string.Join(",", AnglePsi_Method1));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleX1", string.Join(",", AngleX_Method1));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleY1", string.Join(",", AngleY_Method1));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleZ1", string.Join(",", AngleZ_Method1));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleAlpha1", string.Join(",", AngleAlpha_Method1));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleBeta1", string.Join(",", AngleBeta_Method1));

                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AnglePsi2", string.Join(",", AnglePsi_Method2));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleX2", string.Join(",", AngleX_Method2));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleY2", string.Join(",", AngleY_Method2));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleZ2", string.Join(",", AngleZ_Method2));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleAlpha2", string.Join(",", AngleAlpha_Method2));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleBeta2", string.Join(",", AngleBeta_Method2));

                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AnglePsi3", string.Join(",", AnglePsi_Method3));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleX3", string.Join(",", AngleX_Method3));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleY3", string.Join(",", AngleY_Method3));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleZ3", string.Join(",", AngleZ_Method3));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleAlpha3", string.Join(",", AngleAlpha_Method3));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AngleBeta3", string.Join(",", AngleBeta_Method3));

                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "ZeroPointMotorTS", string.Join(",", ZeroPointMotorTS));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "ZeroPointMotorTD", string.Join(",", ZeroPointMotorTD));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "ZeroPointAlpha", string.Join(",", ZeroPointAlpha));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "ZeroPointBeta", string.Join(",", ZeroPointBeta));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "ZeroPointX", string.Join(",", ZeroPointX));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "ZeroPointY", string.Join(",", ZeroPointY));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "ZeroPointZ", string.Join(",", ZeroPointZ));
                IniHelper.INIWriteValue(IniFileName, SectionSDDSetting, "AutoFocus", string.Join(",", AutoFocus));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }   
        }

        public void ReadMeasureSettings()
        {
            try
            {
                if (File.Exists(IniFileName))
                {
                    SampleName = IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "SampleName", "SDD_sample_name,LDA_sample_name").Split(',');
                    SampleSn = IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "SampleSn", "01234,56789").Split(',');
                    MeasureMethod = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "MeasureMethod", "0"));
                    StartDegree = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "StartDegree", "150,150,150,150").Split(','), double.Parse);
                    StopDegree = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "StopDegree", "150, 150, 150, 150").Split(','), double.Parse);
                    MeasureStep = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "MeasureStep", "0.05,0.05,0.05,0.05").Split(','), double.Parse);
                    MeasureTime = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "MeasureTime", "15,15,15,15").Split(','), double.Parse);
                    TubeVoltage = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "TubeVoltage", "15,15,15,15").Split(','), double.Parse);
                    TubeCurrent = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "TubeCurrent", "15,15,15,15").Split(','), double.Parse);
                    FiveAxisInUse = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "FiveAxis", "0,0,0,0").Split(','), int.Parse);

                    AnglePsi_Method0 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AnglePsi0", "0,15,30,45").Split(','), double.Parse);
                    AngleX_Method0 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleX0", "0,15,30,45").Split(','), double.Parse);
                    AngleY_Method0 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleY0", "0,15,30,45").Split(','), double.Parse);
                    AngleZ_Method0 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleZ0", "0,15,30,45").Split(','), double.Parse);
                    AngleAlpha_Method0 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleAlpha0", "0,15,30,45").Split(','), double.Parse);
                    AngleBeta_Method0 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleBeta0", "0,15,30,45").Split(','), double.Parse);

                    AnglePsi_Method1 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AnglePsi1", "0,15,30,45").Split(','), double.Parse);
                    AngleX_Method1 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleX1", "0,15,30,45").Split(','), double.Parse);
                    AngleY_Method1 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleY1", "0,15,30,45").Split(','), double.Parse);
                    AngleZ_Method1 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleZ1", "0,15,30,45").Split(','), double.Parse);
                    AngleAlpha_Method1 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleAlpha1", "0,15,30,45").Split(','), double.Parse);
                    AngleBeta_Method1 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleBeta1", "0,15,30,45").Split(','), double.Parse);

                    AnglePsi_Method2 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AnglePsi2", "0,15,30,45").Split(','), double.Parse);
                    AngleX_Method2 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleX2", "0,15,30,45").Split(','), double.Parse);
                    AngleY_Method2 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleY2", "0,15,30,45").Split(','), double.Parse);
                    AngleZ_Method2 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleZ2", "0,15,30,45").Split(','), double.Parse);
                    AngleAlpha_Method2 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleAlpha2", "0,15,30,45").Split(','), double.Parse);
                    AngleBeta_Method2 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleBeta2", "0,15,30,45").Split(','), double.Parse);

                    AnglePsi_Method3 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AnglePsi3", "0,15,30,45").Split(','), double.Parse);
                    AngleX_Method3 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleX3", "0,15,30,45").Split(','), double.Parse);
                    AngleY_Method3 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleY3", "0,15,30,45").Split(','), double.Parse);
                    AngleZ_Method3 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleZ3", "0,15,30,45").Split(','), double.Parse);
                    AngleAlpha_Method3 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleAlpha3", "0,15,30,45").Split(','), double.Parse);
                    AngleBeta_Method3 = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AngleBeta3", "0,15,30,45").Split(','), double.Parse);

                    ZeroPointMotorTS = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "ZeroPointMotorTS", "0,0").Split(','), double.Parse);
                    ZeroPointMotorTD = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "ZeroPointMotorTD", "0,0").Split(','), double.Parse);
                    ZeroPointAlpha = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "ZeroPointAlpha", "0,0").Split(','), double.Parse);
                    ZeroPointBeta = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "ZeroPointBeta", "0,0").Split(','), double.Parse);
                    ZeroPointX = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "ZeroPointX", "0,0").Split(','), double.Parse);
                    ZeroPointY = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "ZeroPointY", "0,0").Split(','), double.Parse);
                    ZeroPointZ = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "ZeroPointZ", "0,0").Split(','), double.Parse);
                    AutoFocus = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionSDDSetting, "AutoFocus", "0,0").Split(','), double.Parse);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            } 
        }

        public void SaveWarmUpTime()
        {
            try
            {
                WarmUpLastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                IniHelper.INIWriteValue(IniFileName, SectionDevInit, "WarmUpLastTime", WarmUpLastTime);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SaveTextureSettings()
        {
            try
            {
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "SampleNameTxt", string.Join(",", SampleNameTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "SampleSnTxt", string.Join(",", SampleSnTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "FaceExpTxt", string.Join(",", FaceExpTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "PeakDegreeTxt", string.Join(",", PeakDegreeTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "TubeVoltageTxt", string.Join(",", TubeVoltageTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "TubeCurrentTxt", string.Join(",", TubeCurrentTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "BMIndexTxt", string.Join(",", BMIndexTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "ScanMethodTxt", ScanMethodTxt.ToString());
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "PsiStepTxt", string.Join(",", PsiStepTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "PsiStartAngleTxt", string.Join(",", PsiStartAngleTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "PsiStopAngleTxt", string.Join(",", PsiStopAngleTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "PhiSpeedTxt", string.Join(",", PhiSpeedTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "MeasureTimeTxt", string.Join(",", MeasureTimeTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "FaceExpTxt", string.Join(",", FaceExpTxt));
                IniHelper.INIWriteValue(IniFileName, SectionTextureSetting, "MeasureMethodTxt", MeasureMethodTxt.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void ReadTextureSettings()
        {
            try
            {
                if (File.Exists(IniFileName))
                {
                    SampleNameTxt = IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "SampleNameTxt", "sample_name1,sample_name2").Split(',');
                    SampleSnTxt = IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "SampleSnTxt", "01234,01235,56789").Split(',');
                    FaceExpTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "FaceExpTxt", "110,200,210").Split(','), double.Parse);
                    PeakDegreeTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "PeakDegreeTxt", "28.4,28.6,56.1").Split(','), double.Parse);
                    TubeVoltageTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "TubeVoltageTxt", "25,26,30").Split(','), double.Parse);
                    TubeCurrentTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "TubeCurrentTxt", "25,26,30").Split(','), double.Parse);
                    BMIndexTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "BMIndexTxt", "0,0,1").Split(','), int.Parse);
                    ScanMethodTxt = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "ScanMethodTxt", "0"));
                    PsiStepTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "PsiStepTxt", "5,5,1").Split(','), double.Parse);
                    PsiStartAngleTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "PsiStartAngleTxt", "0,0,0").Split(','), double.Parse);
                    PsiStopAngleTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "PsiStopAngleTxt", "70,70,70").Split(','), double.Parse);
                    PhiSpeedTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "PhiSpeedTxt", "5,5,10").Split(','), double.Parse);
                    MeasureTimeTxt = Array.ConvertAll(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "MeasureTimeTxt", "1,0.5,1").Split(','), double.Parse);
                    MeasureMethodTxt = Convert.ToInt32(IniHelper.INIGetStringValue(IniFileName, SectionTextureSetting, "MeasureMethodTxt", "0"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }



}
