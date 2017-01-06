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
        /// ��������ѯһ������Ա(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchFirstLeaveManagerByName(RegiserUserEntity entity)
        {
            string sql = "select ManagerName as ����,ManagerLoginName as �û���,ManagerLoginPassWord as ����,ManagerEnterSchoolTime as ��ְʱ��,ManagerLeaveSchoolTime as ��ְʱ��," +
                         "ManagerIdCard as ���֤��,ManagerAge as ����,ManagerBrithday as ��������,ManagerPhone as ��ϵ�绰,SexName as �Ա�," +
                         "ManagerAddress as ��ͥ��ַ,IdentityName as ��� " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId "+
                         "where  FKIdentityId=5 and ManagerName like '%" + entity.UserName + "%' and ManagerIsExist=1 ";
            string tableName = "SearchFirstLeaveManagerByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��������ѯ��ͨ����Ա(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchManagerByName(RegiserUserEntity entity)
        {
            string sql = "select ManagerName as ����,ManagerLoginName as �û���,ManagerLoginPassWord as ����,ManagerEnterSchoolTime as ��ְʱ��,ManagerLeaveSchoolTime as ��ְʱ��," +
                         "ManagerIdCard as ���֤��,ManagerAge as ����,ManagerBrithday as ��������,ManagerPhone as ��ϵ�绰,SexName as �Ա�,"+
                         "ManagerAddress as ��ͥ��ַ,IdentityName as ��� " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  FKIdentityId=4 and ManagerName like '%" + entity.UserName + "%' and ManagerIsExist=1 ";
            string tableName = "SearchManagerByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��������ѯ������(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherByName(RegiserUserEntity entity)
        {
            string sql = "select  ClassTeacherName as ����,ClassTeacherLoginName as �û���,ClassTeacherLoginPassWord as ����,"+
                         "ClassTeacherEnterSchoolTime as ��ְʱ��,ClassTeacherLeaveSchoolTime as ��ְʱ��,ClassTeacherIdCard as ���֤��," +
                         "ClassTeacherAge as ����,ClassTeacherBrithday as ��������,"+
                         "ClassTeacherPhone as ��ϵ�绰,SexName as �Ա�,ClassTeacherAddress as  ��ͥ��ַ,IdentityName as ��� " +
                         "from ClassTeacherInfo inner join SexInfo "+
                         "on(ClassFKSexId=SexId) "+
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  ClassTeacherName like '%" + entity.UserName + "%' and ClassTeacherIsExist=1 and FKIdentityId=3";
            string tableName = "SearchClassTeacherByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// �������ѯ������(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherByAge(RegiserUserEntity entity)
        {
            string sql = "select  ClassTeacherName as ����,ClassTeacherAge as ����,ClassTeacherLoginName as �û���,ClassTeacherLoginPassWord as ����,"+
                         "ClassTeacherEnterSchoolTime as ��ְʱ��,ClassTeacherLeaveSchoolTime as ��ְʱ��,ClassTeacherIdCard as ���֤��," +
                         "ClassTeacherBrithday as ��������," +
                         "ClassTeacherPhone as ��ϵ�绰,SexName as �Ա�,ClassTeacherAddress as  ��ͥ��ַ,IdentityName as ��� " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  ClassTeacherAge like '%" + entity.UserAge + "%' and ClassTeacherIsExist=1 and FKIdentityId=3 ";
            string tableName = "SearchClassTeacherByAge";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��������ѯ��Ա(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherByName(RegiserUserEntity entity)
        {
            string sql = "select  TeacherName as ����,TeacherLoginName as �û���,TeacherLoginPassWord as ����,"+
                         "TeacherEnterSchoolTime as ��ְʱ��,TeacherLeaveSchoolTime as ��ְʱ��,TeacherIdCard as ���֤��," +
                         "TeacherAge as ����,TeacherBrithday as ��������," +
                         "TeacherPhone as ��ϵ�绰,SexName as �Ա�,TeacherAddress as  ��ͥ��ַ,IdentityName as ��� " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where   TeacherName like '%" + entity.UserName + "%' and TeacherIsExist=1 and FKIdentityId=2";
            string tableName = "SearchTeacherByName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// �������ѯ��Ա(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherByAge(RegiserUserEntity entity)
        {
            string sql = "select  TeacherName as ����,TeacherAge as ����,TeacherLoginName as �û���,TeacherLoginPassWord as ����,"+
                         "TeacherEnterSchoolTime as ��ְʱ��,TeacherLeaveSchoolTime as ��ְʱ��,TeacherIdCard as ���֤��," +
                         "TeacherBrithday as ��������," +
                         "TeacherPhone as ��ϵ�绰,SexName as �Ա�,TeacherAddress as  ��ͥ��ַ,IdentityName as ��� " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  TeacherAge like '%" + entity.UserAge + "%' and TeacherIsExist=1 and FKIdentityId=2";
            string tableName = "SearchTeacherByAge";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��������ѯѧ��(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuByName(RegiserUserEntity entity)
        {
            string sql = "select StuName as ����,StuLoginName as �û���,StuLoginPassWord as ����,StuEnterSchoolTime as ��Уʱ��,StuLeaveSchoolTime as ��Уʱ��,StuIdCard as ���֤��,StuAge as ����,StuBrithday as ��������," +
                         "StuPhone as ��ϵ�绰,SexName as �Ա�,ClassName as �����༶,StuId as ѧ��,StuAddress as ��ͥ��ַ,IdentityName as ��� " +
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
        /// ��ѧ�Ų�ѯѧ��(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuById(RegiserUserEntity entity)
        {
            string sql = "select StuName as ����,StuId as ѧ��,StuLoginName as �û���,StuLoginPassWord as ����,StuEnterSchoolTime as ��Уʱ��,StuLeaveSchoolTime as ��Уʱ��," +
                         "StuIdCard as ���֤��,StuAge as ����,StuBrithday as ��������," +
                         "StuPhone as ��ϵ�绰,SexName as �Ա�,ClassName as �����༶,StuAddress as ��ͥ��ַ,IdentityName as ��� " +
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
        /// �������ѯѧ��(ģ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuByAge(RegiserUserEntity entity)
        {
            string sql = "select StuName as ����,StuAge as ����,StuId as ѧ��,StuLoginName as �û���,StuLoginPassWord as ����,StuEnterSchoolTime as ��Уʱ��,StuLeaveSchoolTime as ��Уʱ��,StuIdCard as ���֤��,StuBrithday as ��������," +
                         "StuPhone as ��ϵ�绰,SexName as �Ա�,ClassName as �����༶,StuAddress as ��ͥ��ַ,IdentityName as ��� " +
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
        /// ��������ѯһ������Ա(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchFirstLeaveManagerByName1()
        {
            string sql = "select ManagerName as ����,ManagerLoginName as �û���,ManagerLoginPassWord as ����,ManagerEnterSchoolTime as ��ְʱ��,ManagerLeaveSchoolTime as ��ְʱ��," +
                         "ManagerIdCard as ���֤��,ManagerAge as ����,ManagerBrithday as ��������,ManagerPhone as ��ϵ�绰,SexName as �Ա�," +
                         "ManagerAddress as ��ͥ��ַ,IdentityName as ��� " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  FKIdentityId=5 and ManagerIsExist=1 ";
            string tableName = "SearchFirstLeaveManagerByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��������ѯ��ͨ����Ա(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchManagerByName1()
        {
            string sql = "select ManagerName as ����,ManagerLoginName as �û���,ManagerLoginPassWord as ����,ManagerEnterSchoolTime as ��ְʱ��,ManagerLeaveSchoolTime as ��ְʱ��," +
                         "ManagerIdCard as ���֤��,ManagerAge as ����,ManagerBrithday as ��������,ManagerPhone as ��ϵ�绰,SexName as �Ա�," +
                         "ManagerAddress as ��ͥ��ַ,IdentityName as ��� " +
                         "from ManagerInfo inner join SexInfo " +
                         "on FKSexId =SexId " +
                         "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  FKIdentityId=4  and ManagerIsExist=1 ";
            string tableName = "SearchManagerByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��������ѯ������(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherByName1()
        {
            string sql = "select  ClassTeacherName as ����,ClassTeacherLoginName as �û���,ClassTeacherLoginPassWord as ����," +
                         "ClassTeacherEnterSchoolTime as ��ְʱ��,ClassTeacherLeaveSchoolTime as ��ְʱ��,ClassTeacherIdCard as ���֤��," +
                         "ClassTeacherAge as ����,ClassTeacherBrithday as ��������," +
                         "ClassTeacherPhone as ��ϵ�绰,SexName as �Ա�,ClassTeacherAddress as  ��ͥ��ַ,IdentityName as ��� " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where ClassTeacherIsExist=1 and FKIdentityId=3";
            string tableName = "SearchClassTeacherByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// �������ѯ������(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchClassTeacherByAge1()
        {
            string sql = "select  ClassTeacherName as ����,ClassTeacherAge as ����,ClassTeacherLoginName as �û���,ClassTeacherLoginPassWord as ����," +
                         "ClassTeacherEnterSchoolTime as ��ְʱ��,ClassTeacherLeaveSchoolTime as ��ְʱ��,ClassTeacherIdCard as ���֤��," +
                         "ClassTeacherBrithday as ��������," +
                         "ClassTeacherPhone as ��ϵ�绰,SexName as �Ա�,ClassTeacherAddress as  ��ͥ��ַ,IdentityName as ��� " +
                         "from ClassTeacherInfo inner join SexInfo " +
                         "on(ClassFKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where where ClassTeacherIsExist=1 and FKIdentityId=3";
            string tableName = "SearchClassTeacherByAge1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��������ѯ��Ա(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherByName1()
        {
            string sql = "select  TeacherName as ����,TeacherLoginName as �û���,TeacherLoginPassWord as ����," +
                         "TeacherEnterSchoolTime as ��ְʱ��,TeacherLeaveSchoolTime as ��ְʱ��,TeacherIdCard as ���֤��," +
                         "TeacherAge as ����,TeacherBrithday as ��������," +
                         "TeacherPhone as ��ϵ�绰,SexName as �Ա�,TeacherAddress as  ��ͥ��ַ,IdentityName as ��� " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  TeacherIsExist=1 and FKIdentityId=2";
            string tableName = "SearchTeacherByName1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// �������ѯ��Ա(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherByAge1()
        {
            string sql = "select  TeacherName as ����,TeacherAge as ����,TeacherLoginName as �û���,TeacherLoginPassWord as ����," +
                         "TeacherEnterSchoolTime as ��ְʱ��,TeacherLeaveSchoolTime as ��ְʱ��,TeacherIdCard as ���֤��," +
                         "TeacherBrithday as ��������," +
                         "TeacherPhone as ��ϵ�绰,SexName as �Ա�,TeacherAddress as  ��ͥ��ַ,IdentityName as ��� " +
                         "from TeacherInfo inner join SexInfo " +
                         "on(FKSexId=SexId) " +
                          "inner join IdentityInfo " +
                         "on FKIdentityId=IdentityId " +
                         "where  TeacherIsExist=1 and FKIdentityId=2";
            string tableName = "SearchTeacherByAge1";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��������ѯѧ��(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuByName1()
        {
            string sql = "select StuName as ����,StuLoginName as �û���,StuLoginPassWord as ����,StuEnterSchoolTime as ��Уʱ��,StuLeaveSchoolTime as ��Уʱ��,StuIdCard as ���֤��,StuAge as ����,StuBrithday as ��������," +
                         "StuPhone as ��ϵ�绰,SexName as �Ա�,ClassName as �����༶,StuId as ѧ��,StuAddress as ��ͥ��ַ,IdentityName as ��� " +
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
        /// ��ѧ�Ų�ѯѧ��(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuById1()
        {
            string sql = "select StuName as ����,StuId as ѧ��,StuLoginName as �û���,StuLoginPassWord as ����,StuEnterSchoolTime as ��Уʱ��,StuLeaveSchoolTime as ��Уʱ��," +
                         "StuIdCard as ���֤��,StuAge as ����,StuBrithday as ��������," +
                         "StuPhone as ��ϵ�绰,SexName as �Ա�,ClassName as �����༶,StuAddress as ��ͥ��ַ,IdentityName as ��� " +
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
        /// �������ѯѧ��(ˢ��)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuByAge1()
        {
            string sql = "select StuName as ����,StuAge as ����,StuId as ѧ��,StuLoginName as �û���,StuLoginPassWord as ����,StuEnterSchoolTime as ��Уʱ��,StuLeaveSchoolTime as ��Уʱ��,StuIdCard as ���֤��,StuBrithday as ��������," +
                         "StuPhone as ��ϵ�绰,SexName as �Ա�,ClassName as �����༶,StuAddress as ��ͥ��ַ,IdentityName as ��� " +
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
