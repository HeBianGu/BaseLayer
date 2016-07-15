using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 双向链表
{
    class Document
    {
        public string Title
        {
            get;
            private set;
        }

        public string Content
        {
            get;
            private set;
        }

        /// <summary> 文档优先级 </summary>
        public int Priority
        {
            get;
            private set;
        }

        public Document(string title,string content, int priority)
        {
            this.Title = title;
            this.Content = content;
            this.Priority = priority;
        }
    }
}
