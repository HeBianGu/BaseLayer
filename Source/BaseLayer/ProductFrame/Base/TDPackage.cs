using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Drawing;
using OPT.Product.Base;

namespace OPT.PEOffice6.GeneralLayer.TD.TDBase
{
#if false
    public abstract class TDPptElement : IBLElement, IBLUIConfigable, IBLUIValue
    {
        protected IBLUIConfig _uiConfig;

        public TDPptElement() { _uiConfig = null; }
        public TDPptElement(IBLUIConfig uiConfig) { _uiConfig = uiConfig; }

        public IBLUIConfig UIConfig { get { return _uiConfig; } set { _uiConfig = value; } }
        public IBLCompound Container { get; set; }
        public abstract string GetUIValue();
        public abstract bool SetUIValue(string val);
    }
    public abstract class TDPptCompoud : TDPptElement, IBLCompound
    {
        public abstract void InitChildUIConfig(IBLUIConfig[] configItems);
        #region IBLCompound 成员
        public abstract IEnumerable<IBLElement> Elements { get; }
        #endregion
    }

    public class TDInt : TDPptElement
    {
        public delegate int GetInt();
        public delegate void SetInt(int val);
        public GetInt _get;
        public SetInt _set;
        public TDInt(object obj, PropertyInfo info)
            : base()
        {
            MemberInfo get = info.GetGetMethod();
            _get = Delegate.CreateDelegate(typeof(TDInt.GetInt), obj, info.GetGetMethod()) as TDInt.GetInt;
            _set = Delegate.CreateDelegate(typeof(TDInt.SetInt), obj, info.GetSetMethod()) as TDInt.SetInt;
        }
        public TDInt(GetInt get, SetInt set)
            : base()
        {
            _get = get;
            _set = set;
        }
        public override string GetUIValue() { return _get().ToString(); }
        public override bool SetUIValue(string val)
        {
            int result;
            if (!int.TryParse(val, out result))
                return false;
            _set(result);
            return true;
        }
    }

    public class TDPoint : TDPptCompoud
    {
        public TDInt x;
        public TDInt y;

        public delegate Point GetValue();
        public delegate void SetValue(Point val);

        public GetValue _get;
        public SetValue _set;

        // object _obj;
        //PropertyInfo _info;

        Point Value { set { _set(value); } get { return _get(); } }

        public TDPoint(GetValue get, SetValue set)
            : base()
        {
            _get = get;
            _set = set;
            x = new TDInt(GetX, SetX);
            y = new TDInt(GetY, SetY);
        }
        public TDPoint(object obj, PropertyInfo info)
        {
            _get = Delegate.CreateDelegate(typeof(GetValue), obj, info.GetGetMethod()) as GetValue;
            _set = Delegate.CreateDelegate(typeof(SetValue), obj, info.GetSetMethod()) as SetValue;
            x = new TDInt(GetX, SetX);
            y = new TDInt(GetY, SetY);
        }

        public override void InitChildUIConfig(IBLUIConfig[] configItems)
        {
            x.UIConfig = configItems[0];
            y.UIConfig = configItems[1];
        }


        public int GetX()
        {
            return Value.X;
        }
        public void SetX(int val)
        {
            Point pt = Value;
            pt.X = val;
            Value = pt;
        }
        public int GetY()
        {
            return Value.Y;
        }
        public void SetY(int val)
        {
            Point pt = Value;
            pt.Y = val;
            Value = pt;
        }

        public override string GetUIValue() { Point pt = Value; return string.Format("{0},{1}", pt.X, pt.Y); }
        public override bool SetUIValue(string val)
        {
            string[] vals = val.Split(",".ToCharArray());
            if (vals.Length != 2)
                return false;
            int tempX, tempY;
            if (!int.TryParse(vals[0], out tempX))
                return false;
            if (!int.TryParse(vals[1], out tempY))
                return false;
            Value = new Point(tempX, tempY);
            return true;
        }

        public override IEnumerable<IBLElement> Elements
        {
            get { return new TDInt[] { x, y }; }
        }
    }





