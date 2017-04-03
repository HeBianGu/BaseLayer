using HebianGu.ComLibModule.Wcf;
using HebianGu.ComLibModule.Wcf.Service.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HeBianGu.Product.RemoteMonitor.Client
{
    public partial class ExcuteClientService : WcfClientBase
    {
        public ExcuteClientService(string ip, string port)
        {
            this.IP = ip;
            this.Port = port;
        }
        /// <summary> 客户端的配置 </summary>
        public WcfConfiger WcfConfiger = WcfConfiger.Instance;

        private WSHttpBinding _wsHttpBinding;
        /// <summary> 说明 </summary>
        public WSHttpBinding WSHttpBinding
        {
            get
            {
                if (_wsHttpBinding == null)
                {
                    _wsHttpBinding = new WSHttpBinding();
                    _wsHttpBinding.MaxBufferPoolSize = int.MaxValue;
                    _wsHttpBinding.MaxReceivedMessageSize = int.MaxValue;
                    _wsHttpBinding.ReceiveTimeout = new TimeSpan(1, 0, 0);
                    _wsHttpBinding.Security = new System.ServiceModel.WSHttpSecurity();
                    _wsHttpBinding.Security.Mode = SecurityMode.None;
                }
                return _wsHttpBinding;
            }
        }


        /// <summary> 地址 </summary>
        public string ExcuteServiceAddress
        {
            get
            {
                return string.Format(WcfConfiger.RomoteFormat, this.IP, this.Port, "ExcuteService");
            }

        }

        /// <summary> 地址 </summary>
        public string ImageSenderAddress
        {
            get
            {
                return string.Format(WcfConfiger.RomoteFormat, this.IP, this.Port, "StreamPrivider");
            }

        }

        /// <summary> 运行Cmd命令 </summary>
        public void ExecuteCmd(string cmdString)
        {
            using (ChannelFactory<IExcuteService> channelFactory = new ChannelFactory<IExcuteService>(WSHttpBinding, ExcuteServiceAddress))
            {
                IExcuteService proxy = channelFactory.CreateChannel();

                proxy.ExecuteCmd(cmdString);
            }
        }

        /// <summary> 运行Cmd命令 </summary>
        public string ExecuteCmdOutPut(string cmdString)
        {
            using (ChannelFactory<IExcuteService> channelFactory = new ChannelFactory<IExcuteService>(WSHttpBinding, ExcuteServiceAddress))
            {
                IExcuteService proxy = channelFactory.CreateChannel();

                return proxy.ExecuteCmdOutPut(cmdString);
            }
        }

        /// <summary> 运行Cmd命令 </summary>
        public Bitmap GetPrintScreen()
        {
            using (ChannelFactory<IStreamPrivider> channelFactory = new ChannelFactory<IStreamPrivider>(WSHttpBinding, ImageSenderAddress))
            {
                IStreamPrivider proxy = channelFactory.CreateChannel();

                MemoryStream writeStream = new MemoryStream();

                proxy.PrintStreenToStream();

                byte[] buffer;

                //获取所用块压缩流，并组装
                while (proxy.ReadNextBuffer())
                {
                    // read bytes from input stream
                    buffer = proxy.GetCurrectBuffer();

                    // write bytes to output stream
                    writeStream.Write(buffer, 0, buffer.Length);
                }

                Bitmap bitmap = new Bitmap(writeStream);

                writeStream.Dispose();

                return bitmap;
            }
        }

        public List<string> GetDrivers()
        {
            return Directory.GetLogicalDrives().ToList();
        }
        public List<string> GetFolder(string parent)
        {
            try
            {
                return Directory.GetDirectories(parent).ToList();
            }
            catch
            {
                return new List<string>();
            }
          
        }

        public List<string> GetFiles(string folder)
        {
            return Directory.GetFiles(folder).ToList();
        }

    }
}
