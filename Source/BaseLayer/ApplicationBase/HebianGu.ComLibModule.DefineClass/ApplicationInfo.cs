using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Define
{
    /// <summary> 应用程序集操作类 </summary>
    public static class ApplicationInfo
    {
        static FileInfo _applicationFile;
        static Assembly _appAss;

        static ApplicationInfo()
        {
            _appAss = Assembly.GetEntryAssembly();
            _applicationFile = new FileInfo(_appAss.Location);
        }

        /// <summary> 应用程序所在目录 </summary>
        public static string Dir
        {
            get
            {
                return _applicationFile.Directory.FullName;
            }
        }

        /// <summary> 应用程序完整文件名 </summary>
        public static string FileName
        {
            get
            {
                return _applicationFile.FullName;
            }
        }

        /// <summary> 应用程序标题 </summary>
        public static string Title
        {
            get
            {
                object[] attS = _appAss.GetCustomAttributes(typeof(AssemblyTitleAttribute), true);
                if (attS != null && attS.Length > 0)
                {
                    return (attS[0] as AssemblyTitleAttribute).Title;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary> 版本号 </summary>
        public static string AssemblyFileVersion
        {
            get
            {
                object[] attS = _appAss.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), true);
                if (attS != null && attS.Length > 0)
                {
                    return (attS[0] as AssemblyFileVersionAttribute).Version;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary> 获取/设置应用程序自动启动 </summary>
        public static bool AutoRun
        {
            set
            {
                RegistryKey run = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (run != null)
                {
                    if (value)
                        run.SetValue(Title, "\"" + FileName + "\" /autostart");
                    else
                        run.DeleteValue(Title);
                }
            }
            get
            {
                RegistryKey run = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (run != null)
                {
                    object o = run.GetValue(Title);
                    if (o != null && o.ToString().IndexOf(FileName) != -1)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

    }

}
