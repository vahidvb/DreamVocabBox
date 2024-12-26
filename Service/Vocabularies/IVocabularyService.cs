using Entities.Form.Vocabularies;
using Entities.Response.Vocabularies;

namespace Service.Vocabularies
{
    public interface IVocabularyService
    {
        Task<VocabularyPagination> GetVocabulariesPagination(FGetVocabularyPagination form);
        Task AddVocabulary(FAddEditVocabulary form);
        Task EditVocabulary(FAddEditVocabulary form);
        Task RemoveVocabulary(FRemoveVocabulary form);
    }
}
