using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HebianGu.ComLibModule.Serialize.Xml
{
    public static class XmlSerializeEx
    {
        /// <summary> 序列化 </summary>
        public static string XmlSerialize(this object o)
        {
            MemoryStream Stream = new MemoryStream();
            XmlSerializer xml = new XmlSerializer(o.GetType());
            try
            {
                //序列化对象
                xml.Serialize(Stream, o);
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            Stream.Position = 0;
            StreamReader sr = new StreamReader(Stream);
            string str = sr.ReadToEnd();

            sr.Dispose();
            Stream.Dispose();

            return str;
        }

        /// <summary> 反序列化 </summary>
        /// <param name="type"> 类型 </param>
        /// <param name="xml"> XML字符串 </param>
        /// <returns> 结果 </returns>
        public static T Deserialize<T>(this string xml, Type type)
        {
            try
            {
                using (StringReader sr = new StringReader(xml))
                {
                    XmlSerializer xmldes = new XmlSerializer(type);
                    return (T)xmldes.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        /// <summary> 反序列化 </summary>
        /// <typeparam name="T"> 类型 </typeparam>
        /// <param name="type"> 类型 </param>
        /// <param name="stream"> 序列化流 </param>
        /// <returns> 结果 </returns>
        public static T Deserialize<T>(Type type, Stream stream)
        {
            XmlSerializer xmldes = new XmlSerializer(type);
            return (T)xmldes.Deserialize(stream);
        }
    }
}
