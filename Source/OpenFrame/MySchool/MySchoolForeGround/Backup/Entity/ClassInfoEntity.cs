using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
     public class ClassInfoEntity
    {
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
    }
}
