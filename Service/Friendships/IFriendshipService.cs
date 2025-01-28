using Entities.Response.Friendships;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Friendships
{
    public interface IFriendshipService
    {
        Task<List<RFriendship>> GetFriendships(int userId);
        Task RequestFriendship(int senderUserId, int receiverUserId);
        Task AcceptFriendship(int senderUserId, int receiverUserId);
        Task CancelFriendship(int senderUserId, int receiverUserId);
        Task DeleteFriendship(int senderUserId, int receiverUserId);
        Task RejectFriendship(int senderUserId, int receiverUserId);
        Task<List<RFriendshipForShare>> GetFriendsListForShareWord(int userId, List<string> Vocabularies);
    }
}
