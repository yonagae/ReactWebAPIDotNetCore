using SproomInbox.Data.Converter.Contract;
using SproomInbox.Data.DTO;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SproomInbox.Data.Converter.Implementations
{
    public class DocumentConverter : IParser<DocumentDTO, Document>, IParser<Document, DocumentDTO>
    {
        public Document Parse(DocumentDTO origin)
        {
            if (origin == null) return null;

            eDocumentType eDT;
            eState eState;

            if(!Enum.TryParse(origin.DocumentType, out eDT)) return null;
            if(!Enum.TryParse(origin.State, out eState)) return null;

            return new Document(origin.Id,  origin.FileReference,  eDT,  eState,  origin.DateTime);
        }

        public DocumentDTO Parse(Document origin)
        {
            if (origin == null) return null;
            return new DocumentDTO
            {
                Id = origin.Id,
                FileReference = origin.FileReference,
                DocumentType = origin.DocumentType.ToString(),
                State = origin.State.ToString(),
                DateTime = origin.DateTime
            };
        }

        public List<Document> Parse(List<DocumentDTO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<DocumentDTO> Parse(List<Document> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
