using System;
using System.Collections;
using System.Xml;
using System.IO;
using System.Runtime.CompilerServices;

namespace FCNS.Calendar
{
    /// <summary>
    /// 软件配置类
    /// </summary>
    public class SoftConfig
    {
        string file;
        /// <summary>
        ///  配置文件属性结构
        /// </summary>
        public struct DataStruct
        {
            /// <summary>
            /// 属性名
            /// </summary>
            public string name;
            /// <summary>
            /// 属性类型
            /// </summary>
            public string type;
            /// <summary>
            /// 属性值
            /// </summary>
            public object value;
            /// <summary>
            /// 初始化 DataStruct 结构的新实例
            /// </summary>
            /// <param name="name">属性名</param>
            /// <param name="type">属性类型</param>
            /// <param name="value">属性值</param>
            public DataStruct(string name, string type, object value)
            {
                this.name = name;
                this.type = type;
                this.value = value;
            }
        }
        /// <summary>
        /// 存储属性项
        /// </summary>
        public ArrayList DataList;

        /// <summary>
        /// 初始化 SoftConfig 新的实例
        /// </summary>
        /// <param name="file">语言文件</param>
        public SoftConfig(string file)
        {
            DataList = new ArrayList();
            LoadFile(file);
        }
        /// <summary>
        /// 加载配置文件
        /// </summary>
        /// <param name="file">文件路径</param>
        public void LoadFile(string file)
        {
            this.file = file;
            DataList.Clear();
            if (File.Exists(file))
            {
                LoadXML();
                newEvent("loadfile", "true");
            }
        }
        void LoadXML()
        {
            XmlTextReader reader = null;

            try
            {
                reader = new XmlTextReader(file);
                reader.WhitespaceHandling = WhitespaceHandling.None;

                string name, type, value;
                name = type = value = null;
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            name = reader.Name;
                            type = reader.GetAttribute("type");
                            break;
                        case XmlNodeType.Text:
                            value = reader.Value;
                            break;
                        case XmlNodeType.EndElement:
                            if ((name != null) && (type != null) && (value != null))
                            {
                                switch (type)
                                {
                                    case "System.Boolean":
                                        Set(name, System.Boolean.Parse(value), false, false);
                                        break;
                                    case "System.String":
                                        Set(name, value, false, false);
                                        break;
                                    case "System.Int16":
                                        Set(name, System.Int16.Parse(value), false, false);
                                        break;
                                    case "System.Int32":
                                        Set(name, System.Int32.Parse(value), false, false);
                                        break;
                                    case "System.Int64":
                                        Set(name, System.Int64.Parse(value), false, false);
                                        break;
                                    case "System.UInt16":
                                        Set(name, System.UInt16.Parse(value), false, false);
                                        break;
                                    case "System.UInt32":
                                        Set(name, System.UInt32.Parse(value), false, false);
                                        break;
                                    case "System.UInt64":
                                        Set(name, System.UInt64.Parse(value), false, false);
                                        break;
                                }
                            }
                            name = type = value = null;
                            break;
                    }
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        void SaveXML()
        {
            XmlTextWriter xWriter = new XmlTextWriter(file, null);
            xWriter.Formatting = System.Xml.Formatting.Indented;
            xWriter.WriteStartElement(null, "config", null);
            for (int i = 0; i < DataList.Count; i++)
            {
                xWriter.WriteStartElement(null, ((DataStruct)DataList[i]).name, null);
                xWriter.WriteStartAttribute(null, "type", null);
                xWriter.WriteString(((DataStruct)DataList[i]).type);
                xWriter.WriteEndAttribute();
                xWriter.WriteString(((DataStruct)DataList[i]).value.ToString());
                xWriter.WriteEndElement();
            }
            xWriter.WriteEndElement();
            xWriter.Close();
        }

        /// <summary>
        /// 读取指定属性的值
        /// </summary>
        /// <param name="key">属性名称</param>
        /// <param name="valuedefault">默认值,如果读取的属性不存在,用此值新增属性</param>
        /// <returns>返回值</returns>
        public object Get(string key, object valuedefault)
        {
            for (int i = 0; i < DataList.Count; i++)
            {
                if (((DataStruct)DataList[i]).name == key)
                {
                    return ((DataStruct)DataList[i]).value;
                }
            }
            //假如值不存在,新增默认值
            Set(key, valuedefault);
            return valuedefault;
        }
        /// <summary>
        /// 获取属性的类型
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="valuedefault">属性默认值</param>
        /// <returns>如果存在返回属性类型,否则返回属性默认值</returns>
        public string GetType(string key, string valuedefault)
        {
            for (int i = 0; i < DataList.Count; i++)
            {
                if (((DataStruct)DataList[i]).name == key)
                {
                    return ((DataStruct)DataList[i]).type;
                }
            }
            return valuedefault;
        }

        /// <summary>
        /// 设置属性的值,保存并调用事件
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="value">属性值</param>
        public void Set(string key, object value)
        {
            Set(key, value, true, true);
        }

        /// <summary>
        /// 设置属性的值
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="value">属性值</param>
        /// <param name="savexml">true 为保存配置文件</param>
        /// <param name="runevent">true 为执行事件</param>
        public void Set(string key, object value, bool savexml, bool runevent)
        {
            int pos = -1;
            for (int i = 0; i < DataList.Count; i++)
            {
                if (((DataStruct)DataList[i]).name == key)
                {
                    pos = i;
                    break;
                }
            }

            if (pos != -1)
            {
                if (((DataStruct)DataList[pos]).value == value)
                    return;
                DataList[pos] = new DataStruct(key, value.GetType().ToString(), value);
            }
            else
            {
                DataList.Add(new DataStruct(key, value.GetType().ToString(), value));
            }
            if (runevent)
            {
                newEvent(key, value);
            }
            if (savexml)
            {
                SaveXML();
            }
        }
        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="key">属性名</param>
        public void Del(string key)
        {
            Del(key, true, true);
        }
        /// <summary>
        /// 删除属性
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="value">属性值</param>
        /// <param name="savexml">true 为保存配置文件</param>
        /// <param name="runevent">true 为执行事件</param>
        public void Del(string key, bool savexml, bool runevent)
        {
            int pos = -1;
            for (int i = 0; i < DataList.Count; i++)
            {
                if (((DataStruct)DataList[i]).name == key)
                {
                    pos = i;
                    break;
                }
            }

            if (pos != -1)
                DataList.RemoveAt(pos);

            if (runevent)
                newEvent(key, null);

            if (savexml)
                SaveXML();
        }
        /// <summary>
        /// 十六进制转换到十进制
        /// </summary>
        /// <param name="hexstr"></param>
        /// <returns></returns>
        public static int HexToInt(String hexstr)
        {
            int hexint = 0;
            String hexarr = hexstr.ToUpper();

            for (int counter = hexarr.Length - 1; counter >= 0; counter--)
            {
                if ((hexarr[counter] >= '0') && (hexarr[counter] <= '9'))
                {
                    hexint += (hexarr[counter] - 48) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
                    continue;
                }
                if ((hexarr[counter] >= 'A') && (hexarr[counter] <= 'F'))
                {
                    hexint += (hexarr[counter] - 55) * ((int)(Math.Pow(16, hexarr.Length - 1 - counter)));
                    continue;
                }
                return 0;
            }
            return hexint;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void newEvent(string key, object value)
        {
            if (OnChange != null) OnChange(key, value);
        }
        /// <summary>
        /// 软件配置类的委托
        /// </summary>
        /// <param name="key">属性名</param>
        /// <param name="val">属性值</param>
        public delegate void KeyHandler(string key, object val);
        /// <summary>
        /// 软件配置属性有更改时引发
        /// </summary>
        public event KeyHandler OnChange;
	}
}
