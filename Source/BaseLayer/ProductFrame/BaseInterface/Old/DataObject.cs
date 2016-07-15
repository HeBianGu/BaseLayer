using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace OPT.Product.BaseInterface
{
    public enum BSDataObjectType
    {
        Invalid = 0,
        Well,
        Unit,
        PipeNet
    }

    public interface IBSDataSource
    {
    }

    public interface IBSDataObject
    {
        IBSDataSource DataSource { get; }
        BSDataObjectType DataType { get; }
        IEnumerable<string> Objects { get; }
    }
}
