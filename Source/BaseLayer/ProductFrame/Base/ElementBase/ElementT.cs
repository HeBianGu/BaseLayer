using System;
using System.Collections.Generic;

namespace OPT.PEOffice6.BaseLayer.Base
{
    public abstract class BxStructValueElementT<T> : BxElementBase, IBxUIValue, IBxPersistString
       where T : struct
    {
        #region  members
        protected T _value = default(T);
        protected bool _valid = false;
        #endregion

        #region  properties
        public T Value { get { return _value; } set { _value = value; } }
        public bool Valid
        {
            get { return _valid; }
            set { _valid = value; }
        }
        #endregion

        #region  constructor
        public BxStructValueElementT(IBxElement container, IBxElementCarrier carrier)
            : base(container, carrier)
        { }
        public BxStructValueElementT(IBxElementEx container)
            : base(container, container.Carrier)
        { }
        #endregion

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            base.SaveStorageNode(node);
            if (!_valid)
                return;
            node.SetElement(BxStorageLable.elementValue, SaveToString());
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            base.LoadStorageNode(node);
            string sVal = node.GetElementValue(BxStorageLable.elementValue);
            if (string.IsNullOrEmpty(sVal))
            {
                _valid = false;
            }
            else
            {
                LoadFromString(sVal);
            }
        }
        #endregion

        #region IBxPersistString 成员
        public virtual string SaveToString()
        {
            return _value.ToString();
        }
        public abstract bool LoadFromString(string s);
        #endregion

        #region IBxUIValue 成员
        public virtual string GetUIValue()
        {
            return SaveToString();
        }
        public virtual bool SetUIValue(string val)
        {
            return LoadFromString(val);
        }
        #endregion
    }


}
