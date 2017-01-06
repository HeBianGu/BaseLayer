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
        public DataSet searchAllUserBySex()
        {
            string sql = "";
            string tableName = "SearchAllUserBySex";
            return DBHelper.searchData(sql,tableName);
        }
        /// <summary>
        /// 按姓名查询一级管理员(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchFirstLeaveManagerByName(RegiserUserEntity entity)
        {
            string sql = "select ManagerName as 姓名,ManagerLoginName as 用户名,ManagerLoginPassWord as 密码,ManagerEnterSchoolTime as 入职时间,ManagerLeaveSchoolTime as 离职时间," +
                         "ManagerIdCard as 身份证号,ManagerAge as 年龄,ManagerBrithday as 出生年月,ManagerPhone as 联系电话,SexName as 性别," +
                         "ManagerAddress as 家庭地址,IdentityName as 身份 " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId "+
                         "where  FKIdentityId=5 and ManagerName like '%" + entity.UserName + "%' and ManagerIsExist=1 ";
            string tableName = "SearchFirstLeaveManagerByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名查询普通管理员(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchManagerByName(RegiserUserEntity entity)
        {
            string sql = "select ManagerName as 姓名,ManagerLoginName as 用户名,ManagerLoginPassWord as 密码,ManagerEnterSchoolTime as 入职时间,ManagerLeaveSchoolTime as 离职时间," +
                         "ManagerIdCard as 身份证号,ManagerAge as 年龄,ManagerBrithday as 出生年月,ManagerPhone as 联系电话,SexName as 性别,"+
                         "ManagerAddress as 家庭地址,IdentityName as 身份 " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  FKIdentityId=4 and ManagerName like '%" + entity.UserName + "%' and ManagerIsExist=1 ";
            string tableName = "SearchManagerByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名查询班主任(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherByName(RegiserUserEntity entity)
        {
            string sql = "select  ClassTeacherName as 姓名,ClassTeacherLoginName as 用户名,ClassTeacherLoginPassWord as 密码,"+
                         "ClassTeacherEnterSchoolTime as 入职时间,ClassTeacherLeaveSchoolTime as 离职时间,ClassTeacherIdCard as 身份证号," +
                         "ClassTeacherAge as 年龄,ClassTeacherBrithday as 出生年月,"+
                         "ClassTeacherPhone as 联系电话,SexName as 性别,ClassTeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from ClassTeacherInfo inner join SexInfo "+
                         "on(ClassFKSexId=SexId) "+
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  ClassTeacherName like '%" + entity.UserName + "%' and ClassTeacherIsExist=1 and FKIdentityId=3";
            string tableName = "SearchClassTeacherByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按年龄查询班主任(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherByAge(RegiserUserEntity entity)
        {
            string sql = "select  ClassTeacherName as 姓名,ClassTeacherAge as 年龄,ClassTeacherLoginName as 用户名,ClassTeacherLoginPassWord as 密码,"+
                         "ClassTeacherEnterSchoolTime as 入职时间,ClassTeacherLeaveSchoolTime as 离职时间,ClassTeacherIdCard as 身份证号," +
                         "ClassTeacherBrithday as 出生年月," +
                         "ClassTeacherPhone as 联系电话,SexName as 性别,ClassTeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  ClassTeacherAge like '%" + entity.UserAge + "%' and ClassTeacherIsExist=1 and FKIdentityId=3 ";
            string tableName = "SearchClassTeacherByAge";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名查询教员(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherByName(RegiserUserEntity entity)
        {
            string sql = "select  TeacherName as 姓名,TeacherLoginName as 用户名,TeacherLoginPassWord as 密码,"+
                         "TeacherEnterSchoolTime as 入职时间,TeacherLeaveSchoolTime as 离职时间,TeacherIdCard as 身份证号," +
                         "TeacherAge as 年龄,TeacherBrithday as 出生年月," +
                         "TeacherPhone as 联系电话,SexName as 性别,TeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where   TeacherName like '%" + entity.UserName + "%' and TeacherIsExist=1 and FKIdentityId=2";
            string tableName = "SearchTeacherByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按年龄查询教员(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherByAge(RegiserUserEntity entity)
        {
            string sql = "select  TeacherName as 姓名,TeacherAge as 年龄,TeacherLoginName as 用户名,TeacherLoginPassWord as 密码,"+
                         "TeacherEnterSchoolTime as 入职时间,TeacherLeaveSchoolTime as 离职时间,TeacherIdCard as 身份证号," +
                         "TeacherBrithday as 出生年月," +
                         "TeacherPhone as 联系电话,SexName as 性别,TeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  TeacherAge like '%" + entity.UserAge + "%' and TeacherIsExist=1 and FKIdentityId=2";
            string tableName = "SearchTeacherByAge";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名查询学生(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuByName(RegiserUserEntity entity)
        {
            string sql = "select StuName as 姓名,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间,StuIdCard as 身份证号,StuAge as 年龄,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,ClassName as 所属班级,StuId as 学号,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo "+
                         "on(FKSexId=SexId) "+
                         "inner join ClassInfo "+
                         "on(FKClassId=ClassId) "+
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  StuName like '%" + entity.UserName + "%' and StuIsExist=1 and FKIdentityId=1";
            string tableName = "SearchStuByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按学号查询学生(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuById(RegiserUserEntity entity)
        {
            string sql = "select StuName as 姓名,StuId as 学号,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间," +
                         "StuIdCard as 身份证号,StuAge as 年龄,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,ClassName as 所属班级,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  StuId like '%" + entity.UserId + "%' and StuIsExist=1 and FKIdentityId=1";
            string tableName = "SearchStuById";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按年龄查询学生(模糊)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuByAge(RegiserUserEntity entity)
        {
            string sql = "select StuName as 姓名,StuAge as 年龄,StuId as 学号,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间,StuIdCard as 身份证号,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,ClassName as 所属班级,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  StuAge like '%" + entity.UserAge + "%' and StuIsExist=1 and FKIdentityId=1";
            string tableName = "SearchStuByAge";
            return DBHelper.searchData(sql, tableName);
        }

        /// <summary>
        /// 按姓名查询一级管理员(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchFirstLeaveManagerByName1()
        {
            string sql = "select ManagerName as 姓名,ManagerLoginName as 用户名,ManagerLoginPassWord as 密码,ManagerEnterSchoolTime as 入职时间,ManagerLeaveSchoolTime as 离职时间," +
                         "ManagerIdCard as 身份证号,ManagerAge as 年龄,ManagerBrithday as 出生年月,ManagerPhone as 联系电话,SexName as 性别," +
                         "ManagerAddress as 家庭地址,IdentityName as 身份 " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  FKIdentityId=5 and ManagerIsExist=1 ";
            string tableName = "SearchFirstLeaveManagerByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名查询普通管理员(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchManagerByName1()
        {
            string sql = "select ManagerName as 姓名,ManagerLoginName as 用户名,ManagerLoginPassWord as 密码,ManagerEnterSchoolTime as 入职时间,ManagerLeaveSchoolTime as 离职时间," +
                         "ManagerIdCard as 身份证号,ManagerAge as 年龄,ManagerBrithday as 出生年月,ManagerPhone as 联系电话,SexName as 性别," +
                         "ManagerAddress as 家庭地址,IdentityName as 身份 " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  FKIdentityId=4  and ManagerIsExist=1 ";
            string tableName = "SearchManagerByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名查询班主任(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherByName1()
        {
            string sql = "select  ClassTeacherName as 姓名,ClassTeacherLoginName as 用户名,ClassTeacherLoginPassWord as 密码," +
                         "ClassTeacherEnterSchoolTime as 入职时间,ClassTeacherLeaveSchoolTime as 离职时间,ClassTeacherIdCard as 身份证号," +
                         "ClassTeacherAge as 年龄,ClassTeacherBrithday as 出生年月," +
                         "ClassTeacherPhone as 联系电话,SexName as 性别,ClassTeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where ClassTeacherIsExist=1 and FKIdentityId=3";
            string tableName = "SearchClassTeacherByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按年龄查询班主任(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherByAge1()
        {
            string sql = "select  ClassTeacherName as 姓名,ClassTeacherAge as 年龄,ClassTeacherLoginName as 用户名,ClassTeacherLoginPassWord as 密码," +
                         "ClassTeacherEnterSchoolTime as 入职时间,ClassTeacherLeaveSchoolTime as 离职时间,ClassTeacherIdCard as 身份证号," +
                         "ClassTeacherBrithday as 出生年月," +
                         "ClassTeacherPhone as 联系电话,SexName as 性别,ClassTeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where where ClassTeacherIsExist=1 and FKIdentityId=3";
            string tableName = "SearchClassTeacherByAge1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名查询教员(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherByName1()
        {
            string sql = "select  TeacherName as 姓名,TeacherLoginName as 用户名,TeacherLoginPassWord as 密码," +
                         "TeacherEnterSchoolTime as 入职时间,TeacherLeaveSchoolTime as 离职时间,TeacherIdCard as 身份证号," +
                         "TeacherAge as 年龄,TeacherBrithday as 出生年月," +
                         "TeacherPhone as 联系电话,SexName as 性别,TeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  TeacherIsExist=1 and FKIdentityId=2";
            string tableName = "SearchTeacherByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按年龄查询教员(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherByAge1()
        {
            string sql = "select  TeacherName as 姓名,TeacherAge as 年龄,TeacherLoginName as 用户名,TeacherLoginPassWord as 密码," +
                         "TeacherEnterSchoolTime as 入职时间,TeacherLeaveSchoolTime as 离职时间,TeacherIdCard as 身份证号," +
                         "TeacherBrithday as 出生年月," +
                         "TeacherPhone as 联系电话,SexName as 性别,TeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  TeacherIsExist=1 and FKIdentityId=2";
            string tableName = "SearchTeacherByAge1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名查询学生(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuByName1()
        {
            string sql = "select StuName as 姓名,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间,StuIdCard as 身份证号,StuAge as 年龄,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,ClassName as 所属班级,StuId as 学号,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where FKIdentityId=1 and StuIsExist=1 ";
            string tableName = "SearchStuByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按学号查询学生(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuById1()
        {
            string sql = "select StuName as 姓名,StuId as 学号,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间," +
                         "StuIdCard as 身份证号,StuAge as 年龄,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,ClassName as 所属班级,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where FKIdentityId=1 and StuIsExist=1 ";
            string tableName = "SearchStuById1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按年龄查询学生(刷新)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuByAge1()
        {
            string sql = "select StuName as 姓名,StuAge as 年龄,StuId as 学号,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间,StuIdCard as 身份证号,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,ClassName as 所属班级,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where FKIdentityId=1 and StuIsExist=1 ";
            string tableName = "SearchStuByAge1";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
