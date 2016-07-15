using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 双向链表
{
    class Program
    {
        //------------------------------------------
        //1、插入速度快
        //2、查找比较慢
        //3、精确插入
        //------------------------------------------
        static void Main(string[] args)
        {

            PrirorityDocumnetManager pdm = new PrirorityDocumnetManager();

            pdm.AddDocument(new Document("one","Sample",2));

            pdm.AddDocument(new Document("two", "Sample", 4));

            pdm.AddDocument(new Document("three", "Sample", 4));

            pdm.AddDocument(new Document("four", "Sample", 8));

            pdm.AddDocument(new Document("five", "Sample", 1));

            pdm.AddDocument(new Document("six", "Sample", 7));

            pdm.AddDocument(new Document("seven", "Sample", 7));

            pdm.DisplayAllNodes();

            Console.Read();
        }
    }
}
