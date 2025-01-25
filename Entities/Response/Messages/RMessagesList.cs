using Entities.Response.Users;

namespace Entities.Response.Messages
{
    public class RMessagesList : RUserPublicInfo
    {
        public string LastMessage { set; get; }
        public int UnreadCount { set; get; }
    }
    public class RMessagesListPagination : Pagination<RMessagesList>
    {
    }
}
