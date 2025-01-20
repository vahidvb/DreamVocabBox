using Entities.Enum.Friendships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Friendships
{
    public class RFriendship
    {
        public int Id { get; set; }
        public int Avatar { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public FriendshipStatusEnum Status { get; set; }
        public bool IsSentByUser { get; set; }
    }
}
