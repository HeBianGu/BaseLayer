using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.NetWork
{
    class Program
    {
        static void Main(string[] args)
        {

            //string ss = Class1.GetIP();

            //Console.WriteLine(ss);
            Test1();
           
        }
        
        // Todo ：测试网卡殷勤 
        static void Test1()
        {
            NetworkInterface ii = NetWorkEngine.Instance.GetNetwork(l => l.NetworkInterfaceType == NetworkInterfaceType.Ethernet);
            ii = NetWorkEngine.Instance.GetDefaltNetwork();
            NetWorkEngine.Instance.Register(ii, l => Console.WriteLine(l));
            NetWorkEngine.Instance.Start();

            Console.Read();
        }


    }
}
