using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.Demo.TestSource
{
    public class Student
    {
        string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        string pid; 

        public string Pid
        {
            get { return pid; }
            set { pid = value; }
        }

        string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        int age;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        bool merryerd;

        public bool Merryerd
        {
            get { return merryerd; }
            set { merryerd = value; }
        }

        DateTime bordTime;

        public DateTime BordTime
        {
            get { return bordTime; }
            set { bordTime = value; }
        }

        int jindu;

        public int Jindu
        {
            get { return jindu; }
            set { jindu = value; }
        }

        int sex;

        public string Sex
        {
            get
            {

                return sex == 0 ? "男" : "女";
            }
            set
            {
                if (value.ToString() == "男")
                {
                    sex = 0;
                }
                else
                {
                    sex = 1;
                }

            }
        }

        public int SexIndex
        {
            get
            {
                return sex;

            }set
            {
                sex = value;
            }
        }

    }
}
