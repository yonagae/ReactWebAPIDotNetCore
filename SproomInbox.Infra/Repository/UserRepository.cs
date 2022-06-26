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
    public class UserRepository : Repository<User, Guid>, IUserRepository
    {
        public UserRepository(DbContext context)
           : base(context)
        {
        }
    }
}
