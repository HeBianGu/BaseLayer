using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace HebianGu.ComLibModule.NetWork
{

    /// <summary> 网卡引擎 </summary>
    partial class NetWorkEngine
    {
        #region - Start 单例模式 -

        /// <summary> 单例模式 </summary>
        private static NetWorkEngine t = null;

        /// <summary> 多线程锁 </summary>
        private static object localLock = new object();

        /// <summary> 创建指定对象的单例实例 </summary>
        public static NetWorkEngine Instance
        {
            get
            {
                if (t == null)
                {
                    lock (localLock)
                    {
                        if (t == null)
                            return t = new NetWorkEngine();
                    }
                }
                return t;
            }
        }
        /// <summary> 禁止外部实例 </summary>
        private NetWorkEngine()
        {

        }
        #endregion - 单例模式 End -

    }

    partial class NetWorkEngine
    {
        /// <summary> 获取所有网卡 </summary>
        public NetworkInterface[] GetAllNetwork()
        {
            return NetworkInterface.GetAllNetworkInterfaces();
        }

        /// <summary> 获取网卡 </summary>
        public NetworkInterface GetNetwork(Predicate<NetworkInterface> match)
        {
            NetworkInterface[] ns = NetworkInterface.GetAllNetworkInterfaces();

            return ns.ToList().Find(l => match(l));
        }

        /// <summary> 获取以太网卡 </summary>
        public NetworkInterface GetNetwork()
        {
            return NetWorkEngine.Instance.GetNetwork(l => l.NetworkInterfaceType == NetworkInterfaceType.Ethernet);
        }

        /// <summary> 获取以太网卡 </summary>
        public NetworkInterface GetDefaltNetwork()
        {
            NetworkInterface[] ns = NetworkInterface.GetAllNetworkInterfaces();

            if (ns != null && ns.Length > 0) return ns[0];

            return null;
        }

        /// <summary> 启动 </summary>
        public void Start()
        {
            string oldBytesSentValue = string.Empty;
            string oldBytesReceivedValue = string.Empty;

            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += (object sender, ElapsedEventArgs e) =>
                {
                    foreach (var nic in _obsolute)
                    {
                        IPv4InterfaceStatistics interfaceStats = nic.Key.GetIPv4Statistics();

                        // Todo ：初始化 
                        if (string.IsNullOrEmpty(oldBytesSentValue))
                        {
                            oldBytesSentValue = interfaceStats.BytesSent.ToString();
                            oldBytesReceivedValue = interfaceStats.BytesReceived.ToString();
                        }

                        // Todo ：更新数据 
                        int bytesSentSpeed = (int)(interfaceStats.BytesSent - double.Parse(oldBytesSentValue)) / 1024;
                        int bytesReceivedSpeed = (int)(interfaceStats.BytesReceived - double.Parse(oldBytesReceivedValue)) / 1024;
                        StringBuilder sb = new StringBuilder();

                        sb.AppendLine("速度：" + nic.Key.Speed.ToString());
                        sb.AppendLine("总上传字节：" + interfaceStats.BytesReceived.ToString());
                        sb.AppendLine("总下载字节：" + interfaceStats.BytesSent.ToString());
                        sb.AppendLine("上传字节/秒：" + bytesSentSpeed.ToString() + " KB/s");
                        sb.AppendLine("下载字节/秒：" + bytesReceivedSpeed.ToString() + " KB/s");
                        sb.AppendLine("网卡类型：" + nic.Key.NetworkInterfaceType.ToString());
                        sb.AppendLine("速度：" + nic.Key.Speed.ToString());

                        oldBytesSentValue = interfaceStats.BytesSent.ToString();
                        oldBytesReceivedValue = interfaceStats.BytesReceived.ToString();

                        // Todo ：触发任务 
                        nic.Value(sb.ToString());
                    }
                };
            timer.Start();
        }


        /// <summary> 停止 </summary>
        public void Stop()
        {
            timer.Stop();
        }

        Timer timer;

        Dictionary<NetworkInterface, Action<string>> _obsolute = new Dictionary<NetworkInterface, Action<string>>();

        /// <summary> 注册获取网卡信息 </summary>
        public void Register(NetworkInterface nic, Action<string> act)
        {
            _obsolute.Add(nic, act);


        }


    }
}
