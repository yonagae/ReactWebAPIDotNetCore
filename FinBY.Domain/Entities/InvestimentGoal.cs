using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public class InvestimentGoal : Entity
    {
        public string Name { get; set; }
        public DateTime DeadLine { get; set; }
        public decimal GoalAmount { get; set; }
        public bool Active { get; set; }
    }
}
