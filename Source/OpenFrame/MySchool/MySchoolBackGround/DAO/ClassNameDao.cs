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
        /// ��ѯ���а༶��Ϣ
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
