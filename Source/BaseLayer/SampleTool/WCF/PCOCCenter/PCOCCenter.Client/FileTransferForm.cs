using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using OPT.PCOCCenter.Service.Interface;
using System.Windows.Forms;
using System.ServiceModel.Channels;
using System.IO;

namespace OPT.PCOCCenter.Client
{
    public partial class FileTransferForm : Form
    {
        public string Result { get; set; }

        #region private
        private System.ComponentModel.BackgroundWorker backgroundUploadWorker = null;
        private System.ComponentModel.BackgroundWorker backgroundDownloadWorker = null;

        public enum FileTransferType
        {
            uploadFile,
            uploadDir,
            downloadFile
        }

        private class FileTransferInfo
        {
            public FileTransferType fileTransferType;
            public string remoteAddress;
            public string localPath;  // 只是路径，不带文件名
            public string serverPath; // 只是路径，不带文件名
            public string fileName;   // 文件名
        }

        private void LogText(string text)
        {
            Result = text;
        }

        private void GetDirectorys(string strPath, ref List<string> lstDirect)
        {
            DirectoryInfo diFliles = new DirectoryInfo(strPath);
            DirectoryInfo[] diArr = diFliles.GetDirectories();

            foreach (DirectoryInfo di in diArr)
            {
                try
                {
                    lstDirect.Add(di.FullName);
                    GetDirectorys(di.FullName, ref lstDirect);

                }
                catch
                {
                    continue;
                }
            }
        }

        private IList<FileInfo> GetFiles(string strPath)
        {
            List<FileInfo> lstFiles = new List<FileInfo>();
            List<string> lstDirect = new List<string>();
            lstDirect.Add(strPath);
            DirectoryInfo diFliles = null;
            GetDirectorys(strPath, ref lstDirect);
            foreach (string str in lstDirect)
            {
                try
                {
                    diFliles = new DirectoryInfo(str);
                    lstFiles.AddRange(diFliles.GetFiles());
                }
                catch
                {
                    continue;
                }
            }
            return lstFiles;
        }

        private string GetUploadPath(string localDirPath, string serverUploadBasePath, string fileFullName)
        {
            string uploadPath = string.Empty;

            int nPos = fileFullName.LastIndexOf("\\");

            if (fileFullName.IndexOf(localDirPath) < 0) return string.Empty;

            if (nPos > 0)
            {
                // 去除文件名
                fileFullName = fileFullName.Substring(0, nPos + 1);

                // 去除本地基本路径
                uploadPath = fileFullName.Substring(localDirPath.Length);
            }

            return uploadPath;
        }
        #endregion

        public FileTransferForm(FileTransferType fileTransferType, string RemoteAddress, string localFullPath, string serverUploadPath)
        {
            InitializeComponent();

            if (fileTransferType == FileTransferType.uploadDir)
            {
                UploadDir(RemoteAddress, localFullPath, serverUploadPath);
            }
            else
            {
                UploadFile(RemoteAddress, localFullPath, serverUploadPath);
            }
        }

        public FileTransferForm(FileTransferType fileTransferType, string RemoteAddress, string localPath, string serverFilePath, string downloadFileName)
        {
            InitializeComponent();

            if (fileTransferType == FileTransferType.downloadFile)
            {
                DownloadFile( RemoteAddress,  localPath,  serverFilePath,  downloadFileName);
            }
        }
        /// <summary>
        /// 取消文件传输
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Result = "Info: 用户取消文件传输！";
            if (backgroundUploadWorker != null && backgroundUploadWorker.IsBusy)
            {
                backgroundUploadWorker.CancelAsync();
            }
            if (backgroundDownloadWorker != null && backgroundDownloadWorker.IsBusy)
            {
                backgroundDownloadWorker.CancelAsync();
            }
        }

