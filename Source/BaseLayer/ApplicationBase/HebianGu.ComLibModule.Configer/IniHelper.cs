/* 示例：
//配置文件位置
string configpath = AppDomain.CurrentDomain.BaseDirectory + "config.ini";
//写入配置
WinAPI.ProfileWriteValue("Setting", "DefaultSerialPort", ssp.SL_PortName, configpath);
//读取配置
WinAPI.ProfileReadValue("Setting", "DefaultSerialPort", configpath);

初始化判断是否存在配置，否则创建文件↓
//判断是否存在配置文件
if (!File.Exists(configpath))
{
  FileStream fs = new FileStream(configpath, FileMode.OpenOrCreate);
}*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Configer
{
    /// <summary> 轻量级的配置文件类型ini </summary>
    public static class IniHelper
    {
        /// <summary> 写入配置文件的接口 </summary>
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        /// <summary> 读取配置文件的接口 </summary>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);


        /// <summary> 向配置文件写入值 </summary>
        public static void ProfileWriteValue(string section, string key, string value, string path)
        {
            WritePrivateProfileString(section, key, value, path);
        }
        /// <summary> 读取配置文件的值 </summary>
        public static string ProfileReadValue(string section, string key, string path)
        {
            StringBuilder sb = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", sb, 255, path);
            return sb.ToString().Trim();
        }


    }
}
