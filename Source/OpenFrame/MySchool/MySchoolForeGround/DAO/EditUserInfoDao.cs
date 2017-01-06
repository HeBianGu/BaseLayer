using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Entity;
using Utility;

namespace DAO
{
    public class EditUserInfoDao
    {
        /// <summary>
        /// 修改学生密码
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editStudent(UserInfoEntity entity)
        {
            string sql = "update StuInfo set StuLoginPassWord='" + entity.UserLoginPwd + "' "+
                         "where StuLoginName='"+entity.UserLoginName+"'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 修改班主任
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editClassTeacher(UserInfoEntity entity)
        {
            string sql = "update ClassTeacherInfo set ClassTeacherLoginPassWord='"+entity.UserLoginPwd+"',ClassTeacherEnterSchoolTime='"+entity.UserEnterSchoolTime+"',"+
                         "ClassTeacherName='"+entity.UserName+"',ClassTeacherIdCard='"+entity.UserIdCard+"',ClassTeacherAge="+entity.UserAge+",ClassTeacherBrithday='"+entity.UserBrithday+"',"+
                         "ClassTeacherPhone='"+entity.UserPhone+"',ClassFKSexId="+entity.UserSex+",ClassTeacherAddress='"+entity.UserAddress+"' "+
                         "where ClassTeacherLoginName='"+entity.UserLoginName+"'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 修改教员
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editTeacher(UserInfoEntity entity)
        {
            string sql = "update TeacherInfo set TeacherLoginPassWord='" + entity.UserLoginPwd + "',TeacherEnterSchoolTime='" + entity.UserEnterSchoolTime + "'," +
                         "TeacherName='" + entity.UserName + "',TeacherIdCard='" + entity.UserIdCard + "',TeacherAge=" + entity.UserAge + ",TeacherBrithday='" + entity.UserBrithday + "'," +
                         "TeacherPhone='" + entity.UserPhone + "',FKSexId=" + entity.UserSex + ",TeacherAddress='" + entity.UserAddress + "' " +
                         "where TeacherLoginName='" + entity.UserLoginName + "'";
            return DBHelper.modifyData(sql);
        }
        
    }
}
