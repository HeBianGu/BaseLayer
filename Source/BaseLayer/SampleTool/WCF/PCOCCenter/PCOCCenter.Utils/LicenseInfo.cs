using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace OPT.PCOCCenter.Service
{
    // 模块许可信息
    [DataContract]
    public class ModuleInfo
    {
        // 版本号
        [DataMember]
        public string Version;
        // 程序名
        [DataMember]
        public string AppName;
        // 模块名
        [DataMember]
        public string ModuleName;
        // 模块可用许可数
        [DataMember]
        public int LicenseCount;
        // 模块已用许可数
        [DataMember]
        public int LicenseUsed;
        // 过期日期
        [DataMember]
        public string ExpiryDate;
        // 过期天数
        [DataMember]
        public string ExpiryDays;
        // 许可类型
        [DataMember]
        public string LicenseType;


        bool SegModuleString(string strModule, ref ArrayList strInfos)
        {
            if (strModule == null) return false;

            string strSeperator = "\\&msp";

            int nPos = strModule.IndexOf(strSeperator);
            while (nPos >= 0)
            {
                strInfos.Add(strModule.Substring(0, nPos));
                strModule = strModule.Substring(nPos + strSeperator.Length, strModule.Length - nPos - strSeperator.Length);

                nPos = strModule.IndexOf(strSeperator);
            }

            return true;
        }

        public bool ModuleFromString(string strModule)
        {
            if (strModule == null) return false;

            ArrayList strInfos = new ArrayList();

            SegModuleString(strModule, ref strInfos);

            if (strInfos.Count >= 7)
            {
                Version = strInfos[0].ToString();
                AppName = strInfos[1].ToString();
                ModuleName = strInfos[2].ToString();
                LicenseCount = int.Parse(strInfos[3].ToString());
                ExpiryDate = strInfos[4].ToString();
                ExpiryDays = strInfos[5].ToString();
                LicenseType = strInfos[6].ToString();
            }
            LicenseUsed = 0;

            return true;
        }
    }

    [DataContract]
    public class LicenseInfo
    {
        [DataMember]
        public string LicenseFilePath;
        [DataMember]
        public string uuid;
        [DataMember]
        public string Version;
        [DataMember]
        public string IsUsed;
        [DataMember]
        public string CreateDate;
        [DataMember]
        public string Customer;
        [DataMember]
        public string HostKey;
        [DataMember]
        public string LicenseType;
        [DataMember]
        public string ErrorInfo;

        // 被注销许可列表
        [DataMember]
        public List<string> LogoffLicenseIDs = new List<string>();
        // 模块许可信息列表
        [DataMember]
        public List<ModuleInfo> ModuleInfos = new List<ModuleInfo>();

        public LicenseInfo()
        {
        }

        bool SegLicenseString(string strLicense, ref ArrayList strInfos)
        {
            if (strLicense == null) return false;

            string strSeperator = "\\&nsp";

            int nPos = strLicense.IndexOf(strSeperator);
            while (nPos >= 0)
            {
                strInfos.Add(strLicense.Substring(0, nPos));
                strLicense = strLicense.Substring(nPos + strSeperator.Length, strLicense.Length - nPos - strSeperator.Length);

                nPos = strLicense.IndexOf(strSeperator);
            }

            return true;
        }

        public bool LicensePropertyFromString(string strLicenseProperty, ref int nModuleCount, ref int nLicenseAppCount)
        {
            if (strLicenseProperty == null) return false;

            ArrayList strInfos = new ArrayList();

            SegLicenseString(strLicenseProperty, ref strInfos);

            if (strInfos.Count >= 8)
            {
                uuid = strInfos[0].ToString();
                Version = strInfos[1].ToString();
                CreateDate = strInfos[2].ToString();
                Customer = strInfos[3].ToString();
                HostKey = strInfos[4].ToString();
                LicenseType = strInfos[5].ToString();
                nModuleCount = int.Parse(strInfos[6].ToString());
                nLicenseAppCount = int.Parse(strInfos[7].ToString());
                if (strInfos.Count >= 9)
                {
                    string logoffLicenseIDStr = strInfos[8].ToString();
                    logoffLicenseIDStr += ";";
                    // 设置注销许可
                    string strSep = ";";
                    int nPos = logoffLicenseIDStr.IndexOf(strSep);
                    while (nPos >= 0)
                    {
                        string logoffID = logoffLicenseIDStr.Substring(0, nPos);
                        if(string.IsNullOrEmpty(logoffID)==false)
                            LogoffLicenseIDs.Add(logoffID);
                        logoffLicenseIDStr = logoffLicenseIDStr.Substring(nPos + strSep.Length, logoffLicenseIDStr.Length - nPos - strSep.Length);
                        nPos = logoffLicenseIDStr.IndexOf(strSep);
                    }
                }
                return true;
            }

            return false;
        }

        public int GetLicenseApps()
        {
            int nApps = 0;
            string appName = "";
            foreach (ModuleInfo moduleInfo in ModuleInfos)
            {
                if (appName != moduleInfo.AppName)
                {
                    appName = moduleInfo.AppName;
                    nApps++;
                }
            }

            return nApps;
        }

        public string GetExpiryDate()
        {
            string expiryDate = "";
            DateTime dateLast = DateTime.MinValue;
            foreach (ModuleInfo moduleInfo in ModuleInfos)
            {
                DateTime date = DateTime.Parse(moduleInfo.ExpiryDate);

                if (dateLast < date)
                {
                    dateLast = date;
                    expiryDate = moduleInfo.ExpiryDate;
                }
            }

            return expiryDate;
        }
    }
}
