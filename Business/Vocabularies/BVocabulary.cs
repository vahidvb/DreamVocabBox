using Common;
using Common.Api;
using Common.Extensions;
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
            if (form.Word.IsEmpty() || form.Meaning.IsEmpty())
                throw new AppException(ApiResultStatusCode.VocabularyWordMeaningIsRequied);

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

            var exist = await DataBase.Vocabularies.AnyAsync(x => x.UserId == info.UserId && x.Word.ToLower() == info.Word.ToLower());
            if (exist)
                throw new AppException(ApiResultStatusCode.VocabularyAlreadyAdded);

            await DataBase.Vocabularies.AddAsync(info);
            await DataBase.SaveChangesAsync();
        }
        public async Task EditVocabulary(FAddEditVocabulary form)
        {
            var exist = await DataBase.Vocabularies.AnyAsync(x => x.Id != form.Id.ToGuid() && x.UserId == form.UserId && x.Word.ToLower() == form.Word.ToLower());
            if (exist)
                throw new AppException(ApiResultStatusCode.EntityExists);

            var vocabulary = await DataBase.Vocabularies.FirstOrDefaultAsync(x => x.Id == form.Id.ToGuid());


            if (vocabulary == null)
                throw new AppException(ApiResultStatusCode.EntityNotFound);

            if (vocabulary.UserId != form.UserId)
                throw new AppException(ApiResultStatusCode.DontAllowAccessThisResource);

            if (vocabulary.Word != form.Word)
                throw new AppException(ApiResultStatusCode.VocabularyCantEditWord);

            vocabulary.Meaning = form.Meaning;
            vocabulary.Description = form.Description;
            vocabulary.Example = form.Example;

            DataBase.Vocabularies.Update(vocabulary);
            await DataBase.SaveChangesAsync();
        }

        private async Task<Vocabulary?> GetUnCheckedVocabularyMethod(FGetUnCheckedVocabulary form)
        {
            var thresholdDate = DateTime.Now.AddDays(-1 * form.BoxNumber);
            var res = await DataBase.Vocabularies
                .FirstOrDefaultAsync(x => x.UserId == form.UserId &&
                                          x.BoxNumber == form.BoxNumber &&
                                          x.LastSeenDateTime < thresholdDate);
            return res;
        }

        public async Task<Vocabulary> GetUnCheckedVocabulary(FGetUnCheckedVocabulary form)
        {
            var res = await GetUnCheckedVocabularyMethod(form);

            if (res == null)
                throw new AppException(ApiResultStatusCode.VocabularyCantFind);

            return res;
        }

        public async Task<Vocabulary> SetVocabularyCheck(FSetVocabularyCheck form)
        {
            var vocabulary = await DataBase.Vocabularies.FirstOrDefaultAsync(x => x.Id == form.VocabularyId.ToGuid() && x.UserId == form.UserId);
            if (vocabulary == null)
                throw new AppException(ApiResultStatusCode.VocabularyCantFind);

            var lastBoxNumber = vocabulary.BoxNumber;
            vocabulary.LastSeenDateTime = DateTime.Now;
            vocabulary.BoxNumber = form.Learned && vocabulary.BoxNumber < 7 ? vocabulary.BoxNumber + 1 : (!form.Learned && vocabulary.BoxNumber > 1 ? vocabulary.BoxNumber - 1 : vocabulary.BoxNumber);

            DataBase.Vocabularies.Update(vocabulary);
            await DataBase.SaveChangesAsync();

            var res = await GetUnCheckedVocabularyMethod(new FGetUnCheckedVocabulary() { BoxNumber = lastBoxNumber, UserId = form.UserId });

            if (res == null)
                throw new AppException(ApiResultStatusCode.VocabularyCheckedButCantFindNewOne);

            return res;

        }

        public async Task<RVocabularyPagination> GetVocabulariesPagination(FGetVocabularyPagination form)
        {
            var totalCount = await DataBase.Vocabularies.CountAsync(x => x.UserId == form.UserId && x.BoxNumber == form.BoxNumber);

            var vocabularies = await DataBase.Vocabularies
                .Where(x => x.UserId == form.UserId && x.BoxNumber == form.BoxNumber)
                .OrderBy(x => x.LastSeenDateTime)
                .Skip(form.ListPosition)
                .Take(form.ListLength)
                .ToListAsync();

            var totalPage = (int)Math.Ceiling((double)totalCount / form.ListLength);

            return new RVocabularyPagination
            {
                Items = vocabularies,
                CurrentPage = (form.ListPosition / form.ListLength) + 1,
                TotalPage = totalPage,
                PageSize = form.ListLength,
                TotalItem = totalCount
            };
        }
        public async Task<List<RVocabularyBox>> GetVocabulariesBoxes(int UserId)
        {
            var result = new List<RVocabularyBox>();
            for (int BoxNumber = 1; BoxNumber <= 7; BoxNumber++)
            {
                var thresholdDate = DateTime.Now.AddDays(-1 * BoxNumber);
                var all = await DataBase.Vocabularies.Where(x => x.UserId == UserId && x.BoxNumber == BoxNumber).ToListAsync();

                result.Add(new RVocabularyBox
                {
                    AllCount = all.Count(),
                    BoxNumber = BoxNumber,
                    CheckedCount = all.Count(x => x.LastSeenDateTime > thresholdDate),
                    UnCheckedCount = all.Count(x => x.LastSeenDateTime < thresholdDate),
                });
            }
            return result;
        }
        public async Task RemoveVocabulary(FRemoveVocabulary form)
        {
            var vocabulary = await DataBase.Vocabularies.FirstOrDefaultAsync(x => x.Id == form.VocabularyId.ToGuid());

            if (vocabulary == null)
                throw new AppException(ApiResultStatusCode.EntityNotFound);

            if (vocabulary.UserId != form.UserId)
                throw new AppException(ApiResultStatusCode.DontAllowAccessThisResource);

            DataBase.Vocabularies.Remove(vocabulary);
        }
    }
}
