using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 建造者模式
{
    class Program
    {
        static void Main(string[] args)
        {

            MyChuFangBuilder chuFangBuilder 
                = new MyChuFangBuilder();

            ChuFangDirector chuFangDirector 
                = new ChuFangDirector(chuFangBuilder);

            ChuFang myChuFang
                = chuFangBuilder.GetChuFang();
        }
    }
}
