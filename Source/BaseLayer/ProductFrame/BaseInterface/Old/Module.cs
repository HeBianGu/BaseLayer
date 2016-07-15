using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Xml;

namespace OPT.Product.BaseInterface
{
    [AttributeUsageAttribute(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
    public class BSModuleGuideAttribute : System.Attribute
    {
        IBSModuleGuide _guide;
        BSModuleGuideAttribute(Type type)
        {
            ConstructorInfo ci = type.GetConstructor(null);
            if (ci == null)
                _guide = null;
            else
                _guide = (IBSModuleGuide)ci.Invoke(null);
        }
        BSModuleGuideAttribute(IBSModuleGuide val) { _guide = val; }
        IBSModuleGuide ModuleGuide { get { return _guide; } set { _guide = value; } }
    }

    public interface IBSModuleGuide
    {
        IEnumerable<IBSModule> Modules { get; }
        IBSModule CreateModule(string id);
    }

    public interface IBSModule
    {
        string ID { get; }
        IBSVisibleObjectGuide VisibleObjectGuide { get; }
        IBSWindowGuide WindowGuide { get; }
        IBSDataGuide DataGuide { get; }
        void Close();
    }

    public interface IBSVisibleObjectGuide
    {
        IEnumerable<Guid> SurpportedObjects { get; }
        IBSVisibleObject InstanceVisibleObject(Guid id);
    }
    public interface IBSWindowGuide
    {
        void InitFrame(IBSFrame frame);
        Control CreateWindow();
    }

    //TODO：IBSFrame
    public interface IBSFrame
    {
    }

    public interface IBSDataGuide
    {
        void InitDataObject(IBSDataObject obj);
        void InitTemplate(object obj);
    }
}
