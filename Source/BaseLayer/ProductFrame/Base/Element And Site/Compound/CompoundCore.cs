using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;
using OPT.Product.BaseInterface;
using System.Linq;

namespace OPT.Product.Base
{
    public class BxCompoundCoreFieldData : IBxStaticUIConfigPregnant
    {
        public static BxXmlUIItem InvalidSUIC = new BxXmlUIItem();

        FieldInfo _fi = null;
        IBxStaticUIConfigProvider _suicProvider = null;
        string _configID = null;
        BxXmlUIItem _staticUIConfig = InvalidSUIC;


        public FieldInfo FieldInfo
        {
            get { return _fi; }
        }

        public BxCompoundCoreFieldData()
        {
            _fi = null;
            _configID = null;
            _suicProvider = null;
            _staticUIConfig = InvalidSUIC;
        }
        public BxCompoundCoreFieldData(FieldInfo fi)
        {
            _fi = fi;
            _configID = null;
            _suicProvider = null;
            _staticUIConfig = InvalidSUIC;
        }

        public void Init(FieldInfo fi, IBxStaticUIConfigProvider suicProvider)
        {
            _fi = fi;
            _configID = null;
            InitSUICProvider(suicProvider);
        }
        public IBxStaticUIConfigProvider SUICProvider
        {
            get { return _suicProvider; }
        }

