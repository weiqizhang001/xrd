using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace XRD_Tool
{

    public class IniEdit:IniHelper
    {
        public string IniFileName="";
        public string FilieName = "";
        IniHelper inihe = new IniHelper();


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
        /// 范围文件内的所有section节点
        /// </summary>
        /// <returns></returns>
        public string[] GetSections()
        {
            if (IniFileName != null && IniFileName != "")
            {
                return inihe.INIGetAllSectionNames(IniFileName);
            }
            else
            {
                return null;
            }
            
        }

        // <summary>  
        /// 在INI文件中，删除指定的节点。  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点</param>  
        /// <returns>操作是否成功</returns>  
        public bool DeleteSection(string section)
        {
            if (IniFileName != null && IniFileName != "")
            {
                return inihe.INIDeleteSection(IniFileName, section);
            }
            else
            {
                return false;
            }

            
        }

        /// <summary>  
        /// 在INI文件中，将指定的键值对写到指定的节点，如果已经存在则替换  
        /// </summary>  
        /// <param name="iniFile">INI文件</param>  
        /// <param name="section">节点，如果不存在此节点，则创建此节点</param>  
        /// <param name="items">键值对，多个用\0分隔,形如key1=value1\0key2=value2</param>  
        /// <returns></returns>  
        public bool INIWriteItems(string section, string items)
        {
            if (IniFileName != null && IniFileName != "")
            {
                return inihe.INIWriteItems(IniFileName, section, items);
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
