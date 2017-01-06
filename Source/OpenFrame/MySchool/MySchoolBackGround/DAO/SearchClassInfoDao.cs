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
        /// ��������ѯ�༶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassByName(RegiserClassEntity entity)
        {
            string sql = "select ClassName as �༶����,ClassStartTime as ����ʱ��,ClassFinishTime as ���ʱ��,ClassStuNum as �༶����," +
                         "ClassTeacherName as ������,TeacherName as ��ʦ "+
                         "from ClassInfo inner join ClassTeacherInfo "+
                         "on(FKClassTeacherId=ClassTeacherId) "+
                         "inner join TeacherInfo on(FKTeacherId=TeacherId) "+
                         "where ClassName like '%" + entity.ClassSearchContent + "%' and ClassIsExist=1";
            string tableName = "SearchClassByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ���༶������ѯ�༶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassByStuNum(RegiserClassEntity entity)
        {
            string sql = "select ClassTeacherName as ������,ClassName as �༶����,ClassStartTime as ����ʱ��,ClassFinishTime as ���ʱ��,ClassStuNum as �༶���� " +
                         "from ClassInfo inner join ClassTeacherInfo " +
                         "on(FKClassTeacherId=ClassTeacherId) " +
                         "where ClassStuNum like '%" + entity.ClassStuNum + "%' and ClassIsExist=1";
            string tableName = "SearchClassByStuNum";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ���༶��Ų�ѯ�༶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassById(RegiserClassEntity entity)
        {
            string sql = "select TeacherName as ��Ա,ClassName as �༶����,ClassStartTime as ����ʱ��,ClassFinishTime as ���ʱ��,ClassStuNum as �༶���� " +
                        "from ClassInfo inner join TeacherInfo " +
                        "on(FKTeacherId=TeacherId) " +
                        "where ClassId like '%" + entity.ClassId + "%' and ClassIsExist=1";
            string tableName = "SearchClassById";
            return DBHelper.searchData(sql, tableName);
        }
        ///// <summary>
        ///// ��ѯ���а༶
        ///// </summary>
        ///// <param name="entity"></param>
        ///// <returns></returns>
        //public DataSet searchAllClass(RegiserClassEntity entity)
        //{
        //    string sql = "select ClassName as �༶����,ClassStartTime as ����ʱ��,ClassStuNum as �༶����," +
        //                 "ClassTeacherName as ������,TeacherName as ��ʦ " +
        //                 "from ClassInfo inner join ClassTeacherInfo " +
        //                 "on(FKClassTeacherId=ClassTeacherId) " +
        //                 "inner join TeacherInfo on(FKTeacherId=TeacherId) ";
        //    string tableName = "SearchAllClass";
        //    return DBHelper.searchData(sql, tableName);
        //}


    }
}
