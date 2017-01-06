using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;

namespace DAO
{
    public class SexInfoDao
    {
        public DataSet searchSexInfo()
        {
            string sql = "select * from SexInfo";
            string tableName = "SearchSexInfo";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
