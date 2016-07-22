using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MEF
{
    /// <summary> ① 必须拥有Export特性  [Export(typeof(IMefDemoBase))] </summary>
    [Export(typeof(IMefDemoBase))]
    public class MefDemoBase : IMefDemoBase
    {
        public void Init()
        {
            throw new NotImplementedException();
        }
    }
}
