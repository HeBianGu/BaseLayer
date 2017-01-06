using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class SearchExamInfoDao
    {
        /// <summary>
        /// ��ѯ����ѡ����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchAllChoice(LessonInfoEntity entity)
        {
            string sql = "select ChoiceSubject as ��Ŀ," +
                         "ChoiceContentA as ѡ��A,ChoiceContentB as ѡ��B,ChoiceContentC as ѡ��C,"+
                         "ChoiceContentD as ѡ��D,ChoiceContentE as ѡ��E,ChoiceRightAnswer as ��" +
                         " from ChoiceInfo "+
                         " where FKlessonId=" + entity.LessonId + " and ChoiceIsExist=1";
            string tableName = "SearchAllChoice";
            return DBHelper.searchData(sql,tableName);
        }
        /// <summary>
        /// ��ѯ���м����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchAllEssayQuestion(LessonInfoEntity entity)
        {
            string sql = "select EssayQuestionSubject as ��Ŀ,EssayQuestionAnswer as ��" +
                         " from EssayQuestionInfo "+
                         " where FKlessonId=" + entity.LessonId + " and EssayQuestionIsExist=1";
            string tableName = "SearchAllEssayQuestion";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
