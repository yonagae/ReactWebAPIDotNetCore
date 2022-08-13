using Microsoft.EntityFrameworkCore;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using FinBY.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Infra.Repository
{
    public class UserRepository : Repository<User, int>, IUserRepository
    {
        public UserRepository(DbContext context)
           : base(context)
        {
        }
    }
}
