using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OPT.PCOCCenter.TaskProxy
{
    public class TaskEvent
    {
        /// <summary>
        /// 任务失败事件
        /// </summary>
        public const Int32 TaskFailed = 0;

        /// <summary>
        /// 任务完成事件
        /// </summary>
        public const Int32 TaskCompeleted = 1;

        /// <summary>
        /// 任务计算中事件
        /// </summary>
        public const Int32 TaskRunning = 2;

        public class TaskEventArgs : EventArgs
        {
            private Int32  msgID;
            private Object msgData;

            public TaskEventArgs(Int32 messageID, Object messageData)
            {
                msgID   = messageID;
                msgData = messageData;
            }

            public Int32 MessageID
            {
                get { return msgID; }
                set { msgID = value; }
            }

            public Object MessageData
            {
                get { return msgData; }
                set { msgData = value; }
            }
        }

        public EventHandler<TaskEventArgs> TaskEventHandler = null;

        public void doEvent(Int32 val, Object data)
        {
            // Copy to a temporary variable to be thread-safe.
            EventHandler<TaskEventArgs>TaskEvent = TaskEventHandler;
            if (TaskEvent != null)TaskEvent(this, new TaskEventArgs(val, data));
        }
    }
}
