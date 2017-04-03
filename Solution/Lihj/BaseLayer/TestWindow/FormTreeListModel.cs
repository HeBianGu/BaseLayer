using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestWindow
{
    public partial class FormTreeListModel : Form
    {
        public FormTreeListModel()
        {
            InitializeComponent();

            this.Load += FormTreeListModel_Load;
        }

        private void FormTreeListModel_Load(object sender, EventArgs e)
        {
            List<TNode> s = new List<TNode>();

            TNode t1 = new TNode();
            t1.ID = 1;
            t1.ParentID = 0;
            t1.Name = "111";

            TNode t2 = new TNode();
            t2.ID = 2;
            t2.ParentID = 1;
            t2.Name = "222";

            TNode t3 = new TNode();
            t3.ID = 3;
            t3.ParentID = 2;
            t3.Name = "333";

            TNode t4 = new TNode();
            t4.ID = 4;
            t4.ParentID = 3;
            t4.Name = "444";

            TNode t5 = new TNode();
            t5.ID = 5;
            t5.ParentID = 3;
            t5.Name = "555";


            s.Add(t1);
            s.Add(t2);
            s.Add(t3);
            s.Add(t4);
            s.Add(t5);

            Action<TreeNode, TreeNode> act = (parent, child) => parent.Nodes.Add(child);

            Func<TNode, TreeNode> func = l =>
            {
                TreeNode t = new TreeNode();
                t.Name = l.ID.ToString();
                t.Text = l.Name;

                return t;
            };

            var nodes = LoadTree(s, func, act);

            foreach (var item in nodes)
            {
                this.treeView1.Nodes.Add(item);
            }

     
        }

        List<T> LoadTree<T>(List<TNode> menuList, Func<TNode, T> transToT, Action<T, T> addNodeAct)
        {
            //初始化层次结构列表以root级别的项目
            List<TNode> firstLevel = menuList
                .Where(rootItem => rootItem.ParentID == 0)
                .ToList();

            List<T> ts = new List<T>();

            // HTodo  ：递归 
            Action<TNode, T> getChilds = null;

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
    }


    public class TNode
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ParentID { get; set; }
    }


}
