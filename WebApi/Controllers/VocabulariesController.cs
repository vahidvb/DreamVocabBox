using Common.Api;
using Entities.Form.Vocabularies;
using Entities.Response.Vocabularies;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ApiResult> RemoveVocabulary(FRemoveVocabulary form)
        {
            form.UserId = CurrentUser.Id;
            await service.RemoveVocabulary(form);
            return new ApiResult(ApiResultStatusCode.VocabularyRemoved);
        }

        [HttpPost]
        public async Task<ApiResult> AddVocabulary(FAddEditVocabulary form)
        {
            form.UserId = CurrentUser.Id;
            await service.AddVocabulary(form);
            return new ApiResult(ApiResultStatusCode.VocabularyAdded);
        }

        [HttpPost]
        public async Task<ApiResult> EditVocabulary(FAddEditVocabulary form)
        {
            form.UserId = CurrentUser.Id;
            await service.EditVocabulary(form);
            return new ApiResult(ApiResultStatusCode.VocabularyUpdated);
        }

        [HttpPost]
        public async Task<ApiResult<VocabularyPagination>> GetVocabularies(FGetVocabularyPagination form)
        {
            form.UserId = CurrentUser.Id;
            return new ApiResult<VocabularyPagination>(await service.GetVocabulariesPagination(form));
        }
    }
}
