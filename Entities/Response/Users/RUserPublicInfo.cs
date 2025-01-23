using Entities.Enum.Friendships;

namespace Entities.Response.Users
{
    public class RUserPublicInfo
    {
        public int UserId { get; set; }
        public int Avatar { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
    }
}
