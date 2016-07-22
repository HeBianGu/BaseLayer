using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.FrameWorkMode.PassvationMode
{
    class Program
    {

        static string FilePath;

        static void Main(string[] args)
        {

            RunTest();
        }

        static void RunTest()
        {

            FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "OrderChecks.xml");

            Console.WriteLine("-- 创建订单 --");

            Order order = BuildOrder();

            Console.WriteLine("-- 按任意键钝化 --");

            Console.Read();

            RunPassvation(order);

            Console.WriteLine("-- 按任意键反钝化 --");

            Console.Read();

            ReRunPassvation(order);

            Console.WriteLine("-- 按任意键关闭程序 --");

            Console.Read();

        }

        /// <summary> 执行钝化 </summary>
        static void RunPassvation(Order order)
        {
            OrderExamineApproveManager approveFlows = OrderExamineApproveManager.CreateFlows();

            //  序列化
            using (Stream stream = File.Open(FilePath, FileMode.Create))
            {
                BinaryFormatter format = new BinaryFormatter();
                format.Serialize(stream, approveFlows);

            }
        }

        /// <summary> 执行反钝化 </summary>
        static void ReRunPassvation(Order order)
        {
            OrderExamineApproveManager outApproverFlows;
            //  反序列化

            using (Stream stream = File.Open(FilePath, FileMode.Open))
            {
                BinaryFormatter format = new BinaryFormatter();
                outApproverFlows = format.Deserialize(stream) as OrderExamineApproveManager;

                outApproverFlows.RunFlows(order);
            }

            //  序列化
            using (Stream stream = File.Open(FilePath, FileMode.Create))
            {
                BinaryFormatter format = new BinaryFormatter();
                format.Serialize(stream, outApproverFlows);

            }
        }

        static Order BuildOrder()
        {
            Order order = new Order()
            {
                Items = new List<OrderItem>(),
                Customer = new CustomerInfo()
                {
                    Email = "Jack@Work.com",
                    Name = "Jack",
                    Phone = "13566894556"
                }

            };


            order.Items.Add(new OrderItem()
            {
                Number = 10,
                Product = new Product()
                {
                    Name = "自行车配件，链条",
                    Price = 100
                }
            });

            order.Items.Add(new OrderItem()
            {
                Number = 1,
                Product = new Product()
                {
                    Name = "齿轮",
                    Price = 500
                }
            });


            return order;
        }
    }
}
