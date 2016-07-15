﻿
namespace Anycmd.Engine.Edi.Messages
{
    using Abstractions;
    using Events;
    using InOuts;

    /// <summary>
    /// 
    /// </summary>
    public sealed class OntologyAddedEvent : DomainEvent
    {
        public OntologyAddedEvent(IAcSession acSession, OntologyBase source, IOntologyCreateIo output)
            : base(acSession, source)
        {
            if (output == null)
            {
                throw new System.ArgumentNullException("output");
            }
            this.Output = output;
        }

        internal OntologyAddedEvent(IAcSession acSession, OntologyBase source, IOntologyCreateIo input, bool isPrivate)
            : this(acSession, source, input)
        {
            this.IsPrivate = isPrivate;
        }

        public IOntologyCreateIo Output { get; private set; }
        internal bool IsPrivate { get; private set; }
    }
}
