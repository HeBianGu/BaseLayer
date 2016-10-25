using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.NetWork
{
    class Class1
    {
        public static string GetIP()
        {
            try
            {
                WebClient client = new WebClient();
                client.Encoding = System.Text.Encoding.Default;
                string response = client.UploadString("http://iframe.ip138.com/ipcity.asp", "");
                Match mc = Regex.Match(response, @"location.href=""(.*)""");
                if (mc.Success && mc.Groups.Count > 1)
                {
                    response = client.UploadString(mc.Groups[1].Value, "");
                    return response;//做相关处理
                }
                return null;
            }
            catch (System.Exception e)
            {
                return null;
            }
        }

        public static string  GetIPNew()
        {
            string tempIP = string.Empty;
            if (System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.Length > 1)
                tempIP = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1].ToString();

            return tempIP;
        }

        //public string readip()
        //{
        //    HtmlDocument hd = webBrowser1.Document;
        //    string stra = "";
        //    for (int i = 0; i < hd.All.Count; i++)
        //    {
        //        if (hd.All[i].OuterText != null)
        //        {
        //            Match m = Regex.Match(hd.All[i].OuterText, @"[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}");
        //            if (m.Success)
        //            {
        //                stra = m.ToString();
        //                MessageBox.Show(m.ToString());
        //                break;
        //            }
        //        }
        //    }


        //}
    }
}
