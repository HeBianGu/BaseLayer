using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace OPT.Product.BaseInterface
{
    public interface IBSVisibleObject
    {
        Point Location { get; set; }
        Size Size { get; set; }
        void Draw(Graphics g);
        Int32 ZOrder { get; set; }
        bool Visible { get; set; }
    }
}
