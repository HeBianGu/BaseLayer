using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace OPT.Product.BaseInterface
{
    public interface IBSContentByType
    {
        T ContentByType<T>();
    }

   
}
namespace OPT.PEOffice6.BaseLayer1
{
    public interface IBLElement
    {
        IBLCompound Parent { get; set; }
    }
    public interface IBLUIElement : IBLElement
    {
        IBLUIConfig UIConfig { get;}
    }

    public interface IBLCompound
    {
        IEnumerable<IBLElement> Elements { get; }
    }

    public interface IBLContainer : IBLCompound
    {
        void Add(IBLElement child);
    }

    public interface IBLUIConfig
    {
        
    }

    public interface IBLVisibleObject
    {
        Point Location { get; set; }
        Size Size { get; set; }
        Int32 ZOrder { get; set; }
        void OnPaint(System.Windows.Forms.PaintEventArgs e);
    }

    public interface IBLControlMsgProvider
    {
        void AddMessageFilter(IMessageFilter value);
        void RemoveMessageFilter(IMessageFilter value);
    }


    public static class MsgParams
    {
        static public Point ToPoint(ref Message m)
        {
            Point pt = new Point();
            pt.X = (Int16)m.LParam;
            pt.Y = (Int16)(m.LParam.ToInt32() >> 16);
            return pt;
        }
    }
}
