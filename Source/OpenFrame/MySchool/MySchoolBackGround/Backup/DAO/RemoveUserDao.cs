using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class RemoveUserDao
    {
        /// <summary>
        /// 查询所有班级
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuClass(RegiserClassEntity entity)
        {
            string sql = "select * from StuInfo where FKClassId=" + entity.ClassId + " ";
            string tableName = "SearchStuClass";
            return DBHelper.searchData(sql,tableName);
        }
        /// <summary>
        /// 按班级名称查询所属学生姓名
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuName(RegiserClassEntity entity)
        {
            string sql = "select * from StuInfo where FKClassId=" + entity.ClassId + " and StuIsExist=1";
            string tableName = "SearchStuName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 学生转班
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entity1"></param>
        /// <returns></returns>
        public bool updateStuToClass(RegiserUserEntity entity)
        {
            string sql = "update StuInfo set "+
                         "FKClassId='" + entity.UserClassId + "',StuRemoveTime='"+entity.UserRemoveTime+"' " +
                         "where StuLoginName='" + entity.UserLoginName + "'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 将学生转入班级修改为现任班级，之前的班级修改为初始班级
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool updateStuClassInfo(RegiserUserEntity entity)
        {
            string sql = "update StuInfo set " +
                         "StuRemoveClass='" + entity.UserRemoveClass1 + "',StuInitializationClass='"+entity.StuInitializationClass1+"' " +
                         "where StuLoginName='" + entity.UserLoginName + "'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 按学生用户名查询班级
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public DataSet searchClassByStuLoginName(RegiserUserEntity entity)
        {
            string sql = "select ClassId,ClassName from StuInfo inner join ClassInfo " +
                         "on(ClassId=FKClassId) "+
                         "where StuLoginName='"+entity.UserLoginName+"' and StuIsExist=1 ";
            string tableName = "SearchClassByStuLoginName";
            return DBHelper.searchData(sql,tableName);
        }
        public DataSet searchClassByStuLoginName1(RegiserUserEntity entity)
        {
            string sql = "select ClassId,ClassName from StuInfo inner join ClassInfo " +
                         "on(ClassId=FKClassId) " +
                         "where StuLoginName='" + entity.UserLoginName + "' and StuIsExist=1 ";
            string tableName = "SearchClassByStuLoginName1";
            return DBHelper.searchData(sql, tableName);
        }
       /// <summary>
       /// 查询学生转班信息
       /// </summary>
       /// <returns></returns>
         public DataSet searchStuRemoveInfo()
        {
            string sql = " select StuName as 姓名,StuInitializationClass as 原来班级,StuRemoveClass as 调动班级,StuRemoveTime as 调动时间,ClassName as 现在班级"+
                        " from StuInfo inner join ClassInfo"+
                        " on(FKClassId=ClassId)"+
                        " where StuInitializationClass!='空'";
            string tableName = "SearchStuRemoveInfo";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
