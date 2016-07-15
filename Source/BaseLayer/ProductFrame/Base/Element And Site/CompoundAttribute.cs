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
        protected string _version = null;
        public BxCompoundCore Core { get { return _core; } }
        public string Version { get { return _version; } }

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

        public BxCompoundAttribute(Type type, string version)
        {
            FieldInfo info = type.GetField("s_instance", BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            _core = (BxCompoundCore)info.GetValue(null);
            _version = version;
        }
    }
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class BxSiteAttribute : Attribute
    {
        protected string _configFileID;
        protected string _configItemID;
        protected string _svt = null;
        protected string _version = null;
        public string ConfigFileID { get { return _configFileID; } set { _configFileID = value; } }
        public string ConfigItemID { get { return _configItemID; } set { _configItemID = value; } }
        public string VersionType { get { return _svt; }  }
        public string Version { get { return _version; } }
        public void GetFileItemID(out string configFileID, out string configItemID)
        {
            configFileID = _configFileID;
            configItemID = _configItemID;
        }

        public string ConfigID
        {
            get
            {
                if (_configFileID == null)
                    return null;
                return _configFileID + "," + _configItemID;
            }
        }

        public BxSiteAttribute() { _configFileID = null; _configItemID = null; }
        public BxSiteAttribute(string configFileID, Int32 configItemID) { _configFileID = configFileID; _configItemID = configItemID.ToString(); }
        public BxSiteAttribute(string configFileID, string configItemID) { _configFileID = configFileID; _configItemID = configItemID; }
        //svt的内容请从类BxSiteVerType中取
        public BxSiteAttribute(string configFileID, Int32 configItemID, string svt, string version = "6.1") 
        {
            _configFileID = configFileID; _configItemID = configItemID.ToString(); _svt = svt; _version = version;
        }
        //svt的内容请从类BxSiteVerType中取
        public BxSiteAttribute(string configFileID, string configItemID, string svt, string version = "6.1")
        {
            _configFileID = configFileID; _configItemID = configItemID; _svt = svt; _version = version;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class BxCarrierElement : Attribute
    {
        public BxCarrierElement() { }
    }

}
