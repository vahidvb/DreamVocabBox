using Common.Api;
using Entities.Model.Dictionaries;
using Entities.Response.Dictionaries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class DictionariesController(IDictionaryService service) : BaseController<IDictionaryService>(service)
    {
        [HttpPost]
        public async Task<ApiResult<List<DictionaryEnglishToEnglish>>> SearchEnglishToEnglish([FromBody] string input)
        {
            return new ApiResult<List<DictionaryEnglishToEnglish>>(await service.SearchEnglishToEnglish(input,10));
        }
        [HttpPost]
        public async Task<ApiResult<REnglishPersian>> FindEnglish([FromBody] string input)
        {
            return new ApiResult<REnglishPersian>(await service.FindEnglish(input));
        }
        [HttpPost]
        public async Task<ApiResult<RSuggestWord>> SuggestWord()
        {
            return new ApiResult<RSuggestWord>(await service.SuggestWord(CurrentUser.Id));
        }
    }
}
