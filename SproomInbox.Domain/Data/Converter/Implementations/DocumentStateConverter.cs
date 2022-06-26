using SproomInbox.Data.Converter.Contract;
using SproomInbox.Data.DTO;
using SproomInbox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using SproomInbox.Domain.Enum;

namespace SproomInbox.Data.Converter.Implementations
{
    public class DocumentStateConverter : IParser<DocumentStateDTO, DocumentState>, IParser<DocumentState, DocumentStateDTO>
    {
        public DocumentState Parse(DocumentStateDTO origin)
        {
            if (origin == null) return null;

            eState eState;
            if (!Enum.TryParse(origin.State, out eState)) return null;

            return new DocumentState(origin.Id, origin.TimeStamp, eState, null);
        }

        public DocumentStateDTO Parse(DocumentState origin)
        {
            if (origin == null) return null;
            return new DocumentStateDTO
            {
                Id = origin.Id,
                TimeStamp = origin.TimeStamp,
                State = origin.State.ToString(),
            };
        }

        public List<DocumentState> Parse(List<DocumentStateDTO> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }

        public List<DocumentStateDTO> Parse(List<DocumentState> origin)
        {
            if (origin == null) return null;
            return origin.Select(item => Parse(item)).ToList();
        }
    }
}
