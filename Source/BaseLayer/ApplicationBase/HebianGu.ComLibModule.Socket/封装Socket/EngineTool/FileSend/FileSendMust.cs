using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HebianGu.ComLibModule.SocketHelper
{
    internal class FileSendMust : FileMustBase,IFileSendMust
    {
        private IFileSendMust fileSendMust = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="FileSendMust">IFileSendMust</param>
        public FileSendMust(IFileSendMust FileSendMust)
            : base(FileSendMust)
        {
            fileSendMust = FileSendMust;
        }

        #region IFileSendMust 成员

        public void SendSuccess(int FileLabel)
        {
            EngineTool.EventInvoket(() => { this.fileSendMust.SendSuccess(FileLabel); });
        }

        public void FileRefuse(int FileLabel)
        {
            EngineTool.EventInvoket(() => { this.fileSendMust.FileRefuse(FileLabel); });
        }

        public void FileStartOn(int FileLabel)
        {
            EngineTool.EventInvoket(() => { this.fileSendMust.FileStartOn(FileLabel); });
        }

        #endregion
    }
}
