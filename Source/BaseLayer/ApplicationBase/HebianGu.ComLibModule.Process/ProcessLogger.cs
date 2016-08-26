using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ProcessHelper
{
    class ProcessLogger : IProcessEngineLog
    {
        public void RunLog(string result)
        {
            Console.WriteLine(result);
        }

        public void ErrLog(string errstring)
        {
            Console.WriteLine(errstring);
        }

        public void ErrLog(Exception ex)
        {
            Console.WriteLine(ex);
        }
    }
}
