using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Serialize.Json
{
    public static class BinarySerializeEx
    {
        /// <summary> 序列化二进制 </summary>
        public static string BinarySerialize<T>(this object target)
        {
            T result = (T)target;

            //  序列化
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter format = new BinaryFormatter();

                format.Serialize(stream, result);

                return Encoding.UTF8.GetString(stream.ToArray()); 

            }
        }

        /// <summary> 返序列化二进制 </summary>
        public static T BinaryDeserialize<T>(this string target)
        {
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(target)))
            {
                BinaryFormatter format = new BinaryFormatter();

                return (T)format.Deserialize(ms);
            }
        }

    }
}
