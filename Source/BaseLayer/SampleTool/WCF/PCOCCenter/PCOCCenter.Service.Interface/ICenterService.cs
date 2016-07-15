using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using OPT.PCOCCenter.Utils;

namespace OPT.PCOCCenter.Service.Interface
{
    [ServiceContract(Name = "PCOCCenterService", Namespace = "OPT.PCOCCenter.Service")]
    public interface ICenterService
    {
        [OperationContract]
        string Login(string loginString);

        [OperationContract]
        int Logout(string LoginID);

        [OperationContract]
        string Verify(string verifyString);

        [OperationContract]
        string GetServerInfo(string LoginID);

        [OperationContract]
        System.Data.DataTable GetOnlineUsers(string LoginID, bool bAllLogin = false);

        [OperationContract]
        void AddLicenseToConfig(string LoginID, string licenseFile);

        [OperationContract]
        List<LicenseInfo> ReloadLicenseFiles(string LoginID);

        [OperationContract]
        List<LicenseInfo> GetLicenseInfos(string LoginID);

        [OperationContract]
        List<string> GetLicenseApps(string LoginID);

        [OperationContract]
        System.Data.DataTable GetUserGroups(string LoginID);

        [OperationContract]
        void DeleteUserGroup(string userGroupID);
        
        [OperationContract]
        void AddUserGroup(string groupName, string ipRanges, string allowApps, string roles);
        
        [OperationContract]
        void UpdateUserGroup(string userGroupID, string groupName, string ipRanges, string allowApps, string roles);

        [OperationContract]
        System.Data.DataTable GetRoles(string LoginID);

        [OperationContract]
        System.Data.DataTable GetUsers(string LoginID);

        [OperationContract]
        string AddUser(string LoginID, string userInfo);

        [OperationContract]
        string EditUser(string LoginID, string userID, string userInfo);

        [OperationContract]
        string DeleteUser(string LoginID, string userIDs);

        //////////////////////////////////////////////////////////////////////////
        // ExtraConfig OperationContract
        //////////////////////////////////////////////////////////////////////////
        [OperationContract]
        string AddExtraConfig(string LoginID, string userName, string configType, string configName, string configFile);

        [OperationContract]
        string DeleteExtraConfig(string LoginID, string configIDs);

        [OperationContract]
        System.Data.DataTable GetExtraConfigNameList(string LoginID, string configType);

        [OperationContract]
        string GetExtraConfigFile(string LoginID, string configID);

        //////////////////////////////////////////////////////////////////////////
        // SimulationHost OperationContract
        //////////////////////////////////////////////////////////////////////////
        [OperationContract]
        string RegSimulationHost(string LoginID, string userName, string hostName, string hostIP, string simulationType, string licensePath, string simulationPath, string hostKey);

        [OperationContract]
        string DeleteSimulationHost(string LoginID, string simulationHostIDs);

        [OperationContract]
        System.Data.DataTable GetSimulationHostList(string LoginID, string simulationType="");

        [OperationContract]
        string GetSimulationHostFilePath(string LoginID, string simulationHostID);
  
        [OperationContract]
        string GetSimulationHostLicensePath(string LoginID, string simulationHostID);

        // 获取数模任务列表
        [OperationContract]
        List<SimTaskInfo> GetSimulationTaskList(string OwnerIP, string FuncID);
    }
}
