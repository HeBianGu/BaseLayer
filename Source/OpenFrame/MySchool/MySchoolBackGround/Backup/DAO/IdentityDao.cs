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
           string sql = "select * from IdentityInfo where IdentityName<>'һ������Ա' and IdentityName<> '������'";
           string tableName = "IdentityNameInfo";
           return DBHelper.searchData(sql, tableName);
        }
        public DataSet IdentityName()
        {
            string sql = "select * from IdentityInfo where IdentityName<>'һ������Ա' and IdentityName<> '����Ա'";
            string tableName = "IdentityName";
            return DBHelper.searchData(sql, tableName);
        }

    }
}
