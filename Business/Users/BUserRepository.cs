using Common.CacheManager;
using Data;
using Entities.Enum.Users;
using Entities.Response.Users;
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
            var userMiniInfo = new VMUserMiniInfo() { Id = user.Id, NickName = user.NickName, UserName = user.UserName, SecurityStamp = user.SecurityStamp.ToString(), BoxScenario = user.BoxScenario, Email = user.Email, Avatar = user.Avatar };
            if (userMiniInfo != null) CacheManager.Add<VMUserMiniInfo>($"{PreCacheKey}{user.Id}", userMiniInfo);
            return userMiniInfo;
        }
        public VMUserMiniInfo Add(VMUserMiniInfo User)
        {
            CacheManager.Add<VMUserMiniInfo>($"{PreCacheKey}{User.Id}", User);
            return User;
        }
        public void Remove(int UserId) => CacheManager.Remove($"{PreCacheKey}{UserId}");
    }
}
