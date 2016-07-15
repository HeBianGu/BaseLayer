using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.Define
{

    public delegate void BackResult(object result);

    /// <summary> 同步顺序化 </summary>
    public class SynchronizedSequence : IDisposable
    {
        #region - Field -

        private Queue<DataStruct> _queue = new Queue<DataStruct>();
        private ManualResetEvent _asynHandle = new ManualResetEvent(true);
        private Thread _t = null;
        private bool _running = false;
        private SynchronizedSequence _back = null;
        private uint _bfSize;

        #endregion

        #region - Constructor -

        public SynchronizedSequence()
            : this(true, 10000 * 10)
        { }

        public SynchronizedSequence(uint bfSize)
            : this(true, bfSize)
        {

        }

        public SynchronizedSequence(bool isBack, uint bfSize)
        {
            _bfSize = bfSize;
            _running = true;
            _t = new Thread(Run) { IsBackground = true };
            _t.Start();

            if (isBack)
            {
                _back = new SynchronizedSequence(false, bfSize);
            }
        }

        ~SynchronizedSequence()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (_t != null)
            {
                _running = false;
                _asynHandle.Set();
                if (!_t.Join(1000 * 20))
                    _t.Abort();
            }
            _t = null;
        }

        #endregion

        #region - Public method -

        /// <summary> 加入同步队列 </summary>
        public void Invoke(Delegate method, params object[] args)
        {
            Invoke(method, null, args);
        }

        /// <summary> 加入同步队列 </summary>
        public void Invoke(Delegate method, BackResult back, params object[] args)
        {
            //System.Threading.Interlocked.
            if (!_running)
            {
                throw new Exception("SynchronizedSequence is disposed.");
            }

            lock (this)
            {
                if (_queue.Count > _bfSize)
                    return;
                _queue.Enqueue(new DataStruct() { Method = method, Back = back, Args = args });
                _asynHandle.Set();
            }
        }

        #endregion

        #region - Main business -

        private void Run()
        {
            try
            {
                do
                {
                    _asynHandle.WaitOne();
                    DataStruct ds = null;
                    lock (this)
                    {
                        if (_queue.Count == 0)
                        {
                            _asynHandle.Reset();
                        }
                        else
                        {
                            ds = _queue.Dequeue();
                        }
                    }
                    //try
                    //{
                    if (ds != null)
                        if (ds.Back == null)
                            ds.Method.DynamicInvoke(ds.Args);
                        else
                            _back.Invoke(ds.Back, ds.Method.DynamicInvoke(ds.Args));
                    //}
                    //catch (MemberAccessException mae)
                    //{
                    //    LONGO.CommonModule.ComMethods.WriteErrLog(this.GetType().FullName, DateTime.Now, mae.InnerException);
                    //}
                    //catch (System.Reflection.TargetException te)
                    //{
                    //    LONGO.CommonModule.ComMethods.WriteErrLog(this.GetType().FullName, DateTime.Now, te.InnerException);
                    //}
                    //catch (System.Reflection.TargetInvocationException tie)
                    //{
                    //    LONGO.CommonModule.ComMethods.WriteErrLog(this.GetType().FullName, DateTime.Now, tie.InnerException);
                    //}
                    //catch (Exception ex)
                    //{
                    //    LONGO.CommonModule.ComMethods.WriteErrLog(this.GetType().FullName, DateTime.Now, ex);
                    //}
                }
                while (_running || _queue.Count > 0);
            }
            catch (Exception ex)
            {
                string str = ex.Message;
            }
            finally
            {
                _running = false;
                if (_back != null)
                    _back.Dispose();
            }
        }

        #endregion

        #region - Internal object define. -

        internal class DataStruct
        {
            public Delegate Method
            {
                set;
                get;
            }

            public BackResult Back
            {
                set;
                get;
            }

            public object[] Args
            {
                set;
                get;
            }
        }

        #endregion
    }
}
