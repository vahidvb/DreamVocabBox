using System.Text.Json.Serialization;

namespace Entities.Form.MessageAttachments
{
    public class FGetMessagePagination
    {
        [JsonIgnore]
        public int UserId { get; set; }
        public int ListLength { get; set; }
        public int ListPosition { get; set; }
    }
}
