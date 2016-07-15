using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxArraySite<T> : BxElementSiteBase
    where T : BxArrayValueBase, new()
    {
        #region  members
        protected T _value;
        #endregion

        public T Value { get { return _value; } }
        public override IBxElement Element { get { return _value; } }

        #region  constructor
        public BxArraySite()
            : base()
        {
            _value = new T();
            _value.Owner = this;
            _value.InitCarrier(_carrier);
        }
        #endregion

        public override void InitCarrier(IBxElementCarrier carrier)
        {
            _carrier = carrier;
            _value.InitCarrier(carrier);
        }
        public override void InitFieldInfo(IBxCompound container, FieldInfo info)
        {
            base.InitFieldInfo(container, info);
            //获取Sub element 的配置
            _value.InitStaticUIConfig(GetXmlUIItem().SubArrayUIItem);
        }
        public override void InitStaticUIConfig(BxXmlUIItem staticItem)
        {
            base.InitStaticUIConfig(staticItem);
            _value.InitStaticUIConfig(staticItem.SubArrayUIItem);
        }


        #region IBxPersistStorageNode 成员
        public override void SaveStorageNode(IBxStorageNode node)
        {
            base.SaveStorageNode(node);
            if (_value.HasReferer)
            {
                string id = node.Storage.SVA.SaveValue(_value);
                node.SetElement(BxStorageLable.elementValueID, id);
            }
            else
            {
                _value.SaveStorageNode(node);
            }
        }
        public override void LoadStorageNode(IBxStorageNode node)
        {
            base.LoadStorageNode(node);
            string id = node.GetElementValue(BxStorageLable.elementValueID);
            if (string.IsNullOrEmpty(id))
            {
                _value.LoadStorageNode(node);
            }
            else
            {
                node.Storage.SVA.LoadValue(_value, id);
            }
        }
        #endregion
    }




}
