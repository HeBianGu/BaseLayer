using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace Utility
{

    public class DBHelper
    {
        /// <summary>
        /// 增删改
        /// </summary>
        private static string strCon = "Data Source=ZJ-0F9713C735B3;Initial Catalog=MySchool;User ID=sa;pwd=sa";
        public static bool modifyData(string sql)
        {
            SqlConnection sqlConnection = null;
            bool flag = false;
            try
            {
                sqlConnection = new SqlConnection(strCon);
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sql;
                int count = sqlCommand.ExecuteNonQuery();
                if (count > 0)
                    flag = true;
                else
                    flag = false;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
            return flag;
        }
        /// <summary>
        /// 登入
        /// </summary>
        /// <returns></returns>
        public static bool searchIsLoginInfo(string sql)
        {
            SqlConnection sqlConnection = null;
            bool flag = false;
            try
            {
                sqlConnection = new SqlConnection(strCon);
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(); 
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = sql;
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
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataSet searchData(string sql, string tableName)
        {
            DataSet ds = null;
            SqlConnection sqlConnection = null;
            try
            {
                sqlConnection = new SqlConnection(strCon);
                if (sqlConnection.State == ConnectionState.Closed)
                    sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                ds = new DataSet();
                sqlDataAdapter.Fill(ds, tableName);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (sqlConnection != null)
                {
                    if (sqlConnection.State == ConnectionState.Open)
                        sqlConnection.Close();
                }
            }
            return ds;
        }

       

    }

}


