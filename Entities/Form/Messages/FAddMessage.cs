using Entities.Form.MessageAttachments;

namespace Entities.Form.Messages
{
    public class FAddMessage
    {
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public string Content { get; set; }
        public List<FMessageAttachment> Attachments { get; set; }
    }
}
