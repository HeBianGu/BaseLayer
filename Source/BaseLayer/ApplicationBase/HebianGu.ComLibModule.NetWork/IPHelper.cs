using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace HebianGu.ComLibModule.NetWork
{
    /// <summary>
    /// IP地址服务类
    /// </summary>
    public class IPHelper
    {
        HttpContextBase context;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContext">请求上下文</param>
        public IPHelper(HttpContextBase httpContext)
        {
            context = httpContext as HttpContextBase;
        }

        /// <summary>
        /// ip正则校验
        /// </summary>
        private Regex RegIP
        {
            get { return new Regex(@"^\d+.\d+.\d+.\d+$"); }
        }

        /// <summary>
        /// 获取ip地址
        /// </summary>
        /// <returns></returns>
        public string GetIpAddress()
        {
            string ipAdress = "172.0.0.1";
            if (RegIP.IsMatch(GetClientIpPv6()))
            {
                ipAdress = GetClientIpPv6();
            }
            if (RegIP.IsMatch(GetClientIPv4()))
            {
                ipAdress = GetClientIPv4();
            }
            return ipAdress;
        }

        /// <summary>
        /// 判断客户端有没有使用代理使用代理返回true,没有使用代理返回false
        /// </summary>
        /// <returns></returns>
        private bool IsProxy()
        {
            return context.Request.ServerVariables["HTTP_VIA"] != null;
        }

        /// <summary>
        /// 获取客户端的IPv6如果获取不到IPv6将自动获取IPv4
        /// </summary>
        /// <returns></returns>
        private string GetClientIpPv6()
        {
            string ipv6 = string.Empty;
            //ipv6 = Request.UserHostAddress;
            if (IsProxy())//客户端有使用代理
            {
                ipv6 = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else//客户端没有使用代理
            {
                ipv6 = context.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            return ipv6;
        }

        /// <summary>
        /// 获取客户端的IPv4
        /// </summary>
        /// <returns></returns>
        private string GetClientIPv4()
        {
            string ipv4 = String.Empty;
            foreach (IPAddress IPA in Dns.GetHostAddresses(context.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = IPA.ToString();
                    break;
                }
            }
            if (ipv4 != String.Empty)
            {
                return ipv4;
            }
            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    ipv4 = IPA.ToString();
                    break;
                }
            }
            return ipv4;
        }

        #region 获得用户IP
        /// <summary>
        /// 获得用户IP
        /// </summary>
        public static string GetUserIp()
        {
            string ip;
            string[] temp;
            bool isErr = false;
            if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"] == null)
                ip = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            else
                ip = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_ForWARDED_For"].ToString();
            if (ip.Length > 15)
                isErr = true;
            else
            {
                temp = ip.Split('.');
                if (temp.Length == 4)
                {
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (temp[i].Length > 3) isErr = true;
                    }
                }
                else
                    isErr = true;
            }

            if (isErr)
                return "1.1.1.1";
            else
                return ip;
        }
        #endregion
    }
}
