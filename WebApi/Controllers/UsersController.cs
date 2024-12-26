using Common.Api;
using Entities.Form.Users;
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
        public async Task<ApiResult<string>> Register([FromBody] RegisterRequest request) => new ApiResult<string>(await service.RegisterAsync(request), ApiResultStatusCode.RegistrationCompleted);

        [HttpPost]
        public async Task<ApiResult<string>> Login([FromBody] LoginRequest request) => new ApiResult<string>(await service.LoginAsync(request), ApiResultStatusCode.LoginCompleted);
    }
}
