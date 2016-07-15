using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.FrameWorkMode.ProvideMode
{
    /// <summary> 测试 </summary>
    public class Test
    {
        public void Run(string baseFilePath)
        {
            //  实例提供模式实体的对象工厂
            ITimeEntityFactory timeEntitysFactory = new EclipseTimeEntityFactory(baseFilePath);

            //  创建实体
            List<ITimeEntity> timeEntitys = timeEntitysFactory.CreateTimeEntitys();

            
            IEngine dynamicCurveEngine = new DnamicCurveEngine("数值模拟");

            //  利用引擎执行提供模式的实体
            dynamicCurveEngine.RunMonitor(timeEntitys);

        }
    }
}
