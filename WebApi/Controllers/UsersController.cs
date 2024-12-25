using Common.Extensions;
using Entities.Model.Users;
using Microsoft.AspNetCore.Mvc;
using Service.Vocabularies;
using Entities.Form;
using Entities.Form.Users;
using Common.Api;
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
        public async Task<ApiResult<string>> Register([FromBody] RegisterRequest request) => new ApiResult<string>(ApiResultStatusCode.RegistrationCompleted, await service.RegisterAsync(request));

        [HttpPost]
        public async Task<ApiResult<string>> Login([FromBody] LoginRequest request) => new ApiResult<string>(ApiResultStatusCode.LoginCompleted, await service.LoginAsync(request));
    }
}
