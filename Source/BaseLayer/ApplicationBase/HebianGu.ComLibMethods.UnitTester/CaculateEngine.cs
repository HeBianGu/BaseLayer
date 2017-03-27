using HebianGu.ComLibModule.CaculateEngine;
using HebianGu.ComLibModule.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace HebianGu.ComLibMethods.UnitTester
{
    [TestClass]
    public class CaculateEngine
    {
        class Node : INode
        {
            public int ID
            {
                get;
                set;
            }

            public string Name
            {
                get;

                set;
            }

            public int ParentID
            {
                get;

                set;
            }
        }


        /// <summary> 将树状结构转换成树形节点 </summary>
        [TestMethod]
        public void LoadTreeTester()
        {
            List<INode> s = new List<INode>();

            INode t1 = new Node();
            t1.ID = 1;
            t1.ParentID = 0;
            t1.Name = "111";

            INode t2 = new Node();
            t2.ID = 2;
            t2.ParentID = 1;
            t2.Name = "222";

            INode t3 = new Node();
            t3.ID = 3;
            t3.ParentID = 2;
            t3.Name = "333";

            INode t4 = new Node();
            t4.ID = 4;
            t4.ParentID = 3;
            t4.Name = "444";

            INode t5 = new Node();
            t5.ID = 5;
            t5.ParentID = 3;
            t5.Name = "555";


            s.Add(t1);
            s.Add(t2);
            s.Add(t3);
            s.Add(t4);
            s.Add(t5);

            Action<TreeNode, TreeNode> act = (parent, child) => parent.Nodes.Add(child);

            Func<INode, TreeNode> func = l =>
            {
                TreeNode t = new TreeNode();
                t.Name = l.ID.ToString();
                t.Text = l.Name;

                return t;
            };


            TreeView tv = new TreeView();

            var nodes = TreeService.Instance.LoadTree<TreeNode>(s, func, act);

            foreach (var item in nodes)
            {
                tv.Nodes.Add(item);
            }
        }




    }


}
