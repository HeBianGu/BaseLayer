using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.PCOCCenter.Client
{
    public class ClientEvent
    {
        /// <summary>
        /// 连接许可服务器失败事件
        /// </summary>
        public const Int32 LicenseFailed = 0;

        /// <summary>
        /// 连接许可服务器成功事件
        /// </summary>
        public const Int32 LicenseSucceed = 1;

        public class ClientEventArgs : EventArgs
        {
            private Int32 msg;

            public ClientEventArgs(Int32 messageData)
            {
                msg = messageData;
            }
            public Int32 Message
            {
                get { return msg; }
                set { msg = value; }
            }
        }

        public EventHandler<ClientEventArgs> ClientEventHandler = null;

        public void doEvent(Int32 val)
        {
            // Copy to a temporary variable to be thread-safe.
            EventHandler<ClientEventArgs> clientEvent = ClientEventHandler;
            if (clientEvent != null) clientEvent(this, new ClientEventArgs(val));
        }
    }
}
