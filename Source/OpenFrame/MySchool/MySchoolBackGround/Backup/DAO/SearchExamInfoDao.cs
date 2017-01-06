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
        /// 查询所有选择题
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchAllChoice(LessonInfoEntity entity)
        {
            string sql = "select ChoiceSubject as 题目," +
                         "ChoiceContentA as 选项A,ChoiceContentB as 选项B,ChoiceContentC as 选项C,"+
                         "ChoiceContentD as 选项D,ChoiceContentE as 选项E,ChoiceRightAnswer as 答案" +
                         " from ChoiceInfo "+
                         " where FKlessonId=" + entity.LessonId + " and ChoiceIsExist=1";
            string tableName = "SearchAllChoice";
            return DBHelper.searchData(sql,tableName);
        }
        /// <summary>
        /// 查询所有简答题
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchAllEssayQuestion(LessonInfoEntity entity)
        {
            string sql = "select EssayQuestionSubject as 题目,EssayQuestionAnswer as 答案" +
                         " from EssayQuestionInfo "+
                         " where FKlessonId=" + entity.LessonId + " and EssayQuestionIsExist=1";
            string tableName = "SearchAllEssayQuestion";
            return DBHelper.searchData(sql, tableName);
        }
    }
}
