using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SproomInbox.Domain.Entities
{
    [Serializable]
    [Table("User")]
    public class User : EntityGuid
    {
        public User(string name)
        {
            Name = name;
        }

        [Required]
        [StringLength(50)]
        public String Name { get; private set; }
    }
}
