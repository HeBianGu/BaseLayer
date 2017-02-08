using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine.委托外包模式
{

    /// <summary> 委托外包 </summary>
    class DelegateFace
    {
        /// <summary> 此方法的说明 </summary>
        public void Build()
        {
            // Todo ：被外包的委托 
            Action<int> act1 = l =>
            {
                Console.WriteLine("第1次");
            };

            // Todo ：外包的委托 
            Action<Action<int>> act2 = l =>
            {
                Console.WriteLine("第2次");
                l(1);
            };

            // Todo ：外包的外包的委托 
            Action<Action<Action<int>>> act3 = l =>
            {
                Console.WriteLine("第3次");
                l(act1);
            };

            // Todo ：外包的外包的委托 
            Action<Action<Action<int>>> act33 = l =>
            {
                Console.WriteLine("另一个第3次");
                l(act1);
            };

            // Todo ：外包的外包的外包的委托 
            Action<Action <Action<Action<int>>>> act4 = l =>
            {
                Console.WriteLine("第4次");
                l(act2);
            };

            if(true)
            {
                act4(act3);
            }
            else
            {
                act4(act33);
            }
           
        }

    }
}
