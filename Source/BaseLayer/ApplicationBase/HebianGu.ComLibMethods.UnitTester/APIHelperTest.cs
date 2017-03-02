using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using HebianGu.ComLibModule.API;

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
    }
}
