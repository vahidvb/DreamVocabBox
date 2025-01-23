using Entities.Response.MessageAttachments;

namespace Entities.Response.Messages
{
    public class RMessage<TModel> 
    {
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public string Content { get; set; }
        public List<TModel> Attachments { get; set; }
    }
    public class RVocabularyMessagePagination<TModel> : Pagination<RMessage<TModel>>
    {
    }
}
