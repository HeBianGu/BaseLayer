using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.PCOCCenter.Service
{
    class Users
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserGroupID { get; set; }
        public string Roles { get; set; }
        public string Telephone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Address { get; set; }
        public string Memo { get; set; }

        public System.Data.DataTable GetUsers(string LoginID)
        {
            // 检查权限

            // 获取用户列表
            string sql = string.Format("select u.ID, u.UserName, u.Password, g.GroupName as UserGroup, u.Roles, u.Telephone, u.MobilePhone, u.Email, u.QQ, u.Address, u.Memo from Users u left join UserGroups g on u.UserGroupID = g.ID order by UserName");
            return CenterService.DB.ExecuteDataTable(sql);
        }

        public string AddUser(string LoginID, string userInfo)
        {
            string[] sArray = userInfo.Split(';');
            if (sArray.Length >= 10)
            {
                UserName = sArray[0].ToString();
                Password = sArray[1].ToString();
                UserGroupID = sArray[2].ToString();
                Roles = sArray[3].ToString();
                Telephone = sArray[4].ToString();
                MobilePhone = sArray[5].ToString();
                Email = sArray[6].ToString();
                QQ = sArray[7].ToString();
                Address = sArray[8].ToString();
                Memo = sArray[9].ToString();
            }

            // 检查权限

            // 添加用户
            string sql = string.Format("insert into Users (UserName, Password, UserGroupID, Roles, Telephone, MobilePhone, Email, QQ, Address, Memo) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}', '{7}', '{8}', '{9}')",
                                        UserName, Password, UserGroupID, Roles, Telephone, MobilePhone, Email, QQ, Address, Memo);

            int ret = CenterService.DB.ExecuteNonQuery(sql);

            return ret.ToString();
        }
        
        public string EditUser(string LoginID, string userID, string userInfo)
        {
            string[] sArray = userInfo.Split(';');
            if (sArray.Length >= 10)
            {
                UserName = sArray[0].ToString();
                Password = sArray[1].ToString();
                UserGroupID = sArray[2].ToString();
                Roles = sArray[3].ToString();
                Telephone = sArray[4].ToString();
                MobilePhone = sArray[5].ToString();
                Email = sArray[6].ToString();
                QQ = sArray[7].ToString();
                Address = sArray[8].ToString();
                Memo = sArray[9].ToString();
            }

            // 检查权限

            // 添加用户
            string sql = string.Format("update Users set UserName = '{0}', Password = '{1}', UserGroupID = '{2}', Roles = '{3}', Telephone = '{4}', MobilePhone = '{5}', Email = '{6}', QQ = '{7}', Address = '{8}', Memo = '{9}' where ID = '{10}'",
                                        UserName, Password, UserGroupID, Roles, Telephone, MobilePhone, Email, QQ, Address, Memo, userID);

            int ret = CenterService.DB.ExecuteNonQuery(sql);

            return ret.ToString();
        }

        public string DeleteUser(string LoginID, string userIDs)
        {
            // 检查权限

            // 删除用户
            string sql = string.Format("delete from Users where ID in ({0})", userIDs);

            int ret = CenterService.DB.ExecuteNonQuery(sql);

            return ret.ToString();
        }
    }
}
