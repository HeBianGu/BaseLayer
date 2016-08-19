using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf.Service
{
    public static class EncryptionEx
    {

        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="filePath"> 文件 </param>
        /// <param name="outFilePath"> 输出文件 </param>
        /// <param name="passWord"> 密码 </param>
        public static  void  EncryptFile(this string filePath,string outFilePath,string passWord)
        {
            Encryption.EncryptFile(filePath, outFilePath, passWord);
        }
        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="filePath"> 文件 </param>
        /// <param name="outFilePath"> 输出文件 </param>
        /// <param name="passWord"> 密码 </param>
        public static void DecryptFile(this string filePath, string outFilePath, string passWord)
        {
            Encryption.DecryptFile(filePath, outFilePath, passWord);
        }


        /// <summary>  加密文件  </summary>
        /// <param name="filePath"> 文件 </param>
        /// <param name="outFilePath"> 输出文件 </param>
        /// <param name="passWord"> 密码 </param>
        public static void EncryptFileToDat(this string filePath, string passWord)
        {

            EncryptFile(filePath, filePath + ".dat", passWord);
        }
        /// <summary>  解密文件  </summary>
        /// <param name="filePath"> 文件 </param>
        /// <param name="outFilePath"> 输出文件 </param>
        /// <param name="passWord"> 密码 </param>
        public static void DecryptFileFromDat(this string filePath, string passWord)
        {

            DecryptFile(filePath, filePath.Substring(0, filePath.Length - 4), passWord);
        }
    }
}
