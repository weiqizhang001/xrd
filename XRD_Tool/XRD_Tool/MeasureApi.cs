using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.IO;
using DllLibrary;
using System.Net;

namespace XRD_Tool
{
 
    public class MeasureApi
    {
        public SDDDetector SDDApi;
        public LDADetector LDAApi;

        public SerialPortCommon myUart;


        #region 测量参数
        public int SDDMotorMode;
        public double DefaultVotage;
        public double DefaultCurrent;

        public string SavePath;
        public string SampleName;
        public string SampleSn;
        public int TargetType;
        public int DetectorType;
        public int MeasureMethod;      // 0=SDD同倾法，1=SDD侧倾法，2=LDA同倾法，3=LDA侧倾法
        public double StartDegree;
        public double StopDegree;
        public double MeasureStep;
        public double MeasureTime;
        public double TubeVoltage;
        public double TubeCurrent;

        public double[] AnglePsi;
        public double[] AngleX;
        public double[] AngleY;
        public double[] AngleZ;
        public double[] AngleAlpha;
        public double[] AngleBeta;

        public double ZeroPointMotorTS;    // 0=SDD 1=LDA
        public double ZeroPointMotorTD;
        public double ZeroPointAlpha;
        public double ZeroPointBeta;
        public double ZeroPointX;
        public double ZeroPointY;
        public double ZeroPointZ;
        public double[] AutoFocus = { 25.0, 116.0, 0.0, 48.0 }; // STT,STH,STA,STZ
        public double recvVoltage = 0.0;
        public double recvCurrent = 0.0;
        public double sendVoltage = 0.0;
        public double sendCurrent = 0.0;
        #endregion


        #region 织构测量参数
        public string SampleNameTxt;
        public string[] SampleSnTxt;
        
        public double[] FaceExpTxt;
        public double[] PeakDegreeTxt;
        public double[] TubeVoltageTxt;
        public double[] TubeCurrentTxt;
        public int[] BMIndexTxt;
        public int ScanMethodTxt;
        public double[] PsiStepTxt;
        public double[] PsiStartAngleTxt;
        public double[] PsiStopAngleTxt;
        public double[] PhiSpeedTxt;
        public double[] MeasureTimeTxt;
        public int MeasureMethodTxt;
        #endregion

        public byte[] UartSendBuf;
        public byte[] TCPSendBuf;
        public int CurrentSendCmd = 0;
        public int SendRetryCount = 0;
        public int UartSendRetryMax = 3;
               
        public double[] DetectorZeroPoint = new double[7];
        public DataTable RecvDataTable = new DataTable("RecvData");
        public string RecvDataFileName = null;
        public string[] RecvDataFileNameArray;
        public FileStream RecvDataFileStream;
        public StreamWriter RecvDataStreamWriter;

        public int RecvDataCount = 0;
        public int TotalDataCount = 0;
        public byte[] SSC_RecvDataArray = new byte[5000 * 20];
        public int SSC_RecvDataWriteIndex = 0;
        public int SSC_RecvDataReadIndex = 0;
        public byte[] SSC_AnalyzeDataArray = new byte[1000 * 20];
        public int SSC_AnalyzeDataCount = 0;

        public string[] RecvDataColumnNames = { "Angle", "Intensity" };

        public TCPClient myTCP;
        public byte[] TCPRecvBytes;
        public int[] TCP_RecvIntensityArray = new int[640];
        
        // 构造函数
        public MeasureApi(FormDeviceInit form)
        {
            myUart = form.myUart;
            myTCP = form.myTCP;

            SDDApi = new SDDDetector();
            LDAApi = new LDADetector();

            RecvDataTableInit();  
        }

