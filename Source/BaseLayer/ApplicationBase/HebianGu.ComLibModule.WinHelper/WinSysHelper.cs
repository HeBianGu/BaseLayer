using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.WinHelper
{
    /// <summary> Windows系统帮助类 </summary> 
    public partial class WinSysHelper
    {
        /// <summary> 获取系统资源文件 </summary>
        public string GetSystemPath(Environment.SpecialFolder folderEnum)
        {
            if (Environment.SpecialFolder.MyComputer == folderEnum)
                return "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";

            return Environment.GetFolderPath(folderEnum);
        }


        /// <summary> 开机启动项 </summary> 
        /// <param name=\"Started\">是否启动</param> 
        /// <param name=\"name\">启动值的名称</param> 
        /// <param name=\"path\">启动程序的路径</param> 
        public void RunWhenStart(bool Started, string name, string path)
        {
            using (RegistryKey hklm = Registry.LocalMachine)
            {
                RegistryKey run = hklm.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\", true);

                if (Started == true)
                {
                    // HTodo  ：已经设置开机启动则返回 
                    if (this.IsAutoRun(name)) return;

                    run.SetValue(name, path);
                }
                else
                {
                    run.DeleteValue(name);
                }
            }
        }

        /// <summary> 检查是否是开机启动 </summary>
        public bool IsAutoRun(string name)
        {
            RegistryKey hklm = Registry.LocalMachine;
            //RegistryKey run = hklm.CreateSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");

            RegistryKey run = hklm.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run\");

            object obj = run.GetValue(name);

            if (obj == null) return false;

            return true;

        }


        /// <summary> 加载所有程序 </summary>
        public List<Tuple<string, string>> LoadPrograms()
        {
            List<Tuple<string, string>> ts = new List<Tuple<string, string>>();

            //HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall 此键的子健为本机所有注册过的软件的卸载程序,通过此思路进行遍历安装的软件
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall");
            string[] key1 = key.GetSubKeyNames();//返回此键所有的子键名称
            List<string> key2 = key1.ToList<string>();//因为有的项木有"DisplayName"或"DisplayName"的键值的时候要把键值所在数组中的的元素进行删除
            RegistryKey subkey = null;

            for (int i = 0; i < key2.Count; i++)
            {

                //通过list泛型数组进行遍历,某款软件项下的子键
                subkey = key.OpenSubKey(key2[i]);

                if (subkey.GetValue("DisplayName") == null) continue;
                if (subkey.GetValue("DisplayIcon") == null) continue;


                string path = subkey.GetValue("DisplayIcon").ToString();
                //截取子键值的最后一位进行判断
                string SubPath = path.Substring(path.Length - 1, 1);

                //如果为o 就是ico 或 找不到exe的 表示为图标文件或只有个标识而没有地址的
                if (SubPath == "o" || path.IndexOf("exe") == -1)
                {
                    //首先删除数组中此索引的元素
                    key2.RemoveAt(i);
                    //把循环条件i的值进行从新复制,否则下面给listview的项的tag属性进行赋值的时候会报错
                    i -= 1;
                    continue;
                }

                //如果为e 就代表着是exe可执行文件,
                if (SubPath == "e")
                {
                    //则表示可以直接把地址赋给tag属性
                    Tuple<string, string> p = new Tuple<string, string>(subkey.GetValue("DisplayName").ToString(), path);
                    ts.Add(p);
                    continue;
                }
                //因为根据观察 取的是DisplayIcon的值 表示为图片所在路径 如果为0或1,则是为可执行文件的图标  
                if (SubPath == "0" || SubPath == "1")
                {
                    //进行字符串截取,
                    path = path.Substring(0, path.LastIndexOf("e") + 1);
                    //则表示可以直接把地址赋给tag属性
                    Tuple<string, string> p = new Tuple<string, string>(subkey.GetValue("DisplayName").ToString(), path);
                    ts.Add(p);
                    continue;
                }
            }
            return ts;
        }
    }


    /// <summary> 此类的说明 </summary>
    partial class WinSysHelper
        {
            #region - Start 单例模式 -

            /// <summary> 单例模式 </summary>
            private static WinSysHelper t = null;

            /// <summary> 多线程锁 </summary>
            private static object localLock = new object();

            /// <summary> 创建指定对象的单例实例 </summary>
            public static WinSysHelper Instance
            {
                get
                {
                    if (t == null)
                    {
                        lock (localLock)
                        {
                            if (t == null)
                                return t = new WinSysHelper();
                        }
                    }
                    return t;
                }
            }
            /// <summary> 禁止外部实例 </summary>
            private WinSysHelper()
            {

            }
            #endregion - 单例模式 End -

        }
    }
