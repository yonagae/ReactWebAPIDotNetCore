using SproomInbox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.Domain.Repositories
{
    public interface IDocumentStateRepository : IRepository<DocumentState, Guid>
    {
        public List<DocumentState> GetDocumentStatesByDocumentId(Guid documentID);
    }
}
