using MediatR;
using SproomInbox.Domain.Commands.Interfaces;
using SproomInbox.Domain.Entities;
using System;


namespace SproomInbox.Domain.Commands
{
    public  class RejectDocumentCommand : ICommand, IRequest<Document>
    {
        public RejectDocumentCommand(Guid document)
        {
            Document = document;
        }

        public Guid Document { get; }

        public void Validate()
        {
            
        }
    }
}
