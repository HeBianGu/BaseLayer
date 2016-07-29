using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.MEF
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Assembly> ass = new List<Assembly>();

            ass.Add(Assembly.GetEntryAssembly());

            MefEntityProvider<IMefDemoBase> provider = MefEntityProvider<IMefDemoBase>.CreateInstance(ass);

            provider.DoList.ForEach(l => Console.WriteLine(l.ToString()));

            Console.Read();

            MefEntityProvider<IDisposable> provider1 = MefEntityProvider<IDisposable>.CreateInstance(ass);

            provider1.DoList.ForEach(l => Console.WriteLine(l.ToString()));

            Console.Read();

        }
    }
}
