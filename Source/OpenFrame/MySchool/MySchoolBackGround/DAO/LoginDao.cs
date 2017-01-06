using System;
using System.Collections.Generic;
using System.Text;
using Utility;
using System.Data;
using System.Data.SqlClient;
using Entity;

namespace DAO
{
    public class LoginDao
    {
       /// <summary>
       /// 登入
       /// </summary>
       /// <param name="entity"></param>
       /// <returns></returns>
        public bool searchIsLoginInfo(RegiserUserEntity entity)
        {
            SqlConnection sqlConnection = null;
            bool flag=false;
            try
            {
                sqlConnection = new SqlConnection("Data Source=ZJ-0F9713C735B3;Initial Catalog=MySchool;User ID=sa;pwd=sa");
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                string sql = string.Format("select count(*) from managerInfo where managerLoginName='{0}'"+
               " and managerLoginPassWord='{1}' and FkIdentityId={2}"+
               "",entity.UserLoginName,entity.UserLoginPwd,entity.UserIdentityId);
                sqlCommand.CommandText = sql;
                sqlCommand.Connection = sqlConnection;
                int count = (int)sqlCommand.ExecuteScalar();
                if (count > 0)
                    flag = true;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == ConnectionState.Closed)
                        sqlConnection.Open();
                }
            }
            return flag;
        }
    }
}
