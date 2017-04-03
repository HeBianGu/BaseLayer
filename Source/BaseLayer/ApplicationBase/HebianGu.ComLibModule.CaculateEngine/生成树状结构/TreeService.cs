using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.CaculateEngine
{

    /// <summary> 树形结构加载方法 </summary>
    public partial class TreeService
    {
        /// <summary> 将带有父子关系的节点转换成自身的树形结构 </summary>
        public List<ITNode> List2ChildList(List<ITNode> menuList)
        {
            foreach (var item in menuList)
            {
                Action<ITNode> SetChildren = null;

                SetChildren = parent =>
                {
                    parent.Children = menuList
                        .Where(childItem => childItem.ParentID == parent.ID)
                        .ToList();

                    //为每个子项递归调用SetChildren方法。
                    parent.Children
                        .ForEach(SetChildren);
                };

                //初始化层次结构列表以root级别的项目
                List<ITNode> hierarchicalItems = menuList
                    .Where(rootItem => rootItem.ParentID == 0)
                    .ToList();

                //调用SetChildren方法来设置子项的每一根级别项目。
                hierarchicalItems.ForEach(SetChildren);

                return hierarchicalItems;
            }
            return new List<ITNode>();
        }

        /// <summary> 根据父子关系和加载方式初始化T泛型树形结构 P2=转换成T方法 P3=父子节点加载方式 </summary>
        public List<T> LoadTree<T>(List<INode> menuList, Func<INode, T> transToT, Action<T, T> addNodeAct)
        {
            //初始化层次结构列表以root级别的项目
            List<INode> firstLevel = menuList.Where(rootItem => rootItem.ParentID == 0).ToList();

            List<T> ts = new List<T>();

            // HTodo  ：递归 
            Action<INode, T> getChilds = null;

            getChilds = (l, n) =>
            {
                var cs = menuList.FindAll(k => k.ParentID == l.ID).ToList();

                foreach (var item in cs)
                {
                    T t = transToT(item);

                    addNodeAct(n, t);

                    getChilds(item, t);
                }
            };

            foreach (var item in firstLevel)
            {
                var k = transToT(item);

                ts.Add(k);

                getChilds(item, k);
            }

            return ts;
        }


        /// <summary> 根据父子关系和加载方式初始化T泛型树形结构(接口的方式实现) </summary>
        public List<T> LoadTree<T>(List<INode> menuList) where T : INodeModel, new()
        {
            //初始化层次结构列表以root级别的项目
            List<INode> firstLevel = menuList.Where(rootItem => rootItem.ParentID == 0).ToList();

            List<T> ts = new List<T>();

            // HTodo  ：递归 
            Action<INode, T> getChilds = null;

            getChilds = (l, n) =>
            {
                var cs = menuList.FindAll(k => k.ParentID == l.ID).ToList();

                foreach (var item in cs)
                {
                    T t = new T();

                    t.BuildFromNode(item);

                    n.AddChild(t);

                    getChilds(item, t);
                }
            };

            foreach (var item in firstLevel)
            {
                T t = new T();
                t.BuildFromNode(item);

                ts.Add(t);

                getChilds(item, t);
            }

            return ts;
        }

    }

    /// <summary> 此类的说明 </summary>
    partial class TreeService
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static TreeService t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static TreeService Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new TreeService();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private TreeService()
        {

        }
        #endregion - 单例模式 End -

    }


}
