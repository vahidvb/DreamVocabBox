using Common;
using Common.Api;
using Data;
using Entities.Form.Vocabularies;
using Entities.Model.Vocabularies;
using Entities.Response.Vocabularies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Vocabularies;

namespace Business.Vocabularies
{
    public class BVocabulary : BaseBusiness, IVocabularyService
    {
        public BVocabulary(DreamVocabBoxContext db, IConfiguration configuration) : base(db, configuration)
        {
        }

        public async Task AddVocabulary(FAddEditVocabulary form)
        {
            var info = new Vocabulary()
            {
                Word = form.Word,
                Meaning = form.Meaning,
                UserId = form.UserId,
                BoxNumber = 1,
                Description = form.Description,
                Example = form.Example,
                IsActive = true,
                LastChangeDate = DateTime.Now,
                RegisterDate = DateTime.Now,
                LastSeenDateTime = DateTime.Now,
                SeenCount = 1,
            };


            var exist = await DataBase.Vocabularies.AnyAsync(x => x.UserId == info.UserId && x.Word.Equals(info.Word, StringComparison.InvariantCultureIgnoreCase));
            if (exist)
                throw new AppException(ApiResultStatusCode.EntityExists);

            await DataBase.Vocabularies.AddAsync(info);
            await DataBase.SaveChangesAsync();
        }
        public async Task EditVocabulary(FAddEditVocabulary form)
        {
            var vocabulary = await DataBase.Vocabularies.FirstOrDefaultAsync(x => x.Id == form.Id);

            if (vocabulary == null)
                throw new AppException(ApiResultStatusCode.EntityNotFound);

            if (vocabulary.UserId != form.UserId)
                throw new AppException(ApiResultStatusCode.DontAllowAccessThisResource);


            vocabulary = new Vocabulary()
            {
                Id = form.Id,
                Word = form.Word,
                Meaning = form.Meaning,
                UserId = form.UserId,
                BoxNumber = vocabulary.BoxNumber,
                Description = form.Description,
                Example = form.Example,
                IsActive = true,
                LastChangeDate = DateTime.Now,
                RegisterDate = DateTime.Now,
                LastSeenDateTime = DateTime.Now,
                SeenCount = vocabulary.SeenCount,
            };

            DataBase.Vocabularies.Update(vocabulary);
            await DataBase.SaveChangesAsync();
        }

        public async Task<VocabularyPagination> GetVocabulariesPagination(FGetVocabularyPagination form)
        {
            var totalCount = await DataBase.Vocabularies.CountAsync(x => x.UserId == form.UserId);

            var vocabularies = await DataBase.Vocabularies
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

        public async Task RemoveVocabulary(FRemoveVocabulary form)
        {
            var vocabulary = await DataBase.Vocabularies.FirstOrDefaultAsync(x => x.Id == form.VocabularyId);

            if (vocabulary == null)
                throw new AppException(ApiResultStatusCode.EntityNotFound);

            if (vocabulary.UserId != form.UserId)
                throw new AppException(ApiResultStatusCode.DontAllowAccessThisResource);

            DataBase.Vocabularies.Remove(vocabulary);
        }
    }
}
