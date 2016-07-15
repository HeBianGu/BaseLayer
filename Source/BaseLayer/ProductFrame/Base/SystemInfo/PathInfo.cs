using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Globalization;




namespace OPT.Product.Base
{
#if false
    public static class BxPathInfo
    {
        static public string ApplicationConfigPath(string applicationID, CultureInfo ci)
        {
            string appConfigPath = Path.Combine(BxSystemInfo.Instance.SystemPath.ApplicationsConfigPath, applicationID);
            string appConfigPathEx = Path.Combine(appConfigPath, ci.Name);
            if (!new System.IO.DirectoryInfo(appConfigPathEx).Exists)
            {
                appConfigPathEx = Path.Combine(appConfigPath, ci.ThreeLetterWindowsLanguageName);
                if (!new System.IO.DirectoryInfo(appConfigPathEx).Exists)
                    appConfigPathEx = Path.Combine(appConfigPath, ci.TwoLetterISOLanguageName);
            }
            return appConfigPathEx;
        }

        static public string ModuleConfigPath(string moduleID, CultureInfo ci)
        {
            string configPath = Path.Combine(BxSystemInfo.Instance.SystemPath.ModulesConfigPath, moduleID);
            string configPathEx = Path.Combine(configPath, ci.Name);
            if (!new System.IO.DirectoryInfo(configPathEx).Exists)
            {
                configPathEx = Path.Combine(configPath, ci.ThreeLetterWindowsLanguageName);
                if (!new System.IO.DirectoryInfo(configPathEx).Exists)
                    configPathEx = Path.Combine(configPath, ci.TwoLetterISOLanguageName);
            }
            return configPathEx;
        }

        static public string GeneralLayerConfigPath(string id, CultureInfo ci)
        {
            string configPath = Path.Combine(BxSystemInfo.Instance.SystemPath.GeneralConfigPath, id);
            string configPathEx = Path.Combine(configPath, ci.Name);
            if (!new System.IO.DirectoryInfo(configPathEx).Exists)
            {
                configPathEx = Path.Combine(configPath, ci.ThreeLetterWindowsLanguageName);
                if (!new System.IO.DirectoryInfo(configPathEx).Exists)
                    configPathEx = Path.Combine(configPath, ci.TwoLetterISOLanguageName);
            }
            return configPathEx;
        }

        static public string ApplicationConfigPath(string applicationID)
        {
            return ApplicationConfigPath(applicationID, BxSystemInfo.Instance.DefaultCultureInfo);
        }

        static public string ModuleConfigPath(string moduleID)
        {
            return ModuleConfigPath(moduleID, BxSystemInfo.Instance.DefaultCultureInfo);
        }

        static public string ModuleDllPath(string moduleID)
        {
            string dllPath = Path.Combine(BxSystemInfo.Instance.SystemPath.ModulesDllPath, moduleID, moduleID + ".dll");
            return dllPath;
        }
    }
#endif
}