        #region Upload
        /// <summary>
        /// 上传文件夹
        /// </summary>
        /// <param name="RemoteAddress"></param>
        /// <param name="localDirFullPath"></param>
        /// <param name="serverUploadPath"></param>
        public void UploadDir(string RemoteAddress, string localDirFullPath, string serverUploadPath)
        {
            this.backgroundUploadWorker = new System.ComponentModel.BackgroundWorker();

            backgroundUploadWorker.WorkerReportsProgress = true;
            backgroundUploadWorker.WorkerSupportsCancellation = true;
            backgroundUploadWorker.DoWork += new DoWorkEventHandler(backgroundUploadWorker_DoWork);
            backgroundUploadWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            backgroundUploadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            FileTransferInfo fileTranserInfo = new FileTransferInfo();
            fileTranserInfo.fileTransferType = FileTransferType.uploadDir;
            fileTranserInfo.remoteAddress = RemoteAddress;
            fileTranserInfo.localPath = localDirFullPath;
            fileTranserInfo.serverPath = serverUploadPath;
            fileTranserInfo.fileName = "*";

            backgroundUploadWorker.RunWorkerAsync(fileTranserInfo);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="RemoteAddress"></param>
        /// <param name="localFileFullPath"></param>
        /// <param name="serverUploadPath"></param>
        public void UploadFile(string RemoteAddress, string localFileFullPath, string serverUploadPath)
        {
            this.backgroundUploadWorker = new System.ComponentModel.BackgroundWorker();

            backgroundUploadWorker.WorkerReportsProgress = true;
            backgroundUploadWorker.WorkerSupportsCancellation = true;
            backgroundUploadWorker.DoWork += new DoWorkEventHandler(backgroundUploadWorker_DoWork);
            backgroundUploadWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            backgroundUploadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            FileTransferInfo fileTranserInfo = new FileTransferInfo();
            fileTranserInfo.fileTransferType = FileTransferType.uploadFile;
            fileTranserInfo.remoteAddress = RemoteAddress;
            int nPos = localFileFullPath.LastIndexOf('\\');
            string localPath = localFileFullPath.Substring(0, nPos);
            string fileName = localFileFullPath.Substring(nPos + 1);
            fileTranserInfo.localPath = localPath;
            fileTranserInfo.serverPath = serverUploadPath;
            fileTranserInfo.fileName = fileName;

            backgroundUploadWorker.RunWorkerAsync(fileTranserInfo);
        }

        //工作完成后执行的事件  
        void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        //工作中执行进度更新  ，C#进度条实现之异步实例
        void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
        }

        void backgroundUploadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FileTransferInfo fileTranserInfo = e.Argument as FileTransferInfo;
                var fileManger = Common.ServiceBroker.FindService<IFileTransferService>(fileTranserInfo.remoteAddress); //创建WCF带来

                string localPath = fileTranserInfo.localPath;

