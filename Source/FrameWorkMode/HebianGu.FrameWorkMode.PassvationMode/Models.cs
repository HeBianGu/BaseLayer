using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.FrameWorkMode.PassvationMode
{
    /// <summary> 订单 </summary>
    public class Order
    {
        public string OrderId
        {
            get;
            set;
        }

        public CustomerInfo Customer
        {
            get;
            set;
        }

        public List<OrderItem> Items
        {
            get;
            set;
        }
    }

    /// <summary> 客户信息 </summary>
    public class CustomerInfo
    {
        public string Name
        {
            get;
            set;
        }

        public string Phone
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }
    }

    /// <summary> 订单项 </summary>
    public class OrderItem
    {
        public Product Product
        {
            get;
            set;
        }


        public int Number
        {
            get;
            set;
        }
    }

    /// <summary> 产品 </summary>
    public class Product
    {
        public string Name
        {
            get;
            set;
        }
        public int Price
        {
            get;
            set;
        }
    }
}
