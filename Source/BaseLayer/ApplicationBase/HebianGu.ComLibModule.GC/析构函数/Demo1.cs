using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.GCHelper
{
    /// <summary> 把对象插入到静态队列_failedDeletions中，使得该对象处于引用状态，这就确保了它仍然保持活着的状态，直到该对象最终从队列中出列 </summary>
    public class TempFileRef
    {
        static ConcurrentQueue<TempFileRef> _failedDeletions = new ConcurrentQueue<TempFileRef>();

        public readonly string FilePath;

        public Exception DeletionError { get; private set; }

        public TempFileRef(string filePath) { FilePath = filePath; }

        ~TempFileRef()
        {
            //  对象垂死状态
            try
            {
                File.Delete(FilePath);
            }
            catch (Exception ex)
            {

                DeletionError = ex;

                //  复活对象
                _failedDeletions.Enqueue(this);

            }
        }
    }

}
