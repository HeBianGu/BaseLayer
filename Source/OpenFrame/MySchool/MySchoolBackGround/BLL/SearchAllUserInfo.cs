using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using Entity;

namespace BLL
{
    public class SearchAllUserInfo
    {
        /// <summary>
        /// 查询所有管理员
        /// </summary>
        /// <returns></returns>
        public DataSet searchAllManage()
        {
            string sql = "select ManagerName as 姓名,ManagerLoginName as 用户名,ManagerLoginPassWord as 密码,ManagerEnterSchoolTime as 入职时间,ManagerLeaveSchoolTime as 离职时间," +
                         "ManagerIdCard as 身份证号,ManagerAge as 年龄,ManagerBrithday as 出生年月,ManagerPhone as 联系电话,SexName as 性别,"+
                         "ManagerAddress as 家庭地址,IdentityName as 身份 " +
                         "from ManagerInfo inner join SexInfo "+
                         "on FKSexId =SexId "+
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId "+
                         "where ManagerIsExist=1";
            string tableName = "SearchAllManage";
            return DBHelper.searchData(sql,tableName);
        }
        /// <summary>
        /// 查询所有教员
        /// </summary>
        /// <returns></returns>
        public DataSet searchAllTeacher()
        {
            string sql = "select  TeacherName as 姓名,TeacherLoginName as 用户名,TeacherLoginPassWord as 密码," +
                         "TeacherEnterSchoolTime as 入职时间,TeacherLeaveSchoolTime as 离职时间,TeacherIdCard as 身份证号," +
                         "TeacherAge as 年龄,TeacherBrithday as 出生年月," +
                         "TeacherPhone as 联系电话,SexName as 性别,TeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where TeacherIsExist=1";
            string tableName = "SearchAllTeacher";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有班主任
        /// </summary>
        /// <returns></returns>
        public DataSet searchAllClassTeacher()
        {
            string sql = "select  ClassTeacherName as 姓名,ClassTeacherLoginName as 用户名,ClassTeacherLoginPassWord as 密码," +
                         "ClassTeacherEnterSchoolTime as 入职时间,ClassTeacherLeaveSchoolTime as 离职时间,ClassTeacherIdCard as 身份证号," +
                         "ClassTeacherAge as 年龄,ClassTeacherBrithday as 出生年月," +
                         "ClassTeacherPhone as 联系电话,SexName as 性别,ClassTeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where ClassTeacherIsExist=1";
            string tableName = "SearchAllClassTeacher";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有学生
        /// </summary>
        /// <returns></returns>
        public DataSet searchAllStudent()
        {
            string sql = "select StuName as 姓名,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间,StuIdCard as 身份证号,StuAge as 年龄,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where StuIsExist=1";
            string tableName = "SearchAllStudent";
            return DBHelper.searchData(sql, tableName);
        }



        /// <summary>
        /// 查询所有男学生
        /// </summary>
        /// <returns></returns>
        public DataSet searchMaleStudent(RegiserUserEntity entity)
        {
            string sql = "select StuName as 姓名,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间,StuIdCard as 身份证号,StuAge as 年龄,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where FKSexId=0 and StuName like '%" + entity.UserName + "%' and StuIsExist=1";
            string tableName = "SearchMaleStudent";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有女学生
        /// </summary>
        /// <returns></returns>
        public DataSet searchFemaleStudent(RegiserUserEntity entity)
        {
            string sql = "select StuName as 姓名,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间,StuIdCard as 身份证号,StuAge as 年龄,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where FKSexId=1 and StuName like '%" + entity.UserName + "%' and StuIsExist=1";
            string tableName = "SearchFemaleStudent";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有男班主任
        /// </summary>
        /// <returns></returns>
        public DataSet searchMaleClassTeacher(RegiserUserEntity entity)
        {
            string sql = "select  ClassTeacherName as 姓名,ClassTeacherLoginName as 用户名,ClassTeacherLoginPassWord as 密码," +
                         "ClassTeacherEnterSchoolTime as 入职时间,ClassTeacherLeaveSchoolTime as 离职时间,ClassTeacherIdCard as 身份证号," +
                         "ClassTeacherAge as 年龄,ClassTeacherBrithday as 出生年月," +
                         "ClassTeacherPhone as 联系电话,SexName as 性别,ClassTeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where ClassFKSexId=0 and ClassTeacherName like '%" + entity.UserName + "%' and ClassTeacherIsExist=1";
            string tableName = "SearchMaleClassTeacher";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有女班主任
        /// </summary>
        /// <returns></returns>
        public DataSet searchFemaleClassTeacher(RegiserUserEntity entity)
        {
            string sql = "select  ClassTeacherName as 姓名,ClassTeacherLoginName as 用户名,ClassTeacherLoginPassWord as 密码," +
                         "ClassTeacherEnterSchoolTime as 入职时间,ClassTeacherLeaveSchoolTime as 离职时间,ClassTeacherIdCard as 身份证号," +
                         "ClassTeacherAge as 年龄,ClassTeacherBrithday as 出生年月," +
                         "ClassTeacherPhone as 联系电话,SexName as 性别,ClassTeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where ClassFKSexId=1 and ClassTeacherName like '%" + entity.UserName + "%' and ClassTeacherIsExist=1";
            string tableName = "SearchFemaleClassTeacher";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有男教员
        /// </summary>
        /// <returns></returns>
        public DataSet searchMaleTeacher(RegiserUserEntity entity)
        {
            string sql = "select  TeacherName as 姓名,TeacherLoginName as 用户名,TeacherLoginPassWord as 密码," +
                         "TeacherEnterSchoolTime as 入职时间,TeacherLeaveSchoolTime as 离职时间,TeacherIdCard as 身份证号," +
                         "TeacherAge as 年龄,TeacherBrithday as 出生年月," +
                         "TeacherPhone as 联系电话,SexName as 性别,TeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where FKSexId=0 and TeacherName like '%" + entity.UserName + "%' and TeacherIsExist=1";
            string tableName = "SearchMaleTeacher";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有女教员
        /// </summary>
        /// <returns></returns>
        public DataSet searchFemaleTeacher(RegiserUserEntity entity)
        {
            string sql = "select  TeacherName as 姓名,TeacherLoginName as 用户名,TeacherLoginPassWord as 密码," +
                         "TeacherEnterSchoolTime as 入职时间,TeacherLeaveSchoolTime as 离职时间,TeacherIdCard as 身份证号," +
                         "TeacherAge as 年龄,TeacherBrithday as 出生年月," +
                         "TeacherPhone as 联系电话,SexName as 性别,TeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where FKSexId=1 and TeacherName like '%" + entity.UserName + "%' and TeacherIsExist=1";
            string tableName = "SearchFemaleTeacher";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有男管理员
        /// </summary>
        /// <returns></returns>
        public DataSet searchMaleManage(RegiserUserEntity entity)
        {
            string sql = "select ManagerName as 姓名,ManagerLoginName as 用户名,ManagerLoginPassWord as 密码,ManagerEnterSchoolTime as 入职时间,ManagerLeaveSchoolTime as 离职时间," +
                         "ManagerIdCard as 身份证号,ManagerAge as 年龄,ManagerBrithday as 出生年月,ManagerPhone as 联系电话,SexName as 性别," +
                         "ManagerAddress as 家庭地址,IdentityName as 身份 " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId "+
                         "where FKSexId=0 and ManagerName like '%" + entity.UserName + "%' and ManagerIsExist=1";
            string tableName = "SearchMaleManage";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 查询所有女管理员
        /// </summary>
        /// <returns></returns>
        public DataSet searchFemaleManage(RegiserUserEntity entity)
        {
            string sql = "select ManagerName as 姓名,ManagerLoginName as 用户名,ManagerLoginPassWord as 密码,ManagerEnterSchoolTime as 入职时间,ManagerLeaveSchoolTime as 离职时间," +
                         "ManagerIdCard as 身份证号,ManagerAge as 年龄,ManagerBrithday as 出生年月,ManagerPhone as 联系电话,SexName as 性别," +
                         "ManagerAddress as 家庭地址,IdentityName as 身份 " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where FKSexId=1 and ManagerName like '%" + entity.UserName + "%' and ManagerIsExist=1";
            string tableName = "SearchFemaleManage";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名模糊查询管理员
        /// </summary>
        /// <returns></returns>
        public DataSet searchAllManageBlur(RegiserUserEntity entity)
        {
            string sql = "select ManagerName as 姓名,ManagerLoginName as 用户名,ManagerLoginPassWord as 密码,ManagerEnterSchoolTime as 入职时间,ManagerLeaveSchoolTime as 离职时间," +
                         "ManagerIdCard as 身份证号,ManagerAge as 年龄,ManagerBrithday as 出生年月,ManagerPhone as 联系电话,SexName as 性别," +
                         "ManagerAddress as 家庭地址,IdentityName as 身份 " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId "+
                         "where ManagerName like '%" + entity.UserName + "%' and ManagerIsExist=1";
            string tableName = "SearchAllManageBlur";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名模糊查询所有班主任
        /// </summary>
        /// <returns></returns>
        public DataSet searchAllClassTeacherBlur(RegiserUserEntity entity)
        {
            string sql = "select  ClassTeacherName as 姓名,ClassTeacherLoginName as 用户名,ClassTeacherLoginPassWord as 密码," +
                         "ClassTeacherEnterSchoolTime as 入职时间,ClassTeacherLeaveSchoolTime as 离职时间,ClassTeacherIdCard as 身份证号," +
                         "ClassTeacherAge as 年龄,ClassTeacherBrithday as 出生年月," +
                         "ClassTeacherPhone as 联系电话,SexName as 性别,ClassTeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where ClassTeacherName like '%" + entity.UserName + "%' and ClassTeacherIsExist=1";
            string tableName = "SearchAllClassTeacherBlur";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// 按姓名模糊查询所有教员
        /// </summary>
        /// <returns></returns>
        public DataSet searchAllTeacherBlur(RegiserUserEntity entity)
        {
            string sql = "select  TeacherName as 姓名,TeacherLoginName as 用户名,TeacherLoginPassWord as 密码," +
                         "TeacherEnterSchoolTime as 入职时间,TeacherLeaveSchoolTime as 离职时间,TeacherIdCard as 身份证号," +
                         "TeacherAge as 年龄,TeacherBrithday as 出生年月," +
                         "TeacherPhone as 联系电话,SexName as 性别,TeacherAddress as  家庭地址,IdentityName as 身份 " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where TeacherName like '%" + entity.UserName + "%' and TeacherIsExist=1";
            string tableName = "SearchAllTeacherBlur";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        ///  按姓名模糊查询所有学生
        /// </summary>
        /// <returns></returns>
        public DataSet searchAllStudentBlur(RegiserUserEntity entity)
        {
            string sql = "select StuName as 姓名,StuLoginName as 用户名,StuLoginPassWord as 密码,StuEnterSchoolTime as 入校时间,StuLeaveSchoolTime as 离校时间,StuIdCard as 身份证号,StuAge as 年龄,StuBrithday as 出生年月," +
                         "StuPhone as 联系电话,SexName as 性别,StuAddress as 家庭地址,IdentityName as 身份 " +
                         "from StuInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                         "inner join ClassInfo " +
                         "on(FKClassId=ClassId) " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where StuName like '%" + entity.UserName + "%' and StuIsExist=1";
            string tableName = "SearchAllStudentBlur";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
