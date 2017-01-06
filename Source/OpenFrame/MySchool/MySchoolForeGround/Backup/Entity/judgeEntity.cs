using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Collections;

namespace Entity
{
    public class judgeEntity
    {
        public int judge1(params string[] pa)
        {
            int message = -1;
            int i = 0;
            foreach (string p in pa)
            {
                if (p.Equals(""))
                {
                    message = i;
                    return message;
                }
                i++;
            }
            return message;
        }
    }
}

