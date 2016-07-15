using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.PCOCCenter.Service
{

    class Logout
    {
        string LoginID;
        string AppName;
        string ModuleName;
        string ModuleVersion;
        string KeyInfo;
        string ClientHost;

        public int UpdateLogout(string logoutString)
        {
            ParseLogoutFromString(logoutString);

            int ret = 1;

            // 登出，返还许可
            GoBackLicense();

            // 更新登出信息
            UpdateLogoutInfo();

            return ret;
        }

        /// <summary>
        /// 登出，返还许可
        /// </summary>
        /// <returns></returns>
        void GoBackLicense()
        {
            CenterService.LicenseManager.GoBackLicense(ClientHost, AppName, ModuleName, ModuleVersion);
        }


        void UpdateLogoutInfo()
        {
            string sql = string.Format("update Login set LogoutTime = '{0}' , status = '{1}' where ID = '{2}'",
                                        DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "登出", LoginID);

            CenterService.DB.ExecuteNonQuery(sql);
        }
        
        // 解析登出字符串
        bool ParseLogoutFromString(string logoutString)
        {
            string[] sArray = logoutString.Split(';');

            if (sArray.Length >= 6)
            {
                LoginID = sArray[0].ToString();
                AppName = sArray[1].ToString();
                ModuleName = sArray[2].ToString();
                ModuleVersion = sArray[3].ToString();
                KeyInfo = sArray[4].ToString();
                ClientHost = sArray[5].ToString();
            }

            return true;
        }

    }
}
