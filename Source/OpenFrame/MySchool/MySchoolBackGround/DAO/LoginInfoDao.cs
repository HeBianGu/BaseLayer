using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using System.Data;

namespace DAO
{
    public class LoginInfoDao
    {
        public bool searchIsLoginInfo(string loginName, string loginPassWord)
        {
            string sql = string.Format("select count(*) from managerInfo where managerLoginName='{0}' and managerLoginPassWord='{1}'",loginName,loginPassWord);
            return DbHelper.LoginInfo(sql);
        }
    }
}
