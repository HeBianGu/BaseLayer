using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace OPT.Product.Base
{
    public class BxCompoundCore
    {
        protected Type _type;
        protected BxCompoundCore _baseCore = null;
        protected FieldInfo[] _fieldsInfo;

        public FieldInfo[] DeclaredFieldsInfo
        {
            get
            {
                if (_fieldsInfo == null)
                    Init();
                return _fieldsInfo;
            }
        }
        public BxCompoundCore BaseCore { get { return _baseCore; } }

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
            List<FieldInfo> usefulFields = new List<FieldInfo>(fields.Length);
            foreach (FieldInfo one in fields)
            {
                if (one.GetCustomAttributes(typeof(BxSiteAttribute), false).Length > 0)
                    usefulFields.Add(one);
            }
            _fieldsInfo = usefulFields.ToArray();
        }

        public FieldInfo[] GetFieldsInfo(bool bDeclaredOnly)
        {
            if (bDeclaredOnly)
                return DeclaredFieldsInfo;

            Stack<FieldInfo[]> all = new Stack<FieldInfo[]>();
            BxCompoundCore core = this;
            int count = 0;
            while (core != null)
            {
                all.Push(core.DeclaredFieldsInfo);
                count += core.DeclaredFieldsInfo.Length;
                core = core.BaseCore;
            }
            List<FieldInfo> outVal = new List<FieldInfo>(count);
            FieldInfo[] one;
            while (all.Count > 0)
            {
                one = all.Pop();
                outVal.AddRange(one);
            }
            return outVal.ToArray();
        }
        public IEnumerable<FieldInfo> GetAllFieldsInfo() { return new FieldsInfoEnumerable(this); }

        #region sub class
        class FieldsInfoEnumerable : IEnumerable<FieldInfo>
        {
            BxCompoundCore _core;
            public FieldsInfoEnumerable(BxCompoundCore core) { _core = core; }

            public IEnumerator<FieldInfo> GetEnumerator() { return new FieldsInfoEnumerator(_core); }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return new FieldsInfoEnumerator(_core);
            }
        }
        class FieldsInfoEnumerator : IEnumerator<FieldInfo>
        {
            BxCompoundCore _core;
            int _index;
            BxCompoundCore _curCore;
            public FieldsInfoEnumerator(BxCompoundCore core) { _core = core; Reset(); }

            #region IEnumerator<FieldInfo> 成员
            public FieldInfo Current { get { return _curCore.DeclaredFieldsInfo[_index]; } }
            public void Dispose() { }
            object System.Collections.IEnumerator.Current { get { return _curCore.DeclaredFieldsInfo[_index]; } }
            public bool MoveNext()
            {
                _index++;
                if (_index >= _curCore.DeclaredFieldsInfo.Length)
                {
                    _curCore = _curCore.BaseCore;
                    _index = 0;
                    if (_curCore == null)
                        return false;
                    if (_curCore.DeclaredFieldsInfo.Length == 0)
                        return MoveNext();
                }
                return true;
            }
            public void Reset()
            {
                _curCore = _core;
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
