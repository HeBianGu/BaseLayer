using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace OPT.PCOCCenter.Service
{
    class ServerInfo
    {
        string LoginID;

        [DllImport("KeyObtain.dll")]
        public static extern IntPtr GetMAC();
        [DllImport("KeyObtain.dll")]
        public static extern IntPtr GetHDSN();
        [DllImport("KeyObtain.dll")]
        public static extern IntPtr GetCPUID();
        [DllImport("KeyObtain.dll")]
        public static extern IntPtr GetHostID();
        [DllImport("KeyObtain.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetKeyString(StringBuilder Deskey);

        //获取本机IP
        public static string GetLocalIP()
        {
            IPAddress[] arrIPAddresses = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ip in arrIPAddresses)
            {
                if (ip.AddressFamily.Equals(AddressFamily.InterNetwork))
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }

        public string GetServerInfo(string LoginID)
        {
            this.LoginID = LoginID;

            IntPtr intPtrHostMAC = GetMAC();
            string hostMAC = Marshal.PtrToStringAnsi(intPtrHostMAC);

            IntPtr intPtrHostHDSN = GetHDSN();
            string hostHDSN = Marshal.PtrToStringAnsi(intPtrHostHDSN);

            IntPtr intPtrHostCPUID = GetCPUID();
            string hostCPUID = Marshal.PtrToStringAnsi(intPtrHostCPUID);

            IntPtr intPtrHostID = GetHostID();
            string hostID = Marshal.PtrToStringAnsi(intPtrHostID);

            IntPtr intPtrKEY = GetKeyString(null);
            string KeyString = Marshal.PtrToStringAnsi(intPtrKEY);

            // 检查权限

            // 获取服务器信息
            string serverInfo = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};",
                CenterService.ServiceVersion, SystemDateTime(), ServerRunTime(),
                GetLocalIP(), CenterService.ServicePort,
                GetOnlineUsersCount(),
                CenterService.GetLicenseAppsCount(), CenterService.GetLicenseModulesCount(),
                CenterService.GetLicenseType(), CenterService.GetLicenseExpiryDate()
                , hostID, KeyString, hostMAC, hostHDSN, hostCPUID);

            return serverInfo;
        }

        string SystemDateTime()
        {
            DateTime oTime = DateTime.Now;

            return oTime.ToString("yyyy-MM-dd HH:mm:ss");
        }

        string ServerRunTime()
        {
            DateTime curTime = DateTime.Now;
            TimeSpan timeSpan = curTime - CenterService.ServerStartTime;

            if (timeSpan.Days > 0)
                return string.Format("{0}{1} {2:00}:{3:00}:{4:00}", timeSpan.Days, Utils.Utils.Translate("天"), timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
            else
                return string.Format("{0:00}:{1:00}:{2:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
        }

        string GetOnlineUsersCount()
        {
            string onlineUsersCount = "0";
            string sql = string.Format("select count(*) as [count] from [Login] where [Status] = '{0}'", Utils.Utils.Translate("登入"));
            try
            {
                DataTable dt = CenterService.DB.ExecuteDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    onlineUsersCount = dt.Rows[0]["count"].ToString();
                }
            }
            catch (System.Exception ex)
            {

            }
            return onlineUsersCount;
        }
    }
}
