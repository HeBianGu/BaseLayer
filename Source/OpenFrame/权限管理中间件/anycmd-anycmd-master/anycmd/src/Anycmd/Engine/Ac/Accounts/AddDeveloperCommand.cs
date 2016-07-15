﻿
namespace Anycmd.Engine.Ac.Accounts
{
    using Commands;
    using Messages;
    using System;

    public sealed class AddDeveloperCommand : Command, IAnycmdCommand
    {
        public AddDeveloperCommand(IAcSession acSession, Guid accountId)
        {
            this.AcSession = acSession;
            this.AccountId = accountId;
        }

        public IAcSession AcSession { get; private set; }
        public Guid AccountId { get; private set; }
    }
}
