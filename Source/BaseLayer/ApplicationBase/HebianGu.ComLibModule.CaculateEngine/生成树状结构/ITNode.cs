using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{

    /// <summary> 带有子项的树形节点接口 </summary>
    public interface ITNode : INode
    {
        List<ITNode> Children { get; set; }
    }
}
