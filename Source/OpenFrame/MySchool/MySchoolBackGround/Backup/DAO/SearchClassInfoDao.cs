using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class SearchClassInfoDao
    {
        /// <summary>
        /// 按班名查询班级
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassByName(RegiserClassEntity entity)
        {
            string sql = "select ClassName as 班级名称,ClassStartTime as 开班时间,ClassFinishTime as 结班时间,ClassStuNum as 班级人数," +
                         "ClassTeacherName as 班主任,TeacherName as 教师 "+
                         "from ClassInfo inner join ClassTeacherInfo "+
                         "on(FKClassTeacherId=ClassTeacherId) "+
                         "inner join TeacherInfo on(FKTeacherId=TeacherId) "+
                         "where ClassName like '%" + entity.ClassSearchContent + "%' and ClassIsExist=1";
            string tableName = "SearchClassByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按班级人数查询班级
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassByStuNum(RegiserClassEntity entity)
        {
            string sql = "select ClassTeacherName as 班主任,ClassName as 班级名称,ClassStartTime as 开班时间,ClassFinishTime as 结班时间,ClassStuNum as 班级人数 " +
                         "from ClassInfo inner join ClassTeacherInfo " +
                         "on(FKClassTeacherId=ClassTeacherId) " +
                         "where ClassStuNum like '%" + entity.ClassStuNum + "%' and ClassIsExist=1";
            string tableName = "SearchClassByStuNum";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按班级编号查询班级
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassById(RegiserClassEntity entity)
        {
            string sql = "select TeacherName as 教员,ClassName as 班级名称,ClassStartTime as 开班时间,ClassFinishTime as 结班时间,ClassStuNum as 班级人数 " +
                        "from ClassInfo inner join TeacherInfo " +
                        "on(FKTeacherId=TeacherId) " +
                        "where ClassId like '%" + entity.ClassId + "%' and ClassIsExist=1";
            string tableName = "SearchClassById";
            return DBHelper.searchData(sql, tableName);
        }
        ///// <summary>
        ///// 查询所有班级
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public DataSet searchAllClass(RegiserClassEntity entity)
        //{
        //    string sql = "select ClassName as 班级名称,ClassStartTime as 开班时间,ClassStuNum as 班级人数," +
        //                 "ClassTeacherName as 班主任,TeacherName as 教师 " +
        //                 "from ClassInfo inner join ClassTeacherInfo " +
        //                 "on(FKClassTeacherId=ClassTeacherId) " +
        //                 "inner join TeacherInfo on(FKTeacherId=TeacherId) ";
        //    string tableName = "SearchAllClass";
        //    return DBHelper.searchData(sql, tableName);
        //}


    }
}
