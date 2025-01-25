using Entities.ViewModel.Users;

namespace Entities.Response.Users
{
    public class RUserProfile : VMUserMiniInfo
    {
        public int FriendshipPending { get; set; }
        public int MessagesUnread { get; set; }
        public List<RUserBoxScenario> Scenarios { get; set; }
    }
}
