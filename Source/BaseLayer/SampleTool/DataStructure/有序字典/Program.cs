using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 有序字典
{
    class Program
    {
        static void Main(string[] args)
        {
            //与SortList比较
            //SortList<TKey,TValue> 类使用的内存比较小，是基于数组的列表
            //SortLDictionary<TKey,TvALUE> 类的元素插入和删除速度比较快


            SortedDictionary<string, string> sortedDictionary 
                = new SortedDictionary<string, string>();

            sortedDictionary.Add("A","222");
            sortedDictionary.Add("b", "222");
            sortedDictionary.Add("e", "222");
            sortedDictionary.Add("c", "222");
            sortedDictionary.Add("u", "222");
            sortedDictionary.Add("g", "222");
            sortedDictionary.Add("r", "222");

            foreach( var s in sortedDictionary)
            {
                Console.WriteLine("Key:{0}  Value {1}", s.Key, s.Value);
            }
            Console.Read();
        }
    }
}
