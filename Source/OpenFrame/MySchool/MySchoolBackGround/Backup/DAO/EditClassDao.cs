using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class EditClassDao
    {
        /// <summary>
        /// É¾³ý°à¼¶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool deleteClassByName(RegiserClassEntity entity)
        {
            string sql = "update ClassInfo set ClassIsExist=0 where ClassName='"+entity.ClassName+"'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ÐÞ¸Ä°à¼¶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editClassInfo(RegiserClassEntity entity)
        {
            string sql = "update ClassInfo set ClassName='" + entity.ClassName + "',"+
                         "ClassFinishTime='" + entity.ClassFinishTime + "',ClassStuNum='"+entity.ClassStuNum+"',"+
                         "FKClassTeacherId='" + entity.ClassTeacherId + "',FKTeacherId='"+entity.TeacherId+"'"+
                         "where ClassName='"+entity.ChooseClassName+"'";
            return DBHelper.modifyData(sql);
        }

    }
}
