using HebianGu.ComLibMethods;
using HebianGu.ComLibModule.ExceptionEx;
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
using CM = HebianGu.ComLibMethods.LogMethod;

namespace HebianGu.ComLibMethods.SocketEx
{

    #region -Start Client-
    public class Client : IDisposable
    {
        #region - Private field -

        private int _port = 4097;
        private IPAddress _ip = new IPAddress(new byte[] { 127, 0, 0, 1 });
        private Socket _skt = null;
        private static System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

        #endregion

        #region - Public Property -

        /// <summary> 是否链接 </summary>
        public bool Connected
        {
            get { return _skt != null && _skt.Connected; }
        }

        public IPAddress IP
        {
            get { return _ip; }
        }

        public int Port
        {
            get { return _port; }
        }

        #endregion

        #region - Constructor -

        public Client() { }

        public Client(string ip)
        {
            try
            {
                _ip = IPAddress.Parse(ip);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Client(string ip, int port)
        {
            try
            {
                _ip = IPAddress.Parse(ip);
                _port = port;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region - Public Method -

        public void Connect()
        {
            try
            {
                _skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _skt.Connect(_ip, _port);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Disconnect()
        {
            try
            {
                if (_skt != null)
                    _skt.Close();
                _skt = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 调用远程方法 </summary>
        /// <param name="InterfaceTag"></param>
        /// <param name="Progress"></param>
        /// <param name="PrmtS"></param>
        /// <returns></returns>
        public object InvokeInterface(string InterfaceTag, Action<long[]> Progress, Action<byte[]> RcvStream, params object[] PrmtS)
        {
            if (_skt == null)
            {
                throw new Exception("Socket未连接。。");
            }

            lock (_skt)
            {

                object rcvObj = null;

                try
                {
                    if (_skt.Connected)
                    {
                        #region - 发送参数 -

                        using (System.IO.MemoryStream sendMS = new System.IO.MemoryStream())
                        {
                            sendMS.Write(new byte[8], 0, 8);

                            List<object> objLi = new List<object>(PrmtS.Length + 1);
                            objLi.Add(InterfaceTag);
                            objLi.AddRange(PrmtS);
                            _bf.Serialize(sendMS, objLi.ToArray());

                            sendMS.Position = 0;
                            sendMS.Write(BitConverter.GetBytes(sendMS.Length - 8), 0, 8);

                            _skt.Send(sendMS.ToArray(), SocketFlags.None);
                        }

                        #endregion

                        #region - 分析头信息 表识(flag)：0-错误；1-对象；2-流。大小(size) -

                        //分析头信息
                        int flag = 0;
                        long size = 0;
                        byte[] title = new byte[9];
                        long rcvCount = 0;
                        while (true)
                        {
                            int rcvAct = _skt.Receive(title, (int)rcvCount, (int)(title.Length - rcvCount), SocketFlags.None);
                            if (rcvAct <= 0)
                                break;
                            rcvCount += rcvAct;

                            if (rcvCount == 9)
                            {
                                flag = title[0];
                                size = BitConverter.ToInt64(title, 1);
                                break;
                            }
                        }

                        #endregion

                        #region - 错误 或 对象的处理 -

                        if ((flag == 0 || flag == 1) && size != 0)
                        {
                            using (System.IO.MemoryStream rcvMS = new System.IO.MemoryStream())
                            {
                                rcvCount = 0;
                                byte[] bs = new byte[1024];
                                while (true)
                                {
                                    int rcv = _skt.Receive(bs, bs.Length, SocketFlags.None);
                                    if (rcv == 0) break;
                                    rcvMS.Write(bs, 0, rcv);
                                    rcvCount += rcv;

                                    if (Progress != null)
                                        Progress(new long[] { size, rcvCount });

                                    if (rcvMS.Length == size)
                                    {
                                        rcvMS.Position = 0;
                                        rcvObj = _bf.Deserialize(rcvMS);
                                        break;
                                    }
                                }
                            }
                        }

                        #endregion

                        #region - 流的处理 -

                        else if ((flag == 2) && size != 0)
                        {
                            using (System.IO.MemoryStream rcvMS = new System.IO.MemoryStream())
                            {
                                rcvCount = 0;
                                byte[] bs = new byte[1024];
                                while (size != rcvCount)
                                {
                                    int rcv = _skt.Receive(bs, bs.Length, SocketFlags.None);
                                    if (rcv == 0) break;
                                    rcvCount += rcv;

                                    byte[] writeBs = new byte[rcv];
                                    Array.Copy(bs, writeBs, rcv);

                                    if (RcvStream != null) RcvStream(writeBs);
                                    if (Progress != null) Progress(new long[] { size, rcvCount });
                                }
                            }
                        }

                        #endregion

                        #region - 两条异常 -
                        else if (flag == 0 && size == 0)
                        {
                            throw new Exception("未知异常！");
                        }

                        #endregion
                    }
                    else
                    {
                        throw new Exception("Socket未连接。。");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (rcvObj is Exception)
                    throw rcvObj as Exception;

                return rcvObj;
            }
        }

        #endregion

        #region - Private Method -

        #endregion

        public void Dispose()
        {
            Disconnect();
        }
    }


    public delegate void SvrStatusHandle(Server svr, DateTime time, SvrStatus st);

    public delegate byte[] DlgInteractiveData(Guid id, byte[] data);

    public enum SvrStatus
    {
        /// <summary> 开启监听 </summary>
        Stt_Listen,

        /// <summary> 关闭监听 </summary>
        Stp_Listen
    }

    #endregion -Client End-

    #region -Start Server-

    public class Server
    {
        public const Int32 MDC_SVR_PORT = 4097;
        public const Int32 COM_UPG_PORT = 7249;
        public const Int32 COM_LCC_PORT = 7813;

        private static readonly Guid FTP_LCS = new Guid(new byte[] { 126, 62, 220, 250, 40, 181, 2, 70, 161, 209, 15, 61, 208, 9, 182, 136 });

        #region - Static member -

        protected static byte[] inOptionValues = null;

        static Server()
        {
            uint dummy = 0;
            inOptionValues = new byte[Marshal.SizeOf(dummy) * 3];
            BitConverter.GetBytes((uint)1).CopyTo(inOptionValues, 0);//是否启用Keep-Alive
            BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy));//多长时间开始第一次探测
            BitConverter.GetBytes((uint)5000).CopyTo(inOptionValues, Marshal.SizeOf(dummy) * 2);//探测时间间隔
        }

        #endregion

        #region - Constructor -

        public Server()
        {
            _methodCache = new DataTable("MethodChache");
            _methodCache.Columns.Add("ClsMtdFullName", Type.GetType("System.String"));
            _methodCache.Columns.Add("ClsMtdName", Type.GetType("System.String"));
            _methodCache.Columns.Add("MtdName", Type.GetType("System.String"));
            _methodCache.Columns.Add("PrmtList", Type.GetType("System.String"));
            _methodCache.Columns.Add("Class", Type.GetType("System.Object"));
            _methodCache.Columns.Add("Method", Type.GetType("System.Reflection.MethodInfo"));
            _methodCache.Columns.Add("ClsFullName", Type.GetType("System.String"));
            _methodCache.Columns.Add("ParamNum", Type.GetType("System.Int32"));
        }

        #endregion

        #region - Event -

        public event DlgMsgHandler OnMsg;

        public event DlgErrHandler OnErr;

        public event SvrStatusHandle ListenStatus;

        public event DlgInteractiveData InteractiveData;

        #endregion

        #region - Private Field -

        private int _port = MDC_SVR_PORT;
        private Thread[] _t_listenS = null;
        private List<object[]> _t_Lisn = new List<object[]>(100);
        private List<object[]> _t_Cmnc = new List<object[]>(100);
        private System.Runtime.Serialization.Formatters.Binary.BinaryFormatter _bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        private DataTable _methodCache = null;

        #endregion

        #region - Property -

        /// <summary>端口号
        /// </summary>
        public int Port
        {
            set { _port = value; }
            get { return _port; }
        }

        public SvrStatus Status
        {
            private set;
            get;
        }

        /// <summary>通信对象列表
        /// </summary>
        public List<object[]> CmncObjList
        {
            get { return _t_Cmnc; }
        }

        #endregion

        #region - Public Method -

        /// <summary> 开始监听 </summary>
        public void Start()
        {
            if (_t_Lisn.Count < 1)
            {
                List<IPAddress> ips = new List<IPAddress>();
                ips.Add(new IPAddress(new byte[] { 127, 0, 0, 1 }));

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
                        ips.Add(forip);
                    }
                }

                _t_listenS = new System.Threading.Thread[ips.Count];
                for (int i = 0; i < ips.Count; i++)
                {
                    try
                    {
                        Socket skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        skt.Bind(new IPEndPoint(ips[i], _port));
                        skt.Listen(100);
                        Thread _t = new Thread(new ParameterizedThreadStart(Listen));
                        object[] lisnPrmt = new object[] { _t, skt };
                        _t.IsBackground = true;
                        _t.Start(lisnPrmt);
                        _t_Lisn.Add(lisnPrmt);
                    }
                    catch (Exception ex)
                    {
                        PopErr(this, DateTime.Now, ex);
                    }
                }
            }

            if (_t_Lisn == null || _t_Lisn.Count < 1)
            {
                PopListenStatus(this, DateTime.Now, SvrStatus.Stp_Listen);
            }
            else
            {
                PopListenStatus(this, DateTime.Now, SvrStatus.Stt_Listen);
            }
        }

        /// <summary> 停止监听 </summary>
        public void Stop()
        {
            while (_t_Lisn.Count > 0)
            {
                Thread t = _t_Lisn[0][0] as Thread;
                if (t != null)
                    t.Abort();

                Socket s = _t_Lisn[0][1] as Socket;
                if (s != null)
                    s.Close();

                _t_Lisn.RemoveAt(0);
            }

            while (_t_Cmnc.Count > 0)
            {
                Socket s = _t_Cmnc[0][1] as Socket;
                if (s != null)
                    s.Close();

                Thread t = _t_Cmnc[0][0] as Thread;
                if (t != null)
                    t.Abort();

                _t_Cmnc.RemoveAt(0);
            }

            PopListenStatus(this, DateTime.Now, SvrStatus.Stp_Listen);
        }

        /// <summary> 注册接口 </summary>
        public void RegistInterface(object o, Type t)
        {
            if (o != null)
            {
                try
                {
                    //Type t = o.GetType();
                    if (_methodCache.Select("ClsFullName = '" + t.FullName + "'").Length > 0)
                    {
                        throw new Exception("已经注册过该对象。");
                    }

                    System.Reflection.MethodInfo[] mis = t.GetMethods();
                    if (mis != null && mis.Length > 0)
                    {
                        foreach (var miFor in mis)
                        {
                            DataRow dr = _methodCache.NewRow();

                            string PrmtList = string.Empty;
                            ParameterInfo[] pis = miFor.GetParameters();
                            if (pis != null && pis.Length > 0)
                            {
                                foreach (var pi in pis)
                                {
                                    PrmtList += pi.ParameterType.FullName + "|";
                                }
                                if (!string.IsNullOrEmpty(PrmtList))
                                    PrmtList = PrmtList.Remove(PrmtList.Length - 1);
                            }

                            dr[0] = t.FullName + "." + miFor.Name;
                            dr[1] = t.Name + "." + miFor.Name;
                            dr[2] = miFor.Name;
                            dr[3] = PrmtList;
                            //System.Text.RegularExpressions.Regex.Match(miFor.ToString(), @"\([^)]*\)").Value;
                            dr[4] = o;
                            dr[5] = miFor;
                            dr[6] = t.FullName;
                            dr[7] = pis.Length;
                            _methodCache.Rows.Add(dr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary> 注册接口 </summary>
        public void RegistInterface(object o)
        {
            try
            {
                RegistInterface(o, o.GetType());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary> 注销接口 </summary>
        public void LogoutInterface(object o)
        {
            LogoutInterface(o.GetType());
        }

        /// <summary> 注销接口 </summary>
        public void LogoutInterface(Type t)
        {
            for (int i = 0; i < _methodCache.Rows.Count; i++)
            {
                if (_methodCache.Rows[i][6].ToString() == t.FullName)
                {
                    _methodCache.Rows.RemoveAt(i);
                    i--;
                }
            }
        }

        /// <summary> 清除接口 </summary>
        public void ClearInterface()
        {
            _methodCache.Clear();
        }

        #endregion

        #region - Private Method -

        /// <summary> 监听 </summary>
        private void Listen(object o)
        {
            object[] oS = o as object[];
            Socket skt = oS[1] as Socket;
            EndPoint ep = skt.LocalEndPoint;
            try
            {
                PopMsg(this, DateTime.Now, "Start listen " + ep);
                while (true)
                {
                    Socket wSkt = skt.Accept();
                    wSkt.IOControl(IOControlCode.KeepAliveValues, inOptionValues, null);

                    Thread _t = new Thread(new ParameterizedThreadStart(Communication));
                    object[] CmncPrams = new object[] { _t, wSkt };
                    _t_Cmnc.Add(CmncPrams);

                    _t.IsBackground = true;
                    _t.Start(CmncPrams);
                }
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                PopErr(this, DateTime.Now, ex);
            }
            finally
            {
                PopMsg(this, DateTime.Now, "Stop listen " + ep);
            }
        }

        /// <summary> 通信 </summary>
        private void Communication(object o)
        {
            object[] obS = null;
            EndPoint rmE = null;
            try
            {
                obS = o as object[];
                if (obS == null || obS.Length < 1)
                {
                    return;
                }

                long? l = null;
                Socket dataSock = obS[1] as Socket;
                rmE = dataSock.RemoteEndPoint;

                PopMsg(this, DateTime.Now, rmE + " Connected.");

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                byte[] data = new byte[2048];
                while (true)
                {
                    #region - Receive Data -

                    int rec = dataSock.Receive(data, data.Length, SocketFlags.None);
                    if (rec <= 0)
                        break;
                    ms.Write(data, 0, rec);

                    if (l == null && ms.Length >= 8)
                    {
                        ms.Position = 0;
                        byte[] btL = new byte[8];
                        ms.Read(btL, 0, 8);
                        l = BitConverter.ToInt64(btL, 0);
                        ms.Position = ms.Length;
                    }

                    #endregion

                    #region - Rrocess Data -

                    if (l != null && ms.Length - 8 == l)
                    {

                        byte[] rstBt = null;
                        if (l >= 16)//(l == 1016)
                        {
                            byte[] btID = new byte[16];
                            ms.Position = 8;
                            ms.Read(btID, 0, 16);
                            try
                            {
                                if (new Guid(btID).Equals(FTP_LCS))
                                {
                                    rstBt = M_InteractiveData(FTP_LCS, ms.ToArray());
                                }
                            }
                            catch (Exception e)
                            {
                                PopErr(this, DateTime.Now, e);
                            }
                        }

                        if (rstBt == null)
                        {
                            #region - 处理参数 -

                            using (System.IO.MemoryStream resultMs = new System.IO.MemoryStream())
                            {
                                bool isSended = false;

                                resultMs.Write(new byte[9], 0, 9);

                                try
                                {
                                    //返回结果
                                    object resultObj = null;

                                    #region - 形成参数 -

                                    ms.Position = 8;
                                    object rcvDataO = _bf.Deserialize(ms);
                                    object[] rcvData = rcvDataO as object[];
                                    List<object> listData = new List<object>(rcvData);
                                    listData.RemoveAt(0);
                                    string methodTag = rcvData[0] as string;

                                    #endregion

                                    #region - 查找 & 执行方法 -

                                    if (methodTag != null)
                                    {
                                        DataRow[] drs = null;
                                        int pointCount = methodTag.Split('.').Length - 1;
                                        if (pointCount == 0)
                                        {
                                            drs = _methodCache.Select(string.Format("MtdName = '{0}' And ParamNum = {1}", methodTag, listData.Count));
                                        }
                                        else if (pointCount == 1)
                                        {
                                            drs = _methodCache.Select(string.Format("ClsMtdName = '{0}' And ParamNum = {1}", methodTag, listData.Count));
                                        }
                                        else
                                        {
                                            drs = _methodCache.Select(string.Format("ClsMtdFullName = '{0}' And ParamNum = {1}", methodTag, listData.Count));
                                        }

                                        int rowIndex = 0;
                                        if (drs == null || drs.Length < 1)
                                        {
                                            throw new NoFindInterfaceException();
                                        }
                                        else if (drs.Length > 1)
                                        {
                                            //查找重载方法
                                            StringBuilder sbPrmt = new StringBuilder();
                                            foreach (var oFor in listData)
                                            {
                                                sbPrmt.Append(oFor.GetType().FullName + "|");
                                            }
                                            if (sbPrmt.Length > 1)
                                                sbPrmt.Remove(sbPrmt.Length - 1, 1);

                                            int findCount = 0;
                                            for (int i = 0; i < drs.Length; i++)
                                            {
                                                if (drs[i][3].ToString() == sbPrmt.ToString())
                                                {
                                                    rowIndex = i;
                                                    findCount++;
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

                                        System.Reflection.MethodInfo mi = drs[rowIndex][5] as System.Reflection.MethodInfo;


                                        if (listData.Count < 1)
                                        {
                                            resultObj = mi.Invoke(drs[rowIndex][4], null);
                                        }
                                        else
                                        {
                                            resultObj = mi.Invoke(drs[rowIndex][4], listData.ToArray());
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception("方法标识为空！");
                                    }

                                    #endregion

                                    if (resultObj == null)
                                    {
                                        resultMs.Position = 0;
                                        resultMs.Write(new byte[] { 1 }, 0, 1);
                                        resultMs.Position = 1;
                                        resultMs.Write(BitConverter.GetBytes((long)0), 0, 8);
                                    }
                                    else if (resultObj is System.IO.Stream)
                                    {
                                        isSended = true;
                                        using (System.IO.Stream stream = resultObj as System.IO.Stream)
                                        {
                                            if (stream != null)
                                            {
                                                dataSock.Send(new byte[] { 2 }, SocketFlags.None);//3：流
                                                dataSock.Send(BitConverter.GetBytes(stream.Length), SocketFlags.None);

                                                byte[] tempBt = new byte[2048];
                                                if (stream.Length > 0)
                                                {
                                                    while (stream.Position < stream.Length)
                                                    {
                                                        int red = stream.Read(tempBt, 0, tempBt.Length);
                                                        dataSock.Send(tempBt, 0, red, SocketFlags.None);
                                                    }
                                                }
                                                stream.Close();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        resultMs.Position = 0;
                                        resultMs.Write(new byte[] { 0 }, 0, 1);
                                        resultMs.Position = 9;
                                        _bf.Serialize(resultMs, resultObj);
                                        resultMs.Position = 1;
                                        resultMs.Write(BitConverter.GetBytes(resultMs.Length - 9), 0, 8);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Exception extemp = ex is TargetInvocationException ? (ex as TargetInvocationException).InnerException : ex;
                                    resultMs.Position = 0;
                                    resultMs.Write(new byte[] { 0 }, 0, 1);
                                    resultMs.Position = 9;
                                    _bf.Serialize(resultMs, extemp);
                                    resultMs.Position = 1;
                                    resultMs.Write(BitConverter.GetBytes(resultMs.Length - 9), 0, 8);
                                }

                                if (!isSended)
                                    rstBt = resultMs.ToArray();
                            }

                            #endregion
                        }

                        if (rstBt != null)
                            dataSock.Send(rstBt, SocketFlags.None);

                        //释放接受到的资源
                        l = null;
                        ms.Dispose();
                        ms = new System.IO.MemoryStream();
                    }

                    #endregion
                }
                _t_Cmnc.Remove(o as object[]);
            }
            catch (ThreadAbortException)
            {

            }
            catch (Exception e)
            {
                PopErr(this, DateTime.Now, e);
            }
            finally
            {
                if (obS != null)
                    _t_Cmnc.Remove(obS);

                if (rmE != null)
                    PopMsg(this, DateTime.Now, rmE + " Disconnected.");
            }
        }

        #endregion

        private void PopMsg(object sender, DateTime time, string message)
        {
            if (sender != null)
                CM.WriteRunLog(sender.GetType().Name, time, message);

            if (OnMsg != null)
            {
                OnMsg(sender, time, message);
            }
        }

        private void PopErr(object sender, DateTime time, Exception e)
        {
            if (sender != null)
                CM.WriteErrLog(sender.GetType().Name, time, e);

            if (OnErr != null)
            {
                OnErr(sender, time, e);
            }
        }

        private void PopListenStatus(Server svr, DateTime time, SvrStatus st)
        {
            Status = st;

            if (ListenStatus != null)
            {
                ListenStatus(svr, time, st);
            }
        }

        private byte[] M_InteractiveData(Guid id, byte[] data)
        {
            if (null != InteractiveData)
            {
                return InteractiveData(id, data);
            }
            else
            {
                return null;
            }
        }
    }

    #endregion -Server End-
}