    [AttributeUsageAttribute(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class TDUIConfigAttribute : Attribute
    {
        Type _type;
        Int32 _configItemID;
        Int32[] _childConfigItemID;
        public string ConfigModuleID { get { return "2D"; } }
        public Int32 ConfigItemID { get { return _configItemID; } set { _configItemID = value; } }
        public Int32[] ChildConfigItemID { get { return _childConfigItemID; } set { _childConfigItemID = value; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">从TDPptElement派生的类型</param>
        public TDUIConfigAttribute(Type type) { _type = type; _configItemID = -1; _childConfigItemID = null; }
        public TDUIConfigAttribute(Type type, Int32 configItemID) { _type = type; _configItemID = configItemID; _childConfigItemID = null; }
        public TDUIConfigAttribute(Type type, Int32 configItemID, Int32[] childConfigItemID) { _type = type; _configItemID = configItemID; _childConfigItemID = childConfigItemID; }
        public TDPptElement CreateTDObject(object obj, PropertyInfo info)
        {
            ConstructorInfo con = _type.GetConstructor(s_types);
            return con.Invoke(new object[] { obj, info }) as TDPptElement;
        }

        static Type[] s_types = new Type[] { typeof(object), typeof(PropertyInfo) };
    }

    public class TDCompoundPackage : IBLCompound
    {
        List<IBLElement> _children = new List<IBLElement>();

        public void AddChild(TDPptElement obj)
        {
            _children.Add(obj);
        }

        #region IBSCompoundElement 成员
        public IBLCompound Container { get; set; }
        #endregion

        #region IBSCompound 成员
        public IEnumerable<IBLElement> Elements
        {
            get { return _children; }
        }
        #endregion
    }


    public  class TDPackageFactory
    {
        public IBLUIConfigs UIConfigs { get; set; }
        public TDPackageFactory() { }
        public TDPackageFactory(IBLUIConfigs uiConfigs) { UIConfigs = uiConfigs; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj">2D的一个图件</param>
        /// <returns></returns>
        public IBLCompound CreatePackage(object obj)
        {
            TDCompoundPackage result = new TDCompoundPackage();
            IBLUIConfigGroup group = UIConfigs.GetConfigGroup("2D");

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.DeclaredOnly);
            object[] tempAttributes = null;
            TDUIConfigAttribute tempAttribute = null;
            TDPptElement temp = null;
            foreach (PropertyInfo one in properties)
            {
                tempAttributes = one.GetCustomAttributes(typeof(TDUIConfigAttribute), false);
                if (tempAttributes.Length <= 0)
                    continue;
                tempAttribute = tempAttributes[0] as TDUIConfigAttribute;
                temp = tempAttribute.CreateTDObject(obj, one);
                if (temp == null)
                    throw new Exception("没有正确生成包装对象，检查图件属性的标签是否设置错误。");
                temp.UIConfig = group.GetItemConfig(tempAttribute.ConfigItemID);
                if(temp is TDPptCompoud)
                {
                    IBLUIConfig[] configs = new IBLUIConfig[tempAttribute.ChildConfigItemID.Length];
                    Int32 index = 0;
                    foreach (Int32 id in tempAttribute.ChildConfigItemID)
                    {
                        configs[index] = group.GetItemConfig(id);
                        index++;
                    }
                    (temp as TDPptCompoud).InitChildUIConfig(configs);
                }
                result.AddChild(temp);
            }
            return result;
        }
    }
    /// <summary>
    /// 假设这是一个2d的图件
    /// </summary>
    public class TDObjectTest
    {
        protected Point _pt;

        [TDUIConfigAttribute(typeof(TDInt),/*XmlID*/100001)]
        public int PenWidth { get; set; }

        [TDUIConfigAttribute(typeof(TDPoint), 100002, new Int32[] { 100003, 100004 })]
        public Point PT { get { return _pt; } set { _pt = value; } }


    }


    public class TDTest
    {
        static public void TestFunc()
        {
            TDObjectTest test = new TDObjectTest();

            //这个指用来传给属性表
           // IBLCompound compound = TDPackageFactory.CreatePackage(test);

            //下面的代码来模拟属性表如何使用compound
           // Analysis(compound);
        }

        //下面的代码来模拟属性表如何使用compound
        static public void Analysis(IBLCompound compound)
        {
            foreach (IBLElement one in compound.Elements)
            {
                if (one is IBLCompound)
                    Analysis(one as IBLCompound);
                else
                {
                    IBLUIConfig config = (one as IBLUIConfigable).UIConfig;
                    IBLUIValue UIValue = one as IBLUIValue;
                    string s = UIValue.GetUIValue();
                    UIValue.SetUIValue("123");
                    string s1 = UIValue.GetUIValue();
                }
            }
        }
    }
#endif

}
