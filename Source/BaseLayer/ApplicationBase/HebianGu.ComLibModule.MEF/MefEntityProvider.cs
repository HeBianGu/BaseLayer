using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MEF
{
    /// <summary> 提供构建组件集合 </summary>
    public class MefEntityProvider<T>
    {
       // ② 必须拥有Export特性  [Export(typeof(IMefDemoBase))]
        [ImportMany]
        List<T> doList;

        /// <summary> 获取所有接口 </summary>
        public List<T> DoList
        {
            get { return doList; }
            set { doList = value; }
        }

        /// <summary> 通过一组应用程序集创建一个MefEntityProvider </summary>
        public static MefEntityProvider<T> CreateInstance(List<Assembly> asses)
        {
            //  ③ 必须拥有Export特性  [Export(typeof(IMefDemoBase))]
            MefEntityProvider<T> provider = new MefEntityProvider<T>();

            var catalog = new AggregateCatalog();

            asses.ForEach(l => catalog.Catalogs.Add(new AssemblyCatalog(l)));

            var _container = new CompositionContainer(catalog);

            _container.ComposeParts(provider);

            return provider;
        }

        /// <summary> 通过一个应用程序集创建一个MefEntityProvider </summary>
        public static MefEntityProvider<T> CreateInstance(Assembly ass)
        {
            MefEntityProvider<T> provider = new MefEntityProvider<T>();

            var catalog = new AggregateCatalog();

            catalog.Catalogs.Add(new AssemblyCatalog(ass));

            var _container = new CompositionContainer(catalog);

            _container.ComposeParts(provider);

            return provider;
        }


    }
}
