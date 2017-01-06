using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Entity
{
    public class LessonInfoEntity
    {   //课程
        int lessonId;

        public int LessonId
        {
            get { return lessonId; }
            set { lessonId = value; }
        }
        string lessonName;

        public string LessonName
        {
            get { return lessonName; }
            set { lessonName = value; }
        }
        int lessonIsExist;

        public int LessonIsExist
        {
            get { return lessonIsExist; }
            set { lessonIsExist = value; }
        }



        //选择题
        int choiceId;

        public int ChoiceId
        {
            get { return choiceId; }
            set { choiceId = value; }
        }
        string choiceSubject;
        string chooseChoiceSubject;

        public string ChooseChoiceSubject
        {
            get { return chooseChoiceSubject; }
            set { chooseChoiceSubject = value; }
        }

        public string ChoiceSubject
        {
            get { return choiceSubject; }
            set { choiceSubject = value; }
        }
        string choiceContentA;

        public string ChoiceContentA
        {
            get { return choiceContentA; }
            set { choiceContentA = value; }
        }
        string choiceContentB;

        public string ChoiceContentB
        {
            get { return choiceContentB; }
            set { choiceContentB = value; }
        }
        string choiceContentC;

        public string ChoiceContentC
        {
            get { return choiceContentC; }
            set { choiceContentC = value; }
        }
        string choiceContentD;

        public string ChoiceContentD
        {
            get { return choiceContentD; }
            set { choiceContentD = value; }
        }
        string choiceContentE;

        public string ChoiceContentE
        {
            get { return choiceContentE; }
            set { choiceContentE = value; }
        }
        string choiceRightAnswer;

        public string ChoiceRightAnswer
        {
            get { return choiceRightAnswer; }
            set { choiceRightAnswer = value; }
        }

        int choiceIsExist;

        public int ChoiceIsExist
        {
            get { return choiceIsExist; }
            set { choiceIsExist = value; }
        }

        //问答题
        int essayQuestionId;

        public int EssayQuestionId
        {
            get { return essayQuestionId; }
            set { essayQuestionId = value; }
        }
        string essayQuestionSubject;

        public string EssayQuestionSubject
        {
            get { return essayQuestionSubject; }
            set { essayQuestionSubject = value; }
        }
        string chooseEssayQuestionSubject;

        public string ChooseEssayQuestionSubject
        {
            get { return chooseEssayQuestionSubject; }
            set { chooseEssayQuestionSubject = value; }
        }

        string essayQuestionAnswer;

        public string EssayQuestionAnswer
        {
            get { return essayQuestionAnswer; }
            set { essayQuestionAnswer = value; }
        }
        int essayQuestionIsExist;

        public int EssayQuestionIsExist
        {
            get { return essayQuestionIsExist; }
            set { essayQuestionIsExist = value; }
        }
        string chooseEssayQuestion;

        public string ChooseEssayQuestion//随机到的问答题
        {
            get { return chooseEssayQuestion; }
            set { chooseEssayQuestion = value; }
        }
        string chooseEssayQuestionAnswer;//随机到的问答题的答案

        public string ChooseEssayQuestionAnswer
        {
            get { return chooseEssayQuestionAnswer; }
            set { chooseEssayQuestionAnswer = value; }
        }
        string stuEssayQuestionAnswer;//学生问答题答案

        public string StuEssayQuestionAnswer
        {
            get { return stuEssayQuestionAnswer; }
            set { stuEssayQuestionAnswer = value; }
        }

       
    }
}
