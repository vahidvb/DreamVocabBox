using Common.Api;
using Entities.Form.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Messages;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class MessagesController(IMessageService service) : BaseController<IMessageService>(service)
    {
        [HttpPost]
        public async Task<ApiResult> AddMessage(FAddMessage form)
        {
            form.SenderUserId = CurrentUser.Id;
            await service.AddMessage(form);
            return new ApiResult(ApiResultStatusCode.MessageSentSuccessfully);
        }
    }
}
