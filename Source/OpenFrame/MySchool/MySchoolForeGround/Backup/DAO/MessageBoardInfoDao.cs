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
        /// �û�����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool insertMessage(MessageBoardEntity entity)
        { 
            string sql="insert into BBS values('"+entity.LoginName1+"','"+entity.MessageContent1+"','"+entity.LeaveMessageTime+"')";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ��ѯ����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchBBSInfo()
        {
            string sql = "select UserLoginName as �û���,UserMessage as ������Ϣ,UserLeaveMessageTime as ����ʱ�� from BBS";
            string tableName = "SearchBBSInfo";
            return DBHelper.searchData(sql,tableName);
        }

      
    }
}
