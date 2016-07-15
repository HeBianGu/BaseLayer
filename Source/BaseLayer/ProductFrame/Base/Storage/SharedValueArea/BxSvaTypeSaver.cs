using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    class BxSvaTypeSaver
    {
        List<BxAssembly> _assList = new List<BxAssembly>();

        public BxSvaTypeSaver() { }

        public string Add(Type type)
        {
            BxAssembly ass = AddAssembly(type.Assembly);
            Int32 typeID = ass.AddType(type);
            return string.Format("{0},{1}", ass.Index, typeID);
        }
        public Type Get(string id)
        {
            int pos = id.IndexOf(',');
            string assID = id.Substring(0, pos);
            string typeID = id.Substring(pos);
            if (string.IsNullOrEmpty(id))
                return null;
            Int32 id1, id2;
            if (Int32.TryParse(assID, out id1))
            {
                BxAssembly ass = _assList[id1];
                if (Int32.TryParse(typeID, out id2))
                    return ass.GetType(id2);
            }
            return null;
        }

        BxAssembly AddAssembly(Assembly ass)
        {
            BxAssembly result = _assList.Find(x => x.Ass == ass);
            if (result == null)
            {
                result = new BxAssembly(ass, _assList.Count);
                _assList.Add(result);
            }
            return result;
        }
        BxAssembly GetAssembly(Int32 id)
        {
            return _assList[id];
        }

        public void Save(IBxStorageNode node)
        {
            if (_assList == null)
                return;

            IBxStorageNode assNode = null;
            foreach (BxAssembly one in _assList)
            {
                assNode = node.CreateChildNode(BxSSL.nodeAss + one.Index.ToString());
                one.Save(assNode);
            }
        }
        public void Load(IBxStorageNode node)
        {
            IEnumerable<IBxStorageNode> childs = node.ChildNodes;
            _assList.Clear();
            BxAssembly temp;
            int index = 0;
            foreach (IBxStorageNode one in childs)
            {
                temp = new BxAssembly(null, index);
                index++;
                _assList.Add(temp);
                temp.Load(one);
            }
        }

    }
    
}
