using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace LifeCalendar
{
    class DBHelper
    {
        static SQLiteConnection _connection;
        /// <summary>
        /// SQLite连接
        /// </summary>
        static SQLiteConnection connection
        {
            get
            {
                if (_connection == null)
                {
                    string datasource = @"database.db";
                    //连接数据库
                    _connection = new SQLiteConnection();

                    SQLiteConnectionStringBuilder connstr = new SQLiteConnectionStringBuilder();

                    connstr.DataSource = datasource;

                    connstr.Password = "admin";//设置密码，SQLite ADO.NET实现了数据库密码保护

                    _connection.ConnectionString = connstr.ToString();
                    //_connection = new SQLiteConnection(string.Format("Data Source={0}/DB/DB.db3;Version=3;", AppDomain.CurrentDomain.SetupInformation.ApplicationBase));
                    _connection.Open();
                }
                return _connection;
            }
        }

        /// <summary>
        /// SQLite增删改
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">所需参数</param>
        /// <returns>所受影响的行数</returns>
        int ExecuteNonQuery(string sql, SQLiteParameter[] parameters)
        {
            int affectedRows = 0;

            DbTransaction transaction = connection.BeginTransaction();
            SQLiteCommand command = new SQLiteCommand(connection);
            command.CommandText = sql;
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            affectedRows = command.ExecuteNonQuery();
            transaction.Commit();

            return affectedRows;
        }

        /// <summary>
        /// SQLite查询
        /// </summary>
        /// <param name="sql">要执行的sql语句</param>
        /// <param name="parameters">所需参数</param>
        /// <returns>结果DataTable</returns>
      public static  DataTable ExecuteDataTable(string sql, SQLiteParameter[] parameters)
        {
            DataTable data = new DataTable();

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            if (parameters != null)
            {
                command.Parameters.AddRange(parameters);
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
            adapter.Fill(data);

            return data;
        }

        /// <summary>
        /// 查询数据库表信息
        /// </summary>
        /// <returns>数据库表信息DataTable</returns>
        DataTable GetSchema()
        {
            DataTable data = new DataTable();

            data = connection.GetSchema("TABLES");

            return data;
        }
    }
}
