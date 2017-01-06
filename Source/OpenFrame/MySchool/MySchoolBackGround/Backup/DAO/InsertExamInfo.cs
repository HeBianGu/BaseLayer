using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Utility;
using Entity;

namespace DAO
{
    public class InsertExamInfo
    {
        public bool insertChoice(LessonInfoEntity entity)
        {
            string sql = "insert into ChoiceInfo values("+entity.LessonId+",'"+entity.ChoiceSubject+"',"+
           " '"+entity.ChoiceContentA+"','"+entity.ChoiceContentB+"','"+entity.ChoiceContentC+"',"+
           " '"+entity.ChoiceContentD+"','"+entity.ChoiceContentE+"','"+entity.ChoiceRightAnswer+"',"+entity.ChoiceIsExist+")";
            return DBHelper.modifyData(sql);
        }
        public bool insertEssayQuestion(LessonInfoEntity entity)
        {
            string sql = "insert into EssayQuestionInfo values(" + entity.LessonId + ",'" + entity.EssayQuestionSubject + "'," +
           " '" + entity.EssayQuestionAnswer + "',"+entity.EssayQuestionIsExist+")";
            return DBHelper.modifyData(sql);
        }
    }
}
