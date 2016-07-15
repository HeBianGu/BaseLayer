﻿
namespace Anycmd.Engine.Edi.Messages
{
    using Commands;
    using Engine.Messages;
    using System;

    public sealed class RemoveOntologyCatalogCommand : Command, IAnycmdCommand
    {
        public RemoveOntologyCatalogCommand(IAcSession acSession, Guid ontologyId, Guid catalogId)
        {
            this.AcSession = acSession;
            this.OntologyId = ontologyId;
            this.CatalogId = catalogId;
        }

        public IAcSession AcSession { get; private set; }

        public Guid OntologyId { get; private set; }

        public Guid CatalogId { get; private set; }
    }
}
