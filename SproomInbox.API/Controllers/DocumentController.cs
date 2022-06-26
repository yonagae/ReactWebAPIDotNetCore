using MediatR;
using Microsoft.AspNetCore.Mvc;
using SproomInbox.Data.Converter.Implementations;
using SproomInbox.Data.DTO;
using SproomInbox.Domain.Commands;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Repositories;

namespace SproomInbox.API
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentRepository _documentRepo;
        private readonly IDocumentStateRepository _documentStateRepo;
        private readonly DocumentConverter _converter;

        public DocumentController(IMediator mediator, 
            ILogger<DocumentController> logger, 
            IDocumentRepository documentRepo, 
            IDocumentStateRepository documentStateRepository)
        {
            this._mediator = mediator;
            _logger = logger;
            _documentRepo = documentRepo;
            _documentStateRepo = documentStateRepository;
            _converter = new DocumentConverter();
        }

        /// <summary>
        /// Get a list of documents
        /// </summary>
        /// <returns>A list of documents</returns>
        [HttpGet]
        public Task<List<DocumentDTO>> GetDocuments()
        {
            return Task.FromResult(
                _converter.Parse(_documentRepo.GetAll().ToList())
                );
        }

        /// <summary>
        /// Reject Document and create a new Document State to record the changes
        /// </summary>
        /// <param name="id">id of the document</param>
        /// <returns>the rejected document</returns>
        [HttpPut]
        [Route("Reject/{id:Guid}")]
        public async Task<DocumentDTO> RejectDocument(Guid id)
        {
            var response = await _mediator.Send(new RejectDocumentCommand(id));
            return await Task.FromResult(_converter.Parse(response));
        }

        /// <summary>
        /// Approve Document and create a new Document State to record the changes
        /// </summary>
        /// <param name="id">id of the document</param>
        /// <returns>the approved document</returns>
        [HttpPut]
        [Route("Approve/{id:Guid}")]
        public async Task<DocumentDTO> ApproveDocument(Guid id)
        {
            var response = await _mediator.Send(new AproveDocumentCommand(id));
            return await Task.FromResult(_converter.Parse(response));
        }
    }
}