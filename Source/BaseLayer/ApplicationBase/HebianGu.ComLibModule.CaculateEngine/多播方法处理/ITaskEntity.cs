using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{
    interface ITaskEntity
    {
        bool Result();
    }

    class TestTaskEntity : ITaskEntity
    {
        Func<bool> function;

        public Func<bool> Function
        {
            get
            {
                return function;
            }

            set
            {
                function = value;
            }
        }

        public bool Result()
        {
            // Todo ：执行的Func方法 
            return Function();
        }
    }

}
