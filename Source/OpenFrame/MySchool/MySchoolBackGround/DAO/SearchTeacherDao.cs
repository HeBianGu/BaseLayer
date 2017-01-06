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
        /// ��ѯ���н�Ա
        /// </summary>
        /// <returns></returns>
        public DataSet searchTeacherName()
        {
            string sql = "select * from TeacherInfo";
            string tableName = "teacherNameInfo";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��ѯ���а�����
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
