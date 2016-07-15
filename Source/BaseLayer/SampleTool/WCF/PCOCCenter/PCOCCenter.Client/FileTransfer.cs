
namespace OPT.PCOCCenter.Client
{
    public class FileTransfer
    {
        #region Upload
        /// <summary>
        /// 文件夹上传
        /// </summary>
        /// <param name="RemoteAddress">远程服务地址，需要从client中获取</param>
        /// <param name="localDirFullPath">需上传本地文件夹全路径名</param>
        /// <param name="serverUploadPath">服务器上传相对路径"(serverDir\\)upload\\"</param>
        public static string UploadDir(string RemoteAddress, string localDirFullPath, string serverUploadPath)
        {
            // 启动文件传输对话框
            FileTransferForm fileTransferForm = new FileTransferForm(FileTransferForm.FileTransferType.uploadDir,
                                                                     RemoteAddress, localDirFullPath, serverUploadPath);
            fileTransferForm.ShowDialog();

            return fileTransferForm.Result;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="RemoteAddress">远程服务地址，需要从client中获取</param>
        /// <param name="localFileFullPath">上传文件全路径名</param>
        /// <param name="serverUploadPath">服务器上传相对路径"(serverDir\\)upload\\"</param>
        public static string UploadFile(string RemoteAddress, string localFileFullPath, string serverUploadPath)
        {
            // 启动文件传输对话框
            FileTransferForm fileTransferForm = new FileTransferForm(FileTransferForm.FileTransferType.uploadFile, 
                                                                     RemoteAddress, localFileFullPath, serverUploadPath);
            fileTransferForm.ShowDialog();

            return fileTransferForm.Result;
        }
        #endregion

        #region Download
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="RemoteAddress">远程服务地址，需要从client中获取</param>
        /// <param name="localPath">本地保存路径，不带文件名</param>
        /// <param name="downloadFileName">需要下载的文件名（远程）</param>
        public static string DownloadFile(string RemoteAddress, string localPath, string serverFilePath, string downloadFileName)
        {
            // 启动文件传输对话框
            FileTransferForm fileTransferForm = new FileTransferForm(FileTransferForm.FileTransferType.downloadFile,
                                                                     RemoteAddress, localPath, serverFilePath, downloadFileName);
            fileTransferForm.ShowDialog();

            return fileTransferForm.Result;
        }

        #endregion
    }
}
