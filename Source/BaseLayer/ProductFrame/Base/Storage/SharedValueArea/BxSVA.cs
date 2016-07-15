using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    class BxSVA : IBxSharedValueArea
    {
        IBxStorageNode _nodeSva = null;
        IBxStorageNode _nodeVS = null;
        List<Item> _items = new List<Item>();
        BxSvaTypeSaver _tSaver = null;

        BxSvaTypeSaver TSaver
        {
            get
            {
                if (_tSaver == null)
                    _tSaver = new BxSvaTypeSaver();
                return _tSaver;
            }
        }
        public void Save()
        {
            if (_tSaver != null)
            {
                if (_nodeSva.HasChildNode(BxSSL.nodeTS))
                {
                    _nodeSva.RemoveChildNode(BxSSL.nodeTS);
                }
                IBxStorageNode nodeTS = _nodeSva.CreateChildNode(BxSSL.nodeTS);
                _tSaver.Save(nodeTS);
            }
        }
        public void Load(IBxStorageNode nodeSva)
        {
            _nodeSva = nodeSva;
            _nodeVS = null;
            _items = null;
            _tSaver = null;

            IBxStorageNode nodeTS = _nodeSva.GetChildNode(BxSSL.nodeTS);
            if (nodeTS != null)
            {
                _tSaver = new BxSvaTypeSaver();
                _tSaver.Load(nodeTS);
            }

            _nodeVS = _nodeSva.GetChildNode(BxSSL.nodeVS);
            if (_nodeVS != null)
            {
                _items = new List<Item>();
                IEnumerable<IBxStorageNode> childs = _nodeVS.ChildNodes;
                int index = 0;
                Item temp = null;
                string id = null;
                Type type = null;
                IBxReferableElement tempVal = null;
                foreach (IBxStorageNode one in childs)
                {
                    temp = new Item(index, one);
                    index++;
                    _items.Add(temp);
                    if (one.HasElement(BxSSL.elementTypeID))
                    {
                        id = one.GetElementValue(BxSSL.elementTypeID);
                        type = _tSaver.Get(id);
                        tempVal = type.GetConstructor(null).Invoke(null) as IBxReferableElement;
                        temp.Value = tempVal;
                    }
                }
            }
        }
        public void New(IBxStorageNode nodeSva)
        {
            _nodeSva = nodeSva;
            _nodeVS = _nodeSva.CreateChildNode(BxSSL.nodeVS);
            _items = new List<Item>();
            _tSaver = null;
        }

        public BxSVA()
        {
        }

        #region methods
        Item AddItem(IBxReferableElement val)
        {
            Item item = new Item(_items.Count, _nodeVS.CreateChildNode(BxSSL.nodeVItem + _items.Count.ToString()));
            item.Value = val;
            _items.Add(item);
            return item;
        }
        Item GetItem(IBxReferableElement val)
        {
            return _items.Find(x => x.Value == val);
        }
        Item GetItem(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;
            int index;
            if (Int32.TryParse(id, out index))
                return _items[index];
            return null;
        }
        #endregion

        #region IBxSharedValueArea 成员
        public string SaveValue(IBxReferableElement val)
        {
            Item item = GetItem(val);
            if (item != null)
                return item.Index.ToString();

            item = AddItem(val);
            IBxStorageNode node = item.Node;
            if (val is IBxPersistStorageNode)
                (val as IBxPersistStorageNode).SaveStorageNode(node);
            if (val.Owner == null)
            {
                string typeID = TSaver.Add(val.GetType());
                node.SetElement(BxSSL.elementTypeID, typeID);
            }
            return item.Index.ToString();
        }
        public void LoadValue(IBxReferableElement val, string id)
        {
            Item item = GetItem(id);
            if (item == null)
                throw new Exception("find share value " + id.ToString() + " failed!");
            if (item.Value != null)
                throw new Exception("a shared value is loaded more than once!");
            item.Value = val;
            (val as IBxPersistStorageNode).LoadStorageNode(item.Node);

            if (item.Refers != null)
            {
                foreach (IBxElementReferer one in item.Refers)
                {
                    one.ReferTo(val);
                }
                item.CLearRefers();
            }
        }
        public void RequestRestoreRefer(IBxElementReferer site, string id)
        {
            Item item = GetItem(id);
            if (item == null)
                throw new Exception("find share value " + id.ToString() + " failed!");
            if (item.Value != null)
            {
                site.ReferTo(item.Value);
            }
            else
            {
                item.AddRefer(site);
            }
        }
        #endregion

        #region subclass_Item
        public class Item
        {
            int _index;
            IBxStorageNode _node;
            IBxReferableElement _val = null;
            List<IBxElementReferer> _refers = null;
            public Item(int index, IBxStorageNode node)
            {
                _index = index;
                _val = null;
                _node = node;
            }
            public int Index { get { return _index; } }
            public IBxReferableElement Value { get { return _val; } set { _val = value; } }
            public IBxStorageNode Node { get { return _node; } }
            public List<IBxElementReferer> Refers { get { return _refers; } }

            public void AddRefer(IBxElementReferer refer)
            {
                if (_refers == null)
                    _refers = new List<IBxElementReferer>();
                _refers.Add(refer);
            }
            public void CLearRefers()
            {
                _refers = null;
            }
        }
        #endregion
    }

}
