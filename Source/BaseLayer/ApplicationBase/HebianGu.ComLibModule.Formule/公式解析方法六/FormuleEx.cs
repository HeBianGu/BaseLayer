using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Formule._6
{
    /// <summary> 公式扩展方法 </summary>
    public static class FormuleEx
    {
        /// <summary> 计算公式 </summary>
        public static string ComputeFormula(this string formule)
        {
           string result=null;
           string strConn = "Data Source=127.0.0.1;Initial Catalog=CementCartDB;Persist Security Info=True;User ID=sa;Password=123456";
           using( SqlConnection conn = new SqlConnection(strConn))
           {
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            string biaodashi = "1&1";
            cmd.CommandText = "select "+biaodashi;
            result= cmd.ExecuteScalar().ToString();
           }
           return result;
        }

    }
}
