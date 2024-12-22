using Data;
using Entities.Form.Vocabularies;
using Entities.Response.Vocabularies;
using Microsoft.EntityFrameworkCore;
using Service.Vocabularies;

namespace Business.Vocabularies
{
    public class BVocabulary : IVocabularyService
    {
        private readonly DreamVocabBoxContext db;

        public BVocabulary(DreamVocabBoxContext db)
        {
            this.db = db;
        }

        public async Task<VocabularyPagination> GetVocabulariesPagination(GetVocabularyPaginationForm form)
        {
            var totalCount = await db.Vocabularies.CountAsync(x => x.UserId == form.UserId);

            var vocabularies = await db.Vocabularies
                .Where(x => x.UserId == form.UserId)
                .Skip(form.ListPosition)
                .Take(form.ListLength)
                .ToListAsync();

            var totalPage = (int)Math.Ceiling((double)totalCount / form.ListLength);

            return new VocabularyPagination
            {
                Items = vocabularies,
                CurrentPage = (form.ListPosition / form.ListLength) + 1,
                TotalPage = totalPage,
                PageSize = form.ListLength,
                TotalItem = totalCount
            };
        }
    }
}
