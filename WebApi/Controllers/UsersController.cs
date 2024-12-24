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
        public async Task<ApiResult<string>> Register([FromBody] RegisterRequest request)
        {
            var user = await service.RegisterAsync(request);
            var userToken = await service.LoginAsync(new LoginRequest() { UserName = request.UserName, Password = user.PasswordHash });
            return new ApiResult<string>(ApiResultStatusCode.RegistrationCompleted, userToken);
        }

        [HttpPost]
        public async Task<ApiResult<string>> Login([FromBody] LoginRequest request)
        {
            var userToken = await service.LoginAsync(request);
            return new ApiResult<string>(ApiResultStatusCode.LoginCompleted, userToken);
        }
    }
}
