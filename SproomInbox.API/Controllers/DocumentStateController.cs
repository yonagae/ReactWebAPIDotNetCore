using Microsoft.AspNetCore.Mvc;
using SproomInbox.Data.Converter.Implementations;
using SproomInbox.Data.DTO;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Repositories;

namespace SproomInbox.API
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentStateController : ControllerBase
    {
        private readonly ILogger<DocumentController> _logger;
        private readonly IDocumentStateRepository _documentRepo;
        private readonly DocumentStateConverter _converter;

        public DocumentStateController(ILogger<DocumentController> logger, IDocumentStateRepository documentRepo)
        {
            _logger = logger;
            _documentRepo = documentRepo;
            _converter = new DocumentStateConverter();
        }

        /// <summary>
        /// Get a list of documents
        /// </summary>
        /// <returns>A list of documents</returns>
        [HttpGet]
        [Route("Index/{id:Guid}")]
        public Task<List<DocumentStateDTO>> GetDocumentStatesByDocumentId(Guid id)
        {
            return Task.FromResult(
                _converter.Parse(_documentRepo.GetDocumentStatesByDocumentId(id))
                );
        }
    }
}