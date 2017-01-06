using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using System.Data;
using Entity;

namespace DAO
{
    public class LoginDao
    {
        /// <summary>
        /// 学生登入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool LoginStuInfo(UserInfoEntity entity)
        {
            string sql = string.Format("select count(*) from StuInfo where StuLoginName='{0}'"+
            " and StuLoginPassWord='{1}' and StuIsExist={2}" +
            "",entity.UserLoginName,entity.UserLoginPwd,entity.StuIsExist1);
            return DBHelper.searchIsLoginInfo(sql);
        }
        /// <summary>
        /// 教员登入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool LoginTeacherInfo(UserInfoEntity entity)
        {
            string sql = string.Format("select count(*) from TeacherInfo where TeacherLoginName='{0}'" +
            " and TeacherLoginPassWord='{1}' and TeacherIsExist={2}" +
            "", entity.UserLoginName, entity.UserLoginPwd,entity.TeacherIsExist1);
            return DBHelper.searchIsLoginInfo(sql);
        }
        /// <summary>
        /// 班主任登入
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool LoginClassTeacherInfo(UserInfoEntity entity)
        {
            string sql = string.Format("select count(*) from ClassTeacherInfo where ClassTeacherLoginName='{0}'" +
            " and ClassTeacherLoginPassWord='{1}' and ClassTeacherIsExist={2}" +
            "", entity.UserLoginName, entity.UserLoginPwd,entity.ClassTeacherIsExist1);
            return DBHelper.searchIsLoginInfo(sql);
        }
    }
}
