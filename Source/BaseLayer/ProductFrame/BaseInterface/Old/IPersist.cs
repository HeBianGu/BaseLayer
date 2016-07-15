using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;


namespace OPT.PEOffice6.BaseLayer.Basex
{

    public interface IBSPersistString
    {
        string ToString();
        bool FromString(string s);
    }

    /// <summary>
    /// 此接口代表了实现此接口的对象有存储到xml结点的功能。
    /// </summary>
    /// <reremarks>
    /// 底层占用了一些名字以存储内部信息，因此下列名字禁止被作为
    /// 一个xmlnode的名字使用：$stg,$ass,$share 
    /// </reremarks>
    public interface IBSPersistXml
    {
        void SaveXml(XmlElement node);
        void LoadXml(XmlElement node);
    }

    /// <summary>
    /// 此接口代表了实现此接口的对象有存储到xml结点的功能。
    /// </summary>
    /// <reremarks>
    /// 底层占用了一些名字以存储内部信息，因此下列名字禁止被作为
    /// 一个xmlnode的名字使用：$stg,$tis,$share 
    /// </reremarks>
    public interface IBSPersistXmlEx
    {
        void SaveXml(XmlElement node, BSXmlStorage saver);
        void LoadXml(XmlElement node, BSXmlStorage saver);
    }

    public class BSXmlStorage : IBSPersistXml
    {
        protected BSSharedObjetStore _sos = null;
        protected BSSharedObjetStore Sos
        {
            get
            {
                if (_sos == null)
                    _sos = new BSSharedObjetStore();
                return _sos;
            }
        }
        protected BSTypeInfoStore _tis = null;
        protected BSTypeInfoStore Tis
        {
            get
            {
                if (_tis == null)
                    _tis = new BSTypeInfoStore();
                return _tis;
            }
        }

        public BSXmlStorage() { }

        #region IBSPersistXml 成员
        public void SaveXml(XmlElement node)
        {
            XmlDocument doc = node.OwnerDocument;
            if (_sos != null)
            {
                XmlElement sharedNode = doc.CreateElement(s_xnnShare);
                node.AppendChild(sharedNode);
                _sos.SaveXml(sharedNode);
            }
            if (_tis != null)
            {
                XmlElement tisNode = doc.CreateElement(s_xnnTis);
                node.AppendChild(tisNode);
                _tis.SaveXml(tisNode);
            }
        }
        public void LoadXml(XmlElement node)
        {
            try
            {
                XmlElement sharedNode = (XmlElement)node.SelectSingleNode(s_xnnShare);
                if (sharedNode != null)
                {
                    _sos = new BSSharedObjetStore();
                    _sos.LoadXml(sharedNode);
                }
                XmlElement tisNode = (XmlElement)node.SelectSingleNode(s_xnnTis);
                if (tisNode != null)
                {
                    _tis = new BSTypeInfoStore();
                    _tis.LoadXml(tisNode);
                }
            }
            catch (System.Exception)
            {
            }
        }
        #endregion

        #region static member
        /// <summary>
        /// storage xmlnode name
        /// </summary>
        /// <remarks>xnn means:XmlNodeName</remarks>
        static readonly string s_xnnStg = "$stg";
        /// <summary>
        /// TypeInfoStore xmlnode name
        /// </summary>
        /// <remarks>xnn means:XmlNodeName</remarks>
        static readonly string s_xnnTis = "$tis";
        /// <summary>
        /// Shared xmlnode name
        /// </summary>
        /// <remarks>xnn means:XmlNodeName</remarks>
        static readonly string s_xnnShare = "$share ";
        #endregion
    }

    public class BSSharedObjetStore : IBSPersistXml
    {
        public bool IsEmpty() { return false; }

        //todo：完成class实体

        #region IBSPersistXml 成员

        public void SaveXml(XmlElement node)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void LoadXml(XmlElement node)
        {
            //TODO
            throw new NotImplementedException();
        }

        #endregion
    }


    public class BSAssembly : IBSPersistXml
    {
        protected List<Type> m_lstTypes = new List<Type>();
        protected Assembly m_ass;
        public Assembly MyAssembly { get { return m_ass; } }

        public BSAssembly() { m_ass = null; }
        public BSAssembly(Assembly ass) { m_ass = ass; }

        public Type GetType(Int32 id)
        {
            if (id < 0)
                return null;
            return m_lstTypes[id];
        }
        public Int32 SetType(Type type)
        {
            Int32 id = m_lstTypes.IndexOf(type);
            if (id < 0)
            {
                m_lstTypes.Add(type);
                return m_lstTypes.Count - 1;
            }
            return id;
        }
        public object GenerateObject(Int32 id)
        {
            if (id < 0)
                return null;
            Type t = m_lstTypes[id];
            ConstructorInfo ci = t.GetConstructor(null);
            if (ci == null)
                return null;
            return ci.Invoke(null);
        }

