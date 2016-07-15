using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Reflection;

namespace OPT.Product.Base
{

    [AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class BxCompoundAttribute : Attribute
    {
        protected BxCompoundCore _core;
        public BxCompoundCore Core { get { return _core; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"> 
        /// type 必须是BxCompoundCore<T>类型，其中T是你声明的Class .
        /// </param>
        public BxCompoundAttribute(Type type)
        {
            FieldInfo info = type.GetField("s_instance", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            _core = (BxCompoundCore)info.GetValue(null);
        }
    }
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class BxSiteAttribute : Attribute
    {
        protected string _configFileID;
        protected Int32 _configItemID;
        public string ConfigFileID { get { return _configFileID; } set { _configFileID = value; } }
        public Int32 ConfigItemID { get { return _configItemID; } set { _configItemID = value; } }

        public BxSiteAttribute() { _configFileID = null; _configItemID = -1; }
        public BxSiteAttribute(string configFileID, Int32 configItemID) { _configFileID = configFileID; _configItemID = configItemID; }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class BxCarrierElement : Attribute
    {
        public BxCarrierElement() { }
    }

}
