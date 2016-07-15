using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 队列
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建文档管理者
            var dm = new DocumentManager();

            //在任务中启动文档监控
            ProcessDocuments.Start(dm);

            for (int i = 0; i < 100; i++)
            {
                var doc = new Document(i.ToString(), "content");
                dm.AddDocument(doc);
                Console.WriteLine("Add documnet {0}", i.ToString());
                Thread.Sleep(new Random().Next(20));

            }
        }
    }
}
