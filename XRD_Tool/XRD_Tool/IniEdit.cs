using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace XRD_Tool
{

    public class IniEdit
    {
        public string IniFileName="";
        public string FilieName = "";


        /// <summary>
        ///ini文件连接   true：连接成功 false：连接失败
        /// </summary>
        /// <param name="FileName">ini文件名称（包含后缀名）</param>
        /// <returns></returns>
        public bool IniEditConfig(string FileName)
        {
            // 获取应用程序的当前工作目录。
            string CurrentPath = System.IO.Directory.GetCurrentDirectory();
            if (!Directory.Exists(CurrentPath))//获取当前路径
            {
                return false;
            }

            IniFileName = CurrentPath + "\\Resources\\" + FileName;
            if (!File.Exists(IniFileName))//是否存在配置文件
            {
                IniFileName = "";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 保存value
        /// </summary>
        /// <param name="_section">节点名称</param>
        /// <param name="_key">key</param>
        /// <param name="_value">value</param>
        /// <returns>保存结果</returns>
        public bool SaveValueWithKey(string _section, string _key, string _value)
        {
            if (IniFileName != null && IniFileName != "")
            {
                return IniHelper.INIWriteValue(IniFileName, _section, _key, _value);
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// 读取value
        /// </summary>
        /// <param name="_section">节点名称</param>
        /// <param name="_key">key</param>
        /// <returns>返回值</returns>
        public string GetVlueWithKey(string _section, string _key)
        {
            if (IniFileName != null && IniFileName != "")
            {
                return IniHelper.INIGetStringValue(IniFileName, _section, _key, "");
            }
            else
            {
                return "Error";
            }

            
        }

    }



}
