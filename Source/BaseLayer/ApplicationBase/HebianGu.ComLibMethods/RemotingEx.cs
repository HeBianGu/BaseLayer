using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibMethods.RemotingEx
{
    #region -Start Client-
    public delegate void SendMsgHandle(DateTime time, string type, object o);

    public delegate void SendSttHandle(DateTime time, string type, string key, object o);

    /// <summary> Remort client view </summary>
    public interface IClientView
    {
        void RcvMsg(DateTime time, string type, object o);

        void RcvStt(DateTime time, string type, string key, object o);
    }

    public partial class Client : MarshalByRefObject
    {
        public IClientView _vi = null;

        public Client(IClientView vi)
        {
            _vi = vi;
            ID = Guid.NewGuid().ToString();
        }

        public string ID
        {
            set;
            get;
        }

        public string LocalInfo
        {
            set;
            get;
        }

        public object BuildInterface(Type t, string name, params object[] args)
        {
            RemotingConfiguration.RegisterWellKnownClientType(
                t,
                __add + "/" + name);
            return Activator.CreateInstance(t, args);
        }

        public void SendMsg(DateTime time, string type, object o)
        {
            try
            {
                if (_vi != null)
                {
                    new SendMsgHandle(_vi.RcvMsg).BeginInvoke(time, type, o, null, null);
                    //_vi.RcvMsg(time, type, o);
                }
            }
            catch (Exception e)
            {
                LogMethod.WriteErrLog("IClientView", DateTime.Now, e);
            }
        }

        public void SendStt(DateTime time, string type, string key, object o)
        {
            try
            {
                if (_vi != null)
                {
                    new SendSttHandle(_vi.RcvStt).BeginInvoke(time, type, key, o, null, null);
                    //_vi.RcvStt(time, type, key, o);
                }
            }
            catch (Exception e)
            {
                LogMethod.WriteErrLog("IClientView", DateTime.Now, e);
            }
        }
    }

    public partial class Client
    {
        //private static bool _isFirst = true;

        static List<string> _isFirst = new List<string>();

        private Server __svr = null;

        private string __add = null;

        private string __cnn = null;

        /// <summary>  </summary>
        /// <param name="vi"></param>
        /// <param name="address">example: "127.0.0.1:13583/PLDC_MntBll"</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static Client BuildConnect(IClientView vi, string ip, string port, string channelName)
        {
            Client localClient = new Client(vi);  // 创建本地对象
            localClient.__cnn = channelName;

            var isFirstSign = ip + port + channelName;

            localClient.__add = "tcp://" + ip + ":" + port + "/" + channelName;

            if (_isFirst.Count == 0)
            {
                // 注册通道
                IChannel chnl = new TcpChannel(0);
                ChannelServices.RegisterChannel(chnl, false);

                server__ =
                    //Activator.GetObject(typeof(Server), localClient.__add + "/ServerActivated")
                    Activator.GetObject(typeof(Server), "tcp://" + ip + ":" + port + "/ServerActivated")
                    as Server;
            }

            if (!_isFirst.Contains(isFirstSign))
                _isFirst.Add(isFirstSign);

            //if(!_isFirst.Contains(isFirstSign))
            //{
            //    // 注册类型
            //    Type t = typeof(Server);
            //    RemotingConfiguration.RegisterWellKnownClientType(
            //        t,
            //        localClient.__add + "/ServerActivated");

            //    _isFirst.Add(isFirstSign);
            //}

            //localClient.__svr = new Server();   // 创建远程对象

            localClient.__svr = server__;

            localClient.__svr.Regiser(localClient, channelName);

            return localClient;
        } static Server server__ = null;

        public void FreeConnect()
        {
            __svr.Logout(this, this.__cnn);
        }
    }
    #endregion -Client End-

    #region -Start Server-
    public partial class Server : MarshalByRefObject, IDisposable
    {
        static Dictionary<string, Server> _instances = new Dictionary<string, Server>();

        IChannel tcpChnl = null;

        public Server()
        {
            ChannelName = Process.GetCurrentProcess().ProcessName;

            var port = System.Configuration.ConfigurationSettings.AppSettings["RMTPort"];
            Port = string.IsNullOrEmpty(port) ? 13587 : int.Parse(port);
        }

        ~Server()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (string.IsNullOrEmpty(this.ChannelName))
                return;

            if (_instances.ContainsKey(this.ChannelName))
            {
                _instances.Remove(this.ChannelName);
                try
                {
                    if (tcpChnl != null)
                        ChannelServices.UnregisterChannel(tcpChnl);
                }
                catch { }
            }
        }

        public string ChannelName
        {
            set;
            get;
        }

        /// <summary> 默认会读取配置文件的AppSettings.[@key='RMTPort']节点，如果结点不存在默认为“"13587” </summary>
        public int Port
        {
            set;
            get;
        }

        public void Listen()
        {
            try
            {
                if (string.IsNullOrEmpty(ChannelName))
                    throw new Exception("Server.Listen 需要设置通道名称。");

                if (_instances.ContainsKey(ChannelName))
                    throw new Exception("Server.Listen 无法监听相同的通道名称。(" + ChannelName + ")");

                // 设置Remoting应用程序名

                if (_isFirstListen)
                {
                    RemotingConfiguration.ApplicationName //= "PLDC_MntBll";
                        //= Process.GetCurrentProcess().ProcessName;
                        = ChannelName;

                    // 设置formatter
                    BinaryServerFormatterSinkProvider formatter;
                    formatter = new BinaryServerFormatterSinkProvider();
                    formatter.TypeFilterLevel = TypeFilterLevel.Full;

                    // 设置通道名称和端口
                    IDictionary propertyDic = new Hashtable();
                    propertyDic["name"] = ChannelName;
                    propertyDic["port"] = Port;

                    // 注册通道
                    tcpChnl = new TcpChannel(propertyDic, null, formatter);
                    ChannelServices.RegisterChannel(tcpChnl, false);

                    // 注册类型
                    Type t = typeof(Server);
                    RemotingConfiguration.RegisterWellKnownServiceType(
                        t, "ServerActivated", WellKnownObjectMode.Singleton);

                    _isFirstListen = false;
                }

                _instances[ChannelName] = this;
            }
            catch (Exception e)
            {
                LogMethod.WriteErrLog(e);
            }
        } static bool _isFirstListen = true;

        public void Regiser(Client c, string channelName)
        {
            _instances[channelName].__cm.AddCLT(c);
            //__cm.AddCLT(c);
        }

        public void Logout(Client c, string channelName)
        {
            _instances[channelName].__cm.RmvCLT(c.ID);
            //__cm.RmvCLT(c.ID);
        }

        /// <summary> 公布接口 </summary>
        public void PublishInterface(Type t, string name)
        {
            // 注册类型
            RemotingConfiguration.RegisterWellKnownServiceType(
                t, name, WellKnownObjectMode.Singleton);
        }
    }

    public partial class Server
    {
        private const string MSG_SIGN = "__WIN_SVR__";

        /// <summary> 客户端管理器 声明 </summary>
        private readonly CltManage __cm = new CltManage();

        public void SendMsg(DateTime time, string type, object vlu)
        {
            try
            {
                __cm.SendMsg(time, type, vlu);
            }
            catch (Exception e)
            {
                LogMethod.WriteErrLog(MSG_SIGN, DateTime.Now, e);
            }
        }

        public void SendStt(DateTime time, string type, string key, object vlu)
        {
            try
            {
                __cm.SendStt(time, type, key, vlu);
            }
            catch (Exception e)
            {
                LogMethod.WriteErrLog(MSG_SIGN, DateTime.Now, e);
            }
        }

        /// <summary> 客户端管理器 定义 </summary>
        internal class CltManage
        {
            /// <summary> 消息缓存 </summary>
            private Dictionary<string, List<MsgP>>
                _msgC = new Dictionary<string, List<MsgP>>();

            /// <summary> 状态缓存 </summary>
            private Dictionary<string, Dictionary<string, SttP>>
                _sttC = new Dictionary<string, Dictionary<string, SttP>>();

            /// <summary> 客户端列表 </summary>
            private Dictionary<string, CltShell>
                _cltL = new Dictionary<string, CltShell>();

            /// <summary> 添加（注册）客户端 </summary>
            public void AddCLT(Client clt)
            {
                CltShell clt_ = null;
                try
                {
                    clt_ = new CltShell() { ID = clt.ID, CltInfo = clt.LocalInfo, Clt = clt };
                }
                catch (Exception e)
                {
                    LogMethod.WriteErrLog(MSG_SIGN, DateTime.Now, e);
                    LogMethod.WriteRunLog(MSG_SIGN, DateTime.Now, "客户端注册失败。" + e.Message);
                    return;
                }

                lock (_cltL)
                {
                    if (!_cltL.ContainsKey(clt.ID))
                    {
                        lock (_msgC)
                        {
                            foreach (var d in _msgC)
                            {
                                foreach (var l in d.Value)
                                {
                                    try
                                    {
                                        clt.SendMsg(l.Tim, l.Typ, l.Vlu);
                                    }
                                    catch (Exception e)
                                    {
                                        LogMethod.WriteErrLog(MSG_SIGN, DateTime.Now, e);
                                        LogMethod.WriteRunLog(MSG_SIGN, DateTime.Now, "客户端" + clt_.CltInfo + "注册失败。" + e.Message);
                                        return;
                                    }
                                }
                            }
                        }

                        lock (_sttC)
                        {
                            foreach (var t in _sttC)
                            {
                                foreach (var k in t.Value)
                                {
                                    try
                                    {
                                        clt.SendStt(k.Value.Tim, k.Value.Typ, k.Value.Key, k.Value.Vlu);
                                    }
                                    catch (Exception e)
                                    {
                                        LogMethod.WriteErrLog(MSG_SIGN, DateTime.Now, e);
                                        LogMethod.WriteRunLog(MSG_SIGN, DateTime.Now, "客户端" + clt_.CltInfo + "注册失败。" + e.Message);
                                        return;
                                    }
                                }
                            }
                        }

                        _cltL.Add(clt_.ID, clt_);

                        LogMethod.WriteRunLog(MSG_SIGN, DateTime.Now, "Add client " + clt_.CltInfo + ".");
                    }
                }
            }

            /// <summary> 移除（注销）客户端  </summary>
            public void RmvCLT(string id)
            {
                lock (_cltL)
                {
                    if (_cltL.ContainsKey(id))
                    {
                        var temp = _cltL[id];

                        _cltL.Remove(id);

                        LogMethod.WriteRunLog(MSG_SIGN, DateTime.Now, "Rmv client " + temp.CltInfo + ".");
                    }
                }
            }

            /// <summary> 发送消息 </summary>
            public void SendMsg(DateTime time, string type, object vlu)
            {
                var msg_ = new MsgP() { Tim = time, Typ = type, Vlu = vlu };

                #region - 添加一类 消息 -

                lock (_msgC)
                {
                    if (!_msgC.ContainsKey(type))
                    {
                        _msgC[type] = new List<MsgP>(500);
                    }
                }

                #endregion

                #region - 维护 消息列表 -

                var msl_ = _msgC[type];
                lock (msl_)
                {
                    if (msl_.Count == 500)
                        msl_.RemoveAt(0);
                    msl_.Add(msg_);
                }

                #endregion

                #region - 获得 客户列表 -

                CltShell[] cls_ = null;
                lock (_cltL)
                {
                    if (_cltL.Count > 0)
                    {
                        cls_ = new CltShell[_cltL.Count];
                        _cltL.Values.CopyTo(cls_, 0);
                    }
                }

                #endregion

                #region - 发送 消息     -

                if (cls_ != null && cls_.Length > 0)
                {
                    for (int i = 0; i < cls_.Length; i++)
                    {
                        try
                        {
                            cls_[i].Clt.SendMsg(time, type, vlu);
                            cls_[i] = null;
                        }
                        catch (Exception e)
                        {
                            LogMethod.WriteErrLog(e);
                            LogMethod.WriteRunLog(MSG_SIGN, DateTime.Now, "Invoke client err - " + cls_[i].CltInfo + ". " + e.Message);
                        }
                    }
                }

                #endregion

                #region - 移除 无效客户 -

                if (cls_ != null && cls_.Length > 0)
                {
                    for (int i = 0; i < cls_.Length; i++)
                    {
                        if (cls_[i] != null)
                        {
                            RmvCLT(cls_[i].ID);
                        }
                    }
                }

                #endregion
            }

            /// <summary> 发送状态 </summary>
            public void SendStt(DateTime time, string type, string key, object vlu)
            {
                var stt_ = new SttP() { Tim = time, Typ = type, Key = key, Vlu = vlu };

                #region - 添加一类 状态 -

                lock (_sttC)
                {
                    if (!_sttC.ContainsKey(type))
                    {
                        _sttC[type] = new Dictionary<string, SttP>();
                    }
                }

                #endregion

                #region - 添加一个 状态 -

                lock (_sttC[type])
                {
                    _sttC[type][key] = stt_;
                }

                #endregion

                #region - 获得 客户列表 -

                CltShell[] cls_ = null;
                lock (_cltL)
                {
                    if (_cltL.Count > 0)
                    {
                        cls_ = new CltShell[_cltL.Count];
                        _cltL.Values.CopyTo(cls_, 0);
                    }
                }

                #endregion

                #region - 发送 状态     -

                if (cls_ != null && cls_.Length > 0)
                {
                    for (int i = 0; i < cls_.Length; i++)
                    {
                        try
                        {
                            cls_[i].Clt.SendStt(time, type, key, vlu);
                            cls_[i] = null;
                        }
                        catch (Exception e)
                        {
                            LogMethod.WriteErrLog(e);
                            LogMethod.WriteRunLog(MSG_SIGN, DateTime.Now, "Invoke client err - " + cls_[i].CltInfo + ". " + e.Message);
                        }
                    }
                }

                #endregion

                #region - 移除 无效客户 -

                if (cls_ != null && cls_.Length > 0)
                {
                    for (int i = 0; i < cls_.Length; i++)
                    {
                        if (cls_[i] != null)
                        {
                            RmvCLT(cls_[i].ID);
                        }
                    }
                }

                #endregion
            }

            internal class MsgP
            {
                public DateTime Tim
                {
                    set;
                    get;
                }

                public string Typ
                {
                    set;
                    get;
                }

                public object Vlu
                {
                    set;
                    get;
                }
            }

            internal class SttP : MsgP
            {
                public string Key
                {
                    set;
                    get;
                }
            }
        }

        /// <summary> 客户端外壳 </summary>
        internal class CltShell
        {
            public string ID
            {
                set;
                get;
            }

            public string CltInfo
            {
                set;
                get;
            }

            public Client Clt
            {
                set;
                get;
            }
        }

        internal class _msgParams
        {
            public DateTime Time
            {
                set;
                get;
            }

            public string Key
            {
                set;
                get;
            }

            public string Message
            {
                set;
                get;
            }
        }
    }
    #endregion-Server End-
}
