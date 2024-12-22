using Common.Extensions;
using Entities.Form.Vocabularies;
using Entities.Response.Vocabularies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Vocabularies;

namespace WebApi.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class VocabulariesController : ControllerBase
    {
        public readonly IVocabularyService service;
        private readonly int UserId;

        public VocabulariesController(IVocabularyService service)
        {
            this.service = service;
            UserId = HttpContext.User.Identity.GetUserId<int>();
        }

        [HttpPost]
        public async Task<VocabularyPagination> GetVocabularies(GetVocabularyPaginationForm form)
        {
            form.UserId = UserId;
            return await service.GetVocabulariesPagination(form);
        }
    }
}
