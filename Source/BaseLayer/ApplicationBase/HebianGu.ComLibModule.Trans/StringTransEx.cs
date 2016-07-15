using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Trans
{
    public static class StringTransEx
    {
        /// <summary> 转换double </summary>
        public static double ToDouble(this string str)
        {
            return double.Parse(str);
        }

        /// <summary> 转换Int </summary>
        public static int ToInt(this string str)
        {
            return int.Parse(str);
        }

        /// <summary> 转换bool</summary>
        public static bool ToBool(this string str)
        {
            switch (str)
            {
                case "1":
                    return true;
                case "0":
                    return false;
                case "true":
                    return true;
                case "false":
                    return false;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
