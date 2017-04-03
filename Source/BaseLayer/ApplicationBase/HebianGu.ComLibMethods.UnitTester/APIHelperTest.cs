using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using HebianGu.ComLibModule.API;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;

namespace HebianGu.ComLibMethods.UnitTester
{
    [TestClass]
    public class APIHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Console.WriteLine("准备锁定计算机！");

            Thread.Sleep(3000);

            //WindowsManager.Lock();

            Console.WriteLine("锁定完成！");

            Console.Read();
        }

        [TestMethod]
        public void WindowEnum()
        {
            WindowsEnumHelper.Instance.Register();

            Console.Read();
        }

        [TestMethod]
        public void WindowGetIP()
        {
            string name = Dns.GetHostName();

            IPAddress[] ipadrlist = Dns.GetHostAddresses(name);
            foreach (IPAddress ipa in ipadrlist)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                    Debug.WriteLine(ipa.ToString());
            }
            
        }
    }
}
