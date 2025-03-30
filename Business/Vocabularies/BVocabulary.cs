using Common;
using Common.Api;
using Common.Extensions;
using Data;
using Entities.Enum.Users;
using Entities.Form.Vocabularies;
using Entities.Model.Users;
using Entities.Model.Vocabularies;
using Entities.Model.VocabularyChecks;
using Entities.Response.Vocabularies;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Users;
using Service.Vocabularies;
using Service.VocabularyChecks;

namespace Business.Vocabularies
{
    public class BVocabulary : BaseBusiness, IVocabularyService
    {
        public BVocabulary(DreamVocabBoxContext db, IConfiguration configuration, IUserRepositoryService userRepositoryService) : base(db, configuration, userRepositoryService)
        {
        }
        public async Task<bool> CheckVocabulary(string text,int userId)
        {
            text = text.ToLowerTrim();
            var exist = await DataBase.Vocabularies.AnyAsync(x => x.UserId == userId && x.Word.ToLower() == text);
            return exist;
        }
        public async Task AddVocabulary(FAddEditVocabulary form)
        {
            form.Word.ToLowerTrim();

            if (form.Word.IsEmpty())
                throw new AppException(ApiResultStatusCode.VocabularyWordMeaningIsRequied);

            var info = new Vocabulary()
            {
                Word = form.Word,
                Meaning = form.Meaning.Trim(),
                UserId = form.UserId,
                BoxNumber = 1,
                Description = form.Description?.Trim(),
                Example = form.Example?.Trim(),
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

            vocabulary.Meaning = form.Meaning.Trim();
            vocabulary.Description = form.Description?.Trim();
            vocabulary.Example = form.Example?.Trim();
            vocabulary.LastEditDateTime = DateTime.Now;

            DataBase.Vocabularies.Update(vocabulary);
            await DataBase.SaveChangesAsync();
        }

        private async Task<RVocabularyChecking?> GetUnCheckedVocabularyMethod(FGetUnCheckedVocabulary form)
        {
            var user = userRepositoryService.Get(form.UserId);

            if (user == null)
                throw new AppException(ApiResultStatusCode.UserNotExistInRepository);

            var calculatedScenario = CalculateScenario(user.BoxScenario, form.BoxNumber);
            var res = await DataBase.Vocabularies
                .FirstOrDefaultAsync(x => x.UserId == form.UserId &&
                                          x.BoxNumber == form.BoxNumber &&
                                          x.LastSeenDateTime < calculatedScenario.ThresholdDate);
            if (res == null) return null;
            var result = res.MapTo<RVocabularyChecking>();
            result.RemainCount = await DataBase.Vocabularies
                .CountAsync(x => x.UserId == form.UserId &&
                                          x.BoxNumber == form.BoxNumber &&
                                          x.LastSeenDateTime < calculatedScenario.ThresholdDate);

            result.Word = result.Word.ToPascalCase();
            result.Meaning = result.Meaning.ToUppercaseFirst();

            return result;
        }

        public async Task<RVocabularyChecking> GetUnCheckedVocabulary(FGetUnCheckedVocabulary form)
        {
            var res = await GetUnCheckedVocabularyMethod(form);

            if (res == null)
                throw new AppException(ApiResultStatusCode.VocabularyCantFind);

            return res;
        }

        public async Task<RVocabularyChecking> SetVocabularyCheck(FSetVocabularyCheck form)
        {
            var vocabulary = await DataBase.Vocabularies.FirstOrDefaultAsync(x => x.Id == form.VocabularyId.ToGuid() && x.UserId == form.UserId);
            if (vocabulary == null)
                throw new AppException(ApiResultStatusCode.VocabularyCantFind);

            var vocabularyCheck = new VocabularyCheck()
            {
                UserId = form.UserId,
                VocabularyId = vocabulary.Id,
                Learned = form.Learned,
                BoxNumber = vocabulary.BoxNumber,
            };

            await DataBase.VocabularyChecks.AddAsync(vocabularyCheck);

            var lastBoxNumber = vocabulary.BoxNumber;
            vocabulary.LastSeenDateTime = DateTime.Now;
            vocabulary.BoxNumber = form.Learned && vocabulary.BoxNumber < 7 ? vocabulary.BoxNumber + 1 : (!form.Learned && vocabulary.BoxNumber > 1 ? vocabulary.BoxNumber - 1 : vocabulary.BoxNumber);
            vocabulary.SeenCount++;

            DataBase.Vocabularies.Update(vocabulary);

            await DataBase.SaveChangesAsync();

            var res = await GetUnCheckedVocabularyMethod(new FGetUnCheckedVocabulary() { BoxNumber = lastBoxNumber, UserId = form.UserId });

            if (res == null)
                throw new AppException(ApiResultStatusCode.VocabularyCheckedButCantFindNewOne);

            return res;

        }

        public async Task<RVocabularyPagination> GetVocabulariesPagination(FGetVocabularyPagination form)
        {
            var totalCount = await DataBase.Vocabularies.CountAsync(x => x.UserId == form.UserId && (string.IsNullOrEmpty(form.SearchText) || x.Word.ToLower().Trim().StartsWith(form.SearchText.Trim().ToLower())) && (form.BoxNumber == 0 || x.BoxNumber == form.BoxNumber));

            var vocabularies = await DataBase.Vocabularies
                .Where(x => x.UserId == form.UserId && (string.IsNullOrEmpty(form.SearchText) || x.Word.ToLower().Trim().StartsWith(form.SearchText.Trim().ToLower())) && (form.BoxNumber == 0 || x.BoxNumber == form.BoxNumber))
                .OrderByDescending(x => x.LastEditDateTime.HasValue ? x.LastEditDateTime : x.RegisterDate)
                .Skip(form.ListPosition)
                .Take(form.ListLength)
                .ToListAsync();

            vocabularies.ForEach(x =>
            {
                x.Word = x.Word.ToPascalCase();
                x.Meaning = x.Meaning.ToUppercaseFirst();
            });

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
        private RScenarioCalculated CalculateScenario(UserBoxScenarioEnum userBoxScenario, int BoxNumber)
        {
            var thresholdDate = DateTime.Now;
            float days = 1;

            switch (userBoxScenario)
            {
                case Entities.Enum.Users.UserBoxScenarioEnum.HalfDayBox:
                    thresholdDate = DateTime.Now.AddHours(-12);
                    days = 0.5f;
                    break;
                case Entities.Enum.Users.UserBoxScenarioEnum.DailyBox:
                default:
                    thresholdDate = DateTime.Now.AddDays(-1);
                    days = 1;
                    break;
                case Entities.Enum.Users.UserBoxScenarioEnum.BoxNumberDays:
                    thresholdDate = DateTime.Now.AddDays(-1 * BoxNumber);
                    days = BoxNumber;
                    break;
            }
            return new RScenarioCalculated()
            {
                ThresholdDate = thresholdDate,
                Days = days,
            };
        }
        public async Task<List<RVocabularyBox>> GetVocabulariesBoxes(int UserId)
        {
            var result = new List<RVocabularyBox>();
            var user = userRepositoryService.Get(UserId);

            if (user == null)
                throw new AppException(ApiResultStatusCode.UserNotExistInRepository);

            for (int BoxNumber = 1; BoxNumber <= 7; BoxNumber++)
            {
                var calculatedScenario = CalculateScenario(user.BoxScenario, BoxNumber);


                var all = await DataBase.Vocabularies.Where(x => x.UserId == UserId && x.BoxNumber == BoxNumber).ToListAsync();

                result.Add(new RVocabularyBox
                {
                    AllCount = all.Count(),
                    BoxNumber = BoxNumber,
                    CheckedCount = all.Count(x => x.LastSeenDateTime > calculatedScenario.ThresholdDate),
                    UnCheckedCount = all.Count(x => x.LastSeenDateTime < calculatedScenario.ThresholdDate),
                    SoonTime = all.Where(x => x.LastSeenDateTime > calculatedScenario.ThresholdDate).OrderBy(x => x.LastSeenDateTime).FirstOrDefault()?.LastChangeDate.ToNotNullable().AddDays(calculatedScenario.Days).ToHumanReadableTime("dhm") ?? "",
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

            var vocabularyChecks = await DataBase.VocabularyChecks.Where(x => x.VocabularyId == form.VocabularyId.ToGuid()).ToListAsync();

            DataBase.Vocabularies.Remove(vocabulary);
            DataBase.VocabularyChecks.RemoveRange(vocabularyChecks);
            await DataBase.SaveChangesAsync();
        }
    }
}
