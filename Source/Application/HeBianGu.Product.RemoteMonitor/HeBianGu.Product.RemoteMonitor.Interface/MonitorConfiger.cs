#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/3/31 16:37:49
 * 文件名：MonitorConfiger
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HeBianGu.Product.RemoteMonitor.Interface
{
    /// <summary> 客户端连接服务端规则 </summary>
    [DataContract]
    public class MonitorConfiger
    {
        private MonitorConfiger()
        {

        }
        public static MonitorConfiger Instance = new MonitorConfiger();

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
