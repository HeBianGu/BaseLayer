using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class RegiserClassEntity
    {
        int removeClassId;

        public int RemoveClassId
        {
            get { return removeClassId; }
            set { removeClassId = value; }
        }

        int classId;

        public int ClassId
        {
            get { return classId; }
            set { classId = value; }
        }
        string className;

        public string ClassName
        {
            get { return className; }
            set { className = value; }
        }
        string chooseClassName;

        public string ChooseClassName
        {
            get { return chooseClassName; }
            set { chooseClassName = value; }
        }
        int classStuNum;

        public int ClassStuNum
        {
            get { return classStuNum; }
            set { classStuNum = value; }
        }

        string classStartTime;

        public string ClassStartTime
        {
            get { return classStartTime; }
            set { classStartTime = value; }
        }
        string classFinishTime;

        public string ClassFinishTime
        {
            get { return classFinishTime; }
            set { classFinishTime = value; }
        }

        int teacherId;

        public int TeacherId
        {
            get { return teacherId; }
            set { teacherId = value; }
        }
        int classTeacherId;

        public int ClassTeacherId
        {
            get { return classTeacherId; }
            set { classTeacherId = value; }
        }
        int classIsExist;

        public int ClassIsExist
        {
            get { return classIsExist; }
            set { classIsExist = value; }
        }

        string classSearchContent;

        public string ClassSearchContent
        {
            get { return classSearchContent; }
            set { classSearchContent = value; }
        }


    }
}