        #region IBxStaticUIConfigMaker 成员
        public string ConfigID
        {
            get
            {
                if (_staticUIConfig == InvalidSUIC)
                {
                    if (string.IsNullOrEmpty(_configID) && (_fi != null))
                    {
                        object[] attribs = _fi.GetCustomAttributes(typeof(BxSiteAttribute), false);
                        if (attribs.Length > 0)
                            _configID = (attribs[0] as BxSiteAttribute).ConfigID;
                    }
                }
                return _configID;
            }
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
                    //Init ID from FieldInfo
                    string id = ConfigID;

                    _staticUIConfig = null;

                    //Init _staticUIConfig
                    if (!string.IsNullOrEmpty(id))
                    {
                        if (_suicProvider != null)
                            _staticUIConfig = _suicProvider.GetStaticUIConfig(id);
                        else
                            _staticUIConfig = BxSystemInfo.Instance.SUICProvider.GetStaticUIConfig(id);
                    }
                }
                return _staticUIConfig;
            }
        }
        #endregion

        public string VersionType
        {
            get
            {
                object[] attribs = _fi.GetCustomAttributes(typeof(BxSiteAttribute), false);
                if (attribs.Length > 0)
                    return (attribs[0] as BxSiteAttribute).VersionType;
                return null;
            }
        }

        public string Version
        {
            get
            {
                object[] attribs = _fi.GetCustomAttributes(typeof(BxSiteAttribute), false);
                if (attribs.Length > 0)
                    return (attribs[0] as BxSiteAttribute).Version;
                return null;
            }
        }
    }

    public class BxCompoundCore
    {
        protected Type _type;
        protected BxCompoundCore _baseCore = null;
        protected BxCompoundCoreFieldData[] _fieldsInfo;

        public Type CompoundType { get { return _type; } }
        public BxCompoundCore BaseCore { get { return _baseCore; } }
        public BxCompoundCoreFieldData[] DeclaredFieldsInfo
        {
            get
            {
                if (_fieldsInfo == null)
                    Init();
                return _fieldsInfo;
            }
        }
        public int FieldsCount
        {
            get { return GetAllFieldsInfo().Count(); }
        }

        public List<BxCompoundCore> InheritanceList
        {
            get
            {
                List<BxCompoundCore> coreList = new List<BxCompoundCore>();
                BxCompoundCore curCore = this;
                while (curCore != null)
                {
                    coreList.Insert(0, curCore);
                    curCore = curCore._baseCore;
                }
                return coreList;
            }

        }

        public BxCompoundCore(Type type)
        {
            _type = type;
            Type baseType = type.BaseType;
            object[] attribs = baseType.GetCustomAttributes(typeof(BxCompoundAttribute), false);
            if (attribs.Length > 0)
            {
                _baseCore = ((BxCompoundAttribute)attribs[0]).Core;
            }

            //while (baseType != typeof(BxCompoundValue))
            //{
            //    baseType = baseType.BaseType;
            //    object[] attribs = baseType.GetCustomAttributes(typeof(BSCompoundAttribute), false);
            //    if (attribs.Length > 0)
            //    {
            //        m_baseCore = ((BSCompoundAttribute)attribs[0]).Core;
            //        break;
            //    }
            //}
            //Init();
        }
        protected void Init()
        {
            FieldInfo[] fields = _type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            List<BxCompoundCoreFieldData> usefulFields = new List<BxCompoundCoreFieldData>(fields.Length);
            foreach (FieldInfo one in fields)
            {
                if (one.GetCustomAttributes(typeof(BxSiteAttribute), false).Length > 0)
                    usefulFields.Add(new BxCompoundCoreFieldData(one));
            }
            _fieldsInfo = usefulFields.ToArray();
        }

        public BxCompoundCoreFieldData[] GetFieldsInfo(bool bDeclaredOnly)
        {
            if (bDeclaredOnly)
                return DeclaredFieldsInfo;

            Stack<BxCompoundCoreFieldData[]> all = new Stack<BxCompoundCoreFieldData[]>();
            BxCompoundCore core = this;
            int count = 0;
            while (core != null)
            {
                all.Push(core.DeclaredFieldsInfo);
                count += core.DeclaredFieldsInfo.Length;
                core = core.BaseCore;
            }
            List<BxCompoundCoreFieldData> outVal = new List<BxCompoundCoreFieldData>(count);
            BxCompoundCoreFieldData[] one;
            while (all.Count > 0)
            {
                one = all.Pop();
                outVal.AddRange(one);
            }
            return outVal.ToArray();
        }
        public IEnumerable<BxCompoundCoreFieldData> GetAllFieldsInfo() { return new FieldsInfoEnumerable(this); }

        #region sub class
        class FieldsInfoEnumerable : IEnumerable<BxCompoundCoreFieldData>
        {
            BxCompoundCore _core;
            public FieldsInfoEnumerable(BxCompoundCore core) { _core = core; }

            public IEnumerator<BxCompoundCoreFieldData> GetEnumerator() { return new FieldsInfoEnumerator(_core); }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return new FieldsInfoEnumerator(_core);
            }
        }
        class FieldsInfoEnumerator : IEnumerator<BxCompoundCoreFieldData>
        {
            BxCompoundCore _core;
            int _index;
            int _coreIndex;
            BxCompoundCore _curCore;
            List<BxCompoundCore> _coreList;
            public FieldsInfoEnumerator(BxCompoundCore core)
            {
                _core = core;

                _coreList = new List<BxCompoundCore>();
                BxCompoundCore curCore = _core;
                while (curCore != null)
                {
                    _coreList.Insert(0, curCore);
                    curCore = curCore._baseCore;
                }
                Reset();
            }

            #region IEnumerator<FieldInfo> 成员
            public BxCompoundCoreFieldData Current { get { return _curCore.DeclaredFieldsInfo[_index]; } }
            public void Dispose() { }
            object System.Collections.IEnumerator.Current { get { return _curCore.DeclaredFieldsInfo[_index]; } }
            public bool MoveNext()
            {
                _index++;
                if (_index >= _curCore.DeclaredFieldsInfo.Length)
                {
                    _coreIndex++;
                    if (_coreIndex >= _coreList.Count)
                        return false;
                    _curCore = _coreList[_coreIndex];
                    _index = 0;

                    if (_curCore.DeclaredFieldsInfo.Length == 0)
                        return MoveNext();
                }
                return true;
            }
            public void Reset()
            {
                _curCore = _coreList[0];
                _coreIndex = 0;
                _index = -1;
            }
            #endregion
        }
        #endregion

    }

    public class BxCompoundCore<T>
    {
        static readonly BxCompoundCore s_instance = new BxCompoundCore(typeof(T));
        static public BxCompoundCore Instance { get { return s_instance; } }
    }

}
