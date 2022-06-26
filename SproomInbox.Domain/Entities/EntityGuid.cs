using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.Domain.Entities
{
    public abstract class EntityGuid : IEntity<Guid>
    {
        public EntityGuid()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        public Guid Id { get; set; }
    }
}
