using System;

namespace SproomInbox.Data.DTO
{
    public class DocumentDTO
    {
        public Guid Id { get; set; }
        public String FileReference { get; set; }
        public string DocumentType { get; set; }
        public string State { get; set; }
        public DateTime DateTime { get; set; }
    }

}
