using Common.Api;
using Entities.Form.Users;
using Entities.Response.Friendships;
using Entities.Response.Users;
using Entities.ViewModel.Users;
using Microsoft.AspNetCore.Mvc;
using Service.Users;
namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController(IUserService service) : BaseController<IUserService>(service)
    {
        [HttpPost]
        public async Task<ApiResult<RUserLogin>> Register([FromBody] RegisterRequest request) => new ApiResult<RUserLogin>(await service.RegisterAsync(request), ApiResultStatusCode.RegistrationCompleted);

        [HttpPost]
        public async Task<ApiResult<RUserLogin>> Login([FromBody] LoginRequest request) => new ApiResult<RUserLogin>(await service.LoginAsync(request), ApiResultStatusCode.LoginCompleted);

        [HttpPost]
        public async Task<ApiResult<RUserLogin>> RegisterAsGuest() => new ApiResult<RUserLogin>(await service.RegisterAsGuestAsync(), ApiResultStatusCode.RegistrationCompleted);

        [HttpPost]
        public async Task<ApiResult<RUserLogin>> UpdateProfile(RUserLogin form)
        {
            form.Id = CurrentUser.Id;
            return new ApiResult<RUserLogin>(await service.UpdateProfileAsync(form));
        }

        [HttpPost]
        public async Task<ApiResult<RUserProfile>> GetProfile() => new ApiResult<RUserProfile>(await service.GetProfile(CurrentUser.Id));

        [HttpPost]
        public async Task<ApiResult<RUserPublicInfo>> GetUserPublic([FromBody] int UserId) => new ApiResult<RUserPublicInfo>(await service.GetUserPublic(UserId));

        [HttpPost]
        public async Task<ApiResult<List<RUserBoxScenario>>> GetScenarios() => new ApiResult<List<RUserBoxScenario>>(await service.GetScenarios());

        [HttpPost]
        public async Task<ApiResult<List<RFriendship>>> SearchUsers([FromBody] string SearchText) => new ApiResult<List<RFriendship>>(await service.SearchUsers(SearchText,CurrentUser.Id));

        [HttpPost]
        public async Task<ApiResult<RUserProfileStatics>> GetUserProfileStatics([FromBody] int UserId) => new ApiResult<RUserProfileStatics>(await service.GetUserProfileStatics(UserId));

    }
}
