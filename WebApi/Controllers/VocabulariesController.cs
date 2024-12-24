using Common.Api;
using Common.Extensions;
using Entities.Form.Vocabularies;
using Entities.Response.Vocabularies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Vocabularies;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class VocabulariesController : BaseController
    {
        public readonly IVocabularyService service;

        public VocabulariesController(IVocabularyService service)
        {
            this.service = service;
        }

        [HttpPost]
        public async Task<ApiResult<VocabularyPagination>> GetVocabularies(GetVocabularyPaginationForm form)
        {
            form.UserId = CurrentUser.Id;
            return new ApiResult<VocabularyPagination>(ApiResultStatusCode.Success, await service.GetVocabulariesPagination(form));
        }
    }
}
