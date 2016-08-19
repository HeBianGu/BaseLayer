using HebianGu.ComLibModule.Wcf.Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HebianGu.ComLibModule.Wcf.Service.Interface
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    /// <summary> 自动流服务接口 </summary>
    [ServiceContract]
    public interface IWorkScreamService
    {
        /// <summary> 执行步骤一 </summary>
        [OperationContract]
        bool ExecutivePreHM(CaseConfiger pCase);

        /// <summary> 获取服务器上所有案例 </summary>
        [OperationContract]
        System.Collections.Generic.List<CaseConfiger> GetCaseDiretroys(string basePath);

        /// <summary> 获取日志信息 </summary>
        [OperationContract]
        string GetLogMessage();

        /// <summary> 获取案例进行的进度 </summary>
        [OperationContract]
        bool GetStepOfCase(CaseConfiger pCase, ref Step step);

        /// <summary> 停止所有进程 </summary>
        [OperationContract]
        bool StopAllProcess();

        /// <summary> 停止京城 </summary>
        [OperationContract]
        bool StopProcess(CaseConfiger pCase);

        /// <summary> 增加案例 </summary>
        [OperationContract]
        bool AddCase(CaseConfiger pCase);

        /// <summary> 删除案例 </summary>
        [OperationContract]
        bool DeleteCase(CaseConfiger pCase);

        /// <summary> 获取案例 </summary>
        [OperationContract]
        CaseConfiger GetCaseByName(string pName);
    }

    // 使用下面示例中说明的数据约定将复合类型添加到服务操作。
    // 可以将 XSD 文件添加到项目中。在生成项目后，可以通过命名空间“HMWorkScream.Service.ContractType”直接使用其中定义的数据类型。
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
