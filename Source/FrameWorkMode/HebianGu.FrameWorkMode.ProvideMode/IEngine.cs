using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.FrameWorkMode.ProvideMode
{
    /// <summary> 引擎接口  </summary>
    public interface IEngine
    {
        void RunMonitor(List<ITimeEntity> entitys);
    }


    /// <summary> 动态曲线监控引擎 </summary>
    public class DnamicCurveEngine : IEngine
    {

        string _caseName = string.Empty;
        public DnamicCurveEngine(string caseName)
        {
            _caseName = caseName;
        }

        /// <summary> 执行监控 </summary>
        public void RunMonitor(List<ITimeEntity> entitys)
        {
            //  具体执行方法
            foreach (var e in entitys)
            {
                if (e is TimeEntity)
                {
                    TimeEntity tentity = e as TimeEntity;
                    tentity.CaseName = _caseName;
                }
            }
        }
    }
}
