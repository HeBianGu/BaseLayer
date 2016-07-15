using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    class BxAssembly
    {
        Assembly _ass;
        List<Type> _types = new List<Type>();
        Int32 _index;

        public Assembly Ass { get { return _ass; } }
        public Int32 Index { get { return _index; } }
        public BxAssembly(Assembly ass, Int32 index)
        {
            _ass = ass;
            _index = index;
        }
        public Int32 AddType(Type type)
        {
            int index = _types.IndexOf(type);
            if (index < 0)
            {
                index = _types.Count;
                _types.Add(type);
            }
            return index;
        }
        public Type GetType(Int32 id)
        {
            return _types[id];
        }

        public void Save(IBxStorageNode node)
        {
            node.SetElement(BxSSL.elementValue, _ass.FullName);

            IBxStorageNode childNode = null;
            int index = 0;
            foreach (Type one in _types)
            {
                childNode = node.CreateChildNode(BxSSL.nodeItem + index.ToString());
                childNode.SetElement(BxSSL.elementValue, one.FullName);
                index++;
            }
        }
        public void Load(IBxStorageNode node)
        {
            string assName = node.GetElementValue(BxSSL.elementValue);
            _ass = Assembly.Load(assName);

            IEnumerable<IBxStorageNode> childs = node.ChildNodes;
            _types.Clear();
            Type temp;
            string typeName;
            foreach (IBxStorageNode one in childs)
            {
                typeName = one.GetElementValue(BxSSL.elementValue);
                temp = _ass.GetType(typeName);
                _types.Add(temp);
            }
        }
    }
}
