﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OPT.PCOCCenter.Service.Interface;
using System.IO;

namespace OPT.PCOCCenter.Service
{
    public class FileTransferService : IFileTransferService
    {
        static private byte[] buffer_currect = null;
        static private int get_buffer_length = 1024 * 10;  //设置每次传10k
        static private long remain_length;
        static private System.IO.FileStream stream = null;


        public RemoteFileInfo DownloadFile(DownloadRequest request)
        {
            RemoteFileInfo result = new RemoteFileInfo();
            result.Length = 0;
            try
            {
                // get some info about the input file
                string filePath = System.IO.Path.Combine(GetUploadFolder(request.FilePath), request.FileName);
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);// 问题，自动删除文件不知道为什么

                // report start
                Console.WriteLine("Sending stream " + request.FileName + " to client");
                Console.WriteLine("Size " + fileInfo.Length);

                // check if exists
                if (!fileInfo.Exists)
                {
                    Console.WriteLine("File {0} not found!", request.FileName);
                }
                else
                {
                    // open stream
                    stream = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    remain_length = fileInfo.Length;

                    // return result
                    result.Name = request.FileName;
                    result.Length = fileInfo.Length;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return result;
        }

        private string GetUploadFolder(string savePath)
        {
            string uploadFolder = System.Configuration.ConfigurationManager.AppSettings["filePath"];
            if (string.IsNullOrEmpty(uploadFolder))
            {
                uploadFolder = AppDomain.CurrentDomain.BaseDirectory;
            }             
            
            if (string.IsNullOrEmpty(savePath)) savePath = @"upload\";

            if (!uploadFolder.EndsWith("\\")) uploadFolder += "\\";
            if (!savePath.EndsWith("\\")) savePath += "\\";

            uploadFolder = uploadFolder + savePath;
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            return uploadFolder;
        }

        public FileResult DeleteFile(RemoteFileInfo file) //根据文件名删除文件，返回删除结果
        {
            FileResult result = new FileResult();
            string filePath = GetUploadFolder(file.Path) + file.Name;
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                }
                catch (System.Exception ex)
                {
                    result.ErrorInfo = ex.Message;
                }
            }

            result.FullPath = filePath;
            result.Result = true;

            return result;
        }

        public RemoteFileInfo GetFile(RemoteFileInfo file)
        {
            string filePath = GetUploadFolder(file.Path) + file.Name;
            if (File.Exists(filePath))
            {
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);
                RemoteFileInfo fileNew = new RemoteFileInfo();
                fileNew.Path = file.Path;
                fileNew.Name = file.Name;
                fileNew.Offset = fs.Length;
                fileNew.FileFullPath = filePath;
                fs.Close();
                return fileNew;
            }
            else
            {
                RemoteFileInfo fileNew = new RemoteFileInfo();
                fileNew.Path = file.Path;
                fileNew.Name = file.Name;
                fileNew.Offset = 0;
                fileNew.FileFullPath = filePath;
                return fileNew;
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public RemoteFileInfo UploadFile(RemoteFileInfo file)
        {
            string filePath = GetUploadFolder(file.Path) + file.Name;//获取文件的路径,已经保存的文件名
            FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate);//打开文件
            long offset = file.Offset;  //file.Offset 文件偏移位置,表示从这个位置开始进行后面的数据添加
            BinaryWriter writer = new BinaryWriter(fs);//初始化文件写入器
            writer.Seek((int)offset, SeekOrigin.Begin);//设置文件的写入位置
            writer.Write(file.Data);//写入数据

            file.Offset = fs.Length;//返回追加数据后的文件位置
            file.Data = null;
            writer.Close();
            fs.Close();

            return file;
        }

        #region IDataTransfers 成员
        /// <summary>
        /// 设置压缩后字节流分块，每一块的大小
        /// </summary>
        /// <param name="length"></param>
        public void SetBufferLength(int length)
        {
            get_buffer_length = length;
        }
        /// <summary>
        /// 读取压缩后字节流一块，并提升字节流的位置
        /// </summary>
        /// <returns></returns>
        public bool ReadNextBuffer()
        {
            bool bo;
            if (remain_length > 0)
            {
                if (remain_length > get_buffer_length)
                {
                    buffer_currect = new byte[get_buffer_length];

                    stream.Read(buffer_currect, 0, get_buffer_length);
                    remain_length -= get_buffer_length;
                }
                else
                {
                    buffer_currect = new byte[remain_length];
                    stream.Read(buffer_currect, 0, (int)remain_length);
                    remain_length = 0;
                }

                bo = true;
            }
            else
                bo = false;
            return bo;

        }
        /// <summary>
        /// 获取当前块的字节流
        /// </summary>
        /// <returns></returns>
        public byte[] GetCurrentBuffer()
        {
            if (buffer_currect != null)
                return buffer_currect;
            else
                return null;

        }

        #endregion
    }
}