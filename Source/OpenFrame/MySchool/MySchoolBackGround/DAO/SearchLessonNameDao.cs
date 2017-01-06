using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;


namespace DAO
{
    public class SearchLessonNameDao
    {
        public DataSet searchLessonName()
        {
            string sql = "select * from LessonInfo where LessonIsExist=1";
            string tableName = "LessonNameInfo";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
