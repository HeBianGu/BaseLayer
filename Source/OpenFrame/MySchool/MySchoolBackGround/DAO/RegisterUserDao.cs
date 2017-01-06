using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class RegisterUserDao
    {
        /// <summary>
        /// 注册学生
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool insertStudentInfo(RegiserUserEntity entity)
        {
            string sql = "insert into stuInfo values('" + entity.UserLoginName + "'," +
            "'" + entity.UserLoginPwd + "','" + entity.UserEnterSchoolTime + "','"+entity.UserLeaveSchoolTime+"'," +
            "'" + entity.UserName + "','" + entity.UserIdCard + "'," + entity.UserAge + ",'" + entity.UserBrithday + "'," +
            " " + entity.UserPhone + "," + entity.UserSex + "," + entity.UserClassId + "," +
            "" + entity.UserId + ",'" + entity.UserAddress + "','"+entity.UserRemoveTime+"','"+entity.UserRemoveClass1+"'," + entity.UserIdentityId + ","+entity.StuIsExist1+",'"+entity.StuInitializationClass1+"')";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 注册普通教员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool insertTeacherInfo(RegiserUserEntity entity)
        {
            string sql = "insert into teacherInfo values('" + entity.UserLoginName + "'," +
            "'" + entity.UserLoginPwd + "','" + entity.UserEnterSchoolTime + "','" + entity.UserLeaveSchoolTime + "'," +
            "'" + entity.UserName + "','" + entity.UserIdCard + "'," + entity.UserAge + ",'" + entity.UserBrithday + "','" + entity.UserPhone + "'," +
            "" + entity.UserSex + ",'" + entity.UserAddress + "','" + entity.UserRemoveTime + "','" + entity.UserRemoveClass1 + "'," + entity.UserIdentityId + "," + entity.TeacherIsExist1 + ")";     
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 注册班主任
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool insertClassTeacherInfo(RegiserUserEntity entity)
        {
            string sql = "insert into ClassTeacherInfo values('" + entity.UserLoginName + "'," +
            "'" + entity.UserLoginPwd + "','" + entity.UserEnterSchoolTime + "','" + entity.UserLeaveSchoolTime + "'," +
            "'" + entity.UserName + "','" + entity.UserIdCard + "'," + entity.UserAge + ",'" + entity.UserBrithday + "','" + entity.UserPhone + "'," +
            "" + entity.UserSex + ",'" + entity.UserAddress + "','" + entity.UserRemoveTime + "','" + entity.UserRemoveClass1 + "'," + entity.UserIdentityId + "," + entity.TeacherIsExist1 + ")";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 注册管理员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool insertManagerInfo(RegiserUserEntity entity)
        {
            string sql = "insert into ManagerInfo values('" + entity.UserLoginName + "'," +
            "'" + entity.UserLoginPwd + "','" + entity.UserEnterSchoolTime + "','" + entity.UserLeaveSchoolTime + "'," +
            "'" + entity.UserName + "','" + entity.UserIdCard + "'," + entity.UserAge + ",'" + entity.UserBrithday + "'," + entity.UserPhone + "," +
            "" + entity.UserSex + ",'" + entity.UserAddress + "'," + entity.UserIdentityId + ","+entity.ManagerIsExist1+")";
            return DBHelper.modifyData(sql);
        }
    }
}