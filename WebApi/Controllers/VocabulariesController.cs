using Common.Api;
using Entities.Form.Vocabularies;
using Entities.Model.Vocabularies;
using Entities.Response.Vocabularies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Vocabularies;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class VocabulariesController(IVocabularyService service) : BaseController<IVocabularyService>(service)
    {
        [HttpPost]
        public async Task<ApiResult> RemoveVocabulary([FromBody] string VocabularyId)
        {
            await service.RemoveVocabulary(new FRemoveVocabulary() { UserId = CurrentUser.Id, VocabularyId = VocabularyId });
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
        public async Task<ApiResult<RVocabularyPagination>> GetVocabularies(FGetVocabularyPagination form)
        {
            form.UserId = CurrentUser.Id;
            return new ApiResult<RVocabularyPagination>(await service.GetVocabulariesPagination(form));
        }

        [HttpPost]
        public async Task<ApiResult<List<RVocabularyBox>>> GetVocabulariesBoxes() => new ApiResult<List<RVocabularyBox>>(await service.GetVocabulariesBoxes(CurrentUser.Id));

        [HttpPost]
        public async Task<ApiResult<Vocabulary>> GetUnCheckedVocabulary(FGetUnCheckedVocabulary form)
        {
            form.UserId = CurrentUser.Id;
            return new ApiResult<Vocabulary>(await service.GetUnCheckedVocabulary(form));
        }

        [HttpPost]
        public async Task<ApiResult<Vocabulary>> SetVocabularyCheck(FSetVocabularyCheck form)
        {
            form.UserId = CurrentUser.Id;
            return new ApiResult<Vocabulary>(await service.SetVocabularyCheck(form));
        }
    }
}
