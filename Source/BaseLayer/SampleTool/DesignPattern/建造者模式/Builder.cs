using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 建造者模式
{
    /// <summary> 厨房抽象建造者结构类 </summary>
    abstract class ChuFangBuilder
    {

        public abstract void BuildCaiban();

        public abstract void BuildWeibolu();

        public abstract void BuildDianfanguo();

        public abstract void BuildCaosao();


        public abstract ChuFang GetChuFang();
    }


    class MyChuFangBuilder : ChuFangBuilder
    {

        private ChuFang cuFang = new ChuFang();
        public override void BuildCaiban()
        {
            Console.WriteLine("杂牌子菜板");
        }

        public override void BuildWeibolu()
        {
            Console.WriteLine("苏宁微波炉");
        }

        public override void BuildDianfanguo()
        {
            Console.WriteLine("美的电饭锅");
        }

        public override void BuildCaosao()
        {
            Console.WriteLine("杂牌子炒勺");
        }

        public override ChuFang GetChuFang()
        {
            return cuFang;
        }
    }
}
