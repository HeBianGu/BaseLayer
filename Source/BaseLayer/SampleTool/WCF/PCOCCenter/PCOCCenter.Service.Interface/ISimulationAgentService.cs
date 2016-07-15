using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace OPT.PCOCCenter.Service.Interface
{
    [ServiceContract]
    [ServiceKnownType(typeof(SimTaskInfo))]
    public interface ISimulationAgentService
    {

        [OperationContract]
        string RequestTask(string taskOwnerIP);

        [OperationContract]
        void StartTask(SimTaskInfo request);

        [OperationContract]
        SimTaskInfo GetSimTaskInfo(SimTaskInfo request);

        [OperationContract]
        SimTaskInfo RemoveTask(SimTaskInfo request);
    }

    [MessageContract]
    [KnownType(typeof(SimTaskInfo))]
    public class SimTaskInfo
    {
        [MessageBodyMember]
        public string ID;          // 任务ID（自动分配）

        [MessageBodyMember]
        public int Flag;           // 状态标记， -1--被删除，0--初始值， 1--排队新任务，2--正在运行，其他表示失败

        [MessageBodyMember]
        public string Name;        // 任务名称 (name.data)

        [MessageBodyMember]
        public string OPath;       // 任务发布者本机路径（D:\\path\\name.data）

        [MessageBodyMember]
        public string ResultPath;   // 结果文件本机路径（不包含具体结果文件名，只有路径）

        [MessageBodyMember]
        public string FuncID;      // 任务功能ID

        [MessageBodyMember]
        public string TaskType;    // 任务类型(功能中存在多类型时，用来识别)

        [MessageBodyMember]
        public string OwnerIP;     // 任务发布者IP
        
        [MessageBodyMember]
        public string SimulatorPath;         // 任务工作主机模拟器路径

        [MessageBodyMember]
        public string SimulatorLicensePath;  // 任务工作主机模拟器许可路径

        [MessageBodyMember]
        public int SimulationDays;           // 任务模拟天数（用于计算进度百分比）

        [MessageBodyMember]
        public float CompletePercent;   // 完成进度

        public Process WorkerProcess;   // 工作进程

        [MessageBodyMember]
        public string RemoteDataPath;   // 远程数据文件路径

        [MessageBodyMember]
        public string MessageInfo;      // 提示信息

        [MessageBodyMember]
        public List<string> ProblemList;// 问题列表（用于记录不中断运行过程中的问题信息，客户端输出到日志列表中）
    }
}
