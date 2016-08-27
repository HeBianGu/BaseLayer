using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.GCHelper
{
    /// <summary> 在下面的例子中，我们试图在一个finalizer中删除一个临时文件。但是如果删除失败，我们就重新注册带对象，以使其在下一次垃圾回收执行过程中被回收。 </summary>
    public class TempFileRef1
    {
        public readonly string FilePath;

        int _deleteAttempt;

        public TempFileRef1(string filePath) { FilePath = filePath; }

        ~TempFileRef1()
        {
            try
            {
                File.Delete(FilePath);
            }
            catch
            {
                if (_deleteAttempt++ < 3)

                    //  一个复活对象的finalizer不会再次运行--除非你调用GC.ReRegisterForFinalize
                    GC.ReRegisterForFinalize(this);
            }
        }
    }
}
