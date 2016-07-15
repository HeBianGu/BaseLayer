using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 队列
{
    /// <summary> 创建一个Manager管理集合 </summary>
    class DocumentManager
    {

        private readonly Queue<Document> documentQueue = new Queue<Document>();

        public void AddDocument(Document document)
        {
            //1、线程安全
            lock (this)
            {
                documentQueue.Enqueue(document);
            }
        }

        public Document GetDocument()
        {
            Document document = null;
            lock (this)
            {

                documentQueue.Dequeue();

            }
            return document;
        }

        public bool IsDocumentAvailable
        {
            get
            {
                return documentQueue.Count > 0;
            }
        }
    }
}
