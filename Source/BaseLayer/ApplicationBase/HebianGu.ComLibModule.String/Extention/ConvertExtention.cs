using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.String.Extention
{
    /// <summary> 字符串转换 </summary>
    public static class ConvertExtention
    {
        /// <summary> 转换为Double类型 </summary>
        public static double ToDouble(this string s)
        {
            return Convert.ToDouble(s);
        }

        /// <summary> 转换为Int类型 </summary>
        public static int ToInt(this string s)
        {
            return Convert.ToInt32(s);
        }
    }
}
