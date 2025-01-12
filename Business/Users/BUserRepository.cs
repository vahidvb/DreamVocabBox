using Common.CacheManager;
using Data;
using Entities.ViewModel.Users;
using Microsoft.Extensions.Configuration;
using Service.Users;
namespace Business.Users
{
    public class BUserRepository : BaseBusiness, IUserRepositoryService
    {
        public BUserRepository(DreamVocabBoxContext db, IConfiguration configuration) : base(db, configuration)
        {
        }
        private string PreCacheKey = "User_";
        public VMUserMiniInfo Get(int UserId) => CacheManager.Get<VMUserMiniInfo>($"{PreCacheKey}{UserId}");
        public VMUserMiniInfo Add(int UserId)
        {
            var user = DataBase.Users.FirstOrDefault(x => x.Id == UserId);
            var userMiniInfo = new VMUserMiniInfo() { Id = user.Id, NickName = user.NickName, UserName = user.UserName, SecurityStamp = user.SecurityStamp.ToString() };
            if (userMiniInfo != null) CacheManager.Add<VMUserMiniInfo>($"{PreCacheKey}{user.Id}", userMiniInfo);
            return userMiniInfo;
        }

        public void Remove(int UserId) => CacheManager.Remove($"{PreCacheKey}{UserId}");
    }
}
