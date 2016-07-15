using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 双向链表
{
    /// <summary> 文档双向链表管理者 </summary>
    class PrirorityDocumnetManager
    {
        //文档双向链表
        private readonly LinkedList<Document> documnetList;

        private readonly List<LinkedListNode<Document>> priorityNodes;

        public PrirorityDocumnetManager()
        {
            documnetList = new LinkedList<Document>();

            priorityNodes = new List<LinkedListNode<Document>>(10);

            //制作10个优先级
            for (int i = 0; i < 10; i++)
            {
                priorityNodes.Add(new LinkedListNode<Document>(null));
            }
        }

        /// <summary> 插入文档 </summary>
        public void AddDocument(Document d)
        {
            if (d == null)
                throw new ArgumentNullException("d");

            AddDocumentToPriortyNode(d, d.Priority);

        }
        /// <summary> 按优先级插入双向链表 </summary>
        private void AddDocumentToPriortyNode(Document doc, int priority)
        {
            if (priority < 0 || priority > 9)
                throw new ArgumentException("Prority must by between 0 and 9 ");


            if (priorityNodes[priority].Value == null)//是否已有一个优先级节点与所传送的优先级相同
            {
                //递归是否有低一级节点
                --priority;
                if (priority >= 0)
                {
                    AddDocumentToPriortyNode(doc, priority);
                }
                else
                {
                    //优先级遍历最小
                    documnetList.AddLast(doc);
                    priorityNodes[doc.Priority] = documnetList.Last;
                }
                return;
            }
            else//存在当前优先级
            {
                LinkedListNode<Document> prioNode = priorityNodes[priority];//获取当前优先级
                if (priority == doc.Priority)//文件优先级和当前设置优先级一样
                {
                    documnetList.AddAfter(prioNode, doc);//存在当前优先级，加到当前优先级后面
                }
                else
                {
                    LinkedListNode<Document> firstPrioNode = prioNode;
                    while (firstPrioNode.Previous != null
                        && firstPrioNode.Previous.Value.Priority == prioNode.Value.Priority)
                    {
                        firstPrioNode = prioNode.Previous;
                        prioNode = firstPrioNode;
                    }
                    documnetList.AddBefore(firstPrioNode, doc);

                    priorityNodes[doc.Priority] = firstPrioNode.Previous;
                }
            }

        }

        /// <summary> 展示所有数据 </summary>
        public void DisplayAllNodes()
        {
            foreach (Document doc in documnetList)
            {
                Console.WriteLine("Priority: {0},title {1}", doc.Priority, doc.Title);
            }
        }

        /// <summary> 获取第一个节点，并移除 </summary>
        public Document GetDocument()
        {
            Document doc = documnetList.First.Value;
            documnetList.RemoveFirst();
            return doc;
        }
    }
}
