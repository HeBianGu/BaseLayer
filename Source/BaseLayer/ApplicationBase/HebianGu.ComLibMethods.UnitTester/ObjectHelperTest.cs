using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using HebianGu.ComLibModule.API;
using System.Diagnostics;
using HebianGu.ObjectBase.ObjectHelper;

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
    }
}
