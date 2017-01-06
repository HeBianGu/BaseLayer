using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class InsertLessonNameDao
    {
        public bool insertLessonName(LessonInfoEntity entity)
        {
            string sql = "insert into LessonInfo values('"+entity.LessonName+"',"+entity.LessonIsExist+")";
            return DBHelper.modifyData(sql);
        }
    }
}
