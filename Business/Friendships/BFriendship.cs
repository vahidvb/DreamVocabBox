using Common;
using Common.Api;
using Data;
using Entities.Enum.Friendships;
using Entities.Model.Friendships;
using Entities.Response.Friendships;
using Entities.ViewModel.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Trace;
using Service.Users;

namespace Business.Friendships
{
    public class BFriendship(DreamVocabBoxContext db, IConfiguration configuration, IUserRepositoryService userRepositoryService) : BaseBusiness(db, configuration, userRepositoryService)
    {
        public async Task RequestFriendship(int senderUserId, int receiverUserId)
        {
            if (senderUserId == receiverUserId)
                throw new AppException(ApiResultStatusCode.BadRequest);

            var validStatuses = new[]
            {
                FriendshipStatusEnum.Pending,
                FriendshipStatusEnum.Accepted,
                FriendshipStatusEnum.Blocked
            };

            var existFriendship = await db.Friendships
                .AnyAsync(x =>
                    ((x.SenderUserId == senderUserId && x.ReceiverUserId == receiverUserId) ||
                    (x.SenderUserId == receiverUserId && x.ReceiverUserId == senderUserId)) &&
                    validStatuses.Contains(x.Status)
                );


            if (existFriendship)
                throw new AppException(ApiResultStatusCode.BadRequest);

            var friendship = new Friendship
            {
                SenderUserId = senderUserId,
                ReceiverUserId = receiverUserId,
                Status = FriendshipStatusEnum.Pending
            };

            await db.Friendships.AddAsync(friendship);
            await db.SaveChangesAsync();
        }
        public async Task AcceptFriendship(int senderUserId, int receiverUserId)
        {
            var friendship = await db.Friendships.FirstOrDefaultAsync(x => x.SenderUserId == senderUserId && x.ReceiverUserId == receiverUserId && x.Status == FriendshipStatusEnum.Pending);
            if (friendship == null)
                throw new AppException(ApiResultStatusCode.NotFound);
            friendship.Status = FriendshipStatusEnum.Accepted;
            await db.SaveChangesAsync();
        }
        public async Task CancelFriendship(int senderUserId, int receiverUserId)
        {
            var friendship = await db.Friendships.FirstOrDefaultAsync(x => x.SenderUserId == senderUserId && x.ReceiverUserId == receiverUserId && x.Status == FriendshipStatusEnum.Pending);
            if (friendship == null)
                throw new AppException(ApiResultStatusCode.NotFound);
            friendship.Status = FriendshipStatusEnum.Canceled;
            await db.SaveChangesAsync();
        }
        public async Task RejectFriendship(int senderUserId, int receiverUserId)
        {
            var friendship = await db.Friendships
                .FirstOrDefaultAsync(x => x.SenderUserId == senderUserId && x.ReceiverUserId == receiverUserId
                                          && x.Status == FriendshipStatusEnum.Pending);
            if (friendship == null || friendship.ReceiverUserId != receiverUserId)
                throw new AppException(ApiResultStatusCode.NotFound);

            friendship.Status = FriendshipStatusEnum.Rejected;
            await db.SaveChangesAsync();
        }
        public async Task BlockFriendship(int blockerUserId, int blockedUserId)
        {
            var friendships = await db.Friendships.Where(x => (x.SenderUserId == blockedUserId && x.ReceiverUserId == blockerUserId) || (x.SenderUserId == blockerUserId && x.ReceiverUserId == blockedUserId)).ToListAsync();
            friendships.ForEach(x => x.Status = FriendshipStatusEnum.Deleted);

            var friendship = new Friendship
            {
                SenderUserId = blockerUserId,
                ReceiverUserId = blockedUserId,
                Status = FriendshipStatusEnum.Blocked,
            };

            await db.Friendships.AddAsync(friendship);
            db.Friendships.UpdateRange(friendships);
            await db.SaveChangesAsync();
        }
        public async Task UnBlockFriendship(int blockerUserId, int blockedUserId)
        {
            var friendships = await db.Friendships.Where(x => ((x.SenderUserId == blockedUserId && x.ReceiverUserId == blockerUserId) || (x.SenderUserId == blockerUserId && x.ReceiverUserId == blockedUserId)) && x.Status == FriendshipStatusEnum.Blocked).ToListAsync();
            friendships.ForEach(x => x.Status = FriendshipStatusEnum.Deleted);

            db.Friendships.UpdateRange(friendships);
            await db.SaveChangesAsync();
        }
        public async Task DeleteFriendship(int senderUserId, int receiverUserId)
        {
            var friendship = await db.Friendships.FirstOrDefaultAsync(x => (x.SenderUserId == senderUserId && x.ReceiverUserId == receiverUserId) || (x.SenderUserId == receiverUserId && x.ReceiverUserId == senderUserId));
            if (friendship == null)
                throw new AppException(ApiResultStatusCode.NotFound);
            friendship.Status = FriendshipStatusEnum.Deleted;
            friendship.DeleterUserId = senderUserId;
            await db.SaveChangesAsync();
        }
        public async Task<List<RFriendship>> GetFriendships(int userId)
        {
            var friendshipIds = await db.Friendships
                .Where(x => (x.SenderUserId == userId || x.ReceiverUserId == userId) && (x.Status == FriendshipStatusEnum.Accepted || x.Status == FriendshipStatusEnum.Pending))
                .Select(x => new
                {
                    FriendId = x.SenderUserId == userId ? x.ReceiverUserId : x.SenderUserId,
                    IsSentByUser = x.SenderUserId == userId,
                    x.Status
                }
                )
                .ToListAsync();

            var result = new List<RFriendship>();
            foreach (var friendship in friendshipIds)
            {
                var friendUser = userRepositoryService.Get(friendship.FriendId);
                if (friendUser != null)
                {
                    result.Add(new RFriendship
                    {
                        Id = friendUser.Id,
                        NickName = friendUser.NickName,
                        Avatar = friendUser.Avatar,
                        UserName = friendUser.UserName,
                        Status = friendship.Status,
                        IsSentByUser = friendship.IsSentByUser
                    });
                }
                else
                {
                    var user = await db.Users
                                    .Where(x => x.Id == friendship.FriendId)
                                    .Select(x => new RFriendship
                                    {
                                        Id = x.Id,
                                        NickName = x.NickName,
                                        Avatar = x.Avatar,
                                        UserName = x.UserName,
                                        Status = friendship.Status,
                                        IsSentByUser = friendship.IsSentByUser
                                    })
                                    .FirstOrDefaultAsync();

                    if (user != null)
                        result.Add(user);
                }
            }
            return result;
        }
    }
}
