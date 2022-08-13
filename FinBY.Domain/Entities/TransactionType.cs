using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public class TransactionType : Entity
    {
        public TransactionType(string name)
        {
            Name = name;
        }

        public TransactionType() { }

        public string Name { get; private set; }
    }
}
