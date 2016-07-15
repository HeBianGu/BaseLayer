using System;
using System.Collections;
using System.Collections.Generic;

namespace OPT.Product.BaseInterface
{
    /// <summary>
    /// 基础底层事件参数的基本类型，
    /// 所有基础底层的事件必须使用此类，或者从此类派生
    /// </summary>
    public interface IBxModified
    {
        bool Modified { get; set; }
    }

}
