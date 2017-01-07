#region <版 本 注 释>
/*
 * ========================================================================
 * Copyright(c) 北京奥伯特石油科技有限公司, All Rights Reserved.
 * ========================================================================
 *    
 * 作者：[李海军]   时间：2017/1/6 16:25:37
 * 文件名：SocketEngineHelper
 * 说明：
 * 
 * 
 * 修改者：           时间：               
 * 修改说明：
 * ========================================================================
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HebianGu.ComLibModule.SocketHelper
{
    /// <summary>
    /// 一个最基础的类,服务器和客户端共同要用到的一些方法和事件
    /// </summary>
    public class SocketEngineService
    {
        /// <summary>注册服务器,返回一个ITxServer类,再从ITxServer中的startServer一个方法启动服务器</summary>
        public static IEngineServer CreateServer(int port)
        {
            IEngineServer server = new SocketServerEngine(port);
            return (server);

        }

        /// <summary> 注册客户端,返回一个ITxServer类,再从ITxClient中的startClient一个方法启动客户端 </summary>
        public static IEngineClient CreateClient(string ip, int port)
        {
            IEngineClient client = new SocketClientEngine(ip, port);
            return (client);
        }
        /// <summary> 注册Udp服务端；端口在Port属性设置，默认为随机；具体到StartEngine启动</summary>
        public static IUdpEngine CreateUdp()
        {
            IUdpEngine udp = new UdpEngine();
            return (udp);
        }
    }
}
