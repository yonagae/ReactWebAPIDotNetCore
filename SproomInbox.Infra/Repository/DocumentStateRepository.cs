using Microsoft.EntityFrameworkCore;
using SproomInbox.Domain.Entities;
using SproomInbox.Domain.Repositories;
using SproomInbox.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.Infra.Repository
{
    public class DocumentStateRepository : Repository<DocumentState, Guid>, IDocumentStateRepository
    {
        public DocumentStateRepository(ApplicationDbContext context)
           : base(context)
        {
  
        }

        public List<DocumentState> GetDocumentStatesByDocumentId(Guid documentID)
        {
            return this.GetAll().Where(x => x.Document.Id == documentID).ToList();
        }
    }
}
