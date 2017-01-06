using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;

namespace DAO
{
    public class ClassNameDao
    {
        /// <summary>
        /// 查询所有班级信息
        /// </summary>
        /// <returns></returns>
        public DataSet searchClassName() 
        {
            string sql = "select * from ClassInfo";
            string tableName = "classNameInfo";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