                if (fileTranserInfo.fileTransferType == FileTransferType.uploadDir)
                {
                }
                else if (fileTranserInfo.fileTransferType == FileTransferType.uploadFile)
                {
                    string fileName = fileTranserInfo.fileName;
                    int maxSiz = 1024 * 10;  //设置每次传10k                              
                    FileStream stream = System.IO.File.OpenRead(localPath + "\\" + fileName);    //读取本地文件

                    RemoteFileInfo fileUpload = new RemoteFileInfo();
                    fileUpload.Path = fileTranserInfo.serverPath;
                    fileUpload.Name = fileName;

                    RemoteFileInfo file = fileManger.GetFile(fileUpload);   //更加文件名,查询服务中是否存在该文件
                    file.Name = fileName;
                    file.Length = stream.Length;
                    if (file.Length == file.Offset) //如果文件的长度等于文件的偏移量，说明文件已经上传完成
                    {
                        Result = file.FileFullPath;
                        return;
                    }
                    else
                    {
                        while (file.Length != file.Offset)  //循环的读取文件,上传，直到文件的长度等于文件的偏移量
                        {
                            file.Data = new byte[file.Length - file.Offset <= maxSiz ? file.Length - file.Offset : maxSiz]; //设置传递的数据的大小
                            stream.Position = file.Offset; //设置本地文件数据的读取位置
                            stream.Read(file.Data, 0, file.Data.Length);//把数据写入到file.Data中
                            file = fileManger.UploadFile(file);     //上传

                            e.Result = file.Offset;
                            (sender as BackgroundWorker).ReportProgress((int)(((double)file.Offset / (double)((long)file.Length)) * 100), file.Offset);
                            if (this.backgroundUploadWorker.CancellationPending)
                            {
                                stream.Close();
                                Result = "用户取消上传！";
                                return;
                            }
                        }
                        Result = file.FileFullPath;
                    }
                    stream.Close();
                }
            }
            catch (System.Exception ex)
            {
                LogText("Exception : " + ex.Message);
                if (ex.InnerException != null) LogText("Inner Exception : " + ex.InnerException.Message);
            }
        }
        #endregion

        #region Download
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="RemoteAddress">远程服务地址，需要从client中获取</param>
        /// <param name="filePath">本地保存路径，不带文件名</param>
        /// <param name="fileName">需要下载的文件名（远程）</param>
        public void DownloadFile(string RemoteAddress, string localPath, string serverFilePath, string downloadFileName)
        {
            this.backgroundDownloadWorker = new System.ComponentModel.BackgroundWorker();

            backgroundDownloadWorker.WorkerReportsProgress = true;
            backgroundDownloadWorker.WorkerSupportsCancellation = true;
            backgroundDownloadWorker.DoWork += new DoWorkEventHandler(backgroundDownloadWorker_DoWork);
            backgroundDownloadWorker.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker_ProgressChanged);
            backgroundDownloadWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker_RunWorkerCompleted);

            FileTransferInfo fileTranserInfo = new FileTransferInfo();
            fileTranserInfo.fileTransferType = FileTransferType.downloadFile;
            fileTranserInfo.remoteAddress = RemoteAddress;
            fileTranserInfo.localPath = localPath;
            fileTranserInfo.serverPath = serverFilePath;
            fileTranserInfo.fileName = downloadFileName;

            backgroundDownloadWorker.RunWorkerAsync(fileTranserInfo);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="RemoteAddress">远程服务地址，需要从client中获取</param>
        /// <param name="filePath">本地保存路径，不带文件名</param>
        /// <param name="fileName">需要下载的文件名（远程）</param>
        void backgroundDownloadWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                FileTransferInfo fileTranserInfo = e.Argument as FileTransferInfo;
                var fileManger = Common.ServiceBroker.FindService<IFileTransferService>(fileTranserInfo.remoteAddress); //创建WCF带来

                LogText("Start");

                // kill target file, if already exists
                string localFilePath = System.IO.Path.Combine(fileTranserInfo.localPath, fileTranserInfo.fileName);
                if (System.IO.File.Exists(localFilePath)) System.IO.File.Delete(localFilePath);

                // get stream from server
                DownloadRequest downloadRequest = new DownloadRequest();
                downloadRequest.FilePath = fileTranserInfo.serverPath;
                downloadRequest.FileName = fileTranserInfo.fileName;

                RemoteFileInfo downloadFileInfo = fileManger.DownloadFile(downloadRequest);
                long length = downloadFileInfo.Length;

                // write server stream to disk
                using (System.IO.FileStream writeStream = new System.IO.FileStream(localFilePath, System.IO.FileMode.CreateNew, System.IO.FileAccess.Write))
                {
                    byte[] buffer;
                    //获取所用块压缩流，并组装
                    while (fileManger.ReadNextBuffer())
                    {
                        // read bytes from input stream
                        buffer = fileManger.GetCurrentBuffer();

                        // write bytes to output stream
                        writeStream.Write(buffer, 0, buffer.Length);

                        // report progress from time to time
                        int Value = (int)(writeStream.Position * 100 / length);

                        (sender as BackgroundWorker).ReportProgress(Value);
                        if (this.backgroundDownloadWorker.CancellationPending)
                        {
                            writeStream.Close();
                            return;
                        }

                    }


                    // report end of progress
                    LogText("Done!");

                    writeStream.Close();
                }

            }
            catch (System.Exception ex)
            {
                LogText("Exception : " + ex.Message);
                if (ex.InnerException != null) LogText("Inner Exception : " + ex.InnerException.Message);
            }
        }
        #endregion
    }
}
