using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using OPT.Product.BaseInterface;

namespace OPT.Product.Base
{
    public class BxCompoundFieldData : IBxStaticUIConfigPregnant
    {
        public static BxXmlUIItem InvalidSUIC = new BxXmlUIItem();

        BxCompoundCoreFieldData _fi;
        IBxStaticUIConfigProvider _suicProvider;
        BxXmlUIItem _staticUIConfig;

        public IBxStaticUIConfigProvider SUICProvider
        {
            get { return _suicProvider; }
        }

        //suicProvider不能是空的
        public BxCompoundFieldData(BxCompoundCoreFieldData fi, IBxStaticUIConfigProvider suicProvider)
        {
            _fi = fi;
            _suicProvider = suicProvider;
            _staticUIConfig = InvalidSUIC;
        }

        #region IBxStaticUIConfigMaker 成员
        public string ConfigID
        {
            get { return _fi.ConfigID; }
        }
        public void InitSUICProvider(IBxStaticUIConfigProvider suicProvider)
        {
            _suicProvider = suicProvider;
            _staticUIConfig = InvalidSUIC;
        }
        public BxXmlUIItem StaticUIConfig
        {
            get
            {
                if (_staticUIConfig == InvalidSUIC)
                {
                    string id = _fi.ConfigID;
                    if (!string.IsNullOrEmpty(id))
                        _staticUIConfig = _suicProvider.GetStaticUIConfig(id);
                    else
                        _staticUIConfig = null;
                }
                return _staticUIConfig;
            }
        }
        #endregion


        public FieldInfo FieldInfo { get { return _fi.FieldInfo; } }

        public string VersionType
        {
            get
            {
                return _fi.VersionType;
            }
        }

        public string Version
        {
            get
            {
                return _fi.Version;
            }
        }
    }

    public class BxCompoundInstanceData
    {
        IBxStaticUIConfigProvider _suicProvider = null;
        BxCompoundFieldData[] _instanceFields = null;

        public IBxStaticUIConfigProvider SUICProvider
        {
            get { return _suicProvider; }
        }
        public BxCompoundFieldData[] InstanceFields
        {
            get { return _instanceFields; }
        }

        public BxCompoundInstanceData(IBxStaticUIConfigProvider suicProvider, BxCompoundCore core)
        {
            _suicProvider = suicProvider;
            _instanceFields = new BxCompoundFieldData[core.FieldsCount];
            int index = 0;
            IEnumerable<BxCompoundCoreFieldData> fieldsInfo = core.GetAllFieldsInfo();
            foreach (BxCompoundCoreFieldData one in fieldsInfo)
            {
                _instanceFields[index] = new BxCompoundFieldData(one, _suicProvider);
                index++;
            }
        }
        public void ResetSUICProvider(IBxStaticUIConfigProvider suicProvider)
        {
            _suicProvider = suicProvider;
            Array.ForEach(_instanceFields, x => x.InitSUICProvider(suicProvider));
        }
    }

}
