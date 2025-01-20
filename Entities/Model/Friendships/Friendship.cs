using Entities.Enum.Friendships;

namespace Entities.Model.Friendships
{
    public class Friendship : BaseModel<int>
    {
        public int SenderUserId { get; set; }
        public int ReceiverUserId { get; set; }
        public int? DeleterUserId { get; set; }
        public FriendshipStatusEnum Status { get; set; }
    }
}
