using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.PCOCCenter.Service
{
    class UserGroups
    {
        string LoginID;

        public System.Data.DataTable GetUserGroups(string LoginID)
        {
            this.LoginID = LoginID;

            // 检查权限

            // 获取用户组
            string sql = string.Format("select * from UserGroups");
            return CenterService.DB.ExecuteDataTable(sql);
        }

        public void DeleteUserGroup(string userGroupID)
        {
            // 删除用户组
            string sql = string.Format("delete from UserGroups where id='{0}'", userGroupID);
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

        public void AddUserGroup(string groupName, string ipRanges, string allowApps, string roles)
        {
            // 添加用户组
            string sql = string.Format("insert into UserGroups (GroupName, IPRanges, AllowApps, Roles) values ('{0}','{1}','{2}','{3}')", groupName, ipRanges, allowApps, roles);
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

        public void UpdateUserGroup(string userGroupID, string groupName, string ipRanges, string allowApps, string roles)
        {
            // 更新用户组
            string sql = string.Format("update UserGroups set GroupName = '{0}', IPRanges = '{1}', AllowApps = '{2}', Roles = '{3}' where ID = '{4}'", groupName, ipRanges, allowApps, roles, userGroupID);
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
    }
}
