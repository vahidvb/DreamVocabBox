using Common.Extensions;
using Data;
using Entities.Model.VocabularyChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Users;
using Service.VocabularyChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.VocabularyChecks
{
    public class BVocabularyCheck : BaseBusiness, IVocabularyCheckService
    {
        public BVocabularyCheck(DreamVocabBoxContext db, IConfiguration configuration, IUserRepositoryService userRepositoryService) : base(db, configuration, userRepositoryService)
        {
        }

        public async Task<List<VocabularyCheck>> GetVocabularyCheckHistory(string VocabularyId, int UserId)
        {
            return await DataBase.VocabularyChecks.Where(x => x.UserId == UserId && x.Id == VocabularyId.ToGuid()).ToListAsync();
        }
    }
}
