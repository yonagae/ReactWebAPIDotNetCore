using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public class Entity : IEntity<int>
    {
        public Entity()
        {
        }

        public int Id { get;  set; }
    }
}
