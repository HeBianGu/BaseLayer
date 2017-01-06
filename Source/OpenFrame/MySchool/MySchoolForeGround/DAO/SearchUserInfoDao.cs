using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class SearchUserInfoDao
    {
        /// <summary>
        /// 查询学生信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuInfo(UserInfoEntity entity)
        {
            string sql = "select StuLoginPassWord,StuEnterSchoolTime,StuName,StuIdCard,StuAge,"+
                         "StuBrithday,StuPhone,SexName,ClassName,StuId,StuAddress "+
                         "from StuInfo inner join SexInfo "+
                         "on  FKSexId=SexId "+
                         "inner join ClassInfo "+
                         "on FKClassId=ClassId "+
                         "where StuLoginName='"+entity.UserLoginName+"'";
            string tableName = "SearchStuInfo";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询班主任信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherInfo(UserInfoEntity entity)
        {
            string sql = "select ClassTeacherLoginPassWord,ClassTeacherEnterSchoolTime," +
                         "ClassTeacherName,ClassTeacherIdCard,ClassTeacherAge,ClassTeacherBrithday,ClassTeacherPhone,SexName," +
                         "ClassTeacherAddress "+
                         "from ClassTeacherInfo inner join SexInfo "+
                         "on ClassFKSexId=SexId " +
                         "where ClassTeacherLoginName='" + entity.UserLoginName + "'";
            string tableName = "SearchClassTeacherInfo";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询教员信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherInfo(UserInfoEntity entity)
        {
            string sql = "select TeacherLoginPassWord,TeacherEnterSchoolTime," +
                         "TeacherName,TeacherIdCard,TeacherAge,TeacherBrithday,TeacherPhone,SexName," +
                         "TeacherAddress " +
                         "from TeacherInfo inner join SexInfo " +
                         "on FKSexId=SexId " +
                         "where TeacherLoginName='" + entity.UserLoginName + "'";
            string tableName = "SearchTeacherInfo";
            return DBHelper.searchData(sql, tableName);
        }
    
    }
}
