using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;

namespace DAO
{
    public class SearchTeacherDao
    {
        /// <summary>
        /// 查询所有教员
        /// </summary>
        /// <returns></returns>
        public DataSet searchTeacherName()
        {
            string sql = "select * from TeacherInfo";
            string tableName = "teacherNameInfo";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有班主任
        /// </summary>
        /// <returns></returns>
        public DataSet searchClassTeacherName()
        {
            string sql = "select * from ClassTeacherInfo";
            string tableName = "classTeacherNameInfo";
            return DBHelper.searchData(sql, tableName);
        }

    }
}
