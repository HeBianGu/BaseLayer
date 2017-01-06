using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Entity;
using Utility;

namespace DAO
{
    public class StuMarkInfoDao
    {
        public DataSet searchAllClass()
        {
            string sql = "select * from ClassInfo ";
            string tableName = "SearchAllClass";

            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ���ѧ�����Ե��ʴ���ʹ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entity1"></param>
        /// <returns></returns>
        public bool insertExamEssayQuestionAnswer(LessonInfoEntity entity,UserInfoEntity entity1)
        {
            string sql = "insert into ExamQuestionInfo values('" + entity1.UserLoginName + "','" + entity.ChooseEssayQuestion + "','"+entity.ChooseEssayQuestionAnswer+"')";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ���ѧ���ʴ����
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entity1"></param>
        /// <returns></returns>
        public bool insertStuEssayQuestionAnswer(LessonInfoEntity entity, UserInfoEntity entity1)
        {
            string sql = "insert into StuEssayQuestionAnswer values('" + entity1.UserLoginName + "','" + entity.StuEssayQuestionAnswer + "')";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ��ѯ��Ա�����༶
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchTeacherClass(UserInfoEntity entity)
        {
            string sql = "select ClassId,ClassName from ClassInfo inner join TeacherInfo"+
                         " on(FKTeacherId=TeacherId)"+
                         " where TeacherLoginName='"+entity.UserLoginName+"' and ClassIsExist=1";
            string tableName = "SearchTeacherClass";
            return DBHelper.searchData(sql,tableName);
        }
        /// <summary>
        /// ���༶��ѯѧ������
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuNameByClass(ClassInfoEntity entity)
        {
            string sql = "select StuLoginName,StuName from StuInfo where FKClassId=" + entity.ClassId + "";
            string tableName = "SearchStuNameByClass";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��ѯѧ���������ʴ�����Ŀ�Ͳο���
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuExamQuestion(UserInfoEntity entity,LessonInfoEntity entity1)
        {
            string sql = "select StuLoginName,EssayQuestionSubject,EssayQuestionAnswer,StuEssayQuestionAnswer,StuName " +
                        "from ExamQuestionInfo inner join StuInfo " +
                        "on(FKStuLoginName=StuLoginName) "+
                        "where StuLoginName='" + entity.UserLoginName + "'and FKlessonId="+entity1.LessonId+"";
            string tableName = "SearchStuExamQuestion";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ��ѯѧ����д���ʴ����
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuExamQuestionAnswer(UserInfoEntity entity)
        {
            string sql = "select StuLoginName,StuEssayQuestionAnswer "+
                        "from ExamQuestionInfo inner join StuInfo " +
                        "on(FKStuLoginName=StuLoginName) "+
                        "where StuLoginName='"+entity.UserLoginName+"'";
            string tableName = "SearchStuExamQuestionAnswer";
            return DBHelper.searchData(sql, tableName);
        }

        /// <summary>
        /// ���ѧ��ѡ����ɼ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entity1"></param>
        /// <returns></returns>
        public bool insertStuChoiceMark(LessonInfoEntity entity, UserInfoEntity entity1,StuMarkEntity entity3)
        {
            string sql = "insert into StuMarkInfo values('" + entity1.UserLoginName + "','" + entity.LessonId + "',"+entity3.StuChoiceMark+","+entity3.StuEssatQuestionMark+","+entity3.StuMark+")";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ���ѧ���ʴ���ɼ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entity1"></param>
        /// <param name="entity3"></param>
        /// <returns></returns>
        public bool updatetStuEssayMark(LessonInfoEntity entity, UserInfoEntity entity1, StuMarkEntity entity3)
        {
            string sql = "update StuMarkInfo set StuEssayQuestionMark=StuEssayQuestionMark+" + entity3.StuEssatQuestionMark + " where FKStuLoginName='" + entity1.UserLoginName + "' and FKlessonId=" + entity.LessonId + "";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ��ѯ��Ա��һ������ѧ���ʴ���ķ���
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public DataSet searchStuExamQuestionMark(UserInfoEntity entity,LessonInfoEntity entity1)
        {
            string sql = "select StuEssayQuestionMark from StuMarkInfo where FKStuLoginName='"+entity.UserLoginName+"' and FKlessonId="+entity1.LessonId+"";
            string tableName = "SearchStuExamQuestionMark";
            return DBHelper.searchData(sql, tableName);
        }
        /// <summary>
        /// ����ѧ���ܷ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entity1"></param>
        /// <param name="entity3"></param>
        /// <returns></returns>
        public bool updateStuMark(LessonInfoEntity entity, UserInfoEntity entity1, StuMarkEntity entity3)
        {
            string sql = "update StuMarkInfo set StuMark=StuChoiceMark+StuEssayQuestionMark where FKStuLoginName='" + entity1.UserLoginName + "' and FKlessonId=" + entity.LessonId + "";
            return DBHelper.modifyData(sql);
        }
        /// <summary>
        /// ��ѯѧ���ɼ�
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entity1"></param>
        /// <returns></returns>
        public DataSet searchStuMark(UserInfoEntity entity, LessonInfoEntity entity1,ClassInfoEntity entity2)
        {
            string sql = "select LessonName as ��Ŀ,StuMark as �ɼ�,StuName as ����,ClassName as �༶ from StuMarkInfo inner join StuInfo" +
                         " on(FKStuLoginName=StuLoginName)" +
                         " inner join ClassInfo" +
                         " on(FKClassId=ClassId)" +
                         " inner join LessonInfo" +
                         " on(FKlessonId=lessonId)" +
                         " where FKlessonId=" + entity1.LessonId + " and ClassId=" + entity2.ClassId + " and StuName like '%"+entity.UserName+"%'";
            string tableName = "SearchStuMark";
            return DBHelper.searchData(sql, tableName);
        }




    }
}
