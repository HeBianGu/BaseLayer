using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class EditUserInfoDao
    {
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool deleteStuByLoginName(RegiserUserEntity entity)
        {
            string sql = "update StuInfo set StuIsExist=0 where StuLoginName='"+entity.UserLoginName+"' ";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 删除教员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool deleteTeacherByLoginName(RegiserUserEntity entity)
        {
            string sql = "update TeacherInfo set TeacherIsExist=0 where TeacherLoginName='" + entity.UserLoginName + "' ";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 删除班主任
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool deleteClassTeacherByLoginName(RegiserUserEntity entity)
        {
            string sql = "update ClassTeacherInfo set ClassTeacherIsExist=0 where ClassTeacherLoginName='" + entity.UserLoginName + "' ";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 删除两种管理员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool deleteManagerByLoginName(RegiserUserEntity entity)
        {
            string sql = "update ManagerInfo set ManagerIsExist=0 where ManagerLoginName='" + entity.UserLoginName + "' ";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 修改管理员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editManagerByLoginName(RegiserUserEntity entity)
        {
            string sql = "update ManagerInfo set ManagerLoginName='" + entity.UserLoginName + "',ManagerLoginPassWord='" + entity.UserLoginPwd + "'," +
            "ManagerEnterSchoolTime='" + entity.UserEnterSchoolTime + "',ManagerLeaveSchoolTime='" + entity.UserLeaveSchoolTime + "',ManagerName='" + entity.UserName + "',ManagerIdCard='" + entity.UserIdCard + "'," +
            "ManagerAge=" + entity.UserAge + ",ManagerBrithday='" + entity.UserBrithday + "',ManagerPhone=" + entity.UserPhone + ",FKSexId=" + entity.UserSex + "," +
            "ManagerAddress='" + entity.UserAddress + "',FKIdentityId=" + entity.UserIdentityId + "" +
            "where ManagerLoginName='"+entity.ChooseUserLoginName+"'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 修改班主任
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editClassTeacherByLoginName(RegiserUserEntity entity)
        {
            string sql = "update ClassTeacherInfo set ClassTeacherLoginName='" + entity.UserLoginName + "',ClassTeacherLoginPassWord='" + entity.UserLoginPwd + "'," +
            "ClassTeacherEnterSchoolTime='" + entity.UserEnterSchoolTime + "',ClassTeacherLeaveSchoolTime='" + entity.UserLeaveSchoolTime + "',ClassTeacherName='" + entity.UserName + "',ClassTeacherIdCard='" + entity.UserIdCard + "'," +
            "ClassTeacherAge=" + entity.UserAge + ",ClassTeacherBrithday='" + entity.UserBrithday + "',ClassTeacherPhone=" + entity.UserPhone + ",ClassFKSexId=" + entity.UserSex + "," +
            "ClassTeacherAddress='" + entity.UserAddress + "',FKIdentityId=" + entity.UserIdentityId + "" +
            "where ClassTeacherLoginName='" + entity.ChooseUserLoginName + "'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 修改教师
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editTeacherByLoginName(RegiserUserEntity entity)
        {
            string sql = "update TeacherInfo set TeacherLoginName='" + entity.UserLoginName + "',TeacherLoginPassWord='" + entity.UserLoginPwd + "'," +
            "TeacherEnterSchoolTime='" + entity.UserEnterSchoolTime + "',TeacherLeaveSchoolTime='" + entity.UserLeaveSchoolTime + "',TeacherName='" + entity.UserName + "',TeacherIdCard='" + entity.UserIdCard + "'," +
            "TeacherAge=" + entity.UserAge + ",TeacherBrithday='" + entity.UserBrithday + "',TeacherPhone=" + entity.UserPhone + ",FKSexId=" + entity.UserSex + "," +
            "TeacherAddress='" + entity.UserAddress + "',FKIdentityId=" + entity.UserIdentityId + "" +
            "where TeacherLoginName='" + entity.ChooseUserLoginName + "'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editStudentByLoginName(RegiserUserEntity entity)
        {
            string sql = "update StuInfo set StuLoginName='" + entity.UserLoginName + "',StuLoginPassWord='" + entity.UserLoginPwd + "'," +
            "StuEnterSchoolTime='" + entity.UserEnterSchoolTime + "',StuLeaveSchoolTime='" + entity.UserLeaveSchoolTime + "',StuName='" + entity.UserName + "',StuIdCard='" + entity.UserIdCard + "'," +
            "StuAge=" + entity.UserAge + ",StuBrithday='" + entity.UserBrithday + "',StuPhone=" + entity.UserPhone + ",FKSexId=" + entity.UserSex + "," +
            "FKClassId=" + entity.UserClassId + ",StuId="+entity.UserId+"" +
            "StuAddress='" + entity.UserAddress + "',FKIdentityId=" + entity.UserIdentityId + "" +
            "where StuLoginName='" + entity.ChooseUserLoginName + "'";
            return DBHelper.modifyData(sql);
        }


    }
}
