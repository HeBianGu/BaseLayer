using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPT.PCOCCenter.Utils;

namespace OPT.PCOCCenter.Service
{
    /// <summary>
    /// 服务器功能管理
    /// 比如：登陆、退出、验证等功能
    ///       服务器信息、在线用户、用户管理等功能
    /// </summary>
    public class ActionManager
    {
        /// <summary>
        /// 数据库操作对象
        /// </summary>
        private static SQLiteHelper sqliteHelper;


        public static SQLiteHelper DB
        {
            get { return sqliteHelper; }
        }

        public ActionManager()
        {
        }

        public bool Init(string configFile)
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            string connString = string.Format("Data Source={0}", appPath + "PEOfficeCenter.db3");
            sqliteHelper = new SQLiteHelper(connString);

            return true;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginString"></param>
        /// <returns></returns>
        public string Login(string loginString)
        {
            Login login = new Login();
            return login.checkLogin(loginString);
        }

        public int Logout(string logoutString)
        {
            Logout logout = new Logout();
            return logout.UpdateLogout(logoutString);
        }

        public string Verify(string verifyString)
        {
            Verify verify = new Verify();
            return verify.clientVerify(verifyString);
        }

        public string AddUser(string LoginID, string userInfo)
        {
            Users users = new Users();
            return users.AddUser(LoginID, userInfo);
        }

        public string EditUser(string LoginID, string userID, string userInfo)
        {
            Users users = new Users();
            return users.EditUser(LoginID, userID, userInfo);
        }

        public string DeleteUser(string LoginID, string userIDs)
        {
            Users users = new Users();
            return users.DeleteUser(LoginID, userIDs);
        }

        public string GetServerInfo(string LoginID)
        {
            ServerInfo serverInfo = new ServerInfo();
            return serverInfo.GetServerInfo(LoginID);
        }

        public System.Data.DataTable GetOnlineUsers(string LoginID, bool bAllLogin=false)
        {
            OnlineUsers onlineUsers = new OnlineUsers();
            return onlineUsers.GetOnlineUsers(LoginID, bAllLogin);
        }

        public System.Data.DataTable GetRoles(string LoginID)
        {
            Roles roles = new Roles();
            return roles.GetRoles(LoginID);
        }

        public System.Data.DataTable GetUserGroups(string LoginID)
        {
            UserGroups userGroups = new UserGroups();
            return userGroups.GetUserGroups(LoginID);
        }

        public System.Data.DataTable GetUsers(string LoginID)
        {
            Users users = new Users();
            return users.GetUsers(LoginID);
        }

        public void DeleteUserGroup(string userGroupID)
        {
            UserGroups userGroups = new UserGroups();
            userGroups.DeleteUserGroup(userGroupID);
        }

        public void AddUserGroup(string groupName, string ipRanges, string allowApps, string roles)
        {
            UserGroups userGroups = new UserGroups();
            userGroups.AddUserGroup(groupName, ipRanges, allowApps, roles);
        }

        public void UpdateUserGroup(string userGroupID, string groupName, string ipRanges, string allowApps, string roles)
        {
            UserGroups userGroups = new UserGroups();
            userGroups.UpdateUserGroup(userGroupID, groupName, ipRanges, allowApps, roles);
        }
    }
}
