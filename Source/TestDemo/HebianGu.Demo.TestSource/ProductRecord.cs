using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.Demo.TestSource
{
    /// <summary> 产品固定 产品信息可变 </summary>
    public class ProductRecord
    {

        public ProductRecord(object c, object p1, object p2, object p3, int ck, int pk = 0)
        {
            this.category = c;
            this.product1 = p1;
            this.product2 = p2;
            this.product3 = p3;
            this.id = ck;
            this.pid = pk;
        }

        int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }



        int pid;

        public int Pid
        {
            get { return pid; }
            set { pid = value; }
        }

       

        object category;

        public object Category
        {
            get { return category; }
            set { category = value; }
        }

        object product1;

        public object Product1
        {
            get { return product1; }
            set { product1 = value; }
        }

        object product2;

        public object Product2
        {
            get { return product2; }
            set { product2 = value; }
        }

        object product3;

        public object Product3
        {
            get { return product3; }
            set { product3 = value; }
        }
    }
}
