using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Wcf.Service.Entity
{
    /// <summary> 描述案例运行的步骤 </summary>
    [DataContract]
    [Flags]
    public enum Step
    {
        /// <summary> 未运行 </summary>
        [EnumMember]
        NoReady = 0,
        /// <summary> 运行步骤一 </summary>
        [EnumMember]
        RunPreHM,
        /// <summary> 运行步骤二 </summary>
        [EnumMember]
        RunEsmDA,
        /// <summary> 运行结束 </summary>
        [EnumMember]
        Over,
        /// <summary> 停止运行 </summary>
        [EnumMember]
        StopOver

    }
}
