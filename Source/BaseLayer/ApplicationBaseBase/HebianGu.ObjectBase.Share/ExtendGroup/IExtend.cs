using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ObjectBase.Share
{
    public interface IExtend<T>
    {

        T Value
        {
            get;
        }

    }
}
