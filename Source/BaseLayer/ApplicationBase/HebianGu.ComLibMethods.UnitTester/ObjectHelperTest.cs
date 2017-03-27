using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using HebianGu.ComLibModule.API;
using System.Diagnostics;
using HebianGu.ObjectBase.ObjectHelper;
using System.Linq;
using System.Drawing;

namespace HebianGu.ComLibMethods.UnitTester
{
    [TestClass]
    public class ObjectHelperTest
    {
        [TestMethod]
        public void TestIsHaveRegisterEvent()
        {
            HookKeyboardEngine.KeyDown += HookKeyboardEngine_KeyDown;

            bool isHave = typeof(HookKeyboardEngine).IsHaveRegisterEvent("s_KeyDown", "HookKeyboardEngine_KeyDown");

            Assert.IsTrue(isHave);
        }

        private void HookKeyboardEngine_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            Debug.WriteLine("键盘按下：" + e.KeyValue.ToString());
        }

        [TestMethod]
        public void TestSwich()
        {
            string typeName = string.Empty;

            int typeId = 2;

            typeId.Switch((string s) => typeName = s)
                .Case(0, "食品")
                .Case(1, "饮料")
                .Case(2, "酒水")
                .Case(3, "毒药")
                .Default("未知");

            Assert.Equals(typeName, "酒水");
        }

        [TestMethod]
        public void TestSwich2()
        {
            string password = "34343";
            Color backColor = default(Color);
            password.Switch(p => p.Length, (int red) => backColor = Color.FromArgb(255, 256 - red, 256 - red))
                .Case(l => l <= 4, 256)
                .Case(l => l <= 6, 192)
                .Case(7, 128)
                .Case(8, 64)
                .Default(0);

            Assert.Equals(backColor, Color.FromArgb(255, 256 - 192, 256 - 192));
        }

        [TestMethod]
        public void TestSwich3()
        {
            int count = 112;
            int score = 0;
            count.Switch((int i) => score += i)
                .Case(c => c > 5, 1, false)
                .Case(c => c > 10, 10, false)
                .Case(c => c > 20, 100, false)
                .Case(c => c > 50, 1000, false)
                .Case(c => c > 100, 10000, false);

            Assert.Equals(score, 11111);
        }

        [TestMethod]
        public void TestIf()
        {
            //扩展方式
            int int0 = -121;
            int int1 = int0.If(i => i < 0, i => -i).If(i => i > 100, i => i - 100).If(i => i % 2 == 1, i => i - 1);

            //常规方式
            int int3 = -121;
            if (int3 < 0) int3 = -int3;
            if (int3 > 100) int3 -= 100;
            if (int3 % 2 == 1) int3--;
        }

        [TestMethod]
        public void TestIf1()
        {
            //从邮箱变换成主页
            string email = "ldp615@163.com";
            string page = email.If(s => s.Contains("@"), s => s.Substring(0, s.IndexOf("@")))
                .If(s => !s.StartsWith("www."), s => s = "www." + s)
                .If(s => !s.EndsWith(".com"), s => s += ".com");
        }

        [TestMethod]
        public  void TestSwitchList()
        {
            string englishName = "apple";
            string chineseName = englishName.Switch(
                new string[] { "apple", "orange", "banana", "pear" },
                new string[] { "苹果", "桔子", "香蕉", "梨" },
                "未知"
                );
            Console.WriteLine(chineseName);
        }
        [TestMethod]

        public static void TestWhile()
        {
            People people = new People { Name = "Wretch" };
            people.While(
                p => p.WorkCount < 7,
                p => p.Work()
                    );
            people.Rest();
        }

    }

    public class People
    {
        public string Name { get; set; }
        public bool IsHungry { get; set; }
        public bool IsThirsty { get; set; }
        public bool IsTired { get; set; }
        public int WorkCount { get; private set; }

        public void Eat()
        {
            Console.WriteLine("Eat");
            IsHungry = false;
        }
        public void Drink()
        {
            Console.WriteLine("Drink");
            IsThirsty = false;
        }
        public void Rest()
        {
            Console.WriteLine("Rest");
            IsTired = false;
        }
        public void Work()
        {
            Console.WriteLine("Work");
            IsHungry = IsThirsty = IsTired = true;
            WorkCount++;
        }
    }
}
