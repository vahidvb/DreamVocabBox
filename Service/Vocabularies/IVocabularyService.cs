using Common.Extensions;
using Entities.Form.Vocabularies;
using Entities.Model.Vocabularies;
using Entities.Response.Vocabularies;

namespace Service.Vocabularies
{
    public interface IVocabularyService
    {
        Task<RVocabularyChecking> SetVocabularyCheck(FSetVocabularyCheck form);
        Task<RVocabularyChecking> GetUnCheckedVocabulary(FGetUnCheckedVocabulary form);
        Task<RVocabularyPagination> GetVocabulariesPagination(FGetVocabularyPagination form);
        Task<List<RVocabularyBox>> GetVocabulariesBoxes(int UserId);
        Task AddVocabulary(FAddEditVocabulary form);
        Task EditVocabulary(FAddEditVocabulary form);
        Task RemoveVocabulary(FRemoveVocabulary form);
        Task<bool> CheckVocabulary(string text, int userId);
    }
}