        public void UartSendCmd(byte[] cmd)
        {
            if (!myUart.port_UART.IsOpen)
            {
                MessageBox.Show("串口未打开");

                return;
            }

            try 
            {
                string strShow = System.Text.Encoding.Default.GetString(cmd);

                byte[] LOG = myUart.SendCmmdData(cmd);
                //timerUartRecv.Interval = 1000;
                //timerUartRecv.Enabled = true;
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        #region 设备初始化
        
        public void CheckDeviceReady()
        {
            CurrentSendCmd = DEVICE_CMD_ID.CHECK_DEV_READY;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.CheckDeviceReady();

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public bool RecvDeviceReady(byte[] data)
        {
            return SDDApi.RecvDeviceReady(data);
        }

        public void CheckHostAttachStatus()
        {
            CurrentSendCmd = DEVICE_CMD_ID.CHECK_HOST_ATTACH_STATUS;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.CheckHostAttachStatus();

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public bool RecvHostAttachStatus(byte[] data)
        {
            return SDDApi.RecvHostAttachStatus(data);
        }

        public void SendWarmUp(double voltage, double current)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_HIGH_VOLTAGE_UP;
            SendRetryCount = 0;

            sendVoltage = voltage;
            sendCurrent = current;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetHighVoltage(voltage, current);
                }
                else
                {
                    UartSendBuf = SDDApi.SetHighVoltage(voltage, current);
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }     
        }

        public void GetDeviceZeroPoint()
        {
            CurrentSendCmd = DEVICE_CMD_ID.GET_DEV_ZERO_POINT;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.GetDeviceZeroPoint();
                }
                else
                {
                    UartSendBuf = SDDApi.GetDeviceZeroPoint();
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }    
        }

        public bool RecvGetDeviceZeroPoint(byte[] data)
        {
            bool result = false;
            try 
            {
                result = SDDApi.RecvDeviceZeroPoint(data, ref DetectorZeroPoint);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + System.Text.Encoding.Default.GetString(data) + "[" + ex.ToString() + "]");
            }

            return result;
        }

        public void FiveAxis_GetZeroPoint()
        {
            CurrentSendCmd = DEVICE_CMD_ID.GET_5_AXIS_ZERO_POINT;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.FiveAxis_GetZeroPoint();
                }
                else
                {
                    UartSendBuf = SDDApi.FiveAxis_GetZeroPoint();
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }   
        }

        public bool RecvFiveAxis_GetZeroPoint(byte[] data)
        {
            return SDDApi.FiveAxis_RecvZeroPoint(data, ref DetectorZeroPoint);
        }

