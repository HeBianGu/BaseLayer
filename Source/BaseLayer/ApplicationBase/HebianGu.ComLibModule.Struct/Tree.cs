using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Struct
{
    /// <summary> 树形结构 </summary>
    [Serializable]
    public class Tree<V> : System.Collections.IEnumerable
    {
        #region - Private Field -

        private V _value = default(V);
        private Tree<V> _parent = null;
        private List<Tree<V>> _child = new List<Tree<V>>();

        #endregion

        #region - Constructor -

        public Tree()
        { }

        public Tree(V value)
        {
            _value = value;
        }

        #endregion

        #region - Index -

        public Tree<V> this[int index]
        {
            get
            {
                try
                {
                    return _child[index];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region Property

        public int Count
        {
            get { return _child.Count; }
        }

        public V Value
        {
            set { _value = value; }
            get { return _value; }
        }

        public Tree<V> Parent
        {
            set
            {
                try
                {
                    if (_parent != null)
                        _parent._child.Remove(this);

                    _parent = value;

                    if (_parent != null)
                        _parent._child.Add(this);
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
            get { return _parent; }
        }

        #endregion

        #region Public Method

        public Tree<V> SetChild(V value)
        {
            return SetChild(new Tree<V>(value));
        }

        public Tree<V> SetChild(Tree<V> child)
        {
            child.Parent = this;
            return child;
        }

        public Tree<V> RmvChild(int index)
        {
            try
            {
                return RmvChild(this[index]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tree<V> RmvChild(Tree<V> child)
        {
            try
            {
                if (child != null)
                {
                    child.Parent = null;
                }

                return child;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Tree<V>[] GetChilds()
        {
            return _child.ToArray();
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            return _child.GetEnumerator();
        }

        #endregion
    }
}
