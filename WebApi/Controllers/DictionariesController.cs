using Common.Api;
using Entities.Model.Dictionaries;
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
        public async Task<ApiResult<DictionaryEnglishToEnglish>> FindEnglishToEnglish([FromBody] string input)
        {
            return new ApiResult<DictionaryEnglishToEnglish>(await service.FindEnglishToEnglish(input));
        }

    }
}
