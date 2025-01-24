using Entities.Enum.Friendships;
using Entities.Response.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Response.Friendships
{
    public class RFriendshipForShare : RUserPublicInfo
    {
        public bool AddedVocabulary { get; set; }
    }
}
