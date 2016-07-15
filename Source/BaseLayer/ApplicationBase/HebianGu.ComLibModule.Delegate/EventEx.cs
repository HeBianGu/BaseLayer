using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.DelegateEx
{


    public delegate void DlgPopEvent(int index, string evName, object[] param);

    public class Test
    {
        event DlgPopEvent privatrEvent;

        public event DlgPopEvent PublicEvent
        {
            add
            {
                privatrEvent = value;
            }
            remove
            {
                if (privatrEvent != null)
                {
                    privatrEvent -= value;
                }
            }
        }
    }

    //public class ClassBox : IDisposable
    //{
    //    #region - Field -

    //    private object _obj = null;
    //    private EventAdaper[] _evta = null;

    //    #endregion

    //    #region - Event -

    //    public event DlgPopEvent OnObjectEvent;

    //    #endregion

    //    #region - Constructor -

    //    public ClassBox(object obj)
    //        : this(obj, obj.GetType())
    //    {

    //    }

    //    public ClassBox(object obj, Type itType)
    //    {
    //        _obj = obj;

    //        EventInfo[] eis = itType.GetEvents();
    //        if (eis != null && eis.Length > 0)
    //        {
    //            _evta = new EventAdaper[eis.Length];
    //            for (int i = 0; i < _evta.Length; i++)
    //            {
    //                _evta[i] = new EventAdaper(_obj, eis[i]);
    //                _evta[i].OnEvent += new DlgEventAdaper(ClassBox_OnEvent);
    //            }
    //        }
    //    }

    //    ~ClassBox()
    //    {
    //        Dispose();
    //    }

    //    public void Dispose()
    //    {
    //        if (_evta != null && _evta.Length > 0)
    //        {
    //            for (int i = 0; i < _evta.Length; i++)
    //            {
    //                _evta[i].Dispose();
    //                _evta[i] = null;
    //            }
    //        }
    //        _evta = null;

    //        if (_obj != null && _obj is IDisposable)
    //        {
    //            (_obj as IDisposable).Dispose();
    //        }
    //        _obj = null;

    //        OnObjectEvent = null;
    //    }

    //    #endregion

    //    #region - Property -

    //    public int Index
    //    {
    //        set;
    //        get;
    //    }

    //    #endregion

    //    #region - Public method -

    //    public object InvokMember(string mbName, object[] param)
    //    {
    //        try
    //        {
    //            MemberInfo[] minfo = _obj.GetType().GetMember(mbName);
    //            if (minfo == null || minfo.Length < 1)
    //            {
    //                throw new NoFindInterfaceException();
    //            }

    //            if (minfo[0] is MethodInfo)
    //            {
    //                return (minfo[0] as MethodInfo).Invoke(_obj, param);
    //            }
    //            else if (minfo[0] is PropertyInfo)
    //            {
    //                if (param != null && param.Length > 0)
    //                {
    //                    (minfo[0] as PropertyInfo).SetValue(_obj, param[0], param.Length > 1 ? param[1] as object[] : null);
    //                }
    //                return (minfo[0] as PropertyInfo).GetValue(_obj, param.Length > 1 ? param[1] as object[] : null);
    //            }
    //            else
    //            {
    //                throw new NoFindInterfaceException();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    #endregion

    //    #region - Private method -

    //    private void ClassBox_OnEvent(string evName, object[] param)
    //    {
    //        try
    //        {
    //            PopEvent(Index, evName, param);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    private void PopEvent(int index, string evName, object[] param)
    //    {
    //        try
    //        {
    //            if (OnObjectEvent != null)
    //            {
    //                OnObjectEvent(index, evName, param);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }

    //    #endregion

    //    #region - Inside object define -

    //    private delegate void DlgEventAdaper(string evName, object[] param);

    //    private class EventAdaper : IDisposable
    //    {
    //        private object _objc = null;
    //        private Delegate _dlgt = null;
    //        private EventInfo _eInf = null;

    //        public string EvName
    //        {
    //            private set;
    //            get;
    //        }

    //        public event DlgEventAdaper OnEvent;

    //        public EventAdaper(object o, EventInfo einfo)
    //        {
    //            EvName = einfo.Name;
    //            _objc = o;
    //            _eInf = einfo;

    //            ParameterInfo[] ps = _eInf.EventHandlerType.GetMethod("Invoke").GetParameters();
    //            MethodInfo minfo = typeof(EventAdaper).GetMethod(
    //                "M",
    //                BindingFlags.Instance | BindingFlags.NonPublic,
    //                null,
    //                Array.ConvertAll<ParameterInfo, Type>(ps, delegate(ParameterInfo pi) { return pi.ParameterType; }),
    //                null);
    //            if (minfo == null)
    //            {
    //                string mname = "M_" + (ps == null ? 0 : ps.Length).ToString("00");
    //                minfo = typeof(EventAdaper).GetMethod(mname, BindingFlags.Instance | BindingFlags.NonPublic);
    //            }
    //            _dlgt = Delegate.CreateDelegate(_eInf.EventHandlerType, this, minfo);
    //            _eInf.AddEventHandler(_objc, _dlgt);
    //        }

    //        ~EventAdaper()
    //        {
    //            Dispose();
    //        }

    //        public void Dispose()
    //        {
    //            if (_eInf != null && _dlgt != null)
    //            {
    //                _eInf.RemoveEventHandler(_objc, _dlgt);
    //            }
    //            GC.SuppressFinalize(this);
    //        }

    //        private void M_00()
    //        {
    //            try
    //            {
    //                PopEvent(null);
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M_01(object o_01)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M_02(object o_01, object o_02)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M_03(object o_01, object o_02, object o_03)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02, o_03 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M_04(object o_01, object o_02, object o_03, object o_04)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02, o_03, o_04 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M_05(object o_01, object o_02, object o_03, object o_04, object o_05)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02, o_03, o_04, o_05 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M(string o_01)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M(DateTime o_01, string o_02)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M(object o_01, DateTime o_02, string o_03)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02, o_03 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M(object o_01, DateTime o_02, Exception o_03)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02, o_03 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M(object o_01, DateTime o_02, string o_03, string o_04)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02, o_03, o_04 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void M(object o_01, string o_02, DateTime o_03, string o_04)
    //        {
    //            try
    //            {
    //                PopEvent(new object[] { o_01, o_02, o_03, o_04 });
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }

    //        private void PopEvent(object[] param)
    //        {
    //            try
    //            {
    //                for (int i = 0; i < param.Length; i++)
    //                {
    //                    if (param[i] != null && !param[i].GetType().IsSerializable && !param[i].GetType().IsValueType)
    //                    {
    //                        param[i] = param[i].ToString();
    //                    }
    //                }

    //                if (OnEvent != null)
    //                {
    //                    OnEvent(EvName, param);
    //                }
    //            }
    //            catch (Exception ex)
    //            {
    //                throw ex;
    //            }
    //        }
    //    }

    //    #endregion
    //}


    //public class ClassTemplate
    //{
    //    public Type Interface
    //    {
    //        set;
    //        get;
    //    }

    //    public Type Class
    //    {
    //        set;
    //        get;
    //    }
    //}


    //public class SessionMng
    //{
    //    int _waitCt = 0;
    //    int _isWaitAll = 0;
    //    int _waitTrdID = 0;
    //    ManualResetEvent _waitAll = new ManualResetEvent(true);

    //    DFItem[] items = new DFItem[byte.MaxValue];

    //    public SessionMng()
    //    {
    //        for (int i = 0; i < items.Length; i++)
    //        {
    //            items[i] = new DFItem() { IsWait = 0, WaitHandle = new ManualResetEvent(true) };
    //        }
    //    }

    //    public bool ReBuild()
    //    {
    //        _isWaitAll = 0;
    //        if (WaitAllComplete())
    //        {
    //            lock (this)
    //            {
    //                for (int i = 0; i < items.Length; i++)
    //                {
    //                    items[i].Result = null;
    //                    items[i].WaitHandle.Set();
    //                    items[i].IsWait = 0;
    //                }
    //                _waitTrdID = 0;
    //                _isWaitAll = 0;
    //            }
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    public bool BuildSession(out byte sessionId)
    //    {
    //        sessionId = 1;
    //        lock (this)
    //        {
    //            if (_isWaitAll == 0 || Thread.CurrentThread.ManagedThreadId == _waitTrdID)
    //            {
    //                for (int i = 0; i < items.Length; i++)
    //                {
    //                    if (Interlocked.CompareExchange(ref items[i].IsWait, 1, 0) == 0)
    //                    {
    //                        items[i].WaitHandle.Reset();
    //                        sessionId = (byte)i;
    //                        IncrementCt();
    //                        return true;
    //                    }
    //                }
    //            }
    //        }
    //        return false;
    //    }

    //    public void SetResult(byte sessionId, object result)
    //    {
    //        lock (this)
    //        {
    //            items[sessionId].Result = result;
    //            items[sessionId].WaitHandle.Set();
    //            items[sessionId].IsWait = 0;
    //            DecrementCt();
    //        }
    //    }

    //    public object GetResult(byte sessionId)
    //    {
    //        if (items[sessionId].WaitHandle.WaitOne())
    //        {
    //            return items[sessionId].Result;
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }

    //    public bool WaitAllComplete()
    //    {
    //        if (Interlocked.CompareExchange(ref _isWaitAll, 1, 0) == 0)
    //        {
    //            if (_waitAll.WaitOne(1000 * 10, true))
    //            {
    //                _waitTrdID = Thread.CurrentThread.ManagedThreadId;
    //                return true;
    //            }
    //            else
    //            {
    //                Interlocked.CompareExchange(ref _isWaitAll, 0, 1);
    //                return false;
    //            }
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    private void IncrementCt()
    //    {
    //        Interlocked.Increment(ref _waitCt);
    //        _waitAll.Reset();
    //    }

    //    private void DecrementCt()
    //    {
    //        if (Interlocked.Decrement(ref _waitCt) == 0)
    //        {
    //            _waitAll.Set();
    //        }
    //    }

    //    internal class DFItem
    //    {
    //        /// <summary> 0 : 未等待（未占用）；1 : 正在等待（已占用）。 </summary>
    //        public int IsWait;

    //        public ManualResetEvent WaitHandle
    //        {
    //            set;
    //            get;
    //        }

    //        public object Result
    //        {
    //            set;
    //            get;
    //        }
    //    }
    //}


}
