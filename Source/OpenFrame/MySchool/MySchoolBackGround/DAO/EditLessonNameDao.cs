using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class EditLessonNameDao
    {
        /// <summary>
        /// 修改科目名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editLesson(LessonInfoEntity entity)
        {
            string sql = "update LessonInfo set LessonName ='"+entity.LessonName+"' where LessonId="+entity.LessonId+"";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 删除科目名称
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool deleteLesson(LessonInfoEntity entity)
        {
            string sql = "update LessonInfo set LessonIsExist=0 where LessonId="+entity.LessonId+"";
            return DBHelper.modifyData(sql);
        }
    }
}
