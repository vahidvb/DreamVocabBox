using Common.Api;
using Entities.Response.Friendships;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Friendships;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class FriendshipsController(IFriendshipService service) : BaseController<IFriendshipService>(service)
    {
        [HttpPost]
        public async Task<ApiResult<List<RFriendship>>> GetFriendships()
        {
            return new ApiResult<List<RFriendship>>(await service.GetFriendships(CurrentUser.Id));
        }
        [HttpPost]
        public async Task<ApiResult<List<RFriendshipForShare>>> GetFriendsListForShareWord([FromBody] List<string> Vocabularies)
        {
            return new ApiResult<List<RFriendshipForShare>>(await service.GetFriendsListForShareWord(CurrentUser.Id, Vocabularies));
        }
        [HttpPost]
        public async Task<ApiResult> RequestFriendship([FromBody] int UserId)
        {
            await service.RequestFriendship(CurrentUser.Id, UserId);
            return new ApiResult(ApiResultStatusCode.FriendRequestSentSuccessfully);
        }
        [HttpPost]
        public async Task<ApiResult> AcceptFriendship([FromBody] int UserId)
        {
            await service.AcceptFriendship(UserId, CurrentUser.Id);
            return new ApiResult(ApiResultStatusCode.FriendRequestAcceptedSuccessfully);
        }

        [HttpPost]
        public async Task<ApiResult> CancelFriendship([FromBody] int UserId)
        {
            await service.CancelFriendship(CurrentUser.Id, UserId);
            return new ApiResult(ApiResultStatusCode.FriendRequestCancelledSuccessfully);
        }

        [HttpPost]
        public async Task<ApiResult> DeleteFriendship([FromBody] int UserId)
        {
            await service.DeleteFriendship(UserId,CurrentUser.Id);
            return new ApiResult(ApiResultStatusCode.FriendRequestDeleteSuccessfully);
        }

        [HttpPost]
        public async Task<ApiResult> RejectFriendship([FromBody] int UserId)
        {
            await service.RejectFriendship(UserId, CurrentUser.Id);
            return new ApiResult(ApiResultStatusCode.FriendRequestRejectedSuccessfully);
        }
    }
}
