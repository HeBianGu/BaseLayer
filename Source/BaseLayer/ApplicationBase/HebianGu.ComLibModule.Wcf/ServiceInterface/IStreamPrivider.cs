using HebianGu.ComLibModule.Wcf.Service.Entity;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HebianGu.ComLibModule.Wcf.Service.Interface
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IService1”。
    /// <summary> 执行程序服务 </summary>
    [ServiceContract]
    public interface IStreamPrivider
    {
        [OperationContract]
        byte[] GetCurrectBuffer();

        [OperationContract]
        void SetStream(Stream stream);

        [OperationContract]
        bool ReadNextBuffer();

        [OperationContract]
        void PrintStreenToStream();
    }
}
