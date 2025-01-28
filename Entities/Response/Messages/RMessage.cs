using Entities.Model.Messages;
using Entities.Response.MessageAttachments;

namespace Entities.Response.Messages
{
    public class RMessage
    {
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public bool IsUserSent { get; set; }
        public string Content { get; set; }
        public DateTime RegisterDate { get; set; }
        public string RegisterDateHumanReadable { get; set; }
        public string ReadAt { get; set; }
        public string? Reaction { get; set; }
        public List<dynamic> Attachments { get; set; } = new List<dynamic>();
    }
    public class RVocabularyMessagePagination : Pagination<RMessage>
    {

    }
}
