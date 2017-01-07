#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/1/7 17:22:01
 * 文件名：EngineTool
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HebianGu.ComLibModule.SocketHelper
{

    /// <summary>普通方法工具箱 </summary>
    internal class EngineTool
    {
        /// <summary> 域名转换为IP地址 </summary>
        internal static string Hostname2ip(string hostname)
        {
            try
            {
                IPAddress ip;

                if (IPAddress.TryParse(hostname, out ip))
                    return ip.ToString();
                else
                    return Dns.GetHostEntry(hostname).AddressList[0].ToString();
            }
            catch
            {
                throw;
            }
        }

        /// <summary> 服务器信息记录 P1=记录地址 P2=记录内容 </summary>
        internal static void FileOperate(string FileLog, string str)
        {
            if (FileLog == "")
                return;
            try
            {
                FileStream fs = new FileStream(FileLog, FileMode.Append, FileAccess.Write);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.WriteLine(str + DateTime.Now.ToString());
                sw.Close();
                fs.Close();
            }
            catch { throw; }
        }

        /// <summary> 外部调用是否需要用Invoket </summary>
        internal static void EventInvoket(Action func)
        {
            Form form = Application.OpenForms.Cast<Form>().FirstOrDefault();

            if (form != null && form.InvokeRequired)
            {
                form.Invoke(func);
            }
            else
            {
                func();
            }
        }

        /// <summary> 具有返回值的 非bool 外部调用是否需要用Invoket </summary>
        internal static object EventInvoket(Func<object> func)
        {
            object haveStr;

            Form form = Application.OpenForms.Cast<Form>().FirstOrDefault();

            if (form != null && form.InvokeRequired)
            {
                haveStr = form.Invoke(func);
            }
            else
            {
                haveStr = func();
            }

            return haveStr;
        }

        /// <summary> 取文本中某个文本的右边文本 </summary>
        internal static string StringRight(string AllDate, string offstr)
        {
            int lastoff = AllDate.LastIndexOf(offstr) + offstr.Length;
            string haveString = AllDate.Substring(lastoff, AllDate.Length - lastoff);
            return haveString;
        }
        /// <summary> throw文本过滤 </summary>
        internal static string BetweenThrow(string str)
        {
            int dd = str.IndexOf(":");

            if (dd == 0) return str;

            return Between(str, ":", ".");

        }
        /// <summary> 取文本中间内容 </summary>
        internal static string Between(string str, string leftstr, string rightstr)
        {
            int i = str.IndexOf(leftstr) + leftstr.Length;

            string temp = str.Substring(i, str.IndexOf(rightstr, i) - i);

            return temp;
        }

        /// <summary> 读文件操作；如果打开正常返回文件流；异常返回null </summary>
        internal static FileStream FileStreamRead(string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                return fs;
            }
            catch
            {
                return null;
            }
        }
        /// <summary> 写文件操作；如果打开正常返回文件流；异常返回null </summary>
        internal static FileStream FileStreamWrite(string fileName)
        {
            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                return fs;
            }
            catch
            {
                return null;
            }

        }
    }
}
