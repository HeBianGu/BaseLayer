using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPT.PCOCCenter.Utils;
using System.Threading;
using System.Collections;
using System.Xml;
using OPT.PEOfficeCenter.Utils;
using System.IO;

namespace OPT.PCOCCenter.Service
{
    /// <summary>
    /// 许可管理
    /// 包括：加载、添加、删除许可
    /// </summary>
    public class LicenseManager
    {
        static bool bInit = false;
        public static List<LicenseInfo> LicenseInfos = null;
        public static Hashtable htLicenseAppModule = null;
        public static List<ClientAppModuleInfo> ClientAppModuleInfos = null;
        public static SystemDateVerify SystemDateVerify = null;

        string ConfigFile { get; set; }

        public LicenseManager()
        {
            bInit = true;
            LicenseInfos = new List<LicenseInfo>();
            htLicenseAppModule = new Hashtable();
            ClientAppModuleInfos = new List<ClientAppModuleInfo>();
            SystemDateVerify = new SystemDateVerify();

            //实例化Timer类，设置间隔时间为5*60 000毫秒(5分钟)；   
            System.Timers.Timer t = new System.Timers.Timer(300000);
            //到达时间的时候执行事件；   
            t.Elapsed += new System.Timers.ElapsedEventHandler(handlerVerifyClientsKeepAlive);
            //设置是执行一次（false）还是一直执行(true)；   
            t.AutoReset = true;
            //是否执行System.Timers.Timer.Elapsed事件；   
            t.Enabled = true;
        }

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

