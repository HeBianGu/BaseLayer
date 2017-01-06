using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using Entity;

namespace DAO
{
    public class RegiserClassDao
    {
        /// <summary>
        /// ×¢²áÐÂ°à¼¶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool insertClassInfo(RegiserClassEntity entity)
        {
            string sql = "insert into ClassInfo values('" + entity.ClassName + "'," +
            " '" + entity.ClassStartTime + "','"+entity.ClassFinishTime+"'," + entity.ClassStuNum + "," +
            " "+entity.ClassTeacherId+"," + entity.TeacherId + ","+entity.ClassIsExist+")";
            return DBHelper.modifyData(sql);
        }
    }
}
