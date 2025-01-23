using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.Messages
{
    public class Message : BaseModel<Guid>
    {
        public required int SenderUserId { get; set; }
        public required int ReceiverUserId { get; set; }
        public required string Content { get; set; }
        public DateTime? ReadAt { get; set; }
        public string? Reaction { get; set; }
    }
}