        public bool Init(string configFile)
        {
            Console.WriteLine(Utils.Utils.Translate("正在初始化服务..."));

            bInit = true;
            ConfigFile = configFile;

            List<string> LogoffLicenseIDs = new List<string>();
            // 读取许可注销信息
            string logoffFile = @"C:\Windows\logoff.dat";
            XmlNodeList logoffLicenseList = XML.GetNodeList(logoffFile, "logoffLicenseFile");
            if (logoffLicenseList != null)
            {
                foreach (XmlNode logoffLicense in logoffLicenseList)
                {
                    string logoffID = XML.Read(logoffLicense, "fileID", "");
                    if (string.IsNullOrEmpty(logoffID)) continue;
                    if (LogoffLicenseIDs.IndexOf(logoffID) >= 0) continue;

                    LogoffLicenseIDs.Add(logoffID);
                }
            }

            // 读取配置文件，获取许可文件
            string xmlFile = configFile;

            LicenseInfos.Clear();
            htLicenseAppModule.Clear();

            int nIndex = 1;
            string licenseFilePath = string.Empty;
            while (true)
            {
                licenseFilePath = XML.Read(xmlFile, "LicenseFile" + nIndex, "file");
                if (licenseFilePath == "" || licenseFilePath == string.Empty)
                {
                    break;
                }
                else
                {
                    LicenseFile oLicenseFile = new LicenseFile();

                    string isUsed = XML.Read(xmlFile, "LicenseFile" + nIndex, "isUsed", "true");
                    LicenseInfo oLicenseInfo = oLicenseFile.GetLicenseInfo(licenseFilePath, isUsed);

                    bool bFind = false;
                    if (oLicenseInfo != null)
                    {
                        if (string.IsNullOrEmpty(oLicenseInfo.uuid))
                        {
                            Console.WriteLine(Utils.Utils.Translate("许可文件uuid错误."));
                            continue;
                        }

                        // 检查是否已经被注销
                        foreach (string logoffLicenseID in LogoffLicenseIDs)
                        {
                            if (String.Equals(oLicenseInfo.uuid, logoffLicenseID, StringComparison.CurrentCultureIgnoreCase))
                            {
                                oLicenseInfo.ErrorInfo = string.Format("{0}\n[{1}]", Utils.Utils.Translate("许可已被注销，请与软件销售商联系！"), licenseFilePath);
                                Console.WriteLine(oLicenseInfo.ErrorInfo);
                                break;
                            }
                        }

                        // 检查是否已经加载License
                        for (int i = 0; i < LicenseInfos.Count; i++)
                        {
                            LicenseInfo licenseInfo = LicenseInfos[i];
                            if (licenseInfo.IsUsed.ToLower() != "true") continue;
                            if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                            if (licenseInfo.uuid == oLicenseInfo.uuid)
                            {
                                bFind = true;
                            }
                        }
                    }
                    else
                    {
                        nIndex++;
                        continue;
                    }

                    DateTime licCreateDate = DateTime.Parse(oLicenseInfo.CreateDate);
                    if (SystemDateVerify.CheckSystemDateWithLicenseFile(licCreateDate) == false)
                    {
                        Console.WriteLine(SystemDateVerify.ErrorInfo);
                        nIndex++;
                        continue;
                    }

                    if (bFind == false) LicenseInfos.Add(oLicenseInfo);

                    // 添加当前许可文件中的注销ID到LogoffLicenseIDs
                    foreach (string logoffID in oLicenseInfo.LogoffLicenseIDs)
                    {
                        if (string.IsNullOrEmpty(logoffID)) continue;
                        if (LogoffLicenseIDs.IndexOf(logoffID) >= 0) continue;

                        LogoffLicenseIDs.Add(logoffID);
                    }

                    // 获取许可配置中的所有模块信息，用于反查出厂时定义的module归属AppName
                    List<string> appModuleList = oLicenseFile.GetAppModuleList(licenseFilePath, isUsed);
                    if (appModuleList != null)
                    {
                        int nSepIndex = 0;
                        string strSeperator = "\\&msp";
                        string strAppName = string.Empty;
                        string strModuleName = string.Empty;
                        foreach (string appModule in appModuleList)
                        {
                            nSepIndex = 0;
                            string strAppModule = appModule;
                            int nPos = strAppModule.IndexOf(strSeperator);
                            while (nPos >= 0)
                            {
                                if (nSepIndex == 0) strAppName = strAppModule.Substring(0, nPos);
                                if (nSepIndex == 1)
                                {
                                    strModuleName = strAppModule.Substring(0, nPos);
                                    break;
                                }
                                strAppModule = strAppModule.Substring(nPos + strSeperator.Length, strAppModule.Length - nPos - strSeperator.Length);

                                nPos = strAppModule.IndexOf(strSeperator);
                                nSepIndex++;
                            }

                            try
                            {
                                htLicenseAppModule.Add(strModuleName, strAppName);
                            }
                            catch (System.Exception ex)
                            {
                            }
                            finally
                            {
                            }
                        }
                    }
                }
                nIndex++;
            }

            // 重新检查所有许可是否被注销
            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (string.IsNullOrEmpty(licenseInfo.uuid)) continue;
                if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                // 检查是否已经被注销
                foreach (string logoffLicenseID in LogoffLicenseIDs)
                {
                    if (String.Equals(licenseInfo.uuid, logoffLicenseID, StringComparison.CurrentCultureIgnoreCase))
                    {
                        licenseInfo.ErrorInfo = string.Format("{0}, [{1}].", Utils.Utils.Translate("发现被注销许可"), licenseFilePath);
                        Console.WriteLine(licenseInfo.ErrorInfo);
                        break;
                    }
                }
            }


            bInit = false;
            Console.WriteLine(Utils.Utils.Translate("服务初始化完成."));

            return true;
        }

        public void AddLicenseToConfig(string LoginID, string licenseFile)
        {
            Console.WriteLine(string.Format("{0}[{1}]...", Utils.Utils.Translate("许可写入配置文件"), licenseFile));

            int nIndex = 1;
            XML.Clear(ConfigFile);
            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (string.IsNullOrEmpty(licenseInfo.uuid)) continue;
                if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                XML.Insert(ConfigFile, "xml", "LicenseFile" + nIndex, "", "file", licenseInfo.LicenseFilePath);
                XML.Insert(ConfigFile, "xml", "LicenseFile" + nIndex, "", "isUsed", "true");

                nIndex++;
            }

