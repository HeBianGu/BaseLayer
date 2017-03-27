using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Share
{

    /// <summary> 用于基本的Object扩展方法分组 </summary>
    public interface IGroupBaseObject : IExtend<object>
    {

    }

    /// <summary> 用于反射用途Object扩展方法分组 </summary>
    public interface IGroupRelectbject : IExtend<object>
    {

    }

    /// <summary> 用于转换用途Object扩展方法分组 </summary>
    public interface IGroupConvertobject : IExtend<object>
    {

    }
}
