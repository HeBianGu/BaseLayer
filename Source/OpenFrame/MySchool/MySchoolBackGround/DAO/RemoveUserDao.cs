using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class RemoveUserDao
    {
        /// <summary>
        /// ��ѯ���а༶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuClass(RegiserClassEntity entity)
        {
            string sql = "select * from StuInfo where FKClassId=" + entity.ClassId + " ";
            string tableName = "SearchStuClass";
            return DBHelper.searchData(sql,tableName);
        }
        /// <summary>
        /// ���༶���Ʋ�ѯ����ѧ������
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuName(RegiserClassEntity entity)
        {
            string sql = "select * from StuInfo where FKClassId=" + entity.ClassId + " and StuIsExist=1";
            string tableName = "SearchStuName";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ѧ��ת��
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entity1"></param>
        /// <returns></returns>
        public bool updateStuToClass(RegiserUserEntity entity)
        {
            string sql = "update StuInfo set "+
                         "FKClassId='" + entity.UserClassId + "',StuRemoveTime='"+entity.UserRemoveTime+"' " +
                         "where StuLoginName='" + entity.UserLoginName + "'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ��ѧ��ת��༶�޸�Ϊ���ΰ༶��֮ǰ�İ༶�޸�Ϊ��ʼ�༶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool updateStuClassInfo(RegiserUserEntity entity)
        {
            string sql = "update StuInfo set " +
                         "StuRemoveClass='" + entity.UserRemoveClass1 + "',StuInitializationClass='"+entity.StuInitializationClass1+"' " +
                         "where StuLoginName='" + entity.UserLoginName + "'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ��ѧ���û�����ѯ�༶
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public DataSet searchClassByStuLoginName(RegiserUserEntity entity)
        {
            string sql = "select ClassId,ClassName from StuInfo inner join ClassInfo " +
                         "on(ClassId=FKClassId) "+
                         "where StuLoginName='"+entity.UserLoginName+"' and StuIsExist=1 ";
            string tableName = "SearchClassByStuLoginName";
            return DBHelper.searchData(sql,tableName);
        }
        public DataSet searchClassByStuLoginName1(RegiserUserEntity entity)
        {
            string sql = "select ClassId,ClassName from StuInfo inner join ClassInfo " +
                         "on(ClassId=FKClassId) " +
                         "where StuLoginName='" + entity.UserLoginName + "' and StuIsExist=1 ";
            string tableName = "SearchClassByStuLoginName1";
            return DBHelper.searchData(sql, tableName);
        }
       /// <summary>
       /// ��ѯѧ��ת����Ϣ
       /// </summary>
       /// <returns></returns>
         public DataSet searchStuRemoveInfo()
        {
            string sql = " select StuName as ����,StuInitializationClass as ԭ���༶,StuRemoveClass as �����༶,StuRemoveTime as ����ʱ��,ClassName as ���ڰ༶"+
                        " from StuInfo inner join ClassInfo"+
                        " on(FKClassId=ClassId)"+
                        " where StuInitializationClass!='��'";
            string tableName = "SearchStuRemoveInfo";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
