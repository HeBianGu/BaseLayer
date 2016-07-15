using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.ExceptionEx
{
    [Serializable]
    public class ReceivedZeroException : Exception
    {
        public ReceivedZeroException(string message)
            : base(message)
        { }
    }

    [Serializable]
    public class NoFindInterfaceException : Exception
    {
        public NoFindInterfaceException()
            : base()
        {

        }

        protected NoFindInterfaceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public override string Message
        {
            get
            {
                return base.Message + ",NoFindInterface";
            }
        }
    }

    [Serializable]
    public class MultiInterfaceException : Exception
    {
        public MultiInterfaceException()
            : base()
        {

        }

        protected MultiInterfaceException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {

        }

        public override string Message
        {
            get
            {
                return base.Message + ",找到多个方法，接口标识不明确。";
            }
        }
    }
}
