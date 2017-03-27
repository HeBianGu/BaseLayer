using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{
    /// <summary> 带有父子关系的节点 </summary>
    public interface INode
    {
        int ParentID { get; set; }
        int ID { get; set; }
        string Name { get; set; }
    }


    public interface INodeModel
    {
        bool BuildFromNode(INode node);

        void AddChild(INodeModel child);
    }
}
 