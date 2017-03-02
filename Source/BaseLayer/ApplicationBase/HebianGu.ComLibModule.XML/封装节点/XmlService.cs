using HebianGu.ObjectBase.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HebianGu.ComLibModule.XML
{
    public class XmlService : BaseFactory<XmlService>
    {
        const string groupNode = "GroupItems";

        /// <summary> 将制定类型序列化为节点 </summary>
        public XmlParam ToXMLNode<T>(T v)
        {
            List<AttributeParameter> atts = new List<AttributeParameter>();

            XmlParam node = new XmlParam();

            Type t = typeof(T);

            node.Name = t.Name;

            node.InnerText = t.Name;

            var ps = t.GetProperties();

            foreach (var item in ps)
            {
                AttributeParameter a = new AttributeParameter();
                a.Name = item.Name;

                // HTodo  ：目前只支持简单类型 
                a.Value = item.GetValue(v).ToString();
                atts.Add(a);
            }

            node.Attributes = atts.ToArray();

            return node;
        }


        /// <summary> 将xml文件中指定节点反序列化成类型 </summary>
        public List<T> ToXMLModelList<T>(string path)
        {
            List<T> ts = new List<T>();

            XmlNode items = XmlHelper.InstanceOfName(path).GetNode(groupNode);

            foreach (XmlNode xns in items.ChildNodes)
            {
                if (xns.Name == typeof(T).Name)
                {
                    T t = this.ToXMLModel<T>(xns);

                    if (t != null)
                    {
                        ts.Add(t);
                    }
                }
            }

            return ts;
        }

        /// <summary> 将xml文件中指定节点反序列化成类型 </summary>
        public List<T> ToXMLModelList<T>()
        {
            List<T> ts = new List<T>();

            XmlNode items = XmlHelper.Instance.GetNode(groupNode);

            foreach (XmlNode xns in items.ChildNodes)
            {
                if (xns.Name == typeof(T).Name)
                {
                    T t = this.ToXMLModel<T>(xns);

                    if (t != null)
                    {
                        ts.Add(t);
                    }
                }
            }

            return ts;
        }


        /// <summary> 将节点转换为实体，匹配字段名称 </summary>
        public T ToXMLModel<T>(XmlNode node)
        {
            Type t = typeof(T);

            T instance = Activator.CreateInstance<T>();

            foreach (XmlAttribute item in node.Attributes)
            {
                var p = t.GetProperty(item.Name);

                if (p != null)
                {
                    p.SetValue(instance, item.Value);
                }
            }

            return instance;
        }

    }
}
