using SproomInbox.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproomInbox.Domain.Entities
{
    [Serializable]
    [Table("DocumentState")]
    public class DocumentState : EntityGuid
    {
        public DocumentState(Guid id, DateTime timeStamp, eState state, Document document)
        {
            Id = id;
            TimeStamp = timeStamp;
            State = state;
            Document = document;
        }

        public DocumentState()
        {
        }

        [Required]
        public DateTime TimeStamp { get; set; }
        [Required]
        public eState State { get; set; }
        [Required]
        public Document Document { get; set; }
    }
}
