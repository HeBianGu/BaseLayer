﻿
namespace Anycmd.Engine.Edi.Messages
{
    using Commands;
    using Engine.Messages;

    // TODO:在界面上添加创建运行时本体元素的按钮
    public sealed class AddSystemElementCommand : Command, IAnycmdCommand
    {
        public AddSystemElementCommand(IAcSession acSession)
        {
            this.AcSession = acSession;
        }

        public IAcSession AcSession { get; private set; }
    }
}
