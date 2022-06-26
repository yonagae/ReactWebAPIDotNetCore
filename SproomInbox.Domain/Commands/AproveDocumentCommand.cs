using MediatR;
using SproomInbox.Domain.Commands.Interfaces;
using SproomInbox.Domain.Entities;
using System;


namespace SproomInbox.Domain.Commands
{
    public  class AproveDocumentCommand : ICommand, IRequest<Document>
    {
        public AproveDocumentCommand(Guid document)
        {
            Document = document;
        }

        public Guid Document { get; }

        public void Validate()
        {
            
        }
    }
}
