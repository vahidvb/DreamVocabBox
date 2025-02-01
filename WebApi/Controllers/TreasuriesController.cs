using Common.Api;
using Entities.Form.Treasuries;
using Entities.Model.Treasuries;
using Entities.Response.Treasuries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Treasuries;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class TreasuriesController(ITreasuryService service) : BaseController<ITreasuryService>(service)
    {
        [HttpPost]
        public async Task<ApiResult> GetById([FromBody] string TreasuryId)
        {
            await service.GetById(TreasuryId,CurrentUser.Id);
            return new ApiResult(ApiResultStatusCode.Success);
        }
        [HttpPost]
        public async Task<ApiResult<RTreasuryPagination>> GetMyAll(FGetTreasuryPagination form)
        {
            form.UserId = CurrentUser.Id;
            return new ApiResult<RTreasuryPagination>(await service.GetAll(form));
        }
        [HttpPost]
        public async Task<ApiResult<RTreasuryPagination>> GetAll(FGetTreasuryPagination form)
        {
            return new ApiResult<RTreasuryPagination>(await service.GetAll(form));
        }
        [HttpPost]
        public async Task<ApiResult<string>> Create(FTreasuryCreate form)
        {
            form.UserId = CurrentUser.Id;
            return new ApiResult<string> (await service.Create(form),ApiResultStatusCode.Success);
        }
    }
}
