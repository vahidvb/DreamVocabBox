using Entities.Enum.MessageAttachments;

namespace Entities.Form.MessageAttachments
{
    public class FMessageAttachment
    {
        public MessageAttachmentTypeEnum Type { get; set; }
        public string Title { get; set; }
        public string Value { get; set; }
    }
}
