using MediatR;
using SproomInbox.Domain.Commands;
using SproomInbox.Domain.Commands.Interfaces;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Enum;
using SproomInbox.Domain.Repositories;
using SproomInbox.Domain.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SproomInbox.Domain.Handlers
{
    public class AproveDocumentCommandHandler : IRequestHandler<AproveDocumentCommand, Document>  
    {
        private readonly IDocumentRepository _documentRepo;
        private readonly IDocumentStateRepository _documentStateRepo;
        private readonly IEmailService _emailService;
        

        private GenericCommandResult result;

        public AproveDocumentCommandHandler(IDocumentRepository documentRepo, IDocumentStateRepository documentStateRepo, IEmailService emailService)
        {
            _documentRepo = documentRepo;
            _documentStateRepo = documentStateRepo;
            _emailService = emailService;
        }
        public Task<Document> Handle(AproveDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _documentRepo.GetById(request.Document);

            if(document == null || document.State == eState.Approved) return Task.FromResult(document);

            document.State = eState.Approved;

            _documentRepo.Update(document);
            _documentRepo.SaveChanges();

            _documentStateRepo.Add(new DocumentState(Guid.NewGuid(), DateTime.Now, document.State, document));
            _documentStateRepo.SaveChanges();

            _emailService.SendEmailAsync("test@test.com", document.FileReference);

            return Task.FromResult(document);
        }
    }
}
