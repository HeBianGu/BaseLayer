using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using OPT.Product.BaseInterface;


namespace OPT.Product.Base
{
    public class BxSUICPregnant : IBxStaticUIConfigPregnant
    {
        string _id;
        IBxStaticUIConfigProvider _suicProvider;
        BxXmlUIItem _suic;

        public BxSUICPregnant() { _suicProvider = null; _suic = BxXmlUIItem.Invalid; }
        public BxSUICPregnant(string fullID)
        {
            _id = fullID;
            _suicProvider = null;
            _suic = BxXmlUIItem.Invalid;
        }
        public BxSUICPregnant(string fullID, IBxStaticUIConfigProvider suicProvider)
        {
            _id = fullID;
            _suicProvider = suicProvider;
            _suic = BxXmlUIItem.Invalid;
        }

        #region IBxStaticUIConfigPregnant 成员
        public string ConfigID
        {
            get { return _id; }
        }
        public void InitSUICProvider(IBxStaticUIConfigProvider suicProvider)
        {
            _suicProvider = suicProvider;
            _suic = BxXmlUIItem.Invalid;
        }
        public BxXmlUIItem StaticUIConfig
        {
            get
            {
                if (_suic == BxXmlUIItem.Invalid)
                {
                    _suic = null;
                    if (_suicProvider != null)
                        _suic = _suicProvider.GetStaticUIConfig(_id);
                    else
                        _suic = BxSystemInfo.Instance.SUICProvider.GetStaticUIConfig(_id);
                }
                return _suic;
            }
        }
        #endregion

        public static BxSUICPregnant Invalid = new BxSUICPregnant(null, null);
    }
}