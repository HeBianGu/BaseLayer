using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
namespace Utility2
{        
    public class Class1
    {
        public string judge(params SqlParameter[]pa )
        {
            string message="";
            foreach(SqlParameter p in pa)
            {
                if (p.Value.Equals(""))
                {
                    message = "文本框不能为空";
                    return message;
                }
                else
                {
                    continue;
                }

            }
            return message;

        }
    
    }
}
