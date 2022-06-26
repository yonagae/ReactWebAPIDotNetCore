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
    [Table("Document")]
    public class Document : EntityGuid
    {
        public Document(Guid id, string fileReference, eDocumentType documentType, eState state, DateTime dateTime)
        {
            Id = id;
            FileReference = fileReference;
            DocumentType = documentType;
            State = state;
            DateTime = dateTime;
        }

        public Document(string fileReference, eDocumentType documentType, eState state, DateTime dateTime)
        {
            FileReference = fileReference;
            DocumentType = documentType;
            State = state;
            DateTime = dateTime;
        }

        [Required]
        [StringLength(100)]
        public String FileReference { get;  set; }

        [Required]
        public eDocumentType DocumentType { get; set; }
        
        [Required]
        public eState State { get; set; }

        [Required]
        public DateTime DateTime { get; set; }
    }
}
