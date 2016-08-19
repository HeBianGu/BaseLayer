using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf.Service.Entity
{
    /// <summary> 客户端服务端案例配置类 </summary>
    [DataContract]
    [KnownType(typeof(CaseConfiger))]
    public class CaseConfiger
    {
        /// <summary> 唯一表示(暂时没用到) </summary>
        [DataMember]
        public string Id;

        /// <summary> 项目的名字 唯一标识 </summary>
        [DataMember]
        public string Name;

        /// <summary> 本地路径 </summary>
        [DataMember]
        public string LocalPath;

        /// <summary> 服务端路径 </summary>
        [DataMember]
        public string ServerPath;

        /// <summary> 成果文件夹路径 </summary>
        [DataMember]
        public string ResultFolder;

        /// <summary> 根节点目录 </summary>
        [DataMember]
        public string ParentDir;

        /// <summary> 消息 </summary>
        [DataMember]
        public string Messager;

        /// <summary> 步骤一进程 不可以加特性 </summary>
        public Process PreHMProcess;

        /// <summary> 步骤二进程 不可以加特性 </summary>
        public Process EsmdaProcess;

        /// <summary> 中间进程 在外协中调用目前没办法绑定测试 </summary>
        public Process EclipseProcess;

        /// <summary> 暂时没用到(运行日志) </summary>
        [DataMember]
        public List<string> ProblemList;

        /// <summary> 运行的步骤 </summary>
        [DataMember]
        public Step Step;
    }



}
