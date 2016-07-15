using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace HebianGu.ComLibModule.Encryption.StringFor
{
    /// <summary> SHA1加密扩展 不可逆 SHA-1比MD5多32位密文，所以更安全 </summary>
    public static class SHA1Ex
    { 
        /// <summary> 序列化 </summary>
        public static string ToSHA1(this string key)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(key, "SHA1"); 
        }
    }
}
