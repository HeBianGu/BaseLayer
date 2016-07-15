using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Struct
{
    /// <summary> 泛型树形 </summary>
    public class GeneralTree<T>
    {
        private GeneralTree<T> _parent = null;
        private List<GeneralTree<T>> _child = new List<GeneralTree<T>>();
        private T _value = default(T);

        public GeneralTree()
        {

        }

        public GeneralTree(T value)
        {
            _value = value;
        }

        public T Value
        {
            set { _value = value; }
            get { return _value; }
        }

        public GeneralTree<T> Parent
        {
            set
            {
                _parent = value;
                if (_parent._child == null)
                    _parent._child = new List<GeneralTree<T>>();

                _parent._child.Add(this);
            }
            get { return _parent; }
        }

        public GeneralTree<T>[] Childs
        {
            get { return _child.ToArray(); }
        }

        public void SetChild(GeneralTree<T> value)
        {
            if (value != null)
            {
                value.Parent = this;
            }
        }
    }
}
