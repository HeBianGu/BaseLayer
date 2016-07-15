using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OPT.PCOCCenter.Service.Interface;
using OPT.PCOCCenter.Utils;
using System.Net;
using System.Net.Sockets;
using OPT.PEOfficeCenter.Utils;

namespace OPT.PCOCCenter.Service
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class CenterService : ICenterService
    {
        public static string ServiceVersion = "Ver6.0";
        public static bool Inited = false;
        public static string ConfigFile = "CenterConfig.xml";
        public static string ServicePort = "22888";
        public static DateTime ServerStartTime = DateTime.Now;
        public static LicenseManager LicenseManager = null;
        public static ActionManager ActionManager = null;
        public static ServerExManager ServerExManager = null;
        public static SimulationHost SimulationHost = null;
        public static SimulationTaskManager SimulationTaskManager = null;

        public static SQLiteHelper DB
        {
            get { return ActionManager.DB; }
        }


        public CenterService()
        {
        }

        public bool InitCenterService(string configFile=null)
        {
            if (Inited) return true;

            if (configFile != null)
            {
                ConfigFile = configFile;
            }

            // 读取配置文件
            ServicePort = XML.Read(ConfigFile, "Service", "Port", ServicePort);

            // 许可管理器
            LicenseManager = new LicenseManager();
            bool bLicenseOK = LicenseManager.Init(ConfigFile);

            // 功能管理器
            ActionManager = new ActionManager();
            bool bActionOK = ActionManager.Init(ConfigFile);

            // 服务器扩展管理器
            ServerExManager = new ServerExManager();

            // 模拟器主机管理器
            SimulationHost = new SimulationHost();

            // 模拟任务管理器
            SimulationTaskManager = new SimulationTaskManager();

            Inited = bLicenseOK && bActionOK;

            return Inited;
        }

        public string Login(string loginString)
        {
            return ActionManager.Login(loginString);
        }

        public int Logout(string logoutString)
        {
            return ActionManager.Logout(logoutString);
        }

        public string Verify(string verifyString)
        {
            return ActionManager.Verify(verifyString);
        }

        public string AddUser(string LoginID, string userInfo)
        {
            return ActionManager.AddUser(LoginID, userInfo);
        }

        public string EditUser(string LoginID, string userID, string userInfo)
        {
            return ActionManager.EditUser(LoginID, userID, userInfo);
        }

        public string DeleteUser(string LoginID, string userIDs)
        {
            return ActionManager.DeleteUser(LoginID, userIDs);
        }

        public string GetServerInfo(string LoginID)
        {
            return ActionManager.GetServerInfo(LoginID);
        }

        public System.Data.DataTable GetOnlineUsers(string LoginID, bool bAllLogin = false)
        {
            return ActionManager.GetOnlineUsers(LoginID, bAllLogin);
        }

        public void AddLicenseToConfig(string LoginID, string licenseFile)
        {
            LicenseManager.AddLicenseToConfig(LoginID, licenseFile);
        }

        public List<LicenseInfo> ReloadLicenseFiles(string LoginID)
        {
            return LicenseManager.ReloadLicenseFiles(LoginID);
        } 

        public List<LicenseInfo> GetLicenseInfos(string LoginID)
        {
            return LicenseManager.GetLicenseInfos(LoginID);
        }

        //获取许可模块列表
        public List<string> GetLicenseApps(string LoginID)
        {
            return LicenseManager.GetLicenseApps(LoginID);
        }

        public System.Data.DataTable GetRoles(string LoginID)
        {
            return ActionManager.GetRoles(LoginID);
        }

        public System.Data.DataTable GetUserGroups(string LoginID)
        {
            return ActionManager.GetUserGroups(LoginID);
        }

        public void DeleteUserGroup(string userGroupID)
        {
            ActionManager.DeleteUserGroup(userGroupID);
        }

        public void AddUserGroup(string groupName, string ipRanges, string allowApps, string roles)
        {
            ActionManager.AddUserGroup(groupName, ipRanges, allowApps, roles);
        }

        public void UpdateUserGroup(string userGroupID, string groupName, string ipRanges, string allowApps, string roles)
        {
            ActionManager.UpdateUserGroup(userGroupID, groupName, ipRanges, allowApps, roles);
        }


        public System.Data.DataTable GetUsers(string LoginID)
        {
            return ActionManager.GetUsers(LoginID);
        }

        //获取许可模块数
        public static string GetLicenseAppsCount()
        {
            return LicenseManager.GetLicenseAppsCount();
        }

        //获取许可功能数
        public static string GetLicenseModulesCount()
        {
            return LicenseManager.GetLicenseModulesCount();
        }

        //获取许可类型
        public static string GetLicenseType()
        {
            return LicenseManager.GetLicenseType();
        }

        //获取许可截至日期
        public static string GetLicenseExpiryDate()
        {
            return LicenseManager.GetLicenseExpiryDate();
        }


        //////////////////////////////////////////////////////////////////////////
        // ExtraConfig OperationContract
        //////////////////////////////////////////////////////////////////////////
        public string AddExtraConfig(string LoginID, string userName, string configType, string configName, string configFile)
        {
            return ServerExManager.AddExtraConfig(LoginID, userName, configType, configName, configFile);
        }

        public string DeleteExtraConfig(string LoginID, string configIDs)
        {
            return ServerExManager.DeleteExtraConfig(LoginID, configIDs);
        }

        public System.Data.DataTable GetExtraConfigNameList(string LoginID, string configType)
        {
            return ServerExManager.GetExtraConfigNameList(LoginID, configType);
        }

        public string GetExtraConfigFile(string LoginID, string configID)
        {
            return ServerExManager.GetExtraConfigFile(LoginID, configID);
        }

        //////////////////////////////////////////////////////////////////////////
        // SimulationHost OperationContract
        //////////////////////////////////////////////////////////////////////////
        public string RegSimulationHost(string LoginID, string userName, string hostName, string hostIP, string simulationType, string licensePath, string simulationPath, string hostKey)
        {
            return SimulationHost.RegSimulationHost(LoginID, userName, hostName, hostIP, simulationType, licensePath, simulationPath, hostKey);
        }

        public string DeleteSimulationHost(string LoginID, string simulationHostIDs)
        {
            return SimulationHost.DeleteSimulationHost(LoginID, simulationHostIDs);
        }

        public System.Data.DataTable GetSimulationHostList(string LoginID, string simulationType="")
        {
            return SimulationHost.GetSimulationHostList(LoginID, simulationType);
        }

        public string GetSimulationHostFilePath(string LoginID, string simulationHostID)
        {
            return SimulationHost.GetSimulationHostFilePath(LoginID, simulationHostID);
        }

        public string GetSimulationHostLicensePath(string LoginID, string simulationHostID)
        {
            return SimulationHost.GetSimulationHostLicensePath(LoginID, simulationHostID);
        }

        // 获取数模任务列表
        public List<SimTaskInfo> GetSimulationTaskList(string OwnerIP, string FuncID)
        {
            return SimulationTaskManager.GetSimTaskList(OwnerIP, FuncID);
        }
    }
}
