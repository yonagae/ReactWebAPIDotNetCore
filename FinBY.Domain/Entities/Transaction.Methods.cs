﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public partial class Transaction : Entity
    {
        public bool AddTransactionAmount(TransactionAmount transactionAmount)
        {
            if (transactionAmount == null) return false;

            _transactionAmounts.Add(transactionAmount);
            TotalAmount += transactionAmount.Amount;

            return true;
        }
    }
}
