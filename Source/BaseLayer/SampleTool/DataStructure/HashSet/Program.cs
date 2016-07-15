using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashSet
{
    class Program
    {
        static void Main(string[] args)
        {
            //不重复元素的无序列表

            var companyTeams = new HashSet<string>()
            {
                "aaa" ,"bbb","ccccc"
            };


            var privateTeams = new HashSet<string>()
            {
                "ddd","bbb"
            };

            if (companyTeams.Add("eee"))//添加成员
                Console.WriteLine("eee add success");

            if (!privateTeams.Add("ddd"))//成员已经存在返回False
                Console.WriteLine("ddd wa already in this set");

            if (companyTeams.IsSubsetOf(privateTeams))
                return;



        }
    }
}
