using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Configuration;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Data.OleDb;


namespace OPT.PCOCCenter.Utils
{
    /// <summary>
    /// 数据库操作(抽象类)
    /// </summary>
    public abstract class DBProvider
    {
        /// <summary>
        /// 打开数据库连接
        /// </summary>
//        public abstract SqlConnection Open(); //打开数据库连接
        public abstract OleDbConnection Open(); //打开数据库连接

        /// <summary>
        /// 关闭数据库连接
        /// </summary>
//        public abstract void Close(SqlConnection conn); //关闭数据库连接
        public abstract void Close(OleDbConnection conn); //关闭数据库连接

        /// <summary>
        /// 返回执行SQL命令的command对象
        /// </summary>
        /// <param name="commandString">
        /// SQL命令
        /// </param>
        /// <returns>
        /// 返回执行SQL命令的command对象
        /// </returns>
//        protected abstract IDbCommand BuildCommand(string commandString, SqlConnection conn);//返回command对象（可以加事务）
        protected abstract IDbCommand BuildCommand(string commandString, OleDbConnection conn);//返回command对象（可以加事务）

        /// <summary>
        /// 执行SQL命令，返回DataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteDataTable(string sql);

        /// <summary>
        /// 执行SQL命令，影响的行数
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public abstract int ExecuteNonQuery(string strSQL);
        
        /// <summary>
        /// 实例化一个数据库操作类
        /// </summary>
        /// <param name="connectionString"> 连接字符串</param>
        /// <returns>返回数据库操作类的实例</returns>
        public static DBProvider Instance(string connectionString)//返回数据库实例
        {
//            return new SqlDBOperate(connectionString);
            return new OleDBOperate(connectionString);
        }
    }
}
