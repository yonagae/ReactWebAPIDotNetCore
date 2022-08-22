using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Data.PagedResult
{
    public class PagedTransactionParams
    {
        public const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public int? UserId { get; set; } = null;
        public int? TransactionType { get; set; } = null;
        public DateTime? DataBegin { get; set; } = null;
        public DateTime? DataEnd { get; set; } = null;
    }
}
