﻿using FinBY.Domain.Data;
using FinBY.Domain.Data.PagedResult;
using FinBY.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Repositories
{
    public interface ITransactionRepository : IRepository<Transaction, int>
    {
        public Task<List<Transaction>> GetAllWithDetailsAsListAsync();

        public Task<PagedResult<Transaction>> GetAllWithDetailsAsPagedResultAsync(PagedTransactionParams transsactionParams);
    }
}
