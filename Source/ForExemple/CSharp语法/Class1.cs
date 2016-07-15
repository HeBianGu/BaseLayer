using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp语法
{

    /// <summary> 泛型的扩展 </summary>
    /*
     * 每个子类都会自动扩展出一个类型PorertyGroup : List<T>类型 
     * 相当于定义了两个类型
     * 前提是每个子类型都有相同的部分
     * 如果有不同的部分需要扩展继承
     */
    public abstract class ItemsKey<T> 
    {
        public ItemsKey()
        {

        }

        private PorertyGroup itemGroup = null;

        /// <summary> 项分组 </summary>
        public PorertyGroup ItemGroup
        {
            get { return itemGroup; }
            set { itemGroup = value; }
        }
        //***扩展处
        /// <summary> 用来记录分组 </summary>
        public class PorertyGroup : List<T>
        {

        }

    }
}
