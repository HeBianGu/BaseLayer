using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Struct
{
    /// <summary> 树结构键值对象 </summary>
    [Serializable]
    public class TreeFrame<V> : IEnumerable
    {
        #region - Private Field -

        private string _key = null;
        private V _value = default(V);
        private TreeFrame<V> _parent = null;
        private List<TreeFrame<V>> _child = new List<TreeFrame<V>>();

        #endregion

        #region - Constructor -

        public TreeFrame(string key)
        {
            _key = key;
        }

        public TreeFrame(string key, V value)
        {
            _key = key;
            _value = value;
        }

        #endregion

        #region - Index -

        public TreeFrame<V> this[int i]
        {
            get { return _child[i]; }
        }

        public TreeFrame<V> this[string key]
        {
            get { return GetChild(key); }
        }

        #endregion

        #region - Property -

        public int Count
        {
            get { return _child.Count; }
        }

        public string Key
        {
            set
            {
                if (_parent != null && _parent.GetChild(value) != null)
                    throw new Exception("节点所在层级已经包括该键“" + value + "”");
                else
                    _key = value;
            }
            get { return _key; }
        }

        public V Value
        {
            set { _value = value; }
            get { return _value; }
        }

        public TreeFrame<V> Parent
        {
            set
            {
                try
                {
                    if (_parent != null)
                        _parent._child.Remove(this);

                    _parent = value;

                    if (_parent != null)
                    {
                        if (_parent.GetChild(this._key) != null)
                            throw new Exception("子集中已经存在键值。");
                        _parent._child.Add(this);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            get { return _parent; }
        }

        #endregion

        #region - Public Method -

        public TreeFrame<V> SetChild(TreeFrame<V> childTn)
        {
            try
            {
                childTn.Parent = this;
                return childTn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TreeFrame<V> SetChild(string key, V value)
        {
            try
            {
                return SetChild(new TreeFrame<V>(key, value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TreeFrame<V> GetChild(string key)
        {
            return
            _child.Find(
                new Predicate<TreeFrame<V>>(
                    delegate(TreeFrame<V> target)
                    {
                        return target._key == key;
                    }));
        }

        public TreeFrame<V> RmvChild(TreeFrame<V> childTn)
        {
            try
            {
                if (childTn == null)
                    return null;
                else
                {
                    childTn.Parent = null;
                    return childTn;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TreeFrame<V> RmvChild(string key)
        {
            try
            {
                return RmvChild(this[key]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return _child.GetEnumerator();
        }

        public TreeFrame<V>[] GetChilds()
        {
            return _child.ToArray();
        }

        public override string ToString()
        {
            return this._key + " = " + (this.Value != null ? this.Value.ToString() : string.Empty);
        }

        #endregion

    }
}
