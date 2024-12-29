using Common.Api;
using Entities.Form.Users;
using Entities.Response.Users;
using Microsoft.AspNetCore.Mvc;
namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public readonly IUserService service;
        public UsersController(IUserService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResult<RUserLogin>> Register([FromBody] RegisterRequest request) => new ApiResult<RUserLogin>(await service.RegisterAsync(request), ApiResultStatusCode.RegistrationCompleted);

        [HttpPost]
        public async Task<ApiResult<RUserLogin>> Login([FromBody] LoginRequest request) => new ApiResult<RUserLogin>(await service.LoginAsync(request), ApiResultStatusCode.LoginCompleted);

        [HttpPost]
        public async Task<ApiResult<RUserLogin>> RegisterAsGuest() => new ApiResult<RUserLogin>(await service.RegisterAsGuestAsync(), ApiResultStatusCode.RegistrationCompleted);
    }
}
