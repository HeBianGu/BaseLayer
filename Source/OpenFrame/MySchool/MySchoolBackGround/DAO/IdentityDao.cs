using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;

namespace DAO
{   
    public class IdentityDao
    {
        public DataSet searchIdentityName()
        {
           string sql = "select * from IdentityInfo where IdentityName<>'一级管理员' and IdentityName<> '班主任'";
           string tableName = "IdentityNameInfo";
           return DBHelper.searchData(sql, tableName);
        }
        public DataSet IdentityName()
        {
            string sql = "select * from IdentityInfo where IdentityName<>'一级管理员' and IdentityName<> '管理员'";
            string tableName = "IdentityName";
            return DBHelper.searchData(sql, tableName);
        }

    }
}
