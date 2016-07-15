using System;
using System.Collections.Generic;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public abstract class BxSingleReferableElement : BxReferableElementBase, IBxPersistString, IBxUIValue
    {
        bool _valid;
        public virtual bool Valid { get { return _valid; } set { _valid = value; } }

        public BxSingleReferableElement() { _valid = false; }

        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            node.SetElement(BxStorageLable.elementValue, SaveToString());
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            LoadFromString(node.GetElementValue(BxStorageLable.elementValue));
        }
        #endregion

        #region IBxUIValue 成员
        public virtual string GetUIValue()
        {
            return SaveToString();
        }
        public virtual bool SetUIValue(string val)
        {
            if (LoadFromString(val))
            {
                Valid = true;
                return true;
            }
            Valid = false;
            return false;
        }
        #endregion

        #region IBxPersistString 成员
        public abstract string SaveToString();
        public abstract bool LoadFromString(string s);
        #endregion
    }


    public abstract class BxSingleReferableElementT<T> : BxSingleReferableElement
    {
        T _value;

        public BxSingleReferableElementT() { _value = default(T); }
        public BxSingleReferableElementT(T val) { _value = val; Valid = true; }

        public virtual T Value
        {
            get
            {
                if (!Valid)
                    throw new Exception("invalid value!");
                return _value;
            }
            set
            {
                Valid = true;
                _value = value;
                OnModified();
            }
        }


        #region IBxPersistString 成员
        public override string SaveToString()
        {
            return _value.ToString();
        }
        #endregion





    }
}
