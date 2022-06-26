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
    public class DocumentRepository : Repository<Document, Guid>, IDocumentRepository
    {
        public DocumentRepository(DbContext context)
           : base(context)
        {
        }
    }
}
