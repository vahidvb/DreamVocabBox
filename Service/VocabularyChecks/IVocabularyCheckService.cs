using Entities.Model.VocabularyChecks;

namespace Service.VocabularyChecks
{
    public interface IVocabularyCheckService
    {
        Task<List<VocabularyCheck>> GetVocabularyCheckHistory(string VocabularyId, int UserId);
    }
}
