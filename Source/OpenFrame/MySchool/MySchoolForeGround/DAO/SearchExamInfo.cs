using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;
namespace DAO
{
    public class SearchExamInfo
    {
        /// <summary>
        /// ����Ŀ��ѯ����ѡ����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchAllChoice(LessonInfoEntity entity)
          {
            string sql = "select ChoiceSubject,ChoiceContentA,ChoiceContentB,ChoiceContentC," +
                         " ChoiceContentD,ChoiceContentE,ChoiceRightAnswer from ChoiceInfo" +
                         " where FKlessonId=" + entity.LessonId + " and ChoiceIsExist=1";

            string tableName = "SearchAllChoice";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ����Ŀ��ѯ���м����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchAllEssayQuestion(LessonInfoEntity entity)
        {
            string sql = "select EssayQuestionSubject,EssayQuestionAnswer " +
                         " from EssayQuestionInfo " +
                         " where FKlessonId=" + entity.LessonId + "  and EssayQuestionIsExist=1";
            string tableName = "SearchAllEssayQuestion";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��ѯѡ��������
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchAllChoiceCount(LessonInfoEntity entity)
        {
            string sql = "select count(*) from ChoiceInfo";
            string tableName = "SearchAllChoiceCount";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ���γ̲�ѯѡ������ȷ��
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchChoiceRightAnswer(LessonInfoEntity entity)
        {
            string sql = "select ChoiceSubject,ChoiceRightAnswer from ChoiceInfo where FKlessonId="+entity.LessonId+"";
            string tableName = "SearchChoiceRightAnswer";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