        public void SendSetSdp(int param)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_SDP;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.SetSDP(param);

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public bool Connect()
        {
            //CurrentSendCmd = DEVICE_CMD_ID.CONNECT;
            //SendRetryCount = 0;

            try
            {
                if (1 == DetectorType)
                {
                    if (myTCP == null)
                    {
                        return false;
                    }

                    if (!myTCP.connected)
                    {
                        myTCP.start();
                    }
                    else
                    {
                        
                    }

                    Reset();

                    return true;

                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return false;
        }

        public void Reset()
        {
            CurrentSendCmd = DEVICE_CMD_ID.RESET;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetDeviceReset();
                    UartSendCmd(UartSendBuf);
                }
                else
                {
                    TCPSendBuf = LDAApi.Reset();
                    TCPSend(TCPSendBuf);
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SetTarget(string name)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_TARGET;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    //UartSendBuf = SDDApi.SetDeviceReset();
                    //UartSendCmd(UartSendBuf);
                }
                else
                {
                    TCPSendBuf = LDAApi.LoadSettings(name);
                    TCPSend(TCPSendBuf);
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }
        #endregion


        #region 测量命令
        public void SendDevicePause()
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_DEV_PAUSE;
            SendRetryCount = 0;

            try 
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetDevicePause();
                }
                else
                {
                    UartSendBuf = SDDApi.SetDevicePause();
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendMotorMode()
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_MOTOR_MODE;
            SendRetryCount = 0;
            
            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetMoterMode(SDDMotorMode);
                }
                else
                {

                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendMeasureTime(double sec)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_MES_TIME;
            SendRetryCount = 0;
     
            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetPulseCountSecond(sec);
                    UartSendCmd(UartSendBuf);
                }
                else
                {
                    //int ms = 100;
                    int ms = (int)(sec * 1000);
                    TCPSendBuf = LDAApi.SetExposureTimeOfOneFrame((ms * 1000 * 1000) / 100 );
                    TCPSend(TCPSendBuf);
                }
                
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendMeasureStep()
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_SCAN_STEP_DISTANCE;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetScanStepDistance(MeasureStep);
                }
                else
                {
                    UartSendBuf = SDDApi.SetScanStepDistance(MeasureStep);
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendAnglePsi(double angle)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_PSI_ANGLE;
            SendRetryCount = 0;
 
            try
            {
                if ((0 == MeasureMethod) || (2 == MeasureMethod))
                {
                    UartSendBuf = SDDApi.FiveAxis_MoveMotorA(angle);
                }
                else if ((1 == MeasureMethod) || (3 == MeasureMethod))
                {
                    UartSendBuf = SDDApi.SetPsiAngle(angle);
                }
  
                else
                {

                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendAngleA(double angle)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_A_ANGLE;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.FiveAxis_MoveMotorA(angle);

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendAngleB(double angle)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_B_ANGLE;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.FiveAxis_MoveMotorB(angle);

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendSpeedB(double speed)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_B_SPEED;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.FiveAxis_SetSpeedMoterB(speed);

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendAngleZ(double angle)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_Z_ANGLE;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.FiveAxis_MoveMotorZ(angle);

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendAngleABXYZ(int xzzIndex, int PsiIndex)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_ABXYZ_ANGLE;
            SendRetryCount = 0;

            double[] angle = new double[5];

            angle[0] = AngleAlpha[xzzIndex];
            angle[1] = AngleBeta[xzzIndex];
            angle[2] = AngleX[xzzIndex];
            angle[3] = AngleY[xzzIndex];
            angle[4] = AngleZ[xzzIndex];

            try
            {
                if ((0 == MeasureMethod) || (2 == MeasureMethod))
                {
                    angle[0] = AnglePsi[PsiIndex];
                    UartSendBuf = SDDApi.FiveAxis_RotateMotorABXYZ(angle);
                }
                else if ((1== MeasureMethod) || (3 == MeasureMethod))
                {
                    UartSendBuf = SDDApi.FiveAxis_RotateMotorABXYZ(angle);
                }
                else
                {

                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendAngleABXYZ(double[] angle)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_ABXYZ_ANGLE;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.FiveAxis_RotateMotorABXYZ(angle);

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendGetAngleABXYZ()
        {
            CurrentSendCmd = DEVICE_CMD_ID.GET_ABXYZ_ANGLE;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.FiveAxis_GetMotorPosOfABXYZ();
                }
                else if (1 == DetectorType)
                {
                    UartSendBuf = SDDApi.FiveAxis_GetMotorPosOfABXYZ();
                }
                else
                {

                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public bool RecvGetAngleABXYZ(byte[] data, ref double[] angle)
        {
            return SDDApi.FiveAxis_RecvMotorPosOfABXYZ(data, ref angle);
        }
        
        public void SendStartMeaurePostion()
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_START_MEA_POS;
            SendRetryCount = 0;

            try
            {
                if ((0 == MeasureMethod) || (2 == MeasureMethod))
                {
                    UartSendBuf = SDDApi.MoveMoterTSAndTD(StartDegree);
                }
                else if ((1 == MeasureMethod) || (3 == MeasureMethod))
                {
                    UartSendBuf = SDDApi.SetStartMeasurePos(StartDegree);
                }
                else
                {

                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendMovMotorTS(double degree)
        {
            CurrentSendCmd = DEVICE_CMD_ID.MOVE_TS;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.MoveMoterTS(degree);
                }
                else
                {
                    //UartSendBuf = SDDApi.SetStartMeasurePos(StartDegree - StopDegree / 2);
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendMovMotorTD(double degree)
        {
            CurrentSendCmd = DEVICE_CMD_ID.MOVE_TD;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.MoveMoterTD(degree);
                }
                else
                {
                    //UartSendBuf = SDDApi.SetStartMeasurePos(StartDegree - StopDegree / 2);
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendHighVoltageUp(double voltage, double current)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_HIGH_VOLTAGE_UP;
            SendRetryCount = 0;

            sendVoltage = voltage;
            sendCurrent = current;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetHighVoltage(voltage, current);
                }
                else
                {
                    UartSendBuf = SDDApi.SetHighVoltage(voltage, current);
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendHighVoltageDown(double voltage, double current)
        {
            CurrentSendCmd = DEVICE_CMD_ID.SET_HIGH_VOLTAGE_DOWN;
            SendRetryCount = 0;
            sendVoltage = voltage;
            sendCurrent = current;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetHighVoltage(voltage, current);
                }
                else
                {
                    UartSendBuf = SDDApi.SetHighVoltage(voltage, current);
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendGetHighVoltage()
        {
            CurrentSendCmd = DEVICE_CMD_ID.GET_HIGH_VOLTAGE;
            SendRetryCount = 0;
  
            try
            {
                UartSendBuf = SDDApi.GetHighVoltage();

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public bool RecvGetHighVoltage(byte[] data)
        {
            return SDDApi.RecvHighVoltage(data, out recvVoltage, out recvCurrent);
        }

        public void SendOpenShutter()
        {
            CurrentSendCmd = DEVICE_CMD_ID.OPEN_LIGHT_SHUTTER;
            SendRetryCount = 0;
            
            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.OpenLightShutter();
                }
                else
                {
                    UartSendBuf = SDDApi.OpenLightShutter();
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendCloseShutter()
        {
            CurrentSendCmd = DEVICE_CMD_ID.CLOSE_LIGHT_SHUTTER;
            SendRetryCount = 0;

            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.CloseLightShutter();
                }
                else
                {
                    UartSendBuf = SDDApi.CloseLightShutter();
                }

                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendStartScan()
        {
            CurrentSendCmd = DEVICE_CMD_ID.START_SCAN;
            SendRetryCount = 0;
 
            try
            {
                if (0 == DetectorType)
                {
                    UartSendBuf = SDDApi.SetStepScanMode(StopDegree - StartDegree);
                    UartSendCmd(UartSendBuf);
                }
                else
                {
                    TCPSendBuf = LDAApi.Start();
                    TCPSend(TCPSendBuf);
                }     
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void SendStartCycleScan(double degree)
        {
            CurrentSendCmd = DEVICE_CMD_ID.START_SCAN;
            SendRetryCount = 0;

            try
            {
                UartSendBuf = SDDApi.SetCycleScanMode(degree);
                UartSendCmd(UartSendBuf);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void ReadFrames(int number)
        {
            CurrentSendCmd = DEVICE_CMD_ID.READ_FRAME;
            SendRetryCount = 0;

            try
            {
                if (1 == DetectorType)
                {
                    TCPSendBuf = LDAApi.ReadOut(number);
                    TCPSend(TCPSendBuf);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void Status()
        {
            CurrentSendCmd = DEVICE_CMD_ID.STATUS;
            SendRetryCount = 0;

            try
            {
                if (1 == DetectorType)
                {
                    TCPSendBuf = LDAApi.GetStatus();
                    TCPSend(TCPSendBuf);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }


        public bool SSC_RecvDataAnalyze(byte[] data)
        {
            bool result = false;
            bool head = false;
            int head_pos = 0;

            try
            {
                System.Array.Copy(data, 0, SSC_RecvDataArray, SSC_RecvDataWriteIndex, data.Length);
                SSC_RecvDataWriteIndex += data.Length;
                if (SSC_RecvDataWriteIndex > SSC_RecvDataArray.Length)
                {
                    SSC_RecvDataWriteIndex = 0;
                }

                for (int i = SSC_RecvDataReadIndex; i < SSC_RecvDataWriteIndex; i++)
                {
                    if (SSC_RecvDataArray[i] == '\n')   // 0x0A
                    {
                        head = true;
                        head_pos = i;
                    }
                    else if (SSC_RecvDataArray[i] == '\r')  // 0x0D
                    {
                        if ((head) && (i - head_pos == 19))
                        {
                            System.Array.Copy(SSC_RecvDataArray, head_pos, SSC_AnalyzeDataArray, SSC_AnalyzeDataCount, 20);
                            SSC_AnalyzeDataCount += 20;
                            if (SSC_AnalyzeDataCount > SSC_AnalyzeDataArray.Length)
                            {
                                SSC_AnalyzeDataCount = 0;
                            }

                            head = false;
                            head_pos = 0;
                            SSC_RecvDataReadIndex = i;
                        }
                    }
                    else
                    {

                    }   
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
            
            return result;
        }

        public bool TCP_RecvDataAnalyze(byte[] data)
        {
            bool result = false;

            try
            {
                System.Array.Copy(data, 0, SSC_RecvDataArray, SSC_RecvDataWriteIndex, data.Length);
                SSC_RecvDataWriteIndex += data.Length;
                if (SSC_RecvDataWriteIndex >= (4 * 640))
                {
                    //string recvStr = Encoding.ASCII.GetString(SSC_RecvDataArray, 0, SSC_RecvDataWriteIndex);
                    //byte[] byteArray = SerialPortCommon.HexStrTobyte(recvStr);
                    
                    int j = 0;
                    int[] intensityArray = new int[640];

                    for (int i = 0; i < (4 * 640); )
                    {
                        //int x = BitConverter.ToInt32(byteArray, i);
                        int x = BitConverter.ToInt32(SSC_RecvDataArray, i);
                        //TCP_RecvIntensityArray[j] = IPAddress.NetworkToHostOrder(x);
                        //TCP_RecvIntensityArray[j] = x;
                        intensityArray[j] = x;
                        i += 4;
                        j++;
                    }

                    //System.Array.Copy(intensityArray, 20, TCP_RecvIntensityArray, 0, 600);
                    System.Array.Copy(intensityArray, 0, TCP_RecvIntensityArray, 0, 640);

                    result = true;
                }
            }
            catch (Exception ex)
            {
                result = false;
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return result;
        }

        public bool TCP_RecvDataSplit(double[] x_data, int[] y_data, int count, out double[] split_x_data, out double[] split_y_data)
        {
            bool result = false;
            split_x_data = new double[x_data.Length * count];
            split_y_data = new double[x_data.Length * count];
            int split_count = 0;

            try
            {
                for (int i = 0; i < x_data.Length; i++)
                {
                    double k, b; 

                    if (i < (x_data.Length - 1))
                    {
                        k = (y_data[i + 1] - y_data[i]) / (x_data[i + 1] - x_data[i]);
                    }
                    else
                    {
                        k = (y_data[i] - y_data[i-1]) / (x_data[i] - x_data[i-1]);
                    }
                    
                    b = y_data[i] - (k * x_data[i]);

                    for (int j = 0; j < count; j++)
                    {
                        split_x_data[split_count] = x_data[i] + ((0.04 / count) * j);
                        split_y_data[split_count] = split_x_data[split_count] * k + b;
                        split_count++;
                    }
                }

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return result;
        }

        public bool TCP_RecvDataSmooth(double[] data, out double[] out_data)
        {
            bool result = false;
            out_data = new double[data.Length];

            try
            {
                double sum = 0;
                int count = 5;

                for (int i = 0; i < data.Length; i++)
                {
                    if (i < data.Length - 4)
                    {
                        sum = data[i] + data[i + 1] + data[i + 2] + data[i + 3] + data[i + 4];
                        count = 5;
                    }
                    else if (i < data.Length - 3)
                    {
                        sum = data[i] + data[i + 1] + data[i + 2] + data[i + 3];
                        count = 4;
                    }
                    else if (i < data.Length - 2)
                    {
                        sum = data[i] + data[i + 1] + data[i + 2];
                        count = 3;
                    }
                    else if (i < data.Length - 1)
                    {
                        sum = data[i] + data[i + 1];
                        count = 2;
                    }
                    else if (i < data.Length)
                    {
                        sum = data[i];
                        count = 1;
                    }
                    else
                    {

                    }

                    out_data[i] = sum / count;
                }

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return result;
        }

        public bool TCP_RecvDataJoin(double[] data, int count, out double[] out_data)
        {
            bool result = false;
            out_data = new double[data.Length / count];

            try
            {
                int out_count = 0;
                for (int i = 0; i < data.Length; )
                {
                    out_data[out_count] = 0;
                    for (int j = 0; j < count; j++)
                    {
                        out_data[out_count] += data[i + j];
                    }
                    i += count;
                    out_count++;
                }

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return result;
        }

        public void RecvDataTableInit()
        {
            try
            {
                DataColumnCollection columns = RecvDataTable.Columns;
                DataColumn column = null;

                column = columns.Add(RecvDataColumnNames[0], typeof(double));
                column = columns.Add(RecvDataColumnNames[1], typeof(int));
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public bool RecvDataTableUpdate(byte[] data, int datalen)
        {
            bool result = true;

            try
            {
                byte[] newData = new byte[datalen];
                Array.Copy(data, 0, newData, 0, datalen);
                string str = System.Text.Encoding.Default.GetString(newData);

                str = str.Replace("\r", " ");
                str = str.Replace("\n", " ");
                str = str.Replace("C=", " ");

                string[] str_array = str.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < str_array.Length-1; )
                {
                    double angle = Convert.ToDouble(str_array[i]);
                    int intensity = Convert.ToInt32(str_array[i + 1]);

                    DataRow row = RecvDataTable.NewRow();

                    row[0] = angle;
                    row[1] = intensity;

                    RecvDataTable.Rows.Add(row);

                    i = i + 2;
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
            
            return result;
        }

        public bool RecvDataTableUpdate(double[] x, int[] y)
        {
            bool result = true;

            try
            {
                for (int i = 0; i < x.Length - 1; )
                {
                    DataRow row = RecvDataTable.NewRow();

                    row[0] = x[i];
                    row[1] = y[i];

                    RecvDataTable.Rows.Add(row);

                    i = i + 2;
                }
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return result;
        }

        public void RecvDataTableSaveFileStart(double PsiAngle, int index)
        {
            try
            {
                RecvDataFileName = SavePath;
                RecvDataFileName += "\\" + SampleName;
                RecvDataFileName += "_" + SampleSn;
                //RecvDataFileName += "_ABXYZ-" + AngleBeta[MeasureIndex_XYZ];
                RecvDataFileName += "_Psi-" + PsiAngle.ToString();
                RecvDataFileName += "_" + DateTime.Now.ToString("yyyyMMdd-HHmmss");
                RecvDataFileName += ".txt";

                if (SerialPortCommon.FileIsUsed(RecvDataFileName))
                {
                    RecvDataFileName = RecvDataFileName.Replace(".txt", "_2.txt");
                    myUart.Pack_Debug_out(null, "FileNameError" + "[" + RecvDataFileName + "]");
                }
                RecvDataFileStream = new FileStream(RecvDataFileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                RecvDataStreamWriter = new StreamWriter(RecvDataFileStream, System.Text.Encoding.ASCII);


                if (index == 0)
                {
                    RecvDataFileNameArray = new string[AnglePsi.Length];
                }
                RecvDataFileNameArray[index] = RecvDataFileName;

            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void RecvDataTableSaveFileProc(byte[] data, int datalen)
        {
            try
            {
                byte[] newData = new byte[datalen];
                Array.Copy(data, 0, newData, 0, datalen);
                string strWrite = System.Text.Encoding.Default.GetString(newData);
                strWrite = strWrite.Replace("C=", "");

                //RecvDataStreamWriter.WriteLine(strWrite);
                RecvDataStreamWriter.Write(strWrite);
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void RecvDataTableSaveFileProc(double[] x, int[] y)
        {
            try
            {
                for (int i = 0; i < x.Length; i++)
                {
                    string xStr = x[i].ToString("0.0000");
                    string yStr = y[i].ToString();
                    
                    xStr = SerialPortCommon.RightAlign_Eight(xStr);
                    yStr = SerialPortCommon.RightAlign_Eight(yStr);

                    string strWrite = "\n" + xStr + yStr + "\r";

                    RecvDataStreamWriter.Write(strWrite);
                }
                
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public void RecvDataTableSaveFileEnd()
        {
            try
            {
                RecvDataStreamWriter.Close();
                RecvDataFileStream.Close();
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }
        #endregion

        #region TCP
        public void TCPSend(byte[] cmd)
        {
            try
            {
                if (myTCP != null && myTCP.connected)
                {
                    string s = System.Text.Encoding.Default.GetString(cmd);

                    myTCP.Send(s);

                    //TCPRecvBytes = myTCP.ReceiveDataFromSocketPort();
                } 
            }
            catch (Exception ex)
            {
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }
        }

        public bool RecvTCPCmdResultCheck(byte[] cmd, out int code)
        {
            bool result = false;

            try
            {
                code = BitConverter.ToInt32(cmd, 0);
                result = true;
            }
            catch (Exception ex)
            {
                code = 0;
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return result;
        }

        public bool RecvTCPCmdStatusCheck(byte[] cmd, out int code)
        {
            bool result = false;

            try
            {
                code = BitConverter.ToInt32(cmd, 0);
                result = true;
            }
            catch (Exception ex)
            {
                code = 0;
                myUart.Pack_Debug_out(null, "Exception" + "[" + ex.ToString() + "]");
            }

            return result;
        }


        #endregion



    }
}
