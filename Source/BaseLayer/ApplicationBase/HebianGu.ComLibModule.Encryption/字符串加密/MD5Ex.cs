using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace HebianGu.ComLibModule.Encryption.StringFor
{/// <summary> MD5加密 不可逆 MD5比SHA-1的运算速度更快 </summary>
    public static class MD5Ex
    {
        /// <summary> 序列化 </summary>
        public static string ToMD5(this string key)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(key, "MD5"); ;
        }

         
        /// <summary> 32位加密 </summary>
        public static  string ToMD5_32(string s, string _input_charset)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(_input_charset).GetBytes(s));
            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        /// <summary> 16位加密 </summary>
        public static string GetMd5_16(string ConvertString)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string t2 = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(ConvertString)), 4, 8);
            t2 = t2.Replace("-", "");
            return t2;
        }

        /// <summary> 返回一个32位MD5加密字符串 </summary>
        /// <param name="encryptString">要加密的字符串</param>
        /// <returns></returns>
        public static string MD5(string encryptString)
        {
            byte[] result = Encoding.Default.GetBytes(encryptString);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            string encryptResult = BitConverter.ToString(output).Replace("-", "");
            return encryptResult.ToUpper();
        }
    }
}
