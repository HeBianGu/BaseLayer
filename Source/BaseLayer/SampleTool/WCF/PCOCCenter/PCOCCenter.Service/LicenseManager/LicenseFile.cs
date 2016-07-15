using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace OPT.PCOCCenter.Service
{
    class LicenseFile
    {
        [DllImport("LicenseFile.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr GetLicenseProperty(StringBuilder lpath);

        [DllImport("LicenseFile.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetLicenseModuleInfo([In]StringBuilder lpath, [In, Out]string[] strModuleInfos, int iarray, int itemMaxLength);

        [DllImport("LicenseFile.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetLicenseAppModuleList([In]StringBuilder lpath, [In, Out]string[] strAppModuleList, int iarray, int itemMaxLength);

        [DllImport("KeyObtain.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr CheckHostKey(StringBuilder hostkey);

        public LicenseFile()
        {
        }

        public List<string> GetAppModuleList(string licenseFilePath, string isUsed)
        {
            if (isUsed == "false") return null;
            Console.WriteLine(string.Format("{0}[{1}]...", Utils.Utils.Translate("正在获取许可功能列表"), licenseFilePath));

            List<string> appModuleList = new List<string>();
            // 获取App和功能列表，用于根据ModuleName反查AppName

            int nModuleCount = 0;
            int nLicenseAppCount = 0; // 授权许可在处理许可数时，只是每个App只有一条记录，所以此处需要AppCount；
            LicenseInfo oLicenseInfo = null;

            StringBuilder licFilePath = new StringBuilder();
            licFilePath.Append(licenseFilePath);

            IntPtr intPtr = GetLicenseProperty(licFilePath);
            if (intPtr != null)
            {
                string strLicenseProperty = Marshal.PtrToStringAnsi(intPtr);
                if (strLicenseProperty != null && strLicenseProperty != "")
                {
                    oLicenseInfo = new LicenseInfo();
                    bool ret = oLicenseInfo.LicensePropertyFromString(strLicenseProperty, ref nModuleCount, ref nLicenseAppCount);

                    if (nModuleCount > 0)
                    {
                        int itemMaxLength = 1024;
                        string[] strAppModules = new string[nModuleCount];
                        for (int i = 0; i < nModuleCount; i++) strAppModules[i] = new string(' ', itemMaxLength);

                        GetLicenseAppModuleList(licFilePath, strAppModules, nModuleCount, itemMaxLength);

                        for (int i = 0; i < nModuleCount; i++)
                        {
                            appModuleList.Add(strAppModules[i]);
                        }
                    }
                }
            }
            Console.WriteLine(Utils.Utils.Translate("许可功能列表获取完成."));

            return appModuleList;
        }

        public LicenseInfo GetLicenseInfo(string licenseFilePath, string isUsed)
        {
            Console.WriteLine(string.Format("{0}[{1}]...", Utils.Utils.Translate("正在获取许可信息"), licenseFilePath));

            int nModuleCount = 0;
            int nLicenseAppCount = 0; // 授权许可在处理许可数时，只是每个App只有一条记录，所以此处需要AppCount；
            LicenseInfo oLicenseInfo = null;

            StringBuilder licFilePath = new StringBuilder();
            licFilePath.Append(licenseFilePath);

            IntPtr intPtr = GetLicenseProperty(licFilePath);
            if (intPtr != null)
            {
                string strLicenseProperty = Marshal.PtrToStringAnsi(intPtr);
                if (strLicenseProperty != null && strLicenseProperty != "")
                {
                    oLicenseInfo = new LicenseInfo();
                    oLicenseInfo.IsUsed = isUsed;
                    oLicenseInfo.ErrorInfo = string.Empty;
                    oLicenseInfo.LicenseFilePath = licenseFilePath;

                    bool ret = oLicenseInfo.LicensePropertyFromString(strLicenseProperty, ref nModuleCount, ref nLicenseAppCount);

                    if (nLicenseAppCount > 0)
                    {
                        StringBuilder hostKey = new StringBuilder();
                        hostKey.Append(oLicenseInfo.HostKey);
                        IntPtr intPtrRet = CheckHostKey(hostKey);
                        string strRet = Marshal.PtrToStringAnsi(intPtrRet);

                        if (strRet != "success")
                        {
                            oLicenseInfo.ErrorInfo = Utils.Utils.Translate("许可文件KEY信息在服务器校验失败！");
                            Console.WriteLine(oLicenseInfo.ErrorInfo);
                        }
                    }
                    else
                    {
                        oLicenseInfo.ErrorInfo = Utils.Utils.Translate("许可文件读取许可数信息失败！");
                        Console.WriteLine(oLicenseInfo.ErrorInfo);
                    }
                }
            }

            if (oLicenseInfo != null && oLicenseInfo.IsUsed.ToLower() == "true" && string.IsNullOrEmpty(oLicenseInfo.ErrorInfo))
            {
                int itemMaxLength = 1024;
                string[] strModuleInfos = new string[nLicenseAppCount];
                for (int i = 0; i < nLicenseAppCount; i++) strModuleInfos[i] = new string(' ', itemMaxLength);

                GetLicenseModuleInfo(licFilePath, strModuleInfos, nLicenseAppCount, itemMaxLength);

                oLicenseInfo.ModuleInfos.Clear();

                for (int i = 0; i < nLicenseAppCount; i++)
                {
                    ModuleInfo oModuleInfo = new ModuleInfo();
                    oModuleInfo.ModuleFromString(strModuleInfos[i]);
                    oLicenseInfo.ModuleInfos.Add(oModuleInfo);
                }
            }

            Console.WriteLine(Utils.Utils.Translate("许可信息解析完成."));

            return oLicenseInfo;
        }
    }
}
