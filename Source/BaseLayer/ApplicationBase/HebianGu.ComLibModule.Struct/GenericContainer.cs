using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Struct
{
    [Serializable]
    public class GenericContainer<A>
    {
        public GenericContainer()
        {
        }

        public GenericContainer(A a)
        {
            Value_A = a;
        }

        public A Value_A
        {
            set;
            get;
        }
    }

    [Serializable]
    public class GenericContainer<A, B> : GenericContainer<A>
    {

        public GenericContainer()
        {
        }

        public GenericContainer(A a, B b)
            : base(a)
        {
            Value_B = b;
        }

        public B Value_B
        {
            get;
            set;
        }

    }

    [Serializable]
    public class GenericContainer<A, B, C> : GenericContainer<A, B>
    {

        public GenericContainer()
        {
        }

        public GenericContainer(A a, B b, C c)
            : base(a, b)
        {
            Value_C = c;
        }

        public C Value_C
        {
            get;
            set;
        }

    }

    [Serializable]
    public class GenericContainer<A, B, C, D> : GenericContainer<A, B, C>
    {

        public GenericContainer()
        {
        }

        public GenericContainer(A a, B b, C c, D d)
            : base(a, b, c)
        {
            Value_D = d;
        }

        public D Value_D
        {
            get;
            set;
        }
    }
}
