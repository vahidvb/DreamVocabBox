using Entities.Enum.MessageAttachments;

namespace Entities.Model.MessageAttachments
{
    public class MessageAttachment : BaseModel<Guid>
    {
        public Guid MessageId { get; set; }
        public MessageAttachmentTypeEnum Type { get; set; }
        public string Value { get; set; }
    }
}
