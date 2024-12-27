using Entities.Form.Vocabularies;
using Entities.Response.Vocabularies;

namespace Service.Vocabularies
{
    public interface IVocabularyService
    {
        Task<RVocabularyPagination> GetVocabulariesPagination(FGetVocabularyPagination form);
        Task<List<RVocabularyBox>> GetVocabulariesBoxes(int UserId);
        Task AddVocabulary(FAddEditVocabulary form);
        Task EditVocabulary(FAddEditVocabulary form);
        Task RemoveVocabulary(FRemoveVocabulary form);
    }
}
