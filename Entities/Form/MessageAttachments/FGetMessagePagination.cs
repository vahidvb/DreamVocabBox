using System.Text.Json.Serialization;

namespace Entities.Form.MessageAttachments
{
    public class FGetMessagePagination
    {
        public int UserId { get; set; }
        public int FriendUserId { get; set; }
        public int ListLength { get; set; }
        public int ListPosition { get; set; }
    }
}
