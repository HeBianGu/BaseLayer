using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Windows服务调用
{
    class Program
    {
        static void Main(string[] args)
        {
            //while (true)
            //{

            try
            {
                Console.WriteLine("输入服务路径");
                string servicePath = Console.ReadLine();
                Console.WriteLine("0:安装，1：卸载，其他：运行服务");

                int p = int.Parse(Console.ReadLine());
                if (p == 0)
                {
                    //  启动服务
                    Process.Start(servicePath, "/i");
                }
                else if (p == 1)
                {
                    //  卸载服务
                    Process.Start(servicePath, "/u");
                }
                else
                {
                    //  运行服务
                    Process.Start(servicePath);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
