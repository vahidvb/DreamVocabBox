using Entities.Response.Users;
using Entities.ViewModel.Users;

namespace Service.Users
{
    public interface IUserRepositoryService
    {
        public VMUserMiniInfo Get(int UserId);
        public VMUserMiniInfo Add(int UserId);
        public VMUserMiniInfo Add(VMUserMiniInfo User);
        public void Remove(int UserId);
    }
}