            XML.Insert(ConfigFile, "xml", "LicenseFile" + nIndex, "", "file", licenseFile);
            XML.Insert(ConfigFile, "xml", "LicenseFile" + nIndex, "", "isUsed", "true");
        }

        void SaveCenterConfig()
        {
            int nIndex = 1;
            XML.Clear(ConfigFile);
            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (string.IsNullOrEmpty(licenseInfo.uuid)) continue;
                if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                XML.Insert(ConfigFile, "xml", "LicenseFile" + nIndex, "", "file", licenseInfo.LicenseFilePath);
                XML.Insert(ConfigFile, "xml", "LicenseFile" + nIndex, "", "isUsed", "true");

                nIndex++;
            }
        }

        void WriteLogoffFile(string licenseFileID)
        {
            string logoffFile = @"C:\Windows\logoff.dat";
            XML.Insert(logoffFile, "xml", "logoffLicenseFile", "fileID", "", licenseFileID);
        }

        /// <summary>
        /// 注销许可
        /// </summary>
        /// <param name="LoginID"></param>
        /// <param name="licenseFileID"></param>
        /// <returns></returns>
        public string LogoffLicenseFile(string LoginID, string licenseFileID)
        {
            Console.WriteLine(Utils.Utils.Translate("开始注销许可..."));

            string logoffCode = string.Empty;

            string licenseFilePath = string.Empty;
            // 删除指定ID的许可文件
            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (string.IsNullOrEmpty(licenseInfo.uuid)) continue;

                if (licenseInfo.uuid == licenseFileID)
                {
                    licenseFilePath = licenseInfo.LicenseFilePath;
                    licenseInfo.ErrorInfo = Utils.Utils.Translate("许可已经删除。");
                    break;
                }
            }
            if (File.Exists(licenseFilePath))
            {
                //如果存在则删除
                try
                {
                    File.Delete(licenseFilePath);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            // 从许可配置文件移除许可信息
            SaveCenterConfig();

            // 在服务器标记此许可文件被注销，下次不能被导入
            WriteLogoffFile(licenseFileID);

            // 删除成功后，返回注销码

            logoffCode = licenseFileID;

            Console.WriteLine(Utils.Utils.Translate("注销许可完成."));
            return logoffCode;
        }

        public List<LicenseInfo> ReloadLicenseFiles(string LoginID)
        {
            Init(ConfigFile);
            SaveCenterConfig();

            return LicenseInfos;
        }

        public List<LicenseInfo> GetLicenseInfos(string LoginID)
        {
            return LicenseInfos;
        }

        //获取许可模块列表
        public List<string> GetLicenseApps(string LoginID)
        {
            List<string> licenseApps = new List<string>();

            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (licenseInfo.IsUsed.ToLower() != "true") continue;
                if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                for (int mIndex = 0; mIndex < licenseInfo.ModuleInfos.Count; mIndex++)
                {
                    string appName = licenseInfo.ModuleInfos[mIndex].AppName;
                    if (licenseApps.IndexOf(appName) >= 0) continue;
                    licenseApps.Add(appName);
                }
            }

            return licenseApps;
        }

        //获取许可模块数
        public static string GetLicenseAppsCount()
        {
            int nApps = 0;
            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (licenseInfo.IsUsed.ToLower() != "true") continue;
                if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                nApps += licenseInfo.GetLicenseApps();
            }

            return nApps.ToString();
        }

        //获取许可功能数
        public static string GetLicenseModulesCount()
        {
            int nModules = 0;
            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (licenseInfo.IsUsed.ToLower() != "true") continue;
                if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                nModules += licenseInfo.ModuleInfos.Count;
            }

            return nModules.ToString();
        }

        //获取许可类型
        public static string GetLicenseType()
        {
            string licenseType = "";
            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (licenseInfo.IsUsed.ToLower() != "true") continue;
                if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                licenseType += licenseInfo.LicenseType;
                licenseType += ",";
            }

            licenseType = licenseType.TrimEnd(',');

            return licenseType;
        }

        //获取许可截至日期
        public static string GetLicenseExpiryDate()
        {
            string expiryDate = "";
            DateTime dateLast = DateTime.MinValue;
            foreach (LicenseInfo licenseInfo in LicenseInfos)
            {
                if (licenseInfo.IsUsed.ToLower() != "true") continue;
                if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                DateTime date = DateTime.Parse(licenseInfo.GetExpiryDate());

                if (dateLast < date)
                {
                    dateLast = date;
                    expiryDate = licenseInfo.GetExpiryDate();
                }
            }

            return expiryDate;
        }

        //////////////////////////////////////////////////////////////////////////
        #region ClientLicense

        /// <summary>
        /// 客户端心跳包
        /// </summary>
        /// <param name="ClientHost"></param>
        /// <param name="AppName"></param>
        /// <param name="ModuleName"></param>
        /// <param name="ModuleVersion"></param>
        public void ClientKeepAlive(string LoginID, string ClientHost, string AppName, List<string> ModuleNameList, string ModuleVersion)
        {
            foreach (string ModuleName in ModuleNameList)
            {
                if (ModuleName == null || ModuleName == "") continue;

                // 通过ModuleName反查许可中的AppName
                if (AppName == null || AppName == "")
                    AppName = GetAppNameFromLicenseAppModuleHT(ModuleName);
                if (AppName == null || AppName == "") continue;

                ClientAppModuleInfo clientAppModuleInfo = CheckClientAppModuleExist(ClientHost, AppName, ModuleName, ModuleVersion);

                if (Monitor.TryEnter(ClientAppModuleInfos, 1000))
                {
                    if (clientAppModuleInfo == null)
                    {
                        RequestClientAppModuleLicense(LoginID, ClientHost, AppName, ModuleName, ModuleVersion);
                    }

                    try
                    {
                        clientAppModuleInfo.nKeepAlive++;

                        if (clientAppModuleInfo.nKeepAlive > 10)
                            clientAppModuleInfo.nKeepAlive = 10;
                    }
                    catch (System.Exception ex)
                    {
                    }
                    finally
                    {
                        Monitor.Exit(ClientAppModuleInfos);
                    }
                }
                else
                {
                    Monitor.Exit(ClientAppModuleInfos);
                }
            }
        }

        /// <summary>
        /// Client登出，返还许可证。 
        /// </summary>
        /// <returns></returns>
        public void GoBackLicense(string ClientHost, string AppName, string ModuleName, string ModuleVersion)
        {
            if (AppName == "PEOfficeCenter" && ModuleName == "PEOfficeCenter.Manager") return;
            if (ModuleName == AppName) return; // 表示引导程序APP退出
            if (ModuleName == "") return; // 表示引导程序APP退出

            // 通过ModuleName反查许可中的AppName
            if (AppName == null || AppName == "")
                AppName = GetAppNameFromLicenseAppModuleHT(ModuleName);
            if (AppName == null || AppName == "") return;
            Console.WriteLine(string.Format("[{0}]{1}.", ModuleName, Utils.Utils.Translate("功能释放许可")));

            // 检查Client是否已经连接此app[module，目前只检查到app一级]，成功直接返回
            ClientAppModuleInfo clientAppModuleInfo = CheckClientAppModuleExist(ClientHost, AppName, ModuleName, ModuleVersion);
            if (clientAppModuleInfo != null)
            {
                bool bRet = false;
                string failInfo = string.Empty;

                DateTime dateToday = DateTime.Today;

                if (Monitor.TryEnter(LicenseInfos, 1000))
                {
                    try
                    {
                        foreach (LicenseInfo licenseInfo in LicenseInfos)
                        {
                            if (bRet) break;
                            if (licenseInfo.IsUsed.ToLower() != "true") continue;
                            if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                            bool bHaveMoreAppRequestCount = false;
                            foreach (ModuleInfo moduleInfo in licenseInfo.ModuleInfos)
                            {
                                if (moduleInfo.AppName == AppName)
                                {
                                    DateTime dateExpiry = DateTime.Parse(moduleInfo.ExpiryDate);
                                    if (dateExpiry >= dateToday)
                                    {
                                        // 免费类型许可
                                        if (moduleInfo.LicenseType == "free")
                                        {
                                            failInfo = string.Empty;
                                            bRet = true;
                                        }
                                        else
                                        {
                                            // 限制类型许可
                                            if (bHaveMoreAppRequestCount == false && clientAppModuleInfo.nAppRequestCount > 0)
                                            {
                                                // 客户端多开处理
                                                clientAppModuleInfo.nAppRequestCount--;
                                                bHaveMoreAppRequestCount = true;
                                                Console.WriteLine(string.Format("[{0}]{1}{2}{3}.", AppName, Utils.Utils.Translate("模块多开还有"), clientAppModuleInfo.nAppRequestCount, Utils.Utils.Translate("次")));
                                            }

                                            if (clientAppModuleInfo.nAppRequestCount <= 0)
                                            {
                                                // App不再被占用时，返回License
                                                if (moduleInfo.LicenseUsed > 0)
                                                {
                                                    moduleInfo.LicenseUsed--; // 成功返还许可
                                                    failInfo = string.Empty;
                                                    bRet = true;

                                                    Console.WriteLine(string.Format("[{0}]{1}.", AppName, Utils.Utils.Translate("模块释放许可成功")));
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        failInfo = Utils.Utils.Translate("许可已经过期，请和管理员联系！");
                                    }
                                }
                                else
                                {
                                    if (bRet == false) failInfo = string.Format(Utils.Utils.Translate("未找到模块[{0}]的许可信息，请和管理员联系！"), AppName);
                                }
                            }
                        }
                    }
                    finally
                    {
                        Monitor.Exit(LicenseInfos);
                    }
                }
                else
                {
                    // 超时后的处理代码
                    bRet = false;
                }

                if (bRet)
                {
                    if (Monitor.TryEnter(ClientAppModuleInfos, 1000))
                    {
                        try
                        {
                            ClientAppModuleInfos.Remove(clientAppModuleInfo);
                        }
                        catch (System.Exception ex)
                        {
                        }
                        finally
                        {
                            Monitor.Exit(ClientAppModuleInfos);
                        }
                    }
                    else
                    {
                        Monitor.Exit(ClientAppModuleInfos);
                    }
                }
                else
                {
                }
            }
        }

        string GetAppNameFromLicenseAppModuleHT(string strModuleName)
        {
            object oAppName = htLicenseAppModule[strModuleName];

            string strAppName = null;
            if (oAppName != null) strAppName = oAppName.ToString();

            return strAppName;
        }

        /// <summary>
        /// 检查模块许可
        /// </summary>
        /// <param name="ClientHostKey"></param>
        /// <param name="AppName"></param>
        /// <param name="ModuleName"></param>
        /// <param name="ModuleVersion"></param>
        /// <returns></returns>
        public string CheckModuleLicense(string LoginID, string ClientHost, string AppName, string ModuleName, string ModuleVersion)
        {
            string ret = "Success";
            if (AppName == "PCOCCenter" && ModuleName == "PCOCCenter.Manager") return ret;
            if (ModuleName == AppName && ModuleName != "") return ret; // 表示引导程序APP启动，功能Module启动时才检测许可

            // 通过ModuleName反查许可中的AppName
            if (AppName == null || AppName == "")
                AppName = GetAppNameFromLicenseAppModuleHT(ModuleName);

            if (AppName == null || AppName == "")
            {
                ret = Utils.Utils.Translate("此功能未授权!");
                return ret;
            }

            // 检查Client是否已经连接此app[module，目前只检查到app一级]，成功直接返回
            ClientAppModuleInfo clientAppModuleInfo = CheckClientAppModuleExist(ClientHost, AppName, ModuleName, ModuleVersion);

            if (clientAppModuleInfo != null)
            {
                if (Monitor.TryEnter(ClientAppModuleInfos, 1000))
                {
                    try
                    {
                        clientAppModuleInfo.nAppRequestCount++;
                        clientAppModuleInfo.nKeepAlive++;
                        Console.WriteLine(string.Format("[{0}]{1}{2}{3}.", AppName, Utils.Utils.Translate("模块多开"), clientAppModuleInfo.nAppRequestCount, Utils.Utils.Translate("次")));
                    }
                    catch (System.Exception ex)
                    {
                    }
                    finally
                    {
                        Monitor.Exit(ClientAppModuleInfos);
                    }
                }
                else
                {
                    Monitor.Exit(ClientAppModuleInfos);
                }

                return ret + @"_exist";
            }

            // Client申请许可证，成功返回“Success”，失败返回原因。
            return RequestClientAppModuleLicense(LoginID, ClientHost, AppName, ModuleName, ModuleVersion);
        }

        /// <summary>
        /// Client申请许可证，成功返回“Success”，失败返回原因。 
        /// </summary>
        /// <returns></returns>
        string RequestClientAppModuleLicense(string LoginID, string ClientHost, string AppName, string ModuleName, string ModuleVersion)
        {
            bool bRet = false;
            string failInfo = string.Empty;

            DateTime dateToday = DateTime.Today;

            if (Monitor.TryEnter(LicenseInfos, 1000))
            {
                try
                {
                    if (LicenseInfos.Count == 0)
                    {
                        failInfo = Utils.Utils.Translate("未发现许可文件，请和管理员联系！");
                    }
                    else
                    {
                        foreach (LicenseInfo licenseInfo in LicenseInfos)
                        {
                            if (bRet) break;
                            if (licenseInfo.IsUsed.ToLower() != "true") continue;
                            if (string.IsNullOrEmpty(licenseInfo.ErrorInfo) == false) continue;

                            failInfo = string.Format("[{0}]{1}", AppName, Utils.Utils.Translate("模块的许可信息未找到，请和管理员联系！"));
                            foreach (ModuleInfo moduleInfo in licenseInfo.ModuleInfos)
                            {
                                if (moduleInfo.AppName == AppName)
                                {
                                    DateTime dateExpiry = DateTime.Parse(moduleInfo.ExpiryDate);
                                    if (dateExpiry >= dateToday)
                                    {
                                        // 免费类型许可
                                        if (moduleInfo.LicenseType == "free")
                                        {
                                            failInfo = string.Empty;
                                            bRet = true;
                                        }
                                        else
                                        {
                                            // 限制类型许可
                                            if (moduleInfo.LicenseUsed < moduleInfo.LicenseCount)
                                            {
                                                moduleInfo.LicenseUsed++; // 成功申请到许可
                                                failInfo = string.Empty;
                                                bRet = true;
                                                Console.WriteLine(string.Format("[{0}]{1}{2}.", AppName, Utils.Utils.Translate("模块被占用许可"), moduleInfo.LicenseUsed));
                                            }
                                            else
                                            {
                                                failInfo = Utils.Utils.Translate("许可已经用完，请和管理员联系！");
                                                Console.WriteLine(string.Format("[{0}]{1}{2}.", AppName, Utils.Utils.Translate("模块"), failInfo));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        failInfo = Utils.Utils.Translate("许可已经过期，请和管理员联系！");
                                        Console.WriteLine(failInfo);
                                    }
                                }
                            }
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(LicenseInfos);
                }
            }
            else
            {
                // 超时后的处理代码
                bRet = false;
            }

            if (bRet)
            {
                ClientAppModuleInfo clientAppModuleInfo = new ClientAppModuleInfo();
                clientAppModuleInfo.LoginID = LoginID;
                clientAppModuleInfo.ClientHost = ClientHost;
                clientAppModuleInfo.AppName = AppName;
                clientAppModuleInfo.ModuleName = ModuleName;
                clientAppModuleInfo.ModuleVersion = ModuleVersion;
                clientAppModuleInfo.nAppRequestCount = 1;
                clientAppModuleInfo.nKeepAlive = 5;

                if (Monitor.TryEnter(ClientAppModuleInfos, 1000))
                {
                    try
                    {
                        ClientAppModuleInfos.Add(clientAppModuleInfo);
                    }
                    catch (System.Exception ex)
                    {
                        failInfo = ex.Message;
                    }
                    finally
                    {
                        Monitor.Exit(ClientAppModuleInfos);
                    }
                }
                else
                {
                    Monitor.Exit(ClientAppModuleInfos);
                }

                return "Success";
            }
            else
            {
                return failInfo;
            }
        }

        ClientAppModuleInfo CheckClientAppModuleExist(string ClientHost, string AppName, string ModuleName, string ModuleVersion)
        {
            foreach (ClientAppModuleInfo clientAppModuleInfo in ClientAppModuleInfos)
            {
                if (ClientHost == clientAppModuleInfo.ClientHost && AppName == clientAppModuleInfo.AppName)
                {
                    return clientAppModuleInfo;
                }
            }

            return null;
        }

        /// <summary>
        /// 校验客户端心跳信息，判断客户端是否异常退出
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void handlerVerifyClientsKeepAlive(object source, System.Timers.ElapsedEventArgs e)
        {
            if (bInit) return;

            for (int i = 0; i < ClientAppModuleInfos.Count; i++)
            {
                ClientAppModuleInfo clientAppModuleInfo = ClientAppModuleInfos[i];
                if (clientAppModuleInfo.nKeepAlive <= 0)
                {
                    clientAppModuleInfo.nAppRequestCount = 0;
                    // 归还许可
                    GoBackLicense(clientAppModuleInfo.ClientHost, clientAppModuleInfo.AppName, clientAppModuleInfo.ModuleName, clientAppModuleInfo.ModuleVersion);

                    // 更新登录状态
                    string sql = string.Format("update Login set LogoutTime = '{0}' , status = '{1}' where ID = '{2}'",
                            DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Utils.Utils.Translate("异常登出"), clientAppModuleInfo.LoginID);

                    try
                    {
                        CenterService.DB.ExecuteNonQuery(sql);
                    }
                    catch (System.Exception ex)
                    {
                    }
                    finally
                    {
                    }
                }
                else
                {
                    clientAppModuleInfo.nKeepAlive--;
                }

                if (ClientAppModuleInfos.Count <= 0) break;
            }
        }

        /// <summary>
        /// 客户端登录信息
        /// APP 手动Login成功后，需要禁用登录按钮（防止多开算法出错，占用许可）
        /// Module点击打开，算APP一次多开，需要LicenseClient调用Login
        /// 关闭Module时，需要LicenseClient调用Logout
        /// 而且关闭APP时，需要LicenseClient调用Logout
        /// </summary>
        public class ClientAppModuleInfo
        {
            public string LoginID;
            public string ClientHost;
            public string AppName;
            public string ModuleName;
            public string ModuleVersion;
            public int nAppRequestCount; // 请求多开次数
            public int nKeepAlive;       // 客户端心跳包
        }

        #endregion
        //////////////////////////////////////////////////////////////////////////

        //////////////////////////////////////////////////////////////////////////
        #region LicenseAppModuleList

        public class AppModule
        {
            public string AppName;
            public string ModuleName;
        }

        #endregion
        //////////////////////////////////////////////////////////////////////////
    }
}
