using System;

namespace SproomInbox.Data.DTO
{
    public class DocumentStateDTO
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string State { get; set; }
    }
}
