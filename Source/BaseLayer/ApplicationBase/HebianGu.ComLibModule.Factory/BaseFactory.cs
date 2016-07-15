#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2015/11/2 18:47:29  计算机名称：DEV-LIHAIJUN
 *
 * 文件名：BaseFactory
 *
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HebianGu.ComLibModule.Factory
{

    /// <summary> 单例工厂父类</summary>
    public class BaseFactory<T> : IDisposable where T : class,new()
    {

        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static T t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static T Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new T();
                    }
                }
                return t;
            }
        }

        #endregion - 单例模式 End -


        #region - Start 多例模式 -

        /// <summary> 多例模式 </summary>
        static Dictionary<string, T> cache = null;

        /// <summary> 通过名称得到多例实例 </summary>
        public static T InstanceByName(string strKey)
        {
            lock (localLock)
            {
                if (cache == null)
                    cache = new Dictionary<string, T>();

                if (!cache.ContainsKey(strKey))
                    cache.Add(strKey, new T());

                return cache[strKey];
            }

        }

        #endregion - 多例模式 End -


        /// <summary> 外部不可以构造 </summary>
        protected BaseFactory()
        { }



        #region - 资源清理 -

        /// <summary> 供程序员显式调用的Dispose方法 </summary>
        public void Dispose()
        {
            //  调用带参数的Dispose方法，释放托管和非托管资源
            Dispose(true);
            //  手动调用了Dispose释放资源，那么析构函数就是不必要的了，这里阻止GC调用析构函数
            System.GC.SuppressFinalize(this);
        }

        
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                ///TODO:在这里加入清理"托管资源"的代码，应该是xxx.Dispose();
            }
            ///TODO:在这里加入清理"非托管资源"的代码
        }

        /// <summary> 供GC调用的析构函数 </summary>
        ~BaseFactory()
        {
            //  释放非托管资源
            Dispose(false);
        }

        #endregion

    }
}
