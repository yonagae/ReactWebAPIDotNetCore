using FinBY.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{

    public interface IUserRepository : IRepository<User, int>
    {
    }
}
