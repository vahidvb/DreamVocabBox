using Entities.Form.Vocabularies;
using Entities.Response.Vocabularies;

namespace Service.Vocabularies
{
    public interface IVocabularyService
    {
        Task<VocabularyPagination> GetVocabulariesPagination(GetVocabularyPaginationForm form);
    }
}
