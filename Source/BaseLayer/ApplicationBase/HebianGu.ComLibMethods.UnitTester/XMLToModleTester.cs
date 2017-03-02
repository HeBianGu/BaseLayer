using HebianGu.ComLibModule.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HebianGu.ComLibMethods.UnitTester
{
    [TestClass]
    public class XMLToModleTester
    {

        [TestMethod]
        public void TestSerualilzeToConfiger()
        {
            List<Model> ms = new List<Model>();

            Model m = new Model("1", "jim", "boy");
            ms.Add(m);
            Model m1 = new Model("2", "Tom", "boy");
            ms.Add(m1);
            Model m2 = new Model("3", "Lucy", "gril");
            ms.Add(m2);

            XmlService.Instance.ToXMLNode<Model>(m);

            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test.xml");

            List<XmlParam> nodes = new List<XmlParam>();

            nodes.Add(XmlService.Instance.ToXMLNode<Model>(m));
            nodes.Add(XmlService.Instance.ToXMLNode<Model>(m1));
            nodes.Add(XmlService.Instance.ToXMLNode<Model>(m2));


            if (File.Exists(path))
            {
                File.Delete(path);
            }
            // HTodo  ：创建文件 
            XmlHelper.InstanceOfName(path).CreateXmlFile(path, "ConfigFile", "GroupItems", nodes.ToArray());

            var newModels = XmlService.Instance.ToXMLModelList<Model>(path);
        }



       
    }


}
