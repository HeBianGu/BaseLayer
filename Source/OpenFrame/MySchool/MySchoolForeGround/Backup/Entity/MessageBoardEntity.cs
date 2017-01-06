using System;
using System.Collections.Generic;
using System.Text;

namespace Entity
{
    public class MessageBoardEntity
    {
        string LoginName;

        public string LoginName1
        {
            get { return LoginName; }
            set { LoginName = value; }
        }
        string MessageContent;

        public string MessageContent1
        {
            get { return MessageContent; }
            set { MessageContent = value; }
        }
        string leaveMessageTime;

        public string LeaveMessageTime
        {
            get { return leaveMessageTime; }
            set { leaveMessageTime = value; }
        }
    }
}
