using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 建造者模式
{
    class ChuFangDirector
    {
        public ChuFangDirector(ChuFangBuilder builder)
        {
            builder.BuildCaiban();
            builder.BuildCaosao();
            builder.BuildDianfanguo();
            builder.BuildWeibolu();
        }
    }
}
