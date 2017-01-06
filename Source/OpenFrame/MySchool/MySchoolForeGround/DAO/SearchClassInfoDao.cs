using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using System.Data;
using Entity;

namespace DAO
{
    public class SearchClassInfoDao
    {
        public DataSet searchClassName()
        {
            string sql = "select * from ClassInfo";
            string tableName = "SearchClassName";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
