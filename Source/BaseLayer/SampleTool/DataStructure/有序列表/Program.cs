using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 有序列表
{
    class Program
    {
        static void Main(string[] args)
        {
            //有序列表 按key自动排序 key值不可以重复
            SortedList<string, string> books = new SortedList<string, string>();

            books.Add("A", "11");
            books.Add("B", "12");
            books.Add("F", "22");
            books.Add("D", "13");
            books.Add("C", "21");

            //遍历节点
            foreach (var book in books)
            {
                Console.WriteLine(book.Key);
                Console.WriteLine(book.Value);

            }

            //遍历健
            foreach (var key in books.Keys)
            {
                Console.WriteLine(key);
            }

            //遍历值
            foreach (var value in books.Values)
            {
                Console.WriteLine(value);
            }

            //是否包含
            if(books.ContainsKey("sss"))
            {
                Console.WriteLine("存在sss健");
            }

            //是否包含
            string getValue;
            if(books.TryGetValue("sss",out getValue))
            {
                Console.WriteLine("没有获取到值！");
            }
            Console.Read();
        }
    }
}
