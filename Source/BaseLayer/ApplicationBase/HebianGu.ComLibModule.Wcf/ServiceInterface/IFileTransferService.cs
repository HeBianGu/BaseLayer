using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.Runtime.Serialization;

namespace HebianGu.ComLibModule.Wcf.Service.Interface
{
    /// <summary> 文件操作接口 </summary>
    [ServiceContract]
    public interface IFileTransferService
    {
        /// <summary> 设置字节流分块，每一块的大小 </summary>
        [OperationContract]
        void SetBufferLength(int length);

        /// <summary> 读取字节流一块，并提升字节流的位置 </summary>
        [OperationContract]
        bool ReadNextBuffer();

        /// <summary> 获取当前块的字节流 </summary>
        [OperationContract]
        byte[] GetCurrentBuffer();       

        /// <summary> 下载文件 </summary>
        [OperationContract]
        RemoteFileInfo DownloadFile(DownloadRequest request);

        /// <summary> 上传文件 </summary>
        [OperationContract]
        RemoteFileInfo UploadFile(RemoteFileInfo file);

        /// <summary> 获取文件 </summary>
        [OperationContract]
        RemoteFileInfo GetFile(RemoteFileInfo file); //根据文件名寻找文件是否存在，返回文件的字节长度

        /// <summary> 删除文件 </summary>
        [OperationContract]
        FileResult DeleteFile(RemoteFileInfo file); //根据文件名删除文件，返回是否成功

        /// <summary> 获取所有文件路径 </summary>
        [OperationContract]
        List<string> GetLoadDirFiles(string pDirectoryPath);

        /// <summary> 清理服务流 </summary>
        [OperationContract]
        void DisposeStream();
    }

    [MessageContract]
    public class DownloadRequest
    {
        [MessageHeader(MustUnderstand = true)]
        public string FilePath;

        [MessageBodyMember]
        public string FileName;
    }

    [MessageContract]
    public class FileResult
    {
        [MessageBodyMember]
        public bool Result { get; set; }

        [MessageBodyMember]
        public string FullPath { get; set; }

        [MessageBodyMember]
        public string ErrorInfo { get; set; }
    }

    [MessageContract]
    public class RemoteFileInfo
    {
        //服务器保存相对路径@"upload\"
        [MessageBodyMember]
        public string Path { get; set; }

        //服务器保存文件全路径
        [MessageBodyMember]
        public string FileFullPath { get; set; }

        //文件名
        [MessageBodyMember]
        public string Name { get; set; }

        //文件大小
        [MessageBodyMember]
        public long Length { get; set; }

        //文件的偏移量
        [MessageBodyMember]
        public long Offset { get; set; }

        //传递的字节数
        [MessageBodyMember]
        public byte[] Data { get; set; }

        //创建时间
        [MessageBodyMember]
        public DateTime CreateTime { get; set; }
    }
}
