using MediatR;
using SproomInbox.Domain.Commands;
using SproomInbox.Domain.Commands.Interfaces;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SproomInbox.Domain.Handlers
{
    public class RejectDocumentCommandHandler : IRequestHandler<RejectDocumentCommand, Document>  
    {
        private readonly IUserRepository _userRepo;
        private readonly IDocumentRepository _documentRepo;
        private readonly IDocumentStateRepository _documentStateRepo;

        private GenericCommandResult result;

        public RejectDocumentCommandHandler(IUserRepository userRepo, IDocumentRepository documentRepo, IDocumentStateRepository documentStateRepo)
        {
            _userRepo = userRepo;
            _documentRepo = documentRepo;
            _documentStateRepo = documentStateRepo;
        }

        public Task<Document> Handle(RejectDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = _documentRepo.GetById(request.Document);

            if (document == null) return Task.FromResult(document);

            document.State = Enum.eState.Rejected;

            _documentRepo.Update(document);
            _documentRepo.SaveChanges();

            _documentStateRepo.Add(new DocumentState(Guid.NewGuid(), DateTime.Now, document.State, document));
            _documentStateRepo.SaveChanges();

            return Task.FromResult(document);
        }
    }
}
