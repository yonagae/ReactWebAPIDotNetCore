using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public class TransactionType : Entity
    {
        [StringLength(100)]
        [Required(ErrorMessage = "TransactionId must be specified")]
        public string Name { get; private set; }

        public TransactionType(string name)
        {
            Name = name;
        }
    }
}
