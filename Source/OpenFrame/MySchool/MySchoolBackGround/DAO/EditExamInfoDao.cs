using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class EditExamInfoDao
    {
        /// <summary>
        /// 修改选择题
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editChoice(LessonInfoEntity entity)
        {
            string sql = "update ChoiceInfo set ChoiceSubject='" + entity.ChoiceSubject + "',ChoiceContentA='" + entity.ChoiceContentA + "'," +
            "ChoiceContentB='" + entity.ChoiceContentB + "',ChoiceContentC='" + entity.ChoiceContentC + "'," +
            "ChoiceContentD='" + entity.ChoiceContentD + "',ChoiceContentE='" + entity.ChoiceContentE + "',"+
            "ChoiceRightAnswer='" + entity.ChoiceRightAnswer + "' "+
            "where ChoiceSubject='" + entity.ChooseChoiceSubject + "' ";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 删除选择题
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool deleteChoice(LessonInfoEntity entity)
        {
            string sql = "update ChoiceInfo set ChoiceIsExist=0 where ChoiceSubject='" + entity.ChoiceSubject + "'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 修改简答题
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool editEssayQuestionInfo(LessonInfoEntity entity)
        {
            string sql = "update EssayQuestionInfo set EssayQuestionSubject='"+entity.EssayQuestionSubject+"',"+
                         "EssayQuestionAnswer='" + entity.EssayQuestionAnswer + "' where EssayQuestionSubject='" + entity.ChooseEssayQuestionSubject + "'";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 删除简答题
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool deleteEssayQuestionInfo(LessonInfoEntity entity)
        {
            string sql = "update EssayQuestionInfo set EssayQuestionIsExist=0 where EssayQuestionSubject='" + entity.EssayQuestionSubject + "'";
            return DBHelper.modifyData(sql);
        }
    }
}
