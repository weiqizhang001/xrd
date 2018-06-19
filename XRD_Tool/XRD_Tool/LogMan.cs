using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
namespace PCTestTools
{
    public class LogMan
    {
       static object obj = new object();
       static bool m_genTxtLog = true;
       //public static void LoadConfig(string projectId)
       //{
       //    bool result = true;
       //    string genTxtLog = ConfigHelper.GetAppConfig(projectId + "GEN_TXT_LOG");
       //    if (bool.TryParse(genTxtLog, out result))
       //    {
       //            m_genTxtLog =result;
       //    }
       //}
       public static string getDataBufferStr(List<byte> readBuffer)
       {
           lock (obj)
           {
               string ReceivedStr = "";
               if (readBuffer != null)
               {
                   StringBuilder recBuffer16 = new StringBuilder();//定义16进制接收缓存
                   int length = readBuffer.Count;
                   for (int i = 0; i < length; i++)
                   {
                       recBuffer16.AppendFormat("{0:X2}" + " ", readBuffer[i]);//X2表示十六进制格式（大写），域宽2位，不足的左边填0。
                   }
                   ReceivedStr = recBuffer16.ToString();
               }
               return ReceivedStr;
           }
       }
       public static string getDataBufferStr(byte[] readBuffer)
       {
           lock (obj)
           {
               string ReceivedStr = "";
               if (readBuffer != null)
               {
                   StringBuilder recBuffer16 = new StringBuilder();//定义16进制接收缓存
                   int length = readBuffer.Length;
                   for (int i = 0; i < length; i++)
                   {
                       recBuffer16.AppendFormat("{0:X2}" + " ", readBuffer[i]);//X2表示十六进制格式（大写），域宽2位，不足的左边填0。
                   }
                   ReceivedStr = recBuffer16.ToString();
               }
               return ReceivedStr;
           }
       }
       public static void DebugErrorLog(byte[] readBuffer, string hint, string filePath)
       {
          
           lock (obj)
           {
               if (!m_genTxtLog) return;
               Stream myStream;
               StreamWriter sw;
               string ReceivedStr = "";
               string logStr = "写入" + hint + " 日志时发生异常";
               try
               {
                   string strPath = Directory.GetCurrentDirectory() + "\\Log";

                   if (Directory.Exists(strPath) == false)
                   {
                       Directory.CreateDirectory(strPath);
                   }

                   string fileName = "\\" + filePath + ".log";
                   myStream = new FileStream(strPath + fileName, FileMode.OpenOrCreate | FileMode.Append);
                   sw = new StreamWriter(myStream, Encoding.GetEncoding("gb2312"));

                   if (readBuffer != null)
                   {
                       StringBuilder recBuffer16 = new StringBuilder();//定义16进制接收缓存
                     
                       for (int i = 0; i < readBuffer.Length; i++)
                       {
                           recBuffer16.AppendFormat("{0:X2}" + " ", readBuffer[i]);//X2表示十六进制格式（大写），域宽2位，不足的左边填0。
                       }
                       ReceivedStr = recBuffer16.ToString();

                       recBuffer16 = null;
                   }

                   logStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "[" + hint + "]:\r\n " + ReceivedStr + "\r\n";
                   sw.WriteLine(logStr);
                   sw.Close();
                   myStream.Close();

               }
               catch (Exception ex)
               {

                   Console.WriteLine(ex.Message);

               }
               finally
               {
                   Console.WriteLine(logStr);
               }
           }

       }
        public static void DebugLog(byte[] readBuffer, string hint,int length=0,int groupId=0)
        {
          
            lock (obj)
            {
                if (!m_genTxtLog)
                {
                    return;
                }
                Stream myStream;
                StreamWriter sw;
                string ReceivedStr = "";
                string logStr = "写入" + hint + " 日志时发生异常";
                try
                {
                    string strPath = Directory.GetCurrentDirectory() + "\\Log";

                    if (Directory.Exists(strPath) == false)
                    {
                        Directory.CreateDirectory(strPath);
                    }
           
                      string  fileName="\\CommunicationLog-"+groupId+".log";
                    myStream = new FileStream(strPath + fileName, FileMode.OpenOrCreate | FileMode.Append);
                    sw = new StreamWriter(myStream, Encoding.GetEncoding("gb2312"));

                    if (readBuffer != null)
                    {
                        StringBuilder recBuffer16 = new StringBuilder();//定义16进制接收缓存
                        if (length == 0) length = readBuffer.Length;
                        for (int i = 0; i < length; i++)
                        {
                            recBuffer16.AppendFormat("{0:X2}" + " ", readBuffer[i]);//X2表示十六进制格式（大写），域宽2位，不足的左边填0。
                        }
                        ReceivedStr = recBuffer16.ToString();
                        
                        recBuffer16 = null;
                    }

                    logStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss fff") + "[" + hint + "]:\r\n " + ReceivedStr + "\r\n";
                    sw.WriteLine(logStr);
                    sw.Close();
                    myStream.Close();
                  
                }
                catch (Exception ex)
                {

                    Console.WriteLine("DebugLog"+ex.Message);

                }
                finally
                {
                    Console.WriteLine(logStr);
                }
            }

        }

        public static void DebugDeviceReceiveLog(byte[] readBuffer, string hint, int length = 0,string deviceNameId="")
        {
         
            lock (obj)
            {
                if (!m_genTxtLog) return;
                Stream myStream;
                StreamWriter sw;
                string ReceivedStr = "";
                string logStr = "写入" + hint + " 日志时发生异常";
                try
                {
                    string strPath = Directory.GetCurrentDirectory() + "\\Log";

                    if (Directory.Exists(strPath) == false)
                    {
                        Directory.CreateDirectory(strPath);
                    }
                    //      string fileName = logFileName;
                    //      if(fileName.Equals(""))
                    string fileName = "\\CommunicationLog-" + deviceNameId + ".log";
                    myStream = new FileStream(strPath + fileName, FileMode.OpenOrCreate | FileMode.Append);
                    sw = new StreamWriter(myStream, Encoding.GetEncoding("gb2312"));

                    if (readBuffer != null)
                    {
                        StringBuilder recBuffer16 = new StringBuilder();//定义16进制接收缓存
                        if (length == 0) length = readBuffer.Length;
                        for (int i = 0; i < length; i++)
                        {
                            recBuffer16.AppendFormat("{0:X2}" + " ", readBuffer[i]);//X2表示十六进制格式（大写），域宽2位，不足的左边填0。
                        }
                        ReceivedStr = recBuffer16.ToString();
                    }

                    logStr = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss  fff") + "[" + hint + "]:\r\n " + ReceivedStr + "\r\n";
                    sw.WriteLine(logStr);
                    sw.Close();
                    myStream.Close();

                    

                }
                catch (Exception ex)
                {

                    Console.WriteLine("DebugDeviceReceiveLog" + ex.Message);

                }
                finally
                {
                    Console.WriteLine(logStr);
                }
            }

        }

    }
}
