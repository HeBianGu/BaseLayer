using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using HebianGu.ComLibModule.DelegateEx;
using HebianGu.ComLibModule.ExceptionEx;

namespace HebianGu.ComLibMethods
{
    #region -Start cmdServer-
    [Serializable]
    public class CmncmdServerInfo
    {
        public string SvrID
        {
            set;
            get;
        }

        public string Host
        {
            set;
            get;
        }

        public string[] IP
        {
            set;
            get;
        }

        public DateTime CreateTime
        {
            set;
            get;
        }

        public DateTime ConnectTime
        {
            set;
            get;
        }

        public CmncmdServerInfo LastOne
        {
            set;
            get;
        }
    }

    public delegate void DlgMsgHandler(object sender, DateTime time, string message);

    public delegate void DlgErrHandler(object sender, DateTime time, Exception e);

    public delegate void DlgStateChanged(object sender, DateTime time, bool state);

    public delegate void DlgClientCntStateHandler(object sender, DateTime time, CmncmdInfo ci, bool state);

    public partial class CmncmdServer : Cmncmd
    {
        #region - Const member -

        //public const byte RQTYPE_NORMAL = 0;
        //public const byte RQTYPE_SETTO = 1;
        //public const byte RQTYPE_RGEVENT = 2;

        //public const byte CBTYPE_NULL = 0;
        //public const byte CBTYPE_NORMAL = 1;
        //public const byte CBTYPE_ERROR = 2;

        #endregion

        #region - static member -

