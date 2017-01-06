using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using Utility;
using System.Data;

namespace DAO
{
    public class MessageBoardInfoDao
    {
        /// <summary>
        /// 用户留言
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool insertMessage(MessageBoardEntity entity)
        { 
            string sql="insert into BBS values('"+entity.LoginName1+"','"+entity.MessageContent1+"','"+entity.LeaveMessageTime+"')";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 查询留言
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchBBSInfo()
        {
            string sql = "select UserLoginName as 用户名,UserMessage as 留言信息,UserLeaveMessageTime as 留言时间 from BBS";
            string tableName = "SearchBBSInfo";
            return DBHelper.searchData(sql,tableName);
        }

      
    }
}
