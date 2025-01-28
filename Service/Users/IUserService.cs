using Entities.Form.Users;
using Entities.Model.Users;
using Entities.Response.Friendships;
using Entities.Response.Users;
using Entities.ViewModel.Users;

public interface IUserService
{
    Task<RUserLogin> UpdateProfileAsync(RUserLogin form);
    Task<RUserLogin> RegisterAsGuestAsync();
    Task<RUserLogin> RegisterAsync(RegisterRequest request);
    Task<RUserLogin> LoginAsync(LoginRequest request);
    Task<bool> IsUserExist(string userName);
    Task<List<RUserBoxScenario>> GetScenarios();
    Task<List<RFriendship>> SearchUsers(string SearchText, int UserId);
    Task<RUserProfileStatics> GetUserProfileStatics(int  UserId);
    Task<RUserProfile> GetProfile(int UserId);
    Task<RUserPublicInfo> GetUserPublic(int UserId);
}