        #region IBSPersistXml 成员
        public void SaveXml(XmlElement node)
        {
            node.SetAttribute("count", m_lstTypes.Count.ToString());
            node.SetAttribute("name", m_ass.GetName().Name);
            node.SetAttribute("fullname", m_ass.FullName);

            //Save Types
            Int32 nIndex = 0;
            XmlElement child = null;
            XmlDocument doc = node.OwnerDocument;
            foreach (Type one in m_lstTypes)
            {
                child = doc.CreateElement("Type");
                node.AppendChild(child);
                child.SetAttribute("id", string.Format("%d", nIndex));
                //child.SetAttribute("name", one.Name);
                child.SetAttribute("fullname", one.FullName);
                nIndex++;
            }
            //end
        }
        public void LoadXml(XmlElement node)
        {
            try
            {
                string name = node.GetAttribute("name");
                string fullname = node.GetAttribute("fullname");
                m_ass = Assembly.Load(fullname);
                if (m_ass == null)
                    m_ass = Assembly.LoadFrom(name);
                if (m_ass == null)
                    throw new Exception("Fail to load Assembly:" + fullname + "," + name);

                Int32 count = Int32.Parse(node.GetAttribute("count"));
                m_lstTypes.Clear();
                m_lstTypes.Capacity = count;
                XmlNodeList list = node.ChildNodes;
                Type type = null;
                foreach (XmlElement one in list)
                {
                    type = m_ass.GetType(one.GetAttribute("fullname"));
                    if (type == null)
                        throw new Exception("Type miss:" + one.GetAttribute("fullname"));
                    m_lstTypes.Add(type);
                }
            }
            catch (System.Exception)
            {
            }
        }
        #endregion
    }

    public class BSTypeInfoStore : IBSPersistXml
    {
        protected List<BSAssembly> m_lstAssembly = new List<BSAssembly>();

        public BSTypeInfoStore() { }

        public Type GetType(UInt64 id)
        {
            UInt32 id1 = (UInt32)(id >> 32);
            UInt32 id2 = (UInt32)id;
            BSAssembly ass = m_lstAssembly[(int)id1];
            return ass.GetType((int)id2);
        }
        public UInt64 SetType(Type type)
        {
            Assembly ass = type.Assembly;
            Int32 id1 = m_lstAssembly.FindIndex(x => x.MyAssembly == ass);
            BSAssembly myAss = null;
            if (id1 < 0)
            {
                myAss = new BSAssembly(ass);
                m_lstAssembly.Add(myAss);
                id1 = m_lstAssembly.Count - 1;
            }

            int id2 = myAss.SetType(type);
            UInt64 id = (UInt32)id1;
            return ((id << 32) | (UInt32)id2);
        }
        public bool IsEmpty() { return m_lstAssembly.Count > 0; }

        #region IBSPersistXml 成员
        public void SaveXml(XmlElement node)
        {
            node.SetAttribute("count", m_lstAssembly.Count.ToString());

            XmlElement child = null;
            XmlDocument doc = node.OwnerDocument;
            foreach (BSAssembly one in m_lstAssembly)
            {
                child = doc.CreateElement("_Ass_");
                node.AppendChild(child);
                one.SaveXml(child);
            }
        }
        public void LoadXml(XmlElement node)
        {
            try
            {
                Int32 count = Int32.Parse(node.GetAttribute("count"));
                m_lstAssembly.Clear();
                m_lstAssembly.Capacity = count;
                XmlNodeList list = node.ChildNodes;
                BSAssembly ass = null;
                foreach (XmlElement one in list)
                {
                    ass = new BSAssembly();
                    ass.LoadXml(one);
                }
            }
            catch (System.Exception)
            {
            }
        }
        #endregion

        //public static object GenerateObject(IBSTypeInfoSaver tis, BSTypeID id)
        //{
        //    Type t = tis.GetType(id);
        //    ConstructorInfo ci = t.GetConstructor(null);
        //    if (ci == null)
        //        return null;
        //    return ci.Invoke(null);
        //}
        //public static object GenerateObject(IBSTypeInfoSaver tis, string sID)
        //{
        //    BSTypeID id = new BSTypeID();
        //    id.FromString(sID);
        //    Type t = tis.GetType(id);
        //    ConstructorInfo ci = t.GetConstructor(null);
        //    if (ci == null)
        //        return null;
        //    return ci.Invoke(null);
        //}
    }


}
