using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace OPT.PCOCCenter.Utils
{
    /// <summary>
    /// 数据库访问的实现类（OLEDB）
    /// </summary>
    class OleDBOperate : DBProvider
    {

        private string connectionString; //数据库连接串

        public OleDBOperate(string connectionString)//构造函数，创建连接对象
        {
            this.connectionString = connectionString;
        }

        public override OleDbConnection Open()//打开数据库连接
        {
            try
            {
                OleDbConnection conn = new OleDbConnection(connectionString);
                conn.Open();
                return conn;
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public override void Close(OleDbConnection conn)//关闭数据库连接
        {
            if (conn.State == ConnectionState.Open)
                conn.Close();
        }

        /// <summary>
        /// 返回执行指定SQL命令的Command对象
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <returns>返回执行指定SQL命令的Command对象</returns>
        protected override IDbCommand BuildCommand(string commandString, OleDbConnection conn)//返回command对象（可以加事务）
        {
            IDbCommand command = new OleDbCommand(commandString, conn);
            command.CommandType = CommandType.Text;

            return command;
        }

        /// <summary>
        /// 执行SQL命令，返回DataTable
        /// </summary>
        /// <param name="commandString">SQL查询字符串</param>
        /// <returns>执行SQL查询返回的DataTable</returns>
        public override DataTable ExecuteDataTable(string commandString)//执行SQL查询返回DataTable
        {
            OleDbConnection conn = null;
            try
            {
                DataTable dataTable = new DataTable();
                conn = this.Open();

                if (conn != null && conn.State == ConnectionState.Open)
                {
                    OleDbDataAdapter sqlDA = new OleDbDataAdapter();

                    OleDbCommand command = this.BuildCommand(commandString, conn) as OleDbCommand;

                    sqlDA.SelectCommand = command;

                    sqlDA.Fill(dataTable);
                        
                    this.Close(conn);

                    dataTable.TableName = "TableName";

                    return dataTable;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    this.Close(conn);
                }
            }

            return null;
        }

        /// <summary>
        /// SQL命令，返回影响的行数
        /// </summary>
        /// <param name="commandString">SQL命令</param>
        /// <param name="rowsAffected">返回影响的行数</param>
        public override int ExecuteNonQuery(string commandString)//执行SQL，返回，受影响的行数
        {
            try
            {
                OleDbConnection conn = this.Open();
                if (conn != null && conn.State == ConnectionState.Open)
                {
                    OleDbCommand command = BuildCommand(commandString, conn) as OleDbCommand;
                    int rowsAffected = command.ExecuteNonQuery();
                    
                    this.Close(conn);
                    
                    return rowsAffected;
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return 0;
        }
    }
}
