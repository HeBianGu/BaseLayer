using System;
using System.Collections.Generic;
using System.Text;
using Entity;
using Utility;
using System.Data;
namespace DAO
{
    public class QuestionOperateDAO
    {
        /// <summary>
        /// 学生应回答的问题和标准答案入库,学生答案都初始化为空
        /// </summary>
        /// <param name="questionEntity"></param>
        /// <returns></returns>
        public bool insertQuestionOperate(Entity.QuestionOperateEntity questionEntity,LessonInfoEntity entity)
        {
            string sql = "insert into ExamQuestionInfo(FKlessonId,FKStuLoginName,EssayQuestionSubject,EssayQuestionAnswer,StuEssayQuestionAnswer) values('{0}','{1}','{2}','{3}','{4}')";
            string sql1 = string.Format(sql,entity.LessonId,questionEntity.StuLoginName, questionEntity.EssayQuestionSubject, questionEntity.EssayQuestionAnswer,questionEntity.StuEssayQuestionAnswer);
            return DBHelper.modifyData(sql1);
        }

        /// <summary>
        /// 将学生回答的答案入库
        /// </summary>
        /// <param name="questionEntity"></param>
        /// <returns></returns>
        public bool updateQuestionOperate(Entity.QuestionOperateEntity questionEntity,LessonInfoEntity entity)
        {
            string sql = "update ExamQuestionInfo set StuEssayQuestionAnswer='"+questionEntity.StuEssayQuestionAnswer+"' where FKStuLoginName='"+questionEntity.StuLoginName+"' and EssayQuestionSubject='"+questionEntity.EssayQuestionSubject+"' and FKlessonId="+entity.LessonId+"";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// 查询学生回答的答案
        /// </summary>
        /// <param name="questionEntity"></param>
        /// <returns></returns>
        public DataSet searchQuestionOperate(QuestionOperateEntity questionEntity)
        {
            string sql = "select StuEssayQuestionAnswer from ExamQuestionInfo" +
                         " where FKStuLoginName='{0}' and EssayQuestionSubject='{1}'";
            string sql1=string.Format(sql,questionEntity.StuLoginName,questionEntity.EssayQuestionSubject);
            string tableName = "SearchQuestionOperate";
            return DBHelper.searchData(sql1,tableName);
        }
        /// <summary>
        /// 查询学生所有回答的题目内容和答案
        /// </summary>
        /// <param name="questionEntity"></param>
        /// <returns></returns>
        public DataSet searchQuestionOperate1(QuestionOperateEntity questionEntity,LessonInfoEntity entity)
        {
            string sql = "select EssayQuestionSubject,StuEssayQuestionAnswer from ExamQuestionInfo" +
                         " where FKStuLoginName='{0}' and FKlessonId='{1}'";
            string sql1 = string.Format(sql, questionEntity.StuLoginName,entity.LessonId);
            string tableName = "SearchQuestionOperate1";
            return DBHelper.searchData(sql1, tableName);
        }
        /// <summary>
        /// 修改答题卡选中的题目答案
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool updateQuestion(QuestionOperateEntity entity)
        {
            string sql = "update ExamQuestionInfo set StuEssayQuestionAnswer='"+entity.StuEssayQuestionAnswer+"' where EssayQuestionSubject='"+entity.EssayQuestionSubject+"'";
            return DBHelper.modifyData(sql);
        }

    }
}
