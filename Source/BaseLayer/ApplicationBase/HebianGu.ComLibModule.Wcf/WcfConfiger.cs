using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf
{
    /// <summary> 客户端连接服务端规则 </summary>
    [DataContract]
    public class WcfConfiger
    {
        private WcfConfiger()
        {

        }
        public static WcfConfiger Instance = new WcfConfiger();

        /// <summary> 服务端IP </summary>
        [DataMember]
        public string IP = "127.0.0.1";

        /// <summary> 通信端口号 </summary>
        [DataMember]
        public string Port = "22121";

        /// <summary> 服务名 </summary>
        [DataMember]
        public string SvcName;

        /// <summary> 连接地址规则 </summary>
        [DataMember]
        public static string RomoteFormat = "http://{0}:{1}/{2}";
    }
}
