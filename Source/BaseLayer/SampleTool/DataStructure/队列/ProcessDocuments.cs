using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 队列
{
    /// <summary> 单独的任务中处理队列中的文档 </summary>
    class ProcessDocuments
    {
        //暴漏在外面的启动任务处理文档的方法
        public static void Start(DocumentManager dm)
        {
            //创建任务传递方法
            Task.Factory.StartNew(new ProcessDocuments(dm).Run);
        }


        protected ProcessDocuments(DocumentManager dm)
        {
            if (dm == null)
                throw new ArgumentNullException("dm");
            documnetManager = dm;
        }

        //文档
        private DocumentManager documnetManager;

        //处理方法
        protected void Run()
        {
            while (true)
            {
                if (documnetManager.IsDocumentAvailable)
                {
                    Document doc = documnetManager.GetDocument();
                    Console.WriteLine("Process Document {0}", doc.Title);
                }
                Thread.Sleep(new Random().Next(20));
            }
        }

    }
}
