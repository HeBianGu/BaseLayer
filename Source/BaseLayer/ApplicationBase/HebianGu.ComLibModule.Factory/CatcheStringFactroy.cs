using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMParamConfigerManager.EclipseFile
{
    /// <summary> 缓存工厂 -- Add By lihaijun </summary>
    public abstract class CatcheStringFactroy
    {

        static CatcheStringFactroy _instance = null;
        /// <summary> 获取实例 </summary>
        public static CatcheStringFactroy CreateInstance()
        {
            if (_instance == null)
            {
                _instance = new CatcheStringFactroy_();
            }

            return _instance;
        }

        /// <summary> 文件缓存 </summary>
        Dictionary<string, Object> cache = new Dictionary<string, Object>();

        /// <summary> 通过缓存创建文件类 </summary>
        /// <typeparam name="T">文件类型</typeparam>
        /// <param name="fileName">文件名称</param>
        /// <param name="args">参数</param>
        /// <returns>文件类型</returns>
        public T CreateFileByName<T>(string fileName, object[] args, bool isCreate) where T : class,new()
        {

            if (!cache.ContainsKey(fileName))
            {
                cache.Add(fileName, CreateObject<T>(args));
            }
            else
            {
                if (isCreate)
                {
                    //  替换
                    cache.Remove(fileName);
                    cache.Add(fileName, CreateObject<T>(args));
                }
            }

            return cache[fileName] as T;
        }

        /// <summary> 通过缓存创建文件类 </summary>
        /// <typeparam name="T">文件类型</typeparam>
        /// <param name="fileName">文件名称</param>
        /// <param name="args">参数</param>
        /// <returns>文件类型</returns>
        public T CreateFileByName<T>(string fileName, object[] args) where T : class,new()
        {
            return CreateFileByName<T>(fileName, args, true);
        }

        private Object CreateObject<T>(object[] args)
        {
            return Activator.CreateInstance(typeof(T), args);
        }

    }
    /// <summary> 内部类用于不允许实例化 </summary>
    internal class CatcheStringFactroy_ : CatcheStringFactroy
    {

    }
}
