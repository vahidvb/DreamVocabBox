using Entities.Enum.Friendships;
using Entities.Response.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Friendships
{
    public class RFriendship : RUserPublicInfo
    {
        public FriendshipStatusEnum FriendshipStatus { get; set; }
        public bool IsSentByUser { get; set; }
    }
}
