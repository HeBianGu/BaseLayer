using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace OPT.PCOCCenter.Utils
{
    /// <summary>
    /// 校验系统时间是否被改变
    /// </summary>
    public class SystemDateVerify
    {
        public static string ErrorInfo;

        /// <summary>
        /// 用系统临时文件的最大值和当前系统时间比较来判断
        /// </summary>
        /// <returns></returns>
        public static bool CheckSystemDateWithUserTempFiles()
        {
            ErrorInfo = "";
            string[] files = Directory.GetFiles(Path.GetTempPath());//路径
            foreach (string file in files)
            {
                FileInfo fi = new FileInfo(file);
                if (fi.LastWriteTime > DateTime.Now)
                {
                    ErrorInfo = "系统日期被改变，发现temp目录中文件日期异常！";
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 获取Internat标准时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetInternatTime()
        {
            ErrorInfo = "";
            DateTime oInternatTime = DateTime.MinValue;

            try
            {
                oInternatTime = GetBeijingTime();                
            }
            catch (System.Exception ex)
            {
                ErrorInfo = ex.Message;
            }

            return oInternatTime;
        }

        /// <summary>
        /// 通过Internat来判断系统时间
        /// </summary>
        /// <returns></returns>
        public static bool CheckSystemDateWithInternat()
        {
            ErrorInfo = "";
            DateTime oInternatTime = GetInternatTime();
            if (oInternatTime != null && oInternatTime > (DateTime.Now) + new TimeSpan(1,0,0,0))
            {
                ErrorInfo = "系统日期小于网络标准日期！";
                return false;
            }
            else
            {
                if (oInternatTime == null || oInternatTime.Year == 2011)
                {
                    ErrorInfo = "InternatError";
                }
                return true;
            }
        }

        /// <summary>
        /// 用许可文件时间判断系统时间，系统时间只能在许可时间之后
        /// </summary>
        /// <param name="r_oLicenseDate"></param>
        /// <returns></returns>
        public static bool CheckSystemDateWithLicenseFile(DateTime r_oLicenseDate)
        {
            ErrorInfo = "";
            if (r_oLicenseDate != null && r_oLicenseDate > DateTime.Now.AddDays(1))
            {
                ErrorInfo = "系统日期小于许可创建日期！";
                return false;
            }
            else
                return true;
        }

        #region 获取网络时间

        public static void ProxySetting(WebRequest request)
        {
            WebProxy proxy = WebProxy.GetDefaultProxy();//获取IE缺省设置

            if (request != null && proxy != null)
            {
                proxy.Credentials = CredentialCache.DefaultCredentials;
                request.Proxy = proxy;//赋予 request.Proxy 
            }

            ////如果缺省设置为空，则有可能是根本不需要代理服务器，如果此时配置文件中也未配置则认为不需Proxy
            //if (proxy.Address == null)
            //    proxy.Address = new Uri("××××××:8080");//按配置文件创建Proxy 地置
            //if (proxy.Address != null)//如果地址为空，则不需要代理服务器
            //{
            //    proxy.Credentials = new NetworkCredential("test123", "123456");//从配置封装参数中创建
            //    request.Proxy = proxy;//赋予 request.Proxy 
            //}
        }

        ///<summary>
        /// 从指定的字符串中获取整数
        ///</summary>
        ///<param name="origin">原始的字符串</param>
        ///<param name="fullMatch">是否完全匹配，若为false，则返回字符串中的第一个整数数字</param>
        ///<returns>整数数字</returns>
        private static int GetInt(string origin, bool fullMatch)
        {
            if (string.IsNullOrEmpty(origin))
            {
                return 0;
            }
            origin = origin.Trim();
            if (!fullMatch)
            {
                string pat = @"-?\d+";
                Regex reg = new Regex(pat);
                origin = reg.Match(origin.Trim()).Value;
            }
            int res = 0;
            int.TryParse(origin, out res);
            return res;
        }

        ///<summary>
        /// 获取标准北京时间1
        ///</summary>
        ///<returns></returns>
        public static DateTime GetBJStandardTime()
        {
            //<?xml version="1.0" encoding="GB2312" ?> 
            //- <ntsc>
            //- <time>
            //  <year>2011</year> 
            //  <month>7</month> 
            //  <day>10</day> 
            //  <Weekday /> 
            //  <hour>19</hour> 
            //  <minite>45</minite> 
            //  <second>37</second> 
            //  <Millisecond /> 
            //  </time>
            //  </ntsc>
            DateTime dt;
            WebRequest wrt = null;
            WebResponse wrp = null;
            try
            {
                wrt = WebRequest.Create("http://www.time.ac.cn/timeflash.asp?user=flash");
                wrt.Credentials = CredentialCache.DefaultCredentials;
                ProxySetting(wrt);

                wrp = wrt.GetResponse();
                StreamReader sr = new StreamReader(wrp.GetResponseStream(), Encoding.UTF8);
                string html = sr.ReadToEnd();

                sr.Close();
                wrp.Close();

                int yearIndex = html.IndexOf("<year>") + 6;
                int monthIndex = html.IndexOf("<month>") + 7;
                int dayIndex = html.IndexOf("<day>") + 5;
                int hourIndex = html.IndexOf("<hour>") + 6;
                int miniteIndex = html.IndexOf("<minite>") + 8;
                int secondIndex = html.IndexOf("<second>") + 8;

                string year = html.Substring(yearIndex, html.IndexOf("</year>") - yearIndex);
                string month = html.Substring(monthIndex, html.IndexOf("</month>") - monthIndex); ;
                string day = html.Substring(dayIndex, html.IndexOf("</day>") - dayIndex);
                string hour = html.Substring(hourIndex, html.IndexOf("</hour>") - hourIndex);
                string minite = html.Substring(miniteIndex, html.IndexOf("</minite>") - miniteIndex);
                string second = html.Substring(secondIndex, html.IndexOf("</second>") - secondIndex);
                dt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minite), int.Parse(second));
            }
            catch (WebException)
            {
                return DateTime.Parse("2011-1-1");
            }
            catch (Exception)
            {
                return DateTime.Parse("2011-1-1");
            }
            finally
            {
                if (wrp != null)
                    wrp.Close();
                if (wrt != null)
                    wrt.Abort();
            }
            return dt;
        }

        ///<summary>
        /// 获取标准北京时间2
        ///</summary>
        ///<returns></returns>
        public static DateTime GetBeijingTime()
        {
            //t0 = new Date().getTime();
            //nyear = 2011;
            //nmonth = 7;
            //nday = 5;
            //nwday = 2;
            //nhrs = 17;
            //nmin = 12;
            //nsec = 12;
            DateTime dt;
            WebRequest wrt = null;
            WebResponse wrp = null;
            try
            {
                wrt = WebRequest.Create("http://www.beijing-time.org/time.asp");
                wrt.Credentials = CredentialCache.DefaultCredentials;
                ProxySetting(wrt);

                wrp = wrt.GetResponse();

                string html = string.Empty;
                using (Stream stream = wrp.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                    {
                        html = sr.ReadToEnd();
                    }
                }

                string[] tempArray = html.Split(';');
                for (int i = 0; i < tempArray.Length; i++)
                {
                    tempArray[i] = tempArray[i].Replace("\r\n", "");
                }

                string year = tempArray[1].Substring(tempArray[1].IndexOf("nyear=") + 6);
                string month = tempArray[2].Substring(tempArray[2].IndexOf("nmonth=") + 7);
                string day = tempArray[3].Substring(tempArray[3].IndexOf("nday=") + 5);
                string hour = tempArray[5].Substring(tempArray[5].IndexOf("nhrs=") + 5);
                string minite = tempArray[6].Substring(tempArray[6].IndexOf("nmin=") + 5);
                string second = tempArray[7].Substring(tempArray[7].IndexOf("nsec=") + 5);
                dt = new DateTime(int.Parse(year), int.Parse(month), int.Parse(day), int.Parse(hour), int.Parse(minite), int.Parse(second));
            }
            catch (WebException)
            {
                return DateTime.Parse("2011-1-1");
            }
            catch (Exception)
            {
                return DateTime.Parse("2011-1-1");
            }
            finally
            {
                if (wrp != null)
                    wrp.Close();
                if (wrt != null)
                    wrt.Abort();
            }
            return dt;
        }

        #endregion
    }
}