        private static System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        public static string[] GetLocalip()
        {
            try
            {
                List<string> ips = new List<string>();
                ips.Add("127.0.0.1");

                IPAddress[] ipAds = System.Net.Dns.GetHostAddresses(Dns.GetHostName());
                if (ipAds != null && ipAds.Length > 0)
                {
                    foreach (var forip in ipAds)
                    {
                        if (forip.AddressFamily == AddressFamily.InterNetworkV6
                            ||
                            forip.ToString() == ips[0].ToString()
                            )
                            continue;
                        ips.Add(forip.ToString());
                    }
                }
                return ips.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendByte(Socket skt, byte cbType, byte[] data)
        {
            try
            {
                long l = data == null ? 0 : data.LongLength;
                skt.Send(BitConverter.GetBytes(l), SocketFlags.None);
                skt.Send(new byte[] { cbType });

                if (data != null && l > 0)
                    skt.Send(data, SocketFlags.None);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendObj(Socket skt, byte cbType, object o)
        {
            try
            {
                byte[] data = null;
                if (o != null)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        _bf.Serialize(ms, o);
                        data = ms.ToArray();
                    }
                }
                SendByte(skt, cbType, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region - field -

        private List<ClsListenInfo> _lsnInfos = new List<ClsListenInfo>();
        //private List<ClsListenInfo> _cltInfos = new List<ClsListenInfo>();
        private List<CmncmdBase> _cltInfos = new List<CmncmdBase>();
        private DataTable _methodCache = null;

        #endregion

        #region - Constructor -

        public CmncmdServer()
        {
            _methodCache = new DataTable("MethodChache");

            foreach (var v in Enum.GetNames(typeof(Clm)))
            {
                switch ((Clm)Enum.Parse(typeof(Clm), v))
                {
                    case Clm.FCMN:
                        _methodCache.Columns.Add(v, Type.GetType("System.String"));
                        break;
                    case Clm._CMN:
                        _methodCache.Columns.Add(v, Type.GetType("System.String"));
                        break;
                    case Clm.__MN:
                        _methodCache.Columns.Add(v, Type.GetType("System.String"));
                        break;
                    case Clm.FCN_:
                        _methodCache.Columns.Add(v, Type.GetType("System.String"));
                        break;
                    case Clm.PLST:
                        _methodCache.Columns.Add(v, typeof(Type[]));
                        break;
                    case Clm.CLAS:
                        _methodCache.Columns.Add(v, Type.GetType("System.Object"));
                        break;
                    case Clm.MDIF:
                        _methodCache.Columns.Add(v, typeof(MethodHandler));
                        break;
                    case Clm.PNUM:
                        _methodCache.Columns.Add(v, Type.GetType("System.Int32"));
                        break;
                    default:
                        break;
                }
            }
        }

        ~CmncmdServer()
        {
            Dispose();
        }

        public override void Dispose()
        {
            Stop();
            OnErr = null;
            OnMsg = null;
            OnStateChanged = null;
            GC.SuppressFinalize(this);
        }

        #endregion

        #region - Event -

        public event DlgMsgHandler OnMsg;

        public event DlgErrHandler OnErr;

        public event DlgStateChanged OnStateChanged;

        public event DlgClientCntStateHandler OnClientCntStateChanged;

        #endregion

        #region - Property -

        public string[] IP
        {
            set;
            get;
        }

        public string Port
        {
            set;
            get;
        }

        public bool IsStarted
        {
            private set;
            get;
        }

        #endregion

        #region - Public method -

        public void Start()
        {
            #region - Validate -

            if (IsStarted)
            {
                PopMsg(this, DateTime.Now, "CmncmdServer is started ! ");
                return;
            }

            if (string.IsNullOrEmpty(Port))
            {
                PopMsg(this, DateTime.Now, "Port can not is 'null' ! ");
                return;
            }

            #endregion

            try
            {
                if (IP == null || IP.Length < 1)
                {
                    IP = GetLocalip();
                }

                _lsnInfos.Clear();

                for (int i = 0; i < IP.Length; i++)
                {
                    ClsListenInfo lsnInfo = new ClsListenInfo()
                    {
                        IP = IP[i],
                        Port = this.Port,
                        Skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp),
                        Trd = new Thread(Listen) { IsBackground = true }
                    };

                    _lsnInfos.Add(lsnInfo);

                    lsnInfo.Skt.Bind(new IPEndPoint(IPAddress.Parse(IP[i]), int.Parse(Port)));
                    lsnInfo.Skt.Listen(100);
                    lsnInfo.Trd.Start(lsnInfo);
                }

                StateChanged(this, DateTime.Now, true);
            }
            catch (Exception ex)
            {
                StateChanged(this, DateTime.Now, false);
                PopErr(this, DateTime.Now, ex);
            }
        }

        public void Stop()
        {
            try
            {
                ClsListenInfo[] lsn = _lsnInfos.ToArray();
                if (lsn != null && lsn.Length > 0)
                {
                    foreach (ClsListenInfo cli in lsn)
                    {
                        cli.Trd.Abort();
                        cli.Skt.Close();
                        cli.Trd.Join();
                    }
                }
                _lsnInfos.Clear();

                CmncmdBase[] cbs = _cltInfos.ToArray();
                if (cbs != null && cbs.Length > 0)
                {
                    foreach (CmncmdBase cb in cbs)
                    {
                        cb.Stop();
                        cb.Dispose();
                    }
                }
                _cltInfos.Clear();
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }
            finally
            {
                StateChanged(this, DateTime.Now, false);
            }
        }

        /// <summary> 注册接口 </summary>
        public void RegistInterface(object o)
        {
            RegistInterface(o, o.GetType());
        }

        /// <summary> 注册接口 </summary>
        public void RegistInterface(object o, Type t)
        {
            if (o != null)
            {
                try
                {
                    if (_methodCache.Select("FCN_ = '" + t.FullName + "'").Length > 0)
                    {
                        PopMsg(this, DateTime.Now, t.FullName + " 已经存在。");
                        return;
                    }

                    System.Reflection.MethodInfo[] mis = t.GetMethods(
                          BindingFlags.Public
                        | BindingFlags.Instance
                        | BindingFlags.DeclaredOnly);

                    if (mis != null && mis.Length > 0)
                    {
                        foreach (var miFor in mis)
                        {
                            DataRow dr = _methodCache.NewRow();

                            ParameterInfo[] pis = miFor.GetParameters();
                            Type[] pit = null;
                            if (pis != null && pis.Length > 0)
                            {
                                pit = new Type[pis.Length];
                                for (int i = 0; i < pit.Length; i++)
                                {
                                    pit[i] = pis[i].ParameterType;
                                }
                            }

                            dr[(int)Clm.FCMN] = t.FullName + "." + miFor.Name;
                            dr[(int)Clm._CMN] = t.Name + "." + miFor.Name;
                            dr[(int)Clm.__MN] = miFor.Name;
                            dr[(int)Clm.PLST] = pit;
                            dr[(int)Clm.CLAS] = o;
                            dr[(int)Clm.MDIF] = new MethodHandler() { Method = miFor, Target = o };
                            dr[(int)Clm.FCN_] = t.FullName;
                            dr[(int)Clm.PNUM] = pis.Length;
                            _methodCache.Rows.Add(dr);
                        }
                    }

                    PopMsg(this, DateTime.Now, t.FullName + " 注册成功。");
                }
                catch (Exception ex)
                {
                    PopErr(this, DateTime.Now, ex);
                }
            }
        }

        public void LogoutInterface(object o)
        {
            LogoutInterface(o.GetType());
        }

        public void LogoutInterface(Type t)
        {
            for (int i = 0; i < _methodCache.Rows.Count; i++)
            {
                if (_methodCache.Rows[i][(int)Clm.FCN_].ToString() == t.FullName)
                {
                    _methodCache.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        public void ClearInterface()
        {
            _methodCache.Clear();
        }

        #endregion

        #region - Private method -

        private void PopMsg(object sender, DateTime time, string msg)
        {
            if (OnMsg != null)
            {
                OnMsg.BeginInvoke(sender, DateTime.Now, msg, null, null);
            }
        }

        private void PopErr(object sender, DateTime time, Exception e)
        {
            if (OnErr != null)
            {
                OnErr.BeginInvoke(sender, time, e, null, null);
            }
        }

        private void StateChanged(object sender, DateTime time, bool state)
        {
            IsStarted = state;
            if (OnStateChanged != null)
            {
                OnStateChanged.BeginInvoke(sender, time, state, null, null);
            }
        }

        private void ClientCntStateChanged(object sender, DateTime time, CmncmdInfo ci, bool state)
        {
            if (OnClientCntStateChanged != null)
            {
                OnClientCntStateChanged.BeginInvoke(sender, time, ci, state, null, null);
            }
        }

        private void Listen(object o)
        {
            ClsListenInfo lsInfo = o as ClsListenInfo;
            try
            {
                while (true)
                {
                    Socket skt = lsInfo.Skt.Accept();
                    DateTime cntTime = DateTime.Now;
                    skt.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                    CmncmdBase cb = new CmncmdBase();
                    Courier gtRst = null;
                    try
                    {
                        gtRst = CmncmdBase.ReadRsut(skt);
                        LinkedList<CmncmdInfo> cInfo = gtRst.Value as LinkedList<CmncmdInfo>;
                        if (cInfo == null)
                            continue;
                        LinkedListNode<CmncmdInfo> nd = cInfo.First;
                        while (nd != null)
                        {
                            cb.CnctList.AddLast(nd.Value);
                            nd = nd.Next;
                        }
                    }
                    catch (Exception ex)
                    {
                        PopErr(this, DateTime.Now, ex);
                        skt.Close();
                        cb.Dispose();
                        continue;
                    }
                    cb.CnctList.First.Value.IP = (skt.LocalEndPoint as IPEndPoint).Address.ToString();
                    cb.CnctList.First.Value.Port = (skt.LocalEndPoint as IPEndPoint).Port.ToString();
                    cb.CnctList.First.Value.ConnectTime = cntTime;
                    CmncmdBase.SendRsut(skt, new Courier() { CrType = RcType.Cb_Noml, Session = gtRst.Session, Value = cb.CnctList });
                    cb.OnNmInvoke += CmncmdBase_OnNmInvoke;
                    cb.OnCntChanged += CmncmdBase_OnCntChanged;
                    cb.OnErr += PopErr;
                    cb.OnMsg += PopMsg;
                    cb.Start(skt);
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }
            finally
            {
                _lsnInfos.Remove(lsInfo);
                try { lsInfo.Skt.Disconnect(false); }
                catch { }
                PopMsg(this, DateTime.Now, lsInfo.IP + ":" + lsInfo.Port + " Stop listen . ");
            }
        }

        private void CmncmdBase_OnCntChanged(object sender, DateTime time, bool state)
        {
            CmncmdBase cb = sender as CmncmdBase;
            if (state)
            {
                _cltInfos.Add(cb);
                ClientCntStateChanged(sender, time, cb.CnctList.Last.Value, state);
            }
            else
            {
                cb.OnNmInvoke -= CmncmdBase_OnNmInvoke;
                cb.OnCntChanged -= CmncmdBase_OnCntChanged;
                cb.OnErr -= PopErr;
                cb.OnMsg -= PopMsg;
                _cltInfos.Remove(sender as CmncmdBase);
                ClientCntStateChanged(sender, time, cb.CnctList.Last.Value, state);
            }
        }

        private object CmncmdBase_OnNmInvoke(string nmName, object[] param)
        {
            try
            {
                MethodHandler method = FindMethod(nmName, param);
                return method.Invoke(param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private MethodHandler FindMethod(string methodTag, object[] pList)
        {
            try
            {
                if (methodTag == null)
                {
                    throw new Exception("方法标识为空！");
                }

                int pNum = pList == null ? 0 : pList.Length;

                DataRow[] drs = null;
                int pointCount = methodTag.Split('.').Length - 1;
                if (pointCount == 0)
                {
                    drs = _methodCache.Select(
                        string.Format(
                        "__MN = '{0}' And PNUM = {1}", methodTag, pNum));
                }
                else if (pointCount == 1)
                {
                    drs = _methodCache.Select(
                        string.Format(
                        "_CMN = '{0}' And PNUM = {1}", methodTag, pNum));
                }
                else
                {
                    drs = _methodCache.Select(
                        string.Format(
                        "FCMN = '{0}' And PNUM = {1}", methodTag, pNum));
                }

                int rowIndex = 0;
                if (drs == null || drs.Length < 1)
                {
                    throw new NoFindInterfaceException();
                }
                else if (drs.Length > 1)
                {
                    //查找重载方法
                    int findCount = 0;
                    for (int i = 0; i < drs.Length; i++)
                    {
                        Type[] pit = drs[i][(int)Clm.PLST] as Type[];
                        if (pit == null && (pList == null || pList.Length < 1))
                        {
                            rowIndex = i;
                            findCount++;
                        }
                        else
                        {
                            bool isSame = true;

                            for (int ii = 0; ii < pit.Length; ii++)
                            {
                                if (!pit[ii].IsAssignableFrom(pList[ii].GetType()))
                                {
                                    isSame = false;
                                    break;
                                }
                            }

                            if (isSame)
                            {
                                rowIndex = i;
                                findCount++;
                            }
                        }
                    }

                    if (findCount > 1)
                    {
                        throw new MultiInterfaceException();
                    }
                    else if (findCount < 1)
                    {
                        throw new NoFindInterfaceException();
                    }
                }

                return drs[rowIndex][(int)Clm.MDIF] as MethodHandler;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }

    public partial class CmncmdServer
    {
        #region - Object define -

        private enum Clm
        {
            /// <summary> 完整类名 方法名 </summary>
            FCMN,
            /// <summary> 类名 方法名 </summary>
            _CMN,
            __MN,
            FCN_,
            PLST,
            CLAS,
            MDIF,
            PNUM
        }

        /// <summary> 监听信息 </summary>
        private class ClsListenInfo
        {
            public string IP
            {
                set;
                get;
            }

            public string Port
            {
                set;
                get;
            }

            public Thread Trd
            {
                set;
                get;
            }

            public Socket Skt
            {
                set;
                get;
            }
        }

        private class CltInfo
        {
            public Thread Trd
            {
                set;
                get;
            }

            public Socket Skt
            {
                set;
                get;
            }
        }

        private class MethodHandler
        {
            public MethodInfo Method
            {
                set;
                get;
            }

            public object Target
            {
                set;
                get;
            }

            public object Invoke(params object[] param)
            {
                try
                {
                    return Method.Invoke(Target, param);
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion
    }

    #endregion - cmdServer End-


    #region -Start cmdClient-

    [Serializable]
    public class CmncmdInfo
    {
        public string CltID
        {
            set;
            get;
        }

        public string Host
        {
            set;
            get;
        }

        /// <summary> IP </summary>
        public string IP
        {
            set;
            get;
        }

        /// <summary> 端口 </summary>
        public string Port
        {
            set;
            get;
        }

        public DateTime CreateTime
        {
            set;
            get;
        }

        public DateTime ConnectTime
        {
            set;
            get;
        }
    }

    public class CmncmdClient : CmncmdBase
    {
        #region - Field -

        #endregion

        #region - Constructor -

        public CmncmdClient()
            : base()
        {

        }

        ~CmncmdClient()
        {
            Dispose();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        #endregion

        #region - Property -

        public string IP
        {
            set;
            get;
        }

        public string Port
        {
            set;
            get;
        }

        #endregion

        #region - Public method -

        public void Connect()
        {
            if (IsConnected)
            {
                PopMsg(this, DateTime.Now, IP + ":" + Port + " is connected . ");
                return;
            }
            Socket skt = null;
            try
            {
                if (!NetMethod.CheckNet(IP, Port, 2000))
                {
                    throw new Exception("IP 地址检查失败。（" + IP + ":" + Port + "）");
                }
                skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                skt.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                skt.Connect(IP, int.Parse(Port));
                CnctList.First.Value.ConnectTime = DateTime.Now;
                CnctList.First.Value.IP = (skt.LocalEndPoint as IPEndPoint).Address.ToString();
                CnctList.First.Value.Port = (skt.LocalEndPoint as IPEndPoint).Port.ToString();
                CmncmdBase.SendRsut(skt, new Courier() { Value = CnctList });
                CnctList.AddLast((CmncmdBase.ReadRsut(skt).Value as LinkedList<CmncmdInfo>).First.Value);
                base.Start(skt);
            }
            catch (Exception ex)
            {
                if (skt != null)
                    skt.Close();

                PopCntState(this, DateTime.Now, false);

                PopErr(this, DateTime.Now, ex);
            }
        }

        public void Disconnect()
        {
            try
            {
                base.Stop();
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }
        }

        #endregion
    }

    #endregion-cmdClient End-


    #region -Start cmdBase-
    public delegate object DlgS_NmInvoke(string nmName, object[] param);

    public delegate void DlgC_EvInvoke(int index, string evName, object[] param);

    /// <summary> 接受数据类型 </summary>
    internal enum RcType
    {
        /// <summary> 命令 正常调用 </summary>
        Cm_Invk = (byte)0,

        /// <summary> 命令 对象调用 </summary>
        Cm_Obiv = (byte)1,

        /// <summary> 命令 创建对象 </summary>
        Cm_Cret = (byte)2,

        /// <summary> 命令 释放对象 </summary>
        Cm_Disp = (byte)3,

        /// <summary> 命令 回调事件 </summary>
        Cm_Evnt = (byte)4,

        /// <summary> 命令 Setto </summary>
        Cm_Stto = (byte)5,

        /// <summary> 命令 Disst </summary>
        Cm_Dsst = (byte)6,

        /// <summary> 反馈 正常 </summary>
        Cb_Noml = (byte)254,

        /// <summary> 反馈 异常 </summary>
        Cb_Erro = (byte)255
    }

    /// <summary> 传递对象 </summary>
    internal class Courier
    {
        public RcType CrType
        {
            set;
            get;
        }

        public Object Value
        {
            set;
            get;
        }

        public byte Session
        {
            set;
            get;
        }
    }

    internal class RequestInfo
    {
        public ManualResetEvent WaitHandle
        {
            set;
            get;
        }

        public bool IsWait
        {
            set;
            get;
        }

        public Courier Result
        {
            set;
            get;
        }
    }

    internal class Interactive
    {
        Thread _t_g = null;
        Thread _t_b = null;
        Socket _skt_f = null;
        Socket _skt_t = null;
        DlgErrHandler _onErr = null;
        ManualResetEvent _mre = new ManualResetEvent(false);

        public Interactive(Socket skt_f, Socket skt_t, DlgErrHandler onErr)
        {
            _skt_f = skt_f;
            _skt_t = skt_t;
            _t_g = new Thread(trsf_g) { IsBackground = true };
            _t_b = new Thread(trsf_b) { IsBackground = true };
            _onErr = onErr;
        }

        public void Bind()
        {
            _mre.Reset();
            _t_g.Start();
            _t_b.Start();
            _mre.WaitOne();
            _t_g.Abort();
            _t_b.Abort();
        }

        private void trsf_g(object o)
        {
            try
            {
                byte[] bf = new byte[2048];
                while (true)
                {
                    int rct =
                    _skt_f.Receive(bf, 0, bf.Length, SocketFlags.None);
                    if (rct == 0)
                        throw new Exception("The client has been closed.");
                    _skt_t.Send(bf, 0, rct, SocketFlags.None);
                }
            }
            catch (ThreadAbortException)
            { }
            catch (Exception ex)
            {
                _onErr(this, DateTime.Now, ex);
                try
                {
                    _skt_f.Close();
                    _skt_t.Close();
                }
                catch { }
            }
            _mre.Set();
        }

        private void trsf_b(object o)
        {
            try
            {
                byte[] bf = new byte[2048];
                while (true)
                {
                    int rct =
                    _skt_t.Receive(bf, 0, bf.Length, SocketFlags.None);
                    if (rct == 0)
                    {
                        _skt_t.Close();
                        break;
                    }
                    _skt_f.Send(bf, 0, rct, SocketFlags.None);
                }
            }
            catch (ThreadAbortException)
            { }
            catch (Exception ex)
            {
                _onErr(this, DateTime.Now, ex);
                try
                {
                    _skt_f.Close();
                    _skt_t.Close();
                }
                catch { }
            }
            _mre.Set();
        }
    }

    public partial class CmncmdBase : Cmncmd
    {
        #region - Field -
        private ManualResetEvent _waitCntStt = new ManualResetEvent(false);
        private Socket _skt;
        private Thread _rdt;
        private ClassBox[] _cboxList = new ClassBox[100];
        private SessionMng _wtSs = new SessionMng();
        private SessionMng _rdSs = new SessionMng();
        //private RequestInfo[] _rqstInfo = new RequestInfo[byte.MaxValue];
        //private WaitCallback _asynInvoik = null;

        #endregion

        #region - Constructor -

        public CmncmdBase()
            : base()
        {
            //for (int i = 0; i < _rqstInfo.Length; i++)
            //{
            //    _rqstInfo[i] = new RequestInfo() { IsWait = false, WaitHandle = new ManualResetEvent(true) };
            //}
            //_asynInvoik = new WaitCallback(AsynInvoke);
        }

        ~CmncmdBase()
        {
            Dispose();
        }

        public override void Dispose()
        {
            if (_skt != null)
            {
                _skt.Close();
                _skt = null;
            }

            if (_rdt != null)
            {
                _rdt.Abort();
                _rdt = null;
            }
            base.Dispose();
        }

        #endregion

        #region - Property -

        public bool IsConnected
        {
            private set;
            get;
        }

        #endregion

        #region - Event -

        public event DlgMsgHandler OnMsg;

        public event DlgErrHandler OnErr;

        public event DlgStateChanged OnCntChanged;

        public event DlgS_NmInvoke OnNmInvoke;

        public event DlgC_EvInvoke OnEvInvoke;

        #endregion

        internal void Start(Socket skt)
        {
            if (IsConnected)
            {
                PopMsg(this, DateTime.Now, "CmncmdBase is started . ");
                return;
            }

            _skt = skt;

            _waitCntStt.Reset();

            _rdt = new Thread(Reader);
            _rdt.IsBackground = true;
            _rdt.Start();

            if (!_waitCntStt.WaitOne(1000 * 20, false))
            {
                PopCntState(this, DateTime.Now, false);
            }
        }

        internal void Stop()
        {
            try
            {
                if (!IsConnected)
                {
                    PopMsg(this, DateTime.Now, "CmncmdBase is stoped . ");
                    return;
                }

                try
                {
                    _skt.Close(5);
                    _skt = null;
                }
                catch { }

                try
                {
                    Thread t = _rdt;
                    _rdt = null;
                    t.Abort();
                    t.Join();
                    t = null;
                }
                catch { }
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }
        }

        private void Reader(object o)
        {
            //for (int i = 0; i < _rqstInfo.Length; i++)
            //{
            //    _rqstInfo[i].IsWait = false;
            //    _rqstInfo[i].Result = null;
            //    _rqstInfo[i].WaitHandle.Set();
            //}
            _wtSs.ReBuild();

            EndPoint ep = _skt.RemoteEndPoint;
            try
            {
                PopCntState(this, DateTime.Now, true);
                while (true)
                {
                    Courier gtResult = ReadRsut(_skt);
                    switch (gtResult.CrType)
                    {
                        case RcType.Cm_Invk:
                        case RcType.Cm_Obiv:
                        case RcType.Cm_Cret:
                        case RcType.Cm_Disp:
                        case RcType.Cm_Evnt:
                            ThreadPool.QueueUserWorkItem(AsynInvoke, gtResult);
                            break;
                        case RcType.Cm_Stto:
                            GetTo(gtResult);
                            break;
                        case RcType.Cm_Dsst:
                            _skt.Close();
                            break;
                        case RcType.Cb_Noml:
                        case RcType.Cb_Erro:
                            _wtSs.SetResult(gtResult.Session, gtResult);
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (System.Net.Sockets.SocketException)
            {

            }
            catch (ReceivedZeroException)
            {

            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }
            finally
            {
                _wtSs.ReBuild();
                //for (int i = 0; i < _rqstInfo.Length; i++)
                //{
                //    _rqstInfo[i].IsWait = false;
                //    _rqstInfo[i].Result = new Courier() { Session = (byte)i, CrType = RcType.Cb_Noml, Value = null };
                //    _rqstInfo[i].WaitHandle.Set();
                //}

                PopCntState(this, DateTime.Now, false);
                for (int i = 0; i < _cboxList.Length; i++)
                {
                    try { S_DspObject(i); }
                    catch { }
                }
                try
                {
                    if (_skt != null)
                        _skt.Close();
                }
                catch { }
                PopMsg(this, DateTime.Now, ep.ToString() + " set free . ");
            }
        }

        private object Writer(Courier info)
        {
            try
            {
                byte sessionID = 0;
                bool isRqst = false;
                lock (this)
                {
                    switch (info.CrType)
                    {
                        case RcType.Cm_Invk:
                        case RcType.Cm_Obiv:
                        case RcType.Cm_Cret:
                        case RcType.Cm_Disp:
                        case RcType.Cm_Evnt:
                        case RcType.Cm_Stto:
                        case RcType.Cm_Dsst:
                            if (!_wtSs.BuildSession(out sessionID))
                            {
                                return null;
                            }
                            info.Session = sessionID;
                            isRqst = true;
                            break;
                        case RcType.Cb_Noml:
                        case RcType.Cb_Erro:
                            break;
                        default:
                            break;
                    }
                    SendRsut(_skt, info);
                }
                if (isRqst)
                {
                    return _wtSs.GetResult(sessionID);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetTo(string ip, string port)
        {
            try
            {
                if (_wtSs.WaitAllComplete())
                {
                    Courier r =
                        Writer(new Courier() { CrType = RcType.Cm_Stto, Value = new string[] { ip, port } }) as Courier;

                    LinkedList<CmncmdInfo> rst = r.Value as LinkedList<CmncmdInfo>;
                    if (rst != null)
                    {
                        CnctList.AddLast(rst.First.Value);
                    }
                    _wtSs.ReBuild();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DisSt()
        {
            try
            {
                if (_wtSs.WaitAllComplete())
                {
                    Courier r =
                       Writer(new Courier() { CrType = RcType.Cm_Dsst }) as Courier;
                    if (r.CrType == RcType.Cb_Noml)
                    {
                        CnctList.RemoveLast();
                        _skt.Send(new byte[] { 0 });//终止转接器的 Go线程
                    }
                    else if (r.CrType == RcType.Cb_Erro && r.Value is Exception)
                    {
                        throw r.Value as Exception;
                    }
                    _wtSs.ReBuild();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetTo(Courier gtResult)
        {
            try
            {
                Courier bdResult = new Courier() { Session = 0 };
                try
                {
                    string[] prg = gtResult.Value as string[];
                    string ip = prg[0];
                    string port = prg[1];

                    Socket skt = null;
                    try
                    {
                        if (!NetMethod.CheckNet(ip, port, 2000))
                        {
                            throw new Exception("IP 地址检查失败。（" + ip + ":" + port + "）");
                        }
                        skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        skt.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);
                        skt.Connect(ip, int.Parse(port));
                        CmncmdBase.SendRsut(skt, new Courier() { Session = gtResult.Session, CrType = RcType.Cb_Noml, Value = CnctList });
                        new Interactive(_skt, skt, PopErr).Bind();
                    }
                    catch (Exception ex)
                    {
                        if (skt != null)
                            skt.Close();
                        throw ex;
                    }

                    bdResult.CrType = RcType.Cb_Noml;
                    bdResult.Value = null;
                }
                catch (Exception ex)
                {
                    bdResult.CrType = RcType.Cb_Erro;
                    bdResult.Value = ex;
                }
                finally
                {
                    Writer(bdResult);
                }
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }
        }

        public object C_NmInvoke(string nmName, params object[] param)
        {
            Courier rsut = null;
            try
            {
                Courier para = new Courier() { CrType = RcType.Cm_Invk };
                object[] objs = new object[param == null ? 1 : 1 + param.Length];
                objs[0] = nmName;
                if (param != null)
                    Array.Copy(param, 0, objs, 1, param.Length);
                para.Value = objs;
                rsut = Writer(para) as Courier;
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }

            if (rsut == null)
            {
                return null;
            }
            else if (rsut.CrType == RcType.Cb_Erro)
            {
                throw rsut.Value as Exception;
            }
            else
            {
                return rsut.Value;
            }
        }

        public object C_ObInvoke(int index, string mbName, params object[] param)
        {
            Courier rsut = null;
            try
            {
                Courier para = new Courier() { CrType = RcType.Cm_Obiv };
                object[] objs = new object[param == null ? 2 : 2 + param.Length];
                objs[0] = index;
                objs[1] = mbName;
                if (param != null)
                    Array.Copy(param, 0, objs, 2, param.Length);
                para.Value = objs;

                rsut = Writer(para) as Courier;
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }

            if (rsut == null)
            {
                return null;
            }
            else if (rsut.CrType == RcType.Cb_Erro)
            {
                throw rsut.Value as Exception;
            }
            else
            {
                return rsut.Value;
            }
        }

        public int C_CrtObject(string fullName, params object[] param)
        {
            Courier rsut = null;
            try
            {
                Courier para = new Courier() { CrType = RcType.Cm_Cret };
                object[] objs = new object[param == null ? 1 : 1 + param.Length];
                objs[0] = fullName;
                if (param != null)
                    Array.Copy(param, 0, objs, 1, param.Length);
                para.Value = objs;

                rsut = Writer(para) as Courier;
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }

            if (rsut == null)
            {
                return 0;
            }
            else if (rsut.CrType == RcType.Cb_Erro)
            {
                throw rsut.Value as Exception;
            }
            else
            {
                return (int)rsut.Value;
            }
        }

        public void C_DspObject(int index)
        {
            Courier rsut = null;
            try
            {
                Courier para = new Courier() { CrType = RcType.Cm_Disp };

                para.Value = index;

                rsut = Writer(para) as Courier;
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }

            if (rsut.CrType == RcType.Cb_Erro)
            {
                throw rsut.Value as Exception;
            }
        }

        private int S_CrtObject(string fullName, object[] param)
        {
            try
            {
                ClassTemplate ci = TypeList.ContainsKey(fullName) ? TypeList[fullName] : new ClassTemplate();
                if (ci.Class == null)
                    ci.Class = Type.GetType(fullName, true, true);

                object o =
                ci.Class.GetConstructor(param == null || param.Length < 1 ? new Type[] { } : Type.GetTypeArray(param))
                    .Invoke(param);

                Type t = ci.Interface == null ? ci.Class : ci.Interface;

                int index = 0;
                for (int i = 0; i < _cboxList.Length; i++)
                {
                    if (_cboxList[i] == null)
                    {
                        index = i;
                        _cboxList[i] = new ClassBox(o, t) { Index = i };
                        _cboxList[i].OnObjectEvent += S_EvInvoke;
                        break;
                    }
                }
                PopMsg(this, DateTime.Now, fullName + " 已创建。 index : " + index);
                return index;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void S_DspObject(int index)
        {
            try
            {
                if (_cboxList[index] != null)
                {
                    _cboxList[index].OnObjectEvent -= S_EvInvoke;
                    _cboxList[index].Dispose();
                    _cboxList[index] = null;
                    PopMsg(this, DateTime.Now, "(" + CnctList.Last.Value.IP + ":" + CnctList.Last.Value.Port + ")" + CnctList.Last.Value.ConnectTime.ToString("yyyyMMddHHmmss") + ",对象释放。 index : " + index);
                }
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
                throw ex;
            }
        }

        private object S_NmInvoke(string nmName, object[] param)
        {
            try
            {
                if (OnNmInvoke != null)
                    return OnNmInvoke(nmName, param);
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private object S_ObInvoke(int index, string mbName, object[] param)
        {
            try
            {
                return _cboxList[index].InvokMember(mbName, param);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 服务端事件回调（对象回调） </summary>
        private void S_EvInvoke(int index, string evName, object[] param)
        {
            Courier rsut = null;
            try
            {
                Courier para = new Courier() { CrType = RcType.Cm_Evnt };
                object[] objs = new object[param == null ? 2 : 2 + param.Length];
                objs[0] = index;
                objs[1] = evName;
                if (param != null)
                    Array.Copy(param, 0, objs, 2, param.Length);
                para.Value = objs;

                rsut = Writer(para) as Courier;
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }

            if (rsut == null || rsut.CrType == RcType.Cb_Erro)
            {
                S_DspObject(index);
            }

            if (rsut == null)
            {
                PopMsg(this, DateTime.Now, "对象" + index + "已经释放，事件回调未知异常。");
            }
            else if (rsut.CrType == RcType.Cb_Erro)
            {
                PopMsg(this, DateTime.Now, "对象" + index + "已经释放，事件回调异常。" + rsut.Value == null ? string.Empty : (rsut.Value as Exception).Message);

                if (rsut.Value != null)
                {
                    PopErr(this, DateTime.Now, rsut.Value as Exception);
                }
            }
        }

        /// <summary> 客户端事件回调（调用客户方法） </summary>
        private void C_EvInvoke(int index, string evName, object[] param)
        {
            if (OnEvInvoke != null)
            {
                OnEvInvoke(index, evName, param);
            }
        }

        private void AsynInvoke(object result)
        {
            Courier gtResult = result as Courier;
            try
            {
                Courier bdResult = new Courier() { Session = gtResult.Session };

                switch (gtResult.CrType)
                {
                    #region - RcType.Cm_Invk -

                    case RcType.Cm_Invk:
                        object[] Cm_Invk_o = gtResult.Value as object[];
                        object[] Cm_Invk_t = null;
                        if (Cm_Invk_o.Length > 1)
                        {
                            Cm_Invk_t = new object[Cm_Invk_o.Length - 1];
                            Array.Copy(Cm_Invk_o, 1, Cm_Invk_t, 0, Cm_Invk_t.Length);
                        }
                        try
                        {
                            bdResult.Value = S_NmInvoke(Cm_Invk_o[0] as string, Cm_Invk_t);
                            bdResult.CrType = RcType.Cb_Noml;
                        }
                        catch (Exception ex)
                        {
                            bdResult.CrType = RcType.Cb_Erro;
                            bdResult.Value = ex;
                        }
                        break;

                    #endregion

                    #region - RcType.Cm_Obiv -

                    case RcType.Cm_Obiv:
                        object[] Cm_Obiv_o = gtResult.Value as object[];
                        object[] Cm_Obiv_t = null;
                        if (Cm_Obiv_o.Length > 2)
                        {
                            Cm_Obiv_t = new object[Cm_Obiv_o.Length - 2];
                            Array.Copy(Cm_Obiv_o, 2, Cm_Obiv_t, 0, Cm_Obiv_t.Length);
                        }

                        try
                        {
                            bdResult.Value = S_ObInvoke((int)Cm_Obiv_o[0], Cm_Obiv_o[1] as string, Cm_Obiv_t);
                            bdResult.CrType = RcType.Cb_Noml;
                        }
                        catch (Exception ex)
                        {
                            bdResult.Value = ex;
                            bdResult.CrType = RcType.Cb_Erro;
                        }
                        break;

                    #endregion

                    #region - RcType.Cm_Cret -

                    case RcType.Cm_Cret:
                        object[] Cm_Cret_o = gtResult.Value as object[];
                        object[] Cm_Cret_t = null;
                        if (Cm_Cret_o.Length > 1)
                        {
                            Cm_Cret_t = new object[Cm_Cret_o.Length - 1];
                            Array.Copy(Cm_Cret_o, 1, Cm_Cret_t, 0, Cm_Cret_t.Length);
                        }

                        try
                        {
                            bdResult.Value = S_CrtObject(Cm_Cret_o[0] as string, Cm_Cret_t);
                            bdResult.CrType = RcType.Cb_Noml;
                        }
                        catch (Exception ex)
                        {
                            bdResult.Value = ex;
                            bdResult.CrType = RcType.Cb_Erro;
                        }
                        break;

                    #endregion

                    #region - RcType.Cm_Disp -

                    case RcType.Cm_Disp:
                        try
                        {
                            int index = (int)gtResult.Value;
                            S_DspObject(index);
                            bdResult.Value = null;
                            bdResult.CrType = RcType.Cb_Noml;
                        }
                        catch (Exception ex)
                        {
                            bdResult.Value = ex;
                            bdResult.CrType = RcType.Cb_Erro;
                        }
                        break;

                    #endregion

                    #region - RcType.Cm_Evnt -

                    case RcType.Cm_Evnt:
                        object[] Cm_Evnt_o = gtResult.Value as object[];
                        object[] Cm_Evnt_t = null;
                        if (Cm_Evnt_o.Length > 2)
                        {
                            Cm_Evnt_t = new object[Cm_Evnt_o.Length - 2];
                            Array.Copy(Cm_Evnt_o, 2, Cm_Evnt_t, 0, Cm_Evnt_t.Length);
                        }

                        try
                        {
                            C_EvInvoke((int)Cm_Evnt_o[0], Cm_Evnt_o[1] as string, Cm_Evnt_t);
                            bdResult.Value = null;
                            bdResult.CrType = RcType.Cb_Noml;
                        }
                        catch (Exception ex)
                        {
                            bdResult.Value = ex;
                            bdResult.CrType = RcType.Cb_Erro;
                        }
                        break;

                    #endregion

                    default:
                        break;
                }

                Writer(bdResult);
            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }
        }

        protected void PopCntState(object sender, DateTime time, bool state)
        {
            IsConnected = state;
            _waitCntStt.Set();
            if (OnCntChanged != null)
            {
                Delegate[] list = OnCntChanged.GetInvocationList();
                foreach (DlgStateChanged v in list)
                {
                    v.BeginInvoke(sender, time, state, null, null);
                }
            }
        }

        protected void PopMsg(object sender, DateTime time, string msg)
        {
            if (OnMsg != null)
            {
                OnMsg.BeginInvoke(sender, DateTime.Now, msg, null, null);
            }
        }

        protected void PopErr(object sender, DateTime time, Exception e)
        {
            if (OnErr != null)
            {
                OnErr.BeginInvoke(sender, time, e, null, null);
            }
        }
    }

    public partial class CmncmdBase
    {
        private static System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        public static byte[] ReadByte(Socket skt)
        {
            try
            {
                int itop = 8;
                int sign_l = 0;
                long data_l = 0;
                byte[] cache = new byte[1024];
                byte[] sign = new byte[itop];
                byte[] data = null;
                while (data == null || data_l < data.LongLength)
                {
                    int rdl =
                        data == null ? sign.Length - sign_l
                        : data.LongLength - data_l > cache.Length ? cache.Length
                        : (int)(data.LongLength - data_l);
                    Thread t = Thread.CurrentThread;
                    int rec = skt.Receive(cache, rdl, SocketFlags.None);
                    if (rec <= 0)
                    {
                        throw new ReceivedZeroException("receive 0 , the end . ");
                    }

                    if (data == null)
                    {
                        Array.Copy(cache, 0, sign, sign_l, sign_l + rec > itop ? itop - sign_l : sign_l + rec);
                        sign_l += rec;

                        if (sign_l >= itop)
                        {
                            long l = BitConverter.ToInt64(sign, 0);
                            if (l == 0)
                                break;

                            data = new byte[l];
                            if (sign_l > itop)
                            {
                                Array.Copy(cache, itop - sign_l + rec, data, data_l, sign_l - itop);
                                data_l += (sign_l - itop);
                            }
                        }
                    }
                    else
                    {
                        Array.Copy(cache, 0, data, data_l, rec);
                        data_l += rec;
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendByte(Socket skt, byte[] data)
        {
            try
            {
                long l = data == null ? 0 : data.LongLength;
                skt.Send(BitConverter.GetBytes(l), SocketFlags.None);
                if (data != null && l > 0)
                    skt.Send(data, SocketFlags.None);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static Courier ReadRsut(Socket skt)
        {
            try
            {
                byte[] data = ReadByte(skt);
                Courier result = new Courier();
                result.CrType = data == null || data.Length < 1 ? RcType.Cb_Noml : (RcType)data[0];
                result.Session = data == null || data.Length < 2 ? (byte)0 : data[1];

                if (data != null && data.LongLength > 2)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream(data))
                    {
                        ms.Position = 2;
                        result.Value = _bf.Deserialize(ms);
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static void SendRsut(Socket skt, Courier result)
        {
            try
            {
                byte[] data = null;
                if (result.Value != null)
                {
                    using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                    {
                        ms.WriteByte((byte)result.CrType);
                        ms.WriteByte(result.Session);
                        //if (result.Value is byte[])
                        //{
                        //    ms.Write(result.Value as byte[], 0, (result.Value as byte[]).Length);
                        //}
                        //else
                        //{
                        _bf.Serialize(ms, result.Value);
                        //}
                        data = ms.ToArray();
                    }
                }
                else
                {
                    data = new byte[] { (byte)result.CrType, result.Session };
                }
                SendByte(skt, data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Dictionary<string, ClassTemplate> TypeList
        {
            private set;
            get;
        }

        static CmncmdBase()
        {
            TypeList = new Dictionary<string, ClassTemplate>();
        }
    }
    #endregion -cmdBase End-


    #region -Start cmd -
    public class Cmncmd : IDisposable
    {
        protected static byte[] inOptionValues = null;

        static Cmncmd()
        {
            uint dummy = 0;
            inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
            BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);//是否启用Keep-Alive
            BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));//多长时间开始第一次探测
            BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);//探测时间间隔
        }

        public Cmncmd()
        {
            CnctList = new LinkedList<CmncmdInfo>();
            CnctList.AddLast(new CmncmdInfo()
            {
                CltID = Guid.NewGuid().ToString(),
                Host = Environment.MachineName,
                CreateTime = DateTime.Now
            });
        }

        public virtual void Dispose()
        {
        }

        public LinkedList<CmncmdInfo> CnctList
        {
            private set;
            get;
        }

        //public CmncmdInfo LocalInfo
        //{
        //    private set;
        //    get;
        //}

        //public CmncmdInfo RemotInfo
        //{
        //    internal set;
        //    get;
        //}

        public object Tag
        {
            set;
            get;
        }
    }
    #endregion -cmd End-
}
