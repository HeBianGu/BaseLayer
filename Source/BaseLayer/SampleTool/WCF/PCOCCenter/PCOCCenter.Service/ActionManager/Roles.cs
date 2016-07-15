using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.PCOCCenter.Service
{
    class Roles
    {
        string LoginID;

        /// <summary>
        /// 获取所有权限列表
        /// </summary>
        /// <param name="LoginID"></param>
        /// <returns></returns>
        public System.Data.DataTable GetRoles(string LoginID)
        {
            this.LoginID = LoginID;

            // 检查权限

            // 获取所有权限列表
            string sql = string.Format("select * from Roles order by Priority");
            return CenterService.DB.ExecuteDataTable(sql);
        }

        /// <summary>
        /// 获取当前登录用户的数据库操作权限（只包含修改和删除数据库连接串的权限）
        /// 创建和读取的权限由数据库权限管理
        /// </summary>
        /// <returns></returns>
        string GetDatabaseConfigRoles(string LoginID)
        {
            string roles = "";

            return roles;
        }

        /*
        public string AddUserGroup(string LoginID, string userGroupInfo)
        {
            this.LoginID = LoginID;

            // 检查权限

            // 添加用户组

            return "1";
        }*/
    }
}
