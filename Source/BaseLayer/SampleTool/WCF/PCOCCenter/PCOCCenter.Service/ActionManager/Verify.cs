using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.PCOCCenter.Service
{
    class Verify
    {
        string LoginID;
        string UserIP;
        string UserName;
        string AppName;
        List<string> ModuleNameList=new List<string>(); // 可以同时接受多个模块的心跳包验证
        string ModuleVersion;
        string KeyInfo;
        string ClientHost;

        public string clientVerify(string verifyString)
        {
            string verifyRet = "Success";
            ParseVerifyFromString(verifyString);

            // 更新许可管理客户端心跳包
            CenterService.LicenseManager.ClientKeepAlive(LoginID, ClientHost, AppName, ModuleNameList, ModuleVersion);

            return verifyRet;
        }

        void ParaseVerifyModuleNames(string ModuleNames)
        {
            if (ModuleNames == null || ModuleNames == "") return;

            ModuleNames = ModuleNames.Replace(", ", ";");
            string[] sArray = ModuleNames.Split(';');

            foreach (string ModuleName in sArray)
            {
                if (ModuleName == null || ModuleName == "") continue;
                ModuleNameList.Add(ModuleName);
            }
        }

        bool ParseVerifyFromString(string verifyString)
        {
            string[] sArray = verifyString.Split(';');
            string ModuleNames = string.Empty;

            if (sArray.Length >= 8)
            {
                LoginID = sArray[0].ToString();
                UserIP = sArray[1].ToString();
                UserName = sArray[2].ToString();
                AppName = sArray[3].ToString();
                ModuleNames = sArray[4].ToString();
                ModuleVersion = sArray[5].ToString();
                KeyInfo = sArray[6].ToString();
                ClientHost = sArray[7].ToString();

                ParaseVerifyModuleNames(ModuleNames);
            }

            return true;
        }
    }
}
