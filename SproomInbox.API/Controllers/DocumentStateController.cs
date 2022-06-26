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
       /// get the document states (change history) of a document
       /// </summary>
       /// <param name="id">the id of the document</param>
       /// <returns>list of document states</returns>
        [HttpGet]
        [Route("Index/{id:Guid}")]
        public async Task<List<DocumentStateDTO>> GetDocumentStatesByDocumentId(Guid id)
        {
            return await Task.FromResult(
                _converter.Parse(_documentRepo.GetDocumentStatesByDocumentId(id))
                );
        }
    }
}