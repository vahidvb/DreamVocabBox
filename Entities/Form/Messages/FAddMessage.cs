using Entities.Form.MessageAttachments;
using System.Text.Json.Serialization;

namespace Entities.Form.Messages
{
    public class FAddMessage
    {
        [JsonIgnore]
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public string Content { get; set; }
        public List<FMessageAttachment> Attachments { get; set; }
    }
}
