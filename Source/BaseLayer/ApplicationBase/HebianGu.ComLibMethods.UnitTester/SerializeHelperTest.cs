using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using HebianGu.ComLibModule.API;
using System.Diagnostics;
using HebianGu.ObjectBase.ObjectHelper;
using HebianGu.ComLibModule.Serialize.Xml;
using System.Collections.Generic;
using System.Reflection;

namespace HebianGu.ComLibMethods.UnitTester
{
    [TestClass]
    public class SerializeHelperTest
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
            string ss = ms.XmlSerialize();

        }


        [TestMethod]
        public void TestTestProperty()
        {
            TestProperty t = new TestProperty();


            string ss = t.MyProperty;

            Debug.WriteLine(ss);

        }

    }

    [Serializable]
   public class Model
    {

        public Model()
        {

        }
        public Model(string id, string name, string value)
        {
            this._id = id;
            this._name = name;
            this._value = value;

        }
        private string _id;
        /// <summary> 说明 </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        /// <summary> 说明 </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }


        private string _value;
        /// <summary> 说明 </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

    }

    public class TestProperty
    {
        private string myVar;
        /// <summary> 说明 </summary>
        public string MyProperty
        {
            get
            {
                var t = MethodBase.GetCurrentMethod();
                return  t.Name;
            }
            set { myVar = value; }
        }

    }
}